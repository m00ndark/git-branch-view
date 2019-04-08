using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace GitBranchView.Model
{
	public class Root
	{
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Guid Id { get; set; } = Guid.NewGuid();

		public string Path { get; set; }
		public bool Expanded { get; set; } = true;

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public List<RootPathFilter> Filters { get; set; } = new List<RootPathFilter>();

		public override string ToString() => Path;

		public Root Clone() => new Root { Id = Id, Path = Path, Expanded = Expanded, Filters = Filters.Select(x => x.Clone()).ToList() };
	}
}
