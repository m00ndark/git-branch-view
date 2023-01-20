using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using GitBranchView.Model;
using Newtonsoft.Json;
using IOPath = System.IO.Path;

namespace GitBranchView
{
	public enum QuickLaunchFilesGrouping
	{
		None,
		ByExtension,
		ByPath
	}

	public enum RepositoryLinkBehavior
	{
		ExecuteCustomCommand,
		LaunchSelectedQuickLaunchFile
	}

	public class Settings : Store
	{
		public const string PATH_IDENTIFIER = "<path>";
		public const string BRANCH_IDENTIFIER = "<branch>";

		private const string SETTINGS_FILE_NAME = "settings.gbv";

		#region Settings

		[DefaultValue(@"C:\Windows\system32\cmd.exe")]
		public string CommandPath { get; set; }

		[DefaultValue("/k cd <path>")]
		public string CommandArgs { get; set; }

		[DefaultValue("Open Command Prompt")]
		public string CommandName { get; set; }

		[DefaultValue(".sln")]
		public string QuickLaunchFilesFilter { get; set; }

		[JsonIgnore]
		public Regex CachedQuickLaunchFilesFilterRegex => QuickLaunchFilesFilter.GetCachedRegex(RegexOptions.IgnoreCase);

		[DefaultValue(QuickLaunchFilesGrouping.ByExtension)]
		public QuickLaunchFilesGrouping QuickLaunchFilesGrouping { get; set; }

		[DefaultValue(false)]
		public bool ShowFrequentQuickLaunchFiles { get; set; }

		[DefaultValue(3)]
		public int FrequentQuickLaunchFilesCount { get; set; }

		[DefaultValue(RepositoryLinkBehavior.ExecuteCustomCommand)]
		public RepositoryLinkBehavior RepositoryLinkBehavior { get; set; }

		[DefaultValue(@"C:\Program Files\Git\bin\git.exe")]
		public string GitPath { get; set; }

		[DefaultValue(null)]
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public List<GitContextMenuCommand> GitContextMenuCommands { get; set; }

		[DefaultValue(null)]
		[JsonProperty("RootPath")]
		public string ObsoleteRootPath { set { if (value != null) Roots.Insert(0, new Root { Path = value }); } }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public List<Root> Roots { get; set; } = new List<Root>();

		[DefaultValue(true)]
		public bool CloseOnLostFocus { get; set; }

		[DefaultValue(false)]
		public bool StartWithWindows { get; set; }

		[DefaultValue(false)]
		public bool EnableLogging { get; set; }

		[DefaultValue(true)]
		public bool EnableRemoteBranchLookup { get; set; }

		[DefaultValue(true)]
		public bool ShowGitCommandOutput { get; set; }

		[DefaultValue(true)]
		public bool ExcludeLfsRepositories { get; set; }

		[DefaultValue(null)]
		public string SelectedRootPath { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Dictionary<string, string> SelectedQuickLaunchFiles { get; set; } = new Dictionary<string, string>();

		#endregion

		// ---------------------------------------------------------------------

		private static Settings _settings = null;
		private static readonly object _lock = new object();
		private static readonly string _filePath = IOPath.Combine(Path, SETTINGS_FILE_NAME);

		protected override string FilePath => _filePath;

		public static Settings Default
		{
			get
			{
				if (_settings == null)
				{
					lock (_lock)
					{
						if (_settings == null)
						{
							bool loadedLegacySettings = false;

							if (!Exist(_filePath))
							{
								string legacyFilePath = IOPath.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Qlik", APPLICATION_FOLDER_NAME, SETTINGS_FILE_NAME);

								if (File.Exists(legacyFilePath))
								{
									string json = File.ReadAllText(legacyFilePath, Encoding.UTF8);
									_settings = Deserialize<Settings>(json).PostProcess();
									loadedLegacySettings = true;
								}
							}

							if (!loadedLegacySettings)
								_settings = Load<Settings>(_filePath).PostProcess();
						}
					}
				}
				return _settings;
			}
		}

		private Settings PostProcess()
		{
			if (GitContextMenuCommands == null)
			{
				GitContextMenuCommands = new List<GitContextMenuCommand>
					{
						new GitContextMenuCommand("Checkout Master Branch", "checkout master"),
						new GitContextMenuCommand("Pull From Remote", "pull"),
						new GitContextMenuCommand("Reset Hard", "reset --hard"),
						new GitContextMenuCommand("Clean All", "clean -fdx")
					};
			}

			return this;
		}
	}
}
