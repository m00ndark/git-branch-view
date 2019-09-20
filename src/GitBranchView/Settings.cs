using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using GitBranchView.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

	public class Settings
	{
		public const string PATH_IDENTIFIER = "<path>";
		public const string BRANCH_IDENTIFIER = "<branch>";

		private const string SETTINGS_FOLDER_NAME = "GitBranchView";
		private const string SETTINGS_FILE_NAME = "settings.gbv";

		[DefaultValue(@"C:\Windows\system32\cmd.exe")]
		public string CommandPath { get; set; }

		[DefaultValue("/k cd <path>")]
		public string CommandArgs { get; set; }

		[DefaultValue("Open Command Prompt")]
		public string CommandName { get; set; }

		[DefaultValue(".sln")]
		public string QuickLaunchFilesFilter { get; set; }

		[DefaultValue(QuickLaunchFilesGrouping.ByExtension)]
		public QuickLaunchFilesGrouping QuickLaunchFilesGrouping { get; set; }

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

		[DefaultValue(null)]
		public string SelectedRootPath { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Dictionary<string, string> SelectedQuickLaunchFiles { get; set; } = new Dictionary<string, string>();

		// ---------------------------------------------------------------------

		private static Settings _settings = null;
		private static readonly object _lock = new object();
		private static readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), SETTINGS_FOLDER_NAME);
		private static readonly string _filePath = Path.Combine(_path, SETTINGS_FILE_NAME);

		private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
			{
				DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
				Converters = new JsonConverter[] { new StringEnumConverter() }
			};

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
							_settings = Load();
						}
					}
				}
				return _settings;
			}
		}

		public static bool Exist => File.Exists(_filePath);

		private static Settings Load()
		{
			string json = null;

			if (!Exist)
			{
				string legacyFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Qlik", SETTINGS_FOLDER_NAME, SETTINGS_FILE_NAME);

				if (File.Exists(legacyFilePath))
					json = File.ReadAllText(legacyFilePath, Encoding.UTF8);
			}

			if (json == null && Exist)
			{
				json = File.ReadAllText(_filePath, Encoding.UTF8);
			}

			return Deserialize(string.IsNullOrWhiteSpace(json) ? "{}" : json).PostProcess();
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

		public void Save()
		{
			Directory.CreateDirectory(_path);

			if (Exist && !Backup()) return;

			try
			{
				File.WriteAllText(_filePath, Serialize(this), Encoding.UTF8);
			}
			catch
			{
				Restore();
			}
		}

		private static bool Backup()
		{
			try
			{
				File.Copy(_filePath, _filePath + ".bak", true);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private static bool Restore()
		{
			try
			{
				File.Copy(_filePath + ".bak", _filePath, true);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private static Settings Deserialize(string json) => JsonConvert.DeserializeObject<Settings>(json, _serializerSettings);

		private static string Serialize(Settings settings) => JsonConvert.SerializeObject(settings, Formatting.Indented, _serializerSettings);
	}
}
