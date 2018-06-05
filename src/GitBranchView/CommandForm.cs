using System;
using System.IO;
using System.Windows.Forms;

namespace GitBranchView
{
	public partial class CommandForm : Form
	{
		public CommandForm(string commandPath, string commandArgs)
		{
			InitializeComponent();
			CommandPath = commandPath;
			CommandArgs = commandArgs;
		}

		public string CommandPath { get; private set; }
		public string CommandArgs { get; private set; }

		private void ShellExeSelectForm_Load(object sender, EventArgs e)
		{
			textBoxPath.Text = CommandPath;
			textBoxArgs.Text = CommandArgs;
		}

		private void ButtonBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog
				{
					Title = "Select an executable...",
					CheckFileExists = true,
					Filter = "Executable Files (*.exe)|*.exe",
					Multiselect = false,
					InitialDirectory = Path.GetDirectoryName(CommandPath),
					FileName = CommandPath
				};

			if (fileDialog.ShowDialog() != DialogResult.OK)
				return;

			textBoxPath.Text = fileDialog.FileName;
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			CommandPath = textBoxPath.Text;
			CommandArgs = textBoxArgs.Text;
			Close();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
