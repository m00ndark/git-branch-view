using System;
using System.Reflection;

namespace GitBranchView
{
	public static class Extensions
	{
		public static string GetVersion(this Assembly assembly)
		{
			return assembly.GetName().Version.ToString();
		}

		public static string GetBuildDate(this Assembly assembly)
		{
			// Assumes that in AssemblyInfo.cs, the version is specified as 1.0.* or the like,
			// with only 2 numbers specified; the next two are generated from the date

			Version version = assembly.GetName().Version;
			DateTime buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
			return $"{buildDate:yyyy-MM-dd}";
		}

		public static string GetBuildVersion(this Assembly assembly)
		{
			return $"{assembly.GetVersion()} @ {assembly.GetBuildDate()}";
		}
	}
}
