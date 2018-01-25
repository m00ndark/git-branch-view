using System;
using System.Configuration;
using ToolComponents.Core;
using ToolComponents.Core.Dialogs;
using ToolComponents.Core.Extensions;
using ToolComponents.Core.Logging;

namespace GitBranchView.Generated
{
	public class Startup
	{
		private static readonly Func<IFolderDialog> _folderDialogProvider;

		static Startup()
		{
			_folderDialogProvider = () => new ToolComponents.Core.WinForms.Dialogs.FolderDialog();
		}

		public static string ExePath => StartupInformation.ExecutablePath;

		public static string Path => StartupInformation.StartupPath;

		public static string[] CommandLineArgs => StartupInformation.CommandLineArgs;

		public static string RawCommandLineArgs => StartupInformation.RawCommandLineArgs;

		public static void EnsureLocallyInstalled(string[] additionalInstallFiles = null)
		{
			LocalInstaller.EnsureInstalled(_folderDialogProvider, additionalInstallFiles);
		}

		public static void ExtractAppConfig(ApplicationSettingsBase settings)
		{
			settings.ExtractAppConfig();
		}

		public static void WrapWithLogging(string appName, Action action)
		{
			try
			{
				Logger.Initialize(appName, DefaultLogEntry.Headers);

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

		public static void RunServiceStartupSequence<TService>(string serviceName, Action standaloneStartAction)
		{
			ServiceStartupSequence.Run<TService>(serviceName, standaloneStartAction);
		}
	}
}
