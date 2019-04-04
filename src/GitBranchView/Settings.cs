using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GitBranchView
{
	public class Settings
	{
		public class Root
		{
			public Guid Id { get; set; } = Guid.NewGuid();
			public string Path { get; set; }
			public bool Expanded { get; set; } = true;

			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
			public List<RootPathFilter> Filters { get; set; } = new List<RootPathFilter>();

			public override string ToString() => Path;

			public Root Clone() => new Root { Id = Id, Path = Path, Expanded = Expanded, Filters = Filters.Select(x => x.Clone()).ToList() };
		}

		public class RootPathFilter
		{
			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
			public FilterType Type { get; set; }

			public string Filter { get; set; }

			[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
			public HighlightColor Color { get; set; } = System.Drawing.Color.White;

			public RootPathFilter Clone() => new RootPathFilter { Type = Type, Filter = Filter, Color = Color };
		}

		public class HighlightColor
		{
			public HighlightColor(Color color)
			{
				A = color.A;
				R = color.R;
				G = color.G;
				B = color.B;
			}

			public byte A { get; set; }
			public byte R { get; set; }
			public byte G { get; set; }
			public byte B { get; set; }

			public static implicit operator Color(HighlightColor color)
				=> Color.FromArgb(color.A, color.R, color.G, color.B);

			public static implicit operator HighlightColor(Color color)
				=> new HighlightColor(color);
		}

		public enum FilterType
		{
			Include,
			Exclude,
			Highlight
		}

		public const string PATH_IDENTIFIER = "<path>";
		private const string SETTINGS_FOLDER_NAME = "GitBranchView";
		private const string SETTINGS_FILE_NAME = "settings.gbv";

		[DefaultValue(@"C:\Program Files\Git\bin\git.exe")]
		public string GitPath { get; set; }

		[DefaultValue(@"C:\Windows\system32\cmd.exe")]
		public string CommandPath { get; set; }

		[DefaultValue(@"/k cd <path>")]
		public string CommandArgs { get; set; }

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

		[DefaultValue(null)]
		public string SelectedRootPath { get; set; }

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

			if (json == null)
			{
				json = Exist
					? File.ReadAllText(_filePath, Encoding.UTF8)
					: "{}";
			}

			return Deserialize(json) ?? Deserialize("{}");
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
