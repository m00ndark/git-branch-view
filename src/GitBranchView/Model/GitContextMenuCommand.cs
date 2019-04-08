namespace GitBranchView.Model
{
	public class GitContextMenuCommand
	{
		public GitContextMenuCommand(string caption = null, string command = null)
		{
			Caption = caption;
			Command = command;
		}

		public string Caption { get; set; }
		public string Command { get; set; }

		public GitContextMenuCommand Clone() => new GitContextMenuCommand(Caption, Command);
	}
}
