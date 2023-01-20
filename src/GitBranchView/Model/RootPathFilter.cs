using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace GitBranchView.Model
{
	public enum FilterType
	{
		Include,
		Exclude,
		Highlight
	}

	[Flags]
	public enum FilterTargets
	{
		None = 0,
		Path = 1 << 0,
		Branch = 1 << 1
	}

	public class RootPathFilter
	{
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
		public FilterType Type { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
		public FilterTargets Target { get; set; } = FilterTargets.Path;

		public string Filter { get; set; }

		[JsonIgnore]
		public Regex CachedFilterRegex => Filter.GetCachedRegex();

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public HighlightColor Color { get; set; } = System.Drawing.Color.White;

		public RootPathFilter Clone() => new RootPathFilter { Type = Type, Target = Target, Filter = Filter, Color = Color };
	}
}
