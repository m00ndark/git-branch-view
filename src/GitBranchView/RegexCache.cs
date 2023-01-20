using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace GitBranchView
{
	public static class RegexCache
	{
		private static readonly ConcurrentDictionary<string, Regex> _cache = new ConcurrentDictionary<string, Regex>();

		public static Regex GetCachedRegex(this string pattern, RegexOptions regexOptions = RegexOptions.None)
		{
			string key = $"{pattern}|{regexOptions}".Hash();
			return _cache.GetOrAdd(key, _ => new Regex(pattern, regexOptions | RegexOptions.Compiled));
		}

		private static string Hash(this string value)
		{
			using (SHA1 sha1 = SHA1.Create())
			{
				byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(value));
				return new Guid(hash.Take(16).ToArray()).ToString("N").ToUpperInvariant();
			}
		}
	}
}