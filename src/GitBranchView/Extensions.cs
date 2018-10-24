using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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

		public static bool GitPathIsValid(this string gitPath, out string error)
		{
			if (string.IsNullOrEmpty(gitPath))
			{
				error = "Path to Git executable not selected.";
				return false;
			}

			if (!File.Exists(gitPath))
			{
				error = "Path to Git executable does not exist.";
				return false;
			}

			error = null;
			return true;
		}

		public static bool IsValid(this Settings.Root root, out string error)
		{
			if (string.IsNullOrWhiteSpace(root?.Path))
			{
				error = "No root folder selected.";
				return false;
			}

			if (!Directory.Exists(root.Path))
			{
				error = $"Root folder '{root.Path}' does not exist.";
				return false;
			}

			error = null;
			return true;
		}

		public static string RelativeTo(this string path, Settings.Root root)
		{
			return root.Path.Length >= path.Length
				? "/"
				: path.Substring(root.Path.Length + 1);
		}

		public static bool ShouldInclude(this Settings.Root root, string path)
		{
			return root.Filters
				.Where(filter => filter.Type == Settings.FilterType.Exclude)
				.All(filter => !Regex.IsMatch(path.RelativeTo(root), filter.Filter));
		}

		public static IEnumerable<string> ScanFolder(this string path)
		{
			if (Directory.Exists(Path.Combine(path, ".git")))
			{
				yield return path;
			}
			else
			{
				foreach (string subPath in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
				{
					foreach (string validSubPath in ScanFolder(subPath))
						yield return validSubPath;
				}
			}
		}
	}
}
