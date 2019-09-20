using System;
using System.Windows.Forms;
using GitBranchView.Forms;
using GitBranchView.Generated;
using ToolComponents.Core.Logging;

namespace GitBranchView
{
	public static class Program
	{
		public const string GIT_BRANCH_VIEW = "Git Branch View";

		[STAThread]
		public static void Main()
		{
			AssemblyResolver.Setup();
			Startup.EnsureLocallyInstalled();

			WrapWithLogging(() =>
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new MainForm());
				});
		}

		private static void WrapWithLogging(Action action)
		{
			try
			{
				Logger.Initialize(null, nameof(GitBranchView), DefaultLogEntry.Headers);

				action();
			}
			catch (Exception ex)
			{
				Logger.Add("Fatal error", ex);
			}
			finally
			{
				Logger.Terminate();
			}
		}

		public static DialogResult ShowInfo(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
		{
			return MessageBox.Show(message, GIT_BRANCH_VIEW, buttons, MessageBoxIcon.Information);
		}

		public static DialogResult ShowQuestion(string message, MessageBoxButtons buttons = MessageBoxButtons.YesNo)
		{
			return MessageBox.Show(message, GIT_BRANCH_VIEW, buttons, MessageBoxIcon.Question);
		}

		public static DialogResult ShowError(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
		{
			return MessageBox.Show(message, GIT_BRANCH_VIEW, buttons, MessageBoxIcon.Error);
		}
	}
}
