using System;
using System.Diagnostics;
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
			Width = linkLabelFolder.Left + labelBranch.Left + labelBranch.Width;
			labelBranch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		}

		private void LinkLabelPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string commandPath = Settings.Default.CommandPath;
			string commandArgs = Settings.Default.CommandArgs.Replace(Settings.PATH_IDENTIFIER, Path);
			try
			{
				using (Process explorerProcess = new Process
					{
						StartInfo =
							{
								FileName = commandPath,
								Arguments = commandArgs
							}
					})
				{
					explorerProcess.Start();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to start link command;" + Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {commandPath}"
					+ Environment.NewLine + $"Args: {commandArgs}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
