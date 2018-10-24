using System;
using System.Windows.Forms;
using GitBranchView.Generated;

namespace GitBranchView
{
	static class Program
	{
		public const string GIT_BRANCH_VIEW = "Git Branch View";

		[STAThread]
		static void Main()
		{
			AssemblyResolver.Setup();
			Startup.EnsureLocallyInstalled();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
