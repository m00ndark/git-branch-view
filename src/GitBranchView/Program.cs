using System;
using System.Windows.Forms;
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
	}
}
