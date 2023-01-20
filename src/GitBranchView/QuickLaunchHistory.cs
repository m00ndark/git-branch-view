using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using IOPath = System.IO.Path;

namespace GitBranchView
{
	public class QuickLaunchItem
	{
		public DateTime LastUsed { get; set; }
		public string FilePath { get; set; }
		public int LaunchCount { get; set; } = 1;
	}

	public class QuickLaunchHistory : Store
	{
		private const string HISTORY_FILE_NAME = "quicklaunchhistory.gbv";

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		private Dictionary<string, List<QuickLaunchItem>> Files { get; set; } = new Dictionary<string, List<QuickLaunchItem>>();

		public bool TryGet(string path, out List<QuickLaunchItem> quickLaunchItems)
		{
			return Files.TryGetValue(path.ToLower(), out quickLaunchItems);
		}

		public bool Add(string path, string filePath)
		{
			path = path.ToLower();

			if (!Files.ContainsKey(path))
			{
				Files.Add(path, new List<QuickLaunchItem>());
			}

			QuickLaunchItem existingItem = Files[path]
				.FirstOrDefault(item => string.Equals(item.FilePath, filePath, StringComparison.InvariantCultureIgnoreCase));

			try
			{
				if (existingItem != null)
				{
					int order = GetOrder(path, existingItem);
					existingItem.LastUsed = DateTime.Now;
					existingItem.LaunchCount++;
					return GetOrder(path, existingItem) != order;
				}
				else
				{
					Files[path].Add(new QuickLaunchItem { LastUsed = DateTime.Now, FilePath = filePath });
					return true;
				}
			}
			finally
			{
				Save();
			}
		}

		private int GetOrder(string path, QuickLaunchItem item)
		{
			return Files[path]
				.OrderByDescending(x => x.LaunchCount)
				.ToList()
				.IndexOf(item);
		}

		// ---------------------------------------------------------------------

		private static QuickLaunchHistory _history = null;
		private static readonly object _lock = new object();
		private static readonly string _filePath = IOPath.Combine(Path, HISTORY_FILE_NAME);

		protected override string FilePath => _filePath;

		public static QuickLaunchHistory Default
		{
			get
			{
				if (_history == null)
				{
					lock (_lock)
					{
						if (_history == null)
						{
							_history = Load<QuickLaunchHistory>(_filePath);
						}
					}
				}
				return _history;
			}
		}
	}
}