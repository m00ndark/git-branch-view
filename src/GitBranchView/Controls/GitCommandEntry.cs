using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitBranchView.Forms;
using GitBranchView.Model;

namespace GitBranchView.Controls
{
	public partial class GitCommandEntry : UserControl
	{
		public class ActionEventArgs : EventArgs
		{
			public ActionEventArgs(EntryAction action)
			{
				Action = action;
			}

			public EntryAction Action { get; }
		}

		public event EventHandler<ActionEventArgs> Action;
		public event EventHandler Changed;

		public GitCommandEntry(GitContextMenuCommand gitCommand)
		{
			InitializeComponent();
			GitCommand = gitCommand;
			UpdateInfo();
		}

		public GitContextMenuCommand GitCommand { get; }

		private void GitCommandEntry_Load(object sender, EventArgs e)
		{
			Anchor = AnchorStyles.Left | AnchorStyles.Right;
		}

		private void ButtonDelete_Click(object sender, EventArgs e)
		{
			RaiseActionEvent(EntryAction.Delete);
		}

		private void ButtonMoveUp_Click(object sender, EventArgs e)
		{
			RaiseActionEvent(EntryAction.MoveUp);
		}

		private void ButtonMoveDown_Click(object sender, EventArgs e)
		{
			RaiseActionEvent(EntryAction.MoveDown);
		}

		private void TextBoxCaption_TextChanged(object sender, EventArgs e)
		{
			GitCommand.Caption = textBoxCaption.Text;
			RaiseChangedEvent();
		}

		private void TextBoxCommand_TextChanged(object sender, EventArgs e)
		{
			GitCommand.Command = textBoxCommand.Text;
			RaiseChangedEvent();
		}

		private void UpdateInfo()
		{
			textBoxCaption.Text = GitCommand.Caption;
			textBoxCommand.Text = GitCommand.Command;
		}

		private void RaiseActionEvent(EntryAction action)
		{
			Action?.BeginInvoke(this, new ActionEventArgs(action), null, null);
		}

		private void RaiseChangedEvent()
		{
			Changed?.BeginInvoke(this, EventArgs.Empty, null, null);
		}
	}
}
