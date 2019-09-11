using Newtonsoft.Json;

namespace GitBranchView.Model
{
	public enum FilterType
	{
		Include,
		Exclude,
		Highlight
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
}
