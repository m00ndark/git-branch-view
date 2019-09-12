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

		public void HighlightChanged()
		{
			SetHighlightColor();
			Refresh();
		}

		public void GitContextMenuCommandsChanged()
		{
			ResetContextMenu();
		}

		private void ButtonMore_Click(object sender, EventArgs e)
		{
			Point position = buttonMore.Parent.PointToScreen(new Point(buttonMore.Location.X, buttonMore.Location.Y + buttonMore.Size.Height));
			contextMenuStrip.Show(position);
		}

		private void LinkLabelPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string commandPath = Settings.Default.CommandPath;
			string commandArgs = Settings.Default.CommandArgs.Replace(Settings.PATH_IDENTIFIER, Path);
			try
			{
				using (Process.Start(new ProcessStartInfo { FileName = commandPath, Arguments = commandArgs })) { }
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to start link command;"
					+ Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {commandPath}"
					+ Environment.NewLine + $"Args: {commandArgs}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void LinkLabelBranchError_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			new ErrorForm(Errors.Select((error, i) => i == 0 ? $"{Path}> {error}" : error)).Show(this);
		}

		private static void SolutionPathMenuItem_Click(object sender, EventArgs e)
		{
			if (!(sender is ToolStripMenuItem menuItem))
				return;

			string solutionPath = menuItem.Tag.ToString();

			try
			{
				using (Process.Start(solutionPath)) { }
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to open solution;"
					+ Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"File: {solutionPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void RefreshMenuItem_Click(object sender, EventArgs e)
		{
			await RefreshInfo(resetRemoteStatus: true);
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
				MessageBox.Show("Failed to get branch and status;"
					+ Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ResetContextMenu()
		{
			contextMenuStrip.InvokeIfRequired(() =>
				{
					DisposeItems(contextMenuStrip.Items);
					contextMenuStrip.Items.Clear();

					List<string> solutionPaths = Directory.EnumerateFiles(Path, "*.sln", SearchOption.AllDirectories).ToList();

					if (solutionPaths.Count > 0)
					{
						foreach (string solutionPath in solutionPaths)
						{
							AddMenuItem<ToolStripMenuItem>(solutionPath.Substring(Path.Length + 1), SolutionPathMenuItem_Click, solutionPath);
						}

						AddMenuItem<ToolStripSeparator>();
					}

					AddMenuItem<ToolStripMenuItem>(MENU_ITEM_REFRESH, RefreshMenuItem_Click);

					if (Settings.Default.GitContextMenuCommands.Any())
					{
						AddMenuItem<ToolStripSeparator>();

						foreach (GitContextMenuCommand gitCommand in Settings.Default.GitContextMenuCommands
							.Where(x => !string.IsNullOrWhiteSpace(x.Caption) && !string.IsNullOrWhiteSpace(x.Command)))
						{
							AddMenuItem<ToolStripMenuItem>(gitCommand.Caption, GitCommand_Click, gitCommand);
						}
					}
				});
		}

		private async void GitCommand_Click(object sender, EventArgs eventArgs)
		{
			if (!((sender as ToolStripMenuItem)?.Tag is GitContextMenuCommand gitCommand))
				return;

			string command = gitCommand.Command.Replace(Settings.BRANCH_IDENTIFIER, Branch);

			bool success = await Task.Run(() =>
				{
					try
					{
						if (!Git.ExecuteCommand(Root, Path, command))
						{
							MessageBox.Show($"Failed to execute Git command '{command}'." + Environment.NewLine
								+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

							return false;
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Failed to execute Git command '{command}';"
							+ Environment.NewLine + ex.Message + Environment.NewLine
							+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					return true;
				});

			if (success)
				await RefreshInfo(resetRemoteStatus: false);
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
				MessageBox.Show("Failed to check remote branch existence;"
					+ Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void DisposeItems(IEnumerable items)
		{
			foreach (IDisposable disposable in items.OfType<IDisposable>().ToArray())
			{
				if (disposable is ToolStripMenuItem item)
				{
					DisposeItems(item.DropDownItems);
					item.Click -= GitCommand_Click;
				}

				disposable.Dispose();
			}
		}

		private void AddMenuItem<T>(string text = null, EventHandler eventHandler = null, object tag = null, bool enabled = true) where T : ToolStripItem, new()
		{
			T checkoutMasterMenuItem = new T { Text = text, Tag = tag, Enabled = enabled };
			if (eventHandler != null) checkoutMasterMenuItem.Click += eventHandler;
			contextMenuStrip.Items.Add(checkoutMasterMenuItem);
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
