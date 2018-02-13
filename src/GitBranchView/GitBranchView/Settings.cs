using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GitBranchView
{
	public class Settings
	{
		public const string PATH_IDENTIFIER = "<path>";

		[DefaultValue(@"C:\Program Files\Git\bin\git.exe")]
		public string GitPath { get; set; }

		[DefaultValue(@"C:\Windows\system32\cmd.exe")]
		public string CommandPath { get; set; }

		[DefaultValue(@"/k cd <path>")]
		public string CommandArgs { get; set; }

		[DefaultValue("")]
		public string RootPath { get; set; }

		[DefaultValue(true)]
		public bool CloseOnLostFocus { get; set; }

		[DefaultValue(false)]
		public bool StartWithWindows { get; set; }

		// ---------------------------------------------------------------------

		private static Settings _settings = null;
		private static readonly object _lock = new object();
		private static readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Qlik", "GitBranchView");
		private static readonly string _filePath = Path.Combine(_path, "settings.gbv");

		private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
			{
				DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
				//ContractResolver = new PokeSettingsContractResolver(),
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
			string json = Exist
				? File.ReadAllText(_filePath, Encoding.UTF8)
				: "{}";

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
