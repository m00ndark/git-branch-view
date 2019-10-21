using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitBranchView.Model;

namespace GitBranchView
{
	public static class Extensions
	{
		public const string ROOT_PATH_RELATIVE = "/";

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

		public static bool IsValid(this Root root, out string error)
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

		public static string RelativeTo(this string path, Root root) => path.RelativeTo(root.Path);

		public static string RelativeTo(this string path, string rootPath)
		{
			return rootPath.Length >= path.Length
				? ROOT_PATH_RELATIVE
				: path.Substring(rootPath.Length + 1);
		}

		public static bool ShouldInclude(this Root root, string path, string branch)
		{
			return root.Filters
				.Where(filter => filter.Type == FilterType.Include || filter.Type == FilterType.Exclude)
				.Aggregate(true, (include, filter) => filter.Target.HasFlag(FilterTargets.Path) && Regex.IsMatch(path.RelativeTo(root), filter.Filter)
					|| filter.Target.HasFlag(FilterTargets.Branch) && Regex.IsMatch(branch, filter.Filter) ? filter.Type == FilterType.Include : include);
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

		public static bool IsValidRegex(this string pattern, bool allowEmpty = false)
		{
			if (string.IsNullOrWhiteSpace(pattern))
				return false;

			try
			{
				// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
				Regex.IsMatch(string.Empty, pattern);
			}
			catch (ArgumentException)
			{
				return false;
			}

			return true;
		}

		public static IEnumerable<T> GetFlagValues<T>(this T target) where T : Enum
		{
			return Enum.GetValues(typeof(T))
				.Cast<T>()
				.Where(x => Convert.ToInt32(x) > 0)
				.Where(x => target.HasFlag(x));
		}

		public static T AddItem<T>(this ContextMenuStrip contextMenuStrip, string text = null, EventHandler clickEventHandler = null, object tag = null, bool enabled = true)
			where T : ToolStripItem, new() => contextMenuStrip.Items.Add<T>(text, clickEventHandler, tag, enabled);

		public static T AddItem<T>(this ToolStripMenuItem toolStripMenuItem, string text = null, EventHandler clickEventHandler = null, object tag = null, bool enabled = true)
			where T : ToolStripItem, new() => toolStripMenuItem.DropDownItems.Add<T>(text, clickEventHandler, tag, enabled);

		public static T Add<T>(this ToolStripItemCollection items, string text = null, EventHandler clickEventHandler = null, object tag = null, bool enabled = true)
			where T : ToolStripItem, new()
		{
			T menuItem = new T { Text = text, Tag = new ToolStripItemTag(clickEventHandler, tag), Enabled = enabled };
			if (clickEventHandler != null) menuItem.Click += clickEventHandler;
			items.Add(menuItem);
			return menuItem;
		}

		public static T GetWhere<T>(this ToolStripItemCollection items, Func<T, bool> predicate) where T : ToolStripItem
		{
			foreach (ToolStripItem item in items)
			{
				if (item is ToolStripMenuItem menuItem)
				{
					T foundItem = menuItem.DropDownItems.GetWhere(predicate);

					if (foundItem != null)
						return foundItem;
				}

				if (item is T targetItem && predicate(targetItem))
				{
					return targetItem;
				}
			}

			return null;
		}

		public static void SetWhere<T>(this ToolStripItemCollection items, Func<T, bool> predicate, Action<T> setter) where T : ToolStripItem
		{
			foreach (ToolStripItem item in items)
			{
				if (item is ToolStripMenuItem menuItem)
				{
					menuItem.DropDownItems.SetWhere(predicate, setter);
				}

				if (item is T targetItem && predicate(targetItem))
				{
					setter(targetItem);
				}
			}
		}

		public static void DisposeItems(this ToolStripItemCollection items)
		{
			foreach (IDisposable disposable in items.OfType<IDisposable>().ToArray())
			{
				if (disposable is ToolStripMenuItem menuItem)
				{
					menuItem.DropDownItems.DisposeItems();
				}

				if (disposable is ToolStripItem item && item.Tag is ToolStripItemTag tag && tag.ClickEventHandler != null)
				{
					item.Click -= tag.ClickEventHandler;
				}

				disposable.Dispose();
			}
		}

		public static T GetItem<T>(this object obj) where T : ToolStripItem
		{
			return obj as T;
		}

		public static ToolStripItemTag GetTag(this object obj)
		{
			return (obj as ToolStripItem).GetTag();
		}

		public static ToolStripItemTag GetTag(this ToolStripItem item)
		{
			return item?.Tag as ToolStripItemTag;
		}

		public static T GetTagValue<T>(this object obj)
		{
			return (obj as ToolStripItem).GetTagValue<T>();
		}

		public static T GetTagValue<T>(this ToolStripItem item)
		{
			return item.GetTag()?.Value is T value ? value : default;
		}

		public static Task<(bool Success, string[] Errors)> SafeGitExecuteAsync(
			this Root root,
			string path,
			string command,
			Action<string> outputLineHandler = null)
		{
			try
			{
				return Git.ExecuteCommandAsync(root, path, command, outputLineHandler);
			}
			catch (Exception ex)
			{
				Program.ShowError($"Failed to execute Git command '{command}';"
					+ Environment.NewLine + ex.Message + Environment.NewLine
					+ Environment.NewLine + $"Path: {path}");

				return Task.FromResult((false, null as string[]));
			}
		}

		public static bool IsEmpty(this IEnumerable<string> items)
		{
			return items?.All(string.IsNullOrWhiteSpace) ?? true;
		}

		public static string[] NullIfEmpty(this IReadOnlyList<string> items)
		{
			return items.IsEmpty() ? null : items.ToArray();
		}

		public static void AddIfNotEmpty(this List<string> list, IReadOnlyList<string> items, params string[] additionalItems)
		{
			if (items?.NullIfEmpty() == null)
				return;

			list.AddRange(items);
			list.AddRange(additionalItems);
		}

		public static string[] PrependIfNotEmpty(this IReadOnlyList<string> items, params string[] additionalItems)
		{
			return items.IsEmpty() ? null : additionalItems.Reverse().Aggregate(items.AsEnumerable(), Enumerable.Prepend).ToArray();
		}

		public static string[] WithoutEmpty(this IEnumerable<string> items)
		{
			return items.Where(item => !string.IsNullOrWhiteSpace(item)).ToArray();
		}
	}
}
