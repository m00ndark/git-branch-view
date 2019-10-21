using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitBranchView.Model;

namespace GitBranchView.Forms
{
	public partial class CloneForm : Form
	{
		private const string REPOSITORY_URL_PATTERN = @"^[^:]+://(?:([^/]+)/)*(.+)\.git$";
		private bool _isTerminating;

		public event EventHandler ClonedSuccessfully;

		public CloneForm(Root root)
		{
			Root = root;
			InitializeComponent();
			labelRootFolder.Text = Root.Path;
		}

		public Root Root { get; }

		public void ShowForm()
		{
			if ((int) Opacity == 0)
			{
				Opacity = 1;
				ClearInput();
			}

			Show();
			try { NativeMethods.SetForegroundWindow(Handle); } catch { ; }
		}

		public void TerminateForm()
		{
			_isTerminating = true;
		}

		private void HideForm()
		{
			Hide();
			Opacity = 0;
		}

		private void RaiseClonedSuccessfullyEvent()
		{
			Task.Run(() => ClonedSuccessfully?.Invoke(this, EventArgs.Empty));
		}

		private void CloneForm_Load(object sender, EventArgs e)
		{
			MinimumSize = Size;
		}

		private void CloneForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_isTerminating)
				return;

			e.Cancel = true;
			HideForm();
		}

		private void TextBoxRepositoryUrl_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(textBoxFolderName.Text))
				return;

			Match match = Regex.Match(textBoxRepositoryUrl.Text, REPOSITORY_URL_PATTERN, RegexOptions.IgnoreCase);

			if (!match.Success)
				return;

			textBoxFolderName.Text = match.Groups[2].Value;
		}

		private async void TextBoxRepositoryUrl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				await Clone();
		}

		private async void TextBoxFolderName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				await Clone();
		}

		private async void ButtonClone_Click(object sender, EventArgs e)
		{
			await Clone();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			textBoxRepositoryUrl.Focus();
			HideForm();
		}

		private void ClearInput()
		{
			textBoxRepositoryUrl.Clear();
			textBoxFolderName.Clear();
			textBoxRepositoryUrl.Focus();
		}

		private bool InputIsValid()
		{
			if (string.IsNullOrWhiteSpace(textBoxRepositoryUrl.Text))
			{
				Program.ShowError("Repository URL cannot be empty!");
				return false;
			}

			if (!Regex.IsMatch(textBoxRepositoryUrl.Text, REPOSITORY_URL_PATTERN, RegexOptions.IgnoreCase))
			{
				Program.ShowError("Repository URL is not a valid git URL!");
				return false;
			}

			return true;
		}

		private async Task Clone()
		{
			if (!InputIsValid())
				return;

			try
			{
				BeginCloning();

				string url = textBoxRepositoryUrl.Text;
				string folder = textBoxFolderName.Text;
				(bool Success, string[] ErrorOutput) result;

				try
				{
					string command = $"clone --progress {url} {folder}";

					if (Settings.Default.ShowGitCommandOutput)
					{
						using (OutputForm errorForm = new OutputForm(outputHandler =>
							Root.SafeGitExecuteAsync(Root.Path, command, outputHandler.AddLine),
							outputIsProgress: true))
						{
							errorForm.ShowDialog(this);
							result = errorForm.OperationResult;
						}
					}
					else
					{
						result = await Root.SafeGitExecuteAsync(Root.Path, command);
					}
				}
				finally
				{
					DoneCloning();
				}

				if (result.Success)
				{
					RaiseClonedSuccessfullyEvent();
					HideForm();
				}
				else if (!Settings.Default.ShowGitCommandOutput)
				{
					using (OutputForm errorForm = new OutputForm(result.ErrorOutput))
					{
						errorForm.ShowDialog(this);
					}
				}
			}
			finally
			{
				EndCloning();
			}
		}

		private void BeginCloning()
		{
			textBoxRepositoryUrl.Enabled = false;
			textBoxFolderName.Enabled = false;
			tableLayoutPanelButtons.Enabled = false;
			tableLayoutPanelButtons.Visible = false;
			tableLayoutPanelProgress.Visible = true;
		}

		private void DoneCloning()
		{
			tableLayoutPanelButtons.Visible = true;
			tableLayoutPanelProgress.Visible = false;
		}

		private void EndCloning()
		{
			textBoxRepositoryUrl.Enabled = true;
			textBoxFolderName.Enabled = true;
			tableLayoutPanelButtons.Enabled = true;
		}
	}
}
