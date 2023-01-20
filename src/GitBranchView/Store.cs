using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using IOPath = System.IO.Path;

namespace GitBranchView
{
	public abstract class Store
	{
		protected const string APPLICATION_FOLDER_NAME = "GitBranchView";

		private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
			{
				DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
				Converters = new JsonConverter[] { new StringEnumConverter() }
			};

		protected static string Path { get; } = IOPath.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), APPLICATION_FOLDER_NAME);

		protected abstract string FilePath { get; }

		protected static bool Exist(string filePath) => File.Exists(filePath);

		protected static TStore Load<TStore>(string filePath)
			where TStore : Store
		{
			string json = Exist(filePath)
				? File.ReadAllText(filePath, Encoding.UTF8)
				: null;

			return Deserialize<TStore>(json);
		}

		public void Save()
		{
			Directory.CreateDirectory(Path);

			if (Exist(FilePath) && !Backup()) return;

			try
			{
				File.WriteAllText(FilePath, Serialize(this), Encoding.UTF8);
			}
			catch
			{
				Restore();
			}
		}

		private bool Backup()
		{
			try
			{
				File.Copy(FilePath, FilePath + ".bak", true);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private bool Restore()
		{
			try
			{
				File.Copy(FilePath + ".bak", FilePath, true);
				return true;
			}
			catch
			{
				return false;
			}
		}

		protected static TStore Deserialize<TStore>(string json)
			where TStore : Store
			=> JsonConvert.DeserializeObject<TStore>(string.IsNullOrWhiteSpace(json) ? "{}" : json, _serializerSettings);

		protected static string Serialize<TStore>(TStore store)
			where TStore : Store
			=> JsonConvert.SerializeObject(store, Formatting.Indented, _serializerSettings);
	}
}