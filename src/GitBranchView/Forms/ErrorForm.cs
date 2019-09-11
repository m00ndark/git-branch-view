using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GitBranchView.Forms
{
	public partial class ErrorForm : Form
	{
		public ErrorForm(IEnumerable<string> errors, string title = null)
		{
			InitializeComponent();
			Errors = errors;
			Title = title;
		}

		public IEnumerable<string> Errors { get; }
		public string Title { get; }

		private void MessageDetailsForm_Load(object sender, EventArgs e)
		{
			textBoxDetails.Text = string.Join(Environment.NewLine, Errors);
			if (Title != null) Text = Title;
			buttonClose.Focus();
		}

		private void ButtonClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
