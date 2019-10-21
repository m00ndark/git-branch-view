using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitBranchView.Forms;
using GitBranchView.Model;
using ToolComponents.Core.Extensions;

namespace GitBranchView.Controls
{
	public partial class FolderEntry : UserControl
	{
		private const string MENU_ITEM_REFRESH = "Refresh";

		public event EventHandler WidthChanged;

		public FolderEntry(Root root, string path, string branch, int trackedChanges, int untrackedChanges, string[] errors)
		{
			InitializeComponent();

			Root = root;
			Path = path;
			Branch = branch;
			TrackedChanges = trackedChanges;
			UntrackedChanges = untrackedChanges;
			Errors = errors;

			SetHighlightColor();
			UpdateInfo(resetRemoteStatus: true);
		}

		public Root Root { get; }
		public string Path { get; }
		public string Branch { get; private set; }
		public int TrackedChanges { get; private set; }
		public int UntrackedChanges { get; private set; }
		public string[] Errors { get; private set; }
		private Color? HighlightColor { get; set; }

		public Func<ToolStripMenuItem, bool> IsQuickLaunchPathMenuItem => x => x.GetTag()?.ClickEventHandler == QuickLaunchPathMenuItem_Click;

		private void RaiseWidthChanged()
		{
			WidthChanged?.Invoke(this, EventArgs.Empty);
		}

		private void SetHighlightColor()
		{
			HighlightColor = Root.Filters
				.Where(filter => filter.Type == FilterType.Highlight)
				.Where(filter => filter.Target.HasFlag(FilterTargets.Path) && Regex.IsMatch(Path.RelativeTo(Root), filter.Filter)
					|| filter.Target.HasFlag(FilterTargets.Branch) && Regex.IsMatch(Branch, filter.Filter))
				.Select(filter => (Color?) filter.Color)
				.FirstOrDefault();
		}

		private void UpdateInfo(bool resetRemoteStatus)
		{
			int width = Width;

			labelBranch.Top = Errors == null ? 3 : -20;
			linkLabelBranchError.Top = Errors != null ? 3 : -20;

			if (TrackedChanges > 0 || UntrackedChanges > 0)
			{
				linkLabelFolder.Font = WithStyle(linkLabelFolder.Font, FontStyle.Bold);
				labelBranch.Font = WithStyle(labelBranch.Font, FontStyle.Bold);
				linkLabelBranchError.Font = WithStyle(linkLabelBranchError.Font, FontStyle.Bold);
				labelChanges.Font = WithStyle(labelChanges.Font, FontStyle.Bold);
				labelChanges.ForeColor = SystemColors.ControlText;
			}
			else
			{
				linkLabelFolder.Font = WithoutStyle(linkLabelFolder.Font, FontStyle.Bold);
				labelBranch.Font = WithoutStyle(labelBranch.Font, FontStyle.Bold);
				linkLabelBranchError.Font = WithoutStyle(linkLabelBranchError.Font, FontStyle.Bold);
				labelChanges.Font = WithoutStyle(labelChanges.Font, FontStyle.Bold);
				labelChanges.ForeColor = SystemColors.ControlDark;
			}

			if (resetRemoteStatus)
			{
				labelBranch.Font = WithoutStyle(labelBranch.Font, FontStyle.Strikeout);
				labelBranch.ForeColor = Settings.Default.EnableRemoteBranchLookup
					? SystemColors.ControlDark
					: SystemColors.ControlText;
			}

			linkLabelFolder.Text = Path.RelativeTo(Root);

			labelBranch.Text = Branch;
			linkLabelBranchError.Text = Branch;

			string trackedChanges = TrackedChanges > -1 ? TrackedChanges.ToString() : "?";
			string untrackedChanges = UntrackedChanges > -1 ? UntrackedChanges.ToString() : "?";
			labelChanges.Text = $"{trackedChanges}|{untrackedChanges}";

			UpdateSize();

			if (Width != width)
				RaiseWidthChanged();

			Task.Run(() =>
				{
					ResetContextMenu();
					CheckRemoteBranchExistance();
				});
		}

		private void UpdateSize()
		{
			linkLabelBranchError.Left = labelBranch.Left = linkLabelFolder.Left + linkLabelFolder.Width + linkLabelFolder.Margin.Right;
			labelChanges.Left = labelBranch.Left + labelBranch.Width + labelBranch.Margin.Right;

			linkLabelBranchError.Anchor = labelBranch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			labelChanges.Anchor = AnchorStyles.Top | AnchorStyles.Left;

			Width = labelBranch.Left + labelBranch.Width + labelChanges.Width + labelChanges.Margin.Right;

			linkLabelBranchError.Anchor = labelBranch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			labelChanges.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		}

		public void ResetContextMenu()
		{
			contextMenuStrip.InvokeIfRequired(() =>
				{
					contextMenuStrip.Items.DisposeItems();
					contextMenuStrip.Items.Clear();

					if (!string.IsNullOrWhiteSpace(Settings.Default.QuickLaunchFilesFilter))
					{
						List<IGrouping<string, string>> groupedQuickLaunchPaths = Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories)
							.Where(filePath => Regex.IsMatch(filePath.RelativeTo(Path), Settings.Default.QuickLaunchFilesFilter, RegexOptions.IgnoreCase))
							.GroupBy(filePath => Settings.Default.QuickLaunchFilesGrouping == QuickLaunchFilesGrouping.ByExtension
								? System.IO.Path.GetExtension(filePath)
								: Settings.Default.QuickLaunchFilesGrouping == QuickLaunchFilesGrouping.ByPath
									? System.IO.Path.GetDirectoryName(filePath)?.RelativeTo(Path) ?? string.Empty
									: string.Empty)
							.ToList();

						if (groupedQuickLaunchPaths.Any())
						{
							Settings.Default.SelectedQuickLaunchFiles.TryGetValue(Path.ToLower(), out string selectedQuickLaunchPath);

							foreach (IGrouping<string, string> quickLaunchPaths in groupedQuickLaunchPaths)
							{
								ToolStripItemCollection menuItems = contextMenuStrip.Items;

								if (groupedQuickLaunchPaths.Count > 1
									&& Settings.Default.QuickLaunchFilesGrouping != QuickLaunchFilesGrouping.None
									&& quickLaunchPaths.Key != Extensions.ROOT_PATH_RELATIVE)
								{
									menuItems = Settings.Default.QuickLaunchFilesGrouping == QuickLaunchFilesGrouping.ByExtension
										? contextMenuStrip.AddItem<ToolStripMenuItem>($"Extension: {quickLaunchPaths.Key.Replace(".", string.Empty)}").DropDownItems
										: contextMenuStrip.AddItem<ToolStripMenuItem>(quickLaunchPaths.Key).DropDownItems;
								}

								foreach (string quickLaunchPath in quickLaunchPaths)
								{
									string menuItemText = groupedQuickLaunchPaths.Count > 1 && Settings.Default.QuickLaunchFilesGrouping == QuickLaunchFilesGrouping.ByPath
										? System.IO.Path.GetFileName(quickLaunchPath)
										: quickLaunchPath.RelativeTo(Path);

									ToolStripMenuItem item = menuItems.Add<ToolStripMenuItem>(menuItemText, QuickLaunchPathMenuItem_Click, quickLaunchPath);

									if (Settings.Default.RepositoryLinkBehavior == RepositoryLinkBehavior.LaunchSelectedQuickLaunchFile
										&& (selectedQuickLaunchPath?.Equals(quickLaunchPath, StringComparison.InvariantCultureIgnoreCase) ?? false))
									{
										item.Checked = true;
									}
								}
							}

							contextMenuStrip.AddItem<ToolStripSeparator>();
						}
					}

					contextMenuStrip.AddItem<ToolStripMenuItem>(MENU_ITEM_REFRESH, RefreshMenuItem_Click);

					if (Settings.Default.RepositoryLinkBehavior == RepositoryLinkBehavior.LaunchSelectedQuickLaunchFile)
					{
						contextMenuStrip.AddItem<ToolStripSeparator>();
						contextMenuStrip.AddItem<ToolStripMenuItem>(Settings.Default.CommandName, CustomCommandMenuItem_Click);
					}

					if (Settings.Default.GitContextMenuCommands.Any())
					{
						contextMenuStrip.AddItem<ToolStripSeparator>();

						foreach (GitContextMenuCommand gitCommand in Settings.Default.GitContextMenuCommands
							.Where(x => !string.IsNullOrWhiteSpace(x.Caption) && !string.IsNullOrWhiteSpace(x.Command)))
						{
							contextMenuStrip.AddItem<ToolStripMenuItem>(gitCommand.Caption, GitCommand_Click, gitCommand);
						}
					}
				});
		}

		public void HighlightChanged()
		{
			SetHighlightColor();
			Refresh();
		}

		private void ButtonMore_Click(object sender, EventArgs e)
		{
			Point position = buttonMore.Parent.PointToScreen(new Point(buttonMore.Location.X, buttonMore.Location.Y + buttonMore.Size.Height));
			contextMenuStrip.Show(position);
		}

		private void LinkLabelFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (Settings.Default.RepositoryLinkBehavior == RepositoryLinkBehavior.LaunchSelectedQuickLaunchFile)
			{
				ToolStripMenuItem item = contextMenuStrip.Items.GetWhere<ToolStripMenuItem>(x => IsQuickLaunchPathMenuItem(x) && x.Checked);

				if (item == null)
				{
					Program.ShowInfo("Git repository link is configured to execute the selected quick launch file but no file has been selected.");
					return;
				}

				ExecuteQuickLaunchFile(item);
			}
			else
			{
				ExecuteCustomCommand();
			}
		}

		private void LinkLabelBranchError_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			new OutputForm(Errors, showInTaskbar: true).Show(this);
		}

		private void QuickLaunchPathMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender.GetItem<ToolStripMenuItem>();

			if (item == null)
				return;

			if (Settings.Default.RepositoryLinkBehavior == RepositoryLinkBehavior.LaunchSelectedQuickLaunchFile)
			{
				contextMenuStrip.Items.SetWhere(IsQuickLaunchPathMenuItem, x => x.Checked = false);

				item.Checked = true;
				Settings.Default.SelectedQuickLaunchFiles[Path.ToLower()] = item.GetTagValue<string>()?.ToLower();
				Settings.Default.Save();
			}
			else
			{
				ExecuteQuickLaunchFile(item);
			}
		}

		private async void RefreshMenuItem_Click(object sender, EventArgs e)
		{
			await RefreshInfo(resetRemoteStatus: true);
		}

		private void CustomCommandMenuItem_Click(object sender, EventArgs e)
		{
			ExecuteCustomCommand();
		}

		private async void GitCommand_Click(object sender, EventArgs eventArgs)
		{
			GitContextMenuCommand gitCommand = sender.GetTagValue<GitContextMenuCommand>();

			if (gitCommand == null)
				return;

			BeginExecutingGitCommand();

			(bool Success, string[] ErrorOutput) result;

			try
			{
				string command = gitCommand.Command.Replace(Settings.BRANCH_IDENTIFIER, Branch);

				if (Settings.Default.ShowGitCommandOutput)
				{
					using (OutputForm errorForm = new OutputForm(outputHandler =>
						Root.SafeGitExecuteAsync(Path, command, outputHandler.AddLine),
						showInTaskbar: true))
					{
						errorForm.ShowDialog(this);
						result = errorForm.OperationResult;
					}
				}
				else
				{
					result = await Root.SafeGitExecuteAsync(Path, command);
				}
			}
			finally
			{
				DoneExecutingGitCommand();
			}

			if (result.Success)
			{
				await RefreshInfo(resetRemoteStatus: false);
			}
			else if (!Settings.Default.ShowGitCommandOutput)
			{
				using (OutputForm errorForm = new OutputForm(result.ErrorOutput, showInTaskbar: true))
				{
					errorForm.ShowDialog(this);
				}
			}
		}

		private void BeginExecutingGitCommand()
		{
			buttonMore.Visible = false;
			pictureBoxProgress.Visible = true;
		}

		private void DoneExecutingGitCommand()
		{
			buttonMore.Visible = true;
			pictureBoxProgress.Visible = false;
		}

		private async Task RefreshInfo(bool resetRemoteStatus)
		{
			try
			{
				await Task.Run(() =>
					{
						Git.TryGetBranch(Root, Path, out string branch, out int trackedChanges, out int untrackedChanges, out string[] errors);

						Branch = branch;
						TrackedChanges = trackedChanges;
						UntrackedChanges = untrackedChanges;
						Errors = errors;
					});

				UpdateInfo(resetRemoteStatus);
			}
			catch (Exception ex)
			{
				Program.ShowError("Failed to get branch and status;"
					+ Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {Path}");
			}
		}

		private static void ExecuteQuickLaunchFile(ToolStripItem item)
		{
			string quickLaunchPath = item.GetTagValue<string>();

			if (quickLaunchPath == null)
				return;

			try
			{
				using (Process.Start(quickLaunchPath)) { }
			}
			catch (Exception ex)
			{
				Program.ShowError("Failed to open file;"
					+ Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"File: {quickLaunchPath}");
			}
		}

		private void ExecuteCustomCommand()
		{
			string commandPath = Settings.Default.CommandPath;
			string commandArgs = Settings.Default.CommandArgs.Replace(Settings.PATH_IDENTIFIER, Path);
			try
			{
				using (Process.Start(new ProcessStartInfo { FileName = commandPath, Arguments = commandArgs })) { }
			}
			catch (Exception ex)
			{
				Program.ShowError("Failed to start custom command;"
					+ Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {commandPath}"
					+ Environment.NewLine + $"Args: {commandArgs}");
			}
		}

		private void CheckRemoteBranchExistance()
		{
			try
			{
				if (!Settings.Default.EnableRemoteBranchLookup)
					return;

				if (Errors != null)
					return;

				bool branchExistRemote = Git.BranchExistRemote(Root, Path, Branch, out string[] errors);
				Errors = errors;

				this.InvokeIfRequired(() =>
					{
						labelBranch.Top = Errors == null ? 3 : -20;
						linkLabelBranchError.Top = Errors != null ? 3 : -20;

						labelBranch.ForeColor = SystemColors.ControlText;
						labelBranch.Font = branchExistRemote
							? WithoutStyle(labelBranch.Font, FontStyle.Strikeout)
							: WithStyle(labelBranch.Font, FontStyle.Strikeout);
					});
			}
			catch (Exception ex)
			{
				Program.ShowError("Failed to check remote branch existence;"
					+ Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {Path}");
			}
		}

		public static Font WithStyle(Font font, FontStyle style) => new Font(font, font.Style | style);
		public static Font WithoutStyle(Font font, FontStyle style) => new Font(font, font.Style & ~style);

		protected override void OnPaint(PaintEventArgs e)
		{
			if (HighlightColor == null)
				return;

			using (Brush brush = new SolidBrush(HighlightColor.Value))
			{
				e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
				e.Graphics.FillRectangle(brush, 1, 1, Width - 2, Height - 3);
			}
		}
	}
}
