using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolComponents.Core.Extensions;

namespace GitBranchView
{
	public partial class FolderEntry : UserControl
	{
		public FolderEntry(string rootPath, string path, string branch, int trackedChanges, int untrackedChanges)
		{
			InitializeComponent();
			RootPath = rootPath;
			Path = path;
			Branch = branch;
			TrackedChanges = trackedChanges;
			UntrackedChanges = untrackedChanges;
			UpdateInfo();
		}

		public string RootPath { get; }
		public string Path { get; }
		public string Branch { get; private set; }
		public int TrackedChanges { get; private set; }
		public int UntrackedChanges { get; private set; }

		private void UpdateInfo(bool restoreWidth = false)
		{
			int width = Width;

			if (TrackedChanges > 0 || UntrackedChanges > 0)
			{
				linkLabelFolder.Font = new Font(linkLabelFolder.Font, FontStyle.Bold);
				labelBranch.Font = new Font(labelBranch.Font, FontStyle.Bold);
				labelChanges.Font = new Font(labelChanges.Font, FontStyle.Bold);
				labelChanges.ForeColor = SystemColors.ControlText;
			}
			else
			{
				linkLabelFolder.Font = new Font(linkLabelFolder.Font, FontStyle.Regular);
				labelBranch.Font = new Font(labelBranch.Font, FontStyle.Regular);
				labelChanges.Font = new Font(labelChanges.Font, FontStyle.Regular);
				labelChanges.ForeColor = SystemColors.ControlDark;
			}

			linkLabelFolder.Text = Path.Substring(RootPath.Length + 1);

			labelBranch.Text = Branch;
			labelBranch.Left = linkLabelFolder.Left + linkLabelFolder.Width + linkLabelFolder.Margin.Right;
			labelBranch.Anchor = AnchorStyles.Top | AnchorStyles.Left;

			string trackedChanges = TrackedChanges > -1 ? TrackedChanges.ToString() : "?";
			string untrackedChanges = UntrackedChanges > -1 ? UntrackedChanges.ToString() : "?";
			labelChanges.Text = $"{trackedChanges}|{untrackedChanges}";
			labelChanges.Left = labelBranch.Left + labelBranch.Width + labelBranch.Margin.Right;
			labelChanges.Anchor = AnchorStyles.Top | AnchorStyles.Left;

			Width = labelBranch.Left + labelBranch.Width + labelChanges.Width + labelChanges.Margin.Right;

			labelBranch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			labelChanges.Anchor = AnchorStyles.Top | AnchorStyles.Right;

			if (restoreWidth)
				Width = width;

			Task.Run(() => CreateContextMenu());
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
				MessageBox.Show("Failed to start link command;" + Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {commandPath}"
					+ Environment.NewLine + $"Args: {commandArgs}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void SolutionPathMenuItem_Click(object sender, EventArgs e)
		{
			if (!(sender is ToolStripMenuItem solutionPathMenuItem))
				return;

			string solutionPath = solutionPathMenuItem.Tag.ToString();

			try
			{
				using (Process.Start(solutionPath)) { }
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to open solution;" + Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"File: {solutionPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CheckoutMasterMenuItem_Click(object sender, EventArgs e)
		{
			if (!(sender is ToolStripMenuItem checkoutMasterMenuItem))
				return;

			string branch = "master";
			string path = checkoutMasterMenuItem.Tag.ToString();

			try
			{
				if (!Git.Checkout(path, branch))
				{
					MessageBox.Show($"Failed to checkout {branch}. Make sure it is not dirty." + Environment.NewLine
						+ Environment.NewLine + $"Path: {path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

					return;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Failed to checkout {branch};" + Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			try
			{
				if (!Git.TryGetBranch(path, out branch, out int trackedChanges, out int untrackedChanges))
				{
					MessageBox.Show("Failed to get branch and status." + Environment.NewLine
						+ Environment.NewLine + $"Path: {path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

					return;
				}

				Branch = branch;
				TrackedChanges = trackedChanges;
				UntrackedChanges = untrackedChanges;

				UpdateInfo(restoreWidth: true);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to get branch and status;" + Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CreateContextMenu()
		{
			contextMenuStrip.InvokeIfRequired(() =>
				{
					DisposeToolStripItems(contextMenuStrip.Items);
					contextMenuStrip.Items.Clear();

					List<string> solutionPaths = Directory.EnumerateFiles(Path, "*.sln", SearchOption.AllDirectories).ToList();

					if (solutionPaths.Count > 0)
					{
						foreach (string solutionPath in solutionPaths)
						{
							ToolStripMenuItem solutionPathMenuItem = new ToolStripMenuItem(solutionPath.Substring(Path.Length + 1)) { Tag = solutionPath };
							solutionPathMenuItem.Click += SolutionPathMenuItem_Click;
							contextMenuStrip.Items.Add(solutionPathMenuItem);
						}

						contextMenuStrip.Items.Add(new ToolStripSeparator());
					}

					ToolStripMenuItem checkoutMasterMenuItem = new ToolStripMenuItem("Checkout Master Branch") { Tag = Path };
					checkoutMasterMenuItem.Click += CheckoutMasterMenuItem_Click;
					contextMenuStrip.Items.Add(checkoutMasterMenuItem);
				});
		}

		private static void DisposeToolStripItems(ToolStripItemCollection items)
		{
			foreach (IDisposable disposable in items.Cast<IDisposable>().ToArray())
			{
				if (disposable is ToolStripMenuItem item)
					DisposeToolStripItems(item.DropDownItems);

				disposable.Dispose();
			}
		}
	}
}
