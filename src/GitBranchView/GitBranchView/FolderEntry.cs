using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitBranchView
{
	public partial class FolderEntry : UserControl
	{
		public FolderEntry(string rootPath, string path, string branch)
		{
			InitializeComponent();
			RootPath = rootPath;
			Path = path;
			Branch = branch;
			UpdateInfo();
			Task.Run(() => ScanForSolutions());
		}

		public string RootPath { get; }
		public string Path { get; }
		public string Branch { get; }

		private void UpdateInfo()
		{
			linkLabelFolder.Text = Path.Substring(RootPath.Length + 1);
			labelBranch.Text = Branch;
			labelBranch.Left = linkLabelFolder.Left + linkLabelFolder.Width + linkLabelFolder.Margin.Right;
			labelBranch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			Width = labelBranch.Left + labelBranch.Width + labelBranch.Margin.Right;
			labelBranch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
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
			if (sender is ToolStripMenuItem solutionPathMenuItem)
			{
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
		}

		private void ScanForSolutions()
		{
			foreach (string solutionPath in Directory.EnumerateFiles(Path, "*.sln", SearchOption.AllDirectories))
			{
				ToolStripMenuItem solutionPathMenuItem = new ToolStripMenuItem(solutionPath.Substring(Path.Length + 1)) { Tag = solutionPath };
				solutionPathMenuItem.Click += SolutionPathMenuItem_Click;
				contextMenuStrip.Items.Add(solutionPathMenuItem);
			}
		}
	}
}
