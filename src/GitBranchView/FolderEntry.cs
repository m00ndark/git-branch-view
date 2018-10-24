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
using ToolComponents.Core.Extensions;

namespace GitBranchView
{
	public partial class FolderEntry : UserControl
	{
		private const string MENU_ITEM_REFRESH = "Refresh";
		private const string MENU_ITEM_CHECKOUT_MASTER_BRANCH = "Checkout Master Branch";
		private const string MENU_ITEM_PULL_FROM_REMOTE = "Pull From Remote";
		private const string MENU_ITEM_RESET_HARD = "Reset Hard And Clean All";

		private const string MASTER_BRANCH = "master";

		public event EventHandler WidthChanged;

		public FolderEntry(Settings.Root root, string path, string branch, int trackedChanges, int untrackedChanges)
		{
			InitializeComponent();

			Root = root;
			Path = path;
			Branch = branch;
			TrackedChanges = trackedChanges;
			UntrackedChanges = untrackedChanges;
			HighlightColor = root.Filters
				.Where(filter => filter.Type == Settings.FilterType.Highlight)
				.Where(filter => Regex.IsMatch(path.RelativeTo(root), filter.Filter))
				.Select(filter => (Color?) filter.Color)
				.FirstOrDefault();

			UpdateInfo();
		}

		public Settings.Root Root { get; private set; }
		public string Path { get; }
		public string Branch { get; private set; }
		public int TrackedChanges { get; private set; }
		public int UntrackedChanges { get; private set; }
		private Color? HighlightColor { get; set; }

		private void RaiseWidthChanged()
		{
			WidthChanged?.Invoke(this, EventArgs.Empty);
		}

		private void UpdateInfo()
		{
			int width = Width;

			if (TrackedChanges > 0 || UntrackedChanges > 0)
			{
				linkLabelFolder.Font = WithStyle(linkLabelFolder.Font, FontStyle.Bold);
				labelBranch.Font = WithStyle(labelBranch.Font, FontStyle.Bold);
				labelChanges.Font = WithStyle(labelChanges.Font, FontStyle.Bold);
				labelChanges.ForeColor = SystemColors.ControlText;
			}
			else
			{
				linkLabelFolder.Font = WithoutStyle(linkLabelFolder.Font, FontStyle.Bold);
				labelBranch.Font = WithoutStyle(labelBranch.Font, FontStyle.Bold);
				labelChanges.Font = WithoutStyle(labelChanges.Font, FontStyle.Bold);
				labelChanges.ForeColor = SystemColors.ControlDark;
			}

			if (labelBranch.Text != Branch)
			{
				labelBranch.Font = WithoutStyle(labelBranch.Font, FontStyle.Strikeout);
				labelBranch.ForeColor = SystemColors.ControlDark;
			}

			linkLabelFolder.Text = Path.RelativeTo(Root);
			
			labelBranch.Text = Branch;

			string trackedChanges = TrackedChanges > -1 ? TrackedChanges.ToString() : "?";
			string untrackedChanges = UntrackedChanges > -1 ? UntrackedChanges.ToString() : "?";
			labelChanges.Text = $"{trackedChanges}|{untrackedChanges}";

			UpdateSize();

			if (Width != width)
				RaiseWidthChanged();

			Task.Run(() =>
				{
					CreateContextMenu();
					CheckRemoteBranchExistance();
				});
		}

		public void UpdateSize()
		{
			labelBranch.Left = linkLabelFolder.Left + linkLabelFolder.Width + linkLabelFolder.Margin.Right;
			labelChanges.Left = labelBranch.Left + labelBranch.Width + labelBranch.Margin.Right;

			labelBranch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			labelChanges.Anchor = AnchorStyles.Top | AnchorStyles.Left;

			Width = labelBranch.Left + labelBranch.Width + labelChanges.Width + labelChanges.Margin.Right;

			labelBranch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			labelChanges.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		}

		public void RefreshRoot(Settings.Root root)
		{
			Root = root;
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
			await RefreshInfo();
		}

		private async void CheckoutMasterMenuItem_Click(object sender, EventArgs e)
		{
			bool success = await Task.Run(() =>
				{
					//if (MessageBox.Show(this, "Check out master branch?" + Environment.NewLine + path, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					//	return;

					try
					{
						if (!Git.Checkout(Path, MASTER_BRANCH))
						{
							MessageBox.Show($"Failed to checkout {MASTER_BRANCH}. Make sure it is not dirty." + Environment.NewLine
								+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

							return false;
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Failed to checkout {MASTER_BRANCH};"
							+ Environment.NewLine + ex.Message + Environment.NewLine
							+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					return true;
				});

			if (success)
				await RefreshInfo();
		}

		private async void PullFromRemoteMenuItem_Click(object sender, EventArgs e)
		{
			bool success = await Task.Run(() =>
				{
					try
					{
						if (!Git.Pull(Path))
						{
							MessageBox.Show("Failed to pull. Make sure branch is not dirty." + Environment.NewLine
								+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

							return false;
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Failed to pull;"
							+ Environment.NewLine + ex.Message + Environment.NewLine
							+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					return true;
				});

			if (success)
				await RefreshInfo();
		}

		private async void ResetAndCleanMenuItem_Click(object sender, EventArgs e)
		{
			bool success = await Task.Run(() =>
				{
					try
					{
						if (!Git.ResetHard(Path))
						{
							MessageBox.Show("Failed to reset." + Environment.NewLine
								+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

							return false;
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Failed to reset;"
							+ Environment.NewLine + ex.Message + Environment.NewLine
							+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					try
					{
						if (!Git.CleanAll(Path))
						{
							MessageBox.Show("Failed to clean." + Environment.NewLine
								+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

							return false;
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Failed to clean;"
							+ Environment.NewLine + ex.Message + Environment.NewLine
							+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					return true;
				});

			if (success)
				await RefreshInfo();
		}

		private async Task RefreshInfo()
		{
			try
			{
				await Task.Run(() =>
					{
						if (!Git.TryGetBranch(Path, out string branch, out int trackedChanges, out int untrackedChanges))
						{
							MessageBox.Show("Failed to get branch and status." + Environment.NewLine
								+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

							return;
						}

						Branch = branch;
						TrackedChanges = trackedChanges;
						UntrackedChanges = untrackedChanges;
					});

				UpdateInfo();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to get branch and status;"
					+ Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CreateContextMenu()
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
					AddMenuItem<ToolStripSeparator>();
					AddMenuItem<ToolStripMenuItem>(MENU_ITEM_CHECKOUT_MASTER_BRANCH, CheckoutMasterMenuItem_Click, enabled: Branch != MASTER_BRANCH);
					AddMenuItem<ToolStripMenuItem>(MENU_ITEM_PULL_FROM_REMOTE, PullFromRemoteMenuItem_Click);
					AddMenuItem<ToolStripMenuItem>(MENU_ITEM_RESET_HARD, ResetAndCleanMenuItem_Click);
				});
		}

		private void CheckRemoteBranchExistance()
		{
			try
			{
				bool branchExistRemote = Git.BranchExistRemote(Path, Branch);

				this.InvokeIfRequired(() =>
					{
						labelBranch.ForeColor = SystemColors.ControlText;
						labelBranch.Font = branchExistRemote
							? WithoutStyle(labelBranch.Font, FontStyle.Strikeout)
							: WithStyle(labelBranch.Font, FontStyle.Strikeout);

						ToolStripItem pullFromRemoteMenuItem = contextMenuStrip.Items
							.Cast<ToolStripItem>()
							.FirstOrDefault(x => x.Text == MENU_ITEM_PULL_FROM_REMOTE);

						if (pullFromRemoteMenuItem != null)
							pullFromRemoteMenuItem.Enabled = branchExistRemote;
					});
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to check remote branch existance;"
					+ Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {Path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private static void DisposeItems(IEnumerable items)
		{
			foreach (IDisposable disposable in items.OfType<IDisposable>().ToArray())
			{
				if (disposable is ToolStripMenuItem item)
					DisposeItems(item.DropDownItems);

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
