using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GitBranchView.Generated
{
	public static class AssemblyResolver
	{
		public enum CachedAssemblyType
		{
			Embedded,
			Retargetable
		}

		private class CachedAssembly
		{
			public CachedAssembly(Assembly assembly, CachedAssemblyType type)
			{
				Assembly = assembly;
				Type = type;
			}

			public Assembly Assembly { get; }
			public CachedAssemblyType Type { get; }
		}

		private const string DEFAULT_ASSEMBLY_LOCATION = "External";

		private static readonly ConcurrentDictionary<string, CachedAssembly> _cachedAssemblies = new ConcurrentDictionary<string, CachedAssembly>();
		private static readonly ConcurrentDictionary<string, string> _assemblyShortNames = new ConcurrentDictionary<string, string>();

		public static IEnumerable<Assembly> CachedAssemblies => _cachedAssemblies.Values.Select(x => x.Assembly).ToArray();

		public static void Setup(bool suppressConsoleWriting = false, Assembly resourceAssembly = null, string assemblyLocation = DEFAULT_ASSEMBLY_LOCATION)
		{
			resourceAssembly = resourceAssembly ?? Assembly.GetExecutingAssembly();

			CacheEmbeddedAssemblies(resourceAssembly, assemblyLocation, suppressConsoleWriting);

			AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
				{
					// e.Name: "Newtonsoft.Json, Version=4.5.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed"

					bool resolvedDifferentVersion = false;
					CachedAssembly cachedAssembly;

					if (!_cachedAssemblies.TryGetValue(e.Name, out cachedAssembly))
					{
						// Any retargetable assembly should be resolved directly using normal load e.g. System.Core issue: 
						// http://stackoverflow.com/questions/18793959/filenotfoundexception-when-trying-to-load-autofac-as-an-embedded-assembly
						if (e.Name.EndsWith("Retargetable=Yes"))
						{
							Assembly assembly = Assembly.Load(new AssemblyName(e.Name));
							cachedAssembly = _cachedAssemblies.GetOrAdd(e.Name, new CachedAssembly(assembly, CachedAssemblyType.Retargetable));
						}
						else
						{
							string cachedAssemblyFullName;
							string assemblyShortName = new string(e.Name.TakeWhile(x => x != ',').ToArray());
							resolvedDifferentVersion = _assemblyShortNames.TryGetValue(assemblyShortName, out cachedAssemblyFullName)
								&& _cachedAssemblies.TryGetValue(cachedAssemblyFullName, out cachedAssembly);
						}
					}

					if (!suppressConsoleWriting && cachedAssembly?.Assembly != null)
					{
						Console.WriteLine(resolvedDifferentVersion
							? $"WARNING: Resolved {cachedAssembly.Type.ToString().ToLower()} assembly of different version [{cachedAssembly.Assembly.FullName}], queried assembly [{e.Name}]"
							: $"Resolved {cachedAssembly.Type.ToString().ToLower()} assembly [{cachedAssembly.Assembly.FullName}]");
					}

					return cachedAssembly?.Assembly;
				};
		}

		private static void CacheEmbeddedAssemblies(Assembly resourceAssembly, string assemblyLocation, bool suppressConsoleWriting)
		{
			string resourceAssemblyName = resourceAssembly.GetName().Name;

			foreach (string resourceName in resourceAssembly.GetManifestResourceNames()
				.Where(name => name.StartsWith($"{resourceAssemblyName}.{assemblyLocation}.") && name.EndsWith(".dll")))
			{
				try
				{
					using (Stream resourceStream = resourceAssembly.GetManifestResourceStream(resourceName))
					{
						if (resourceStream == null)
							continue;

						int capacity = resourceStream.CanSeek ? (int) resourceStream.Length : 0;
						using (MemoryStream assemblyStream = new MemoryStream(capacity))
						{
							resourceStream.CopyTo(assemblyStream);
							Assembly assembly = Assembly.Load(assemblyStream.ToArray());

							if (assembly == null)
								continue;

							if (!suppressConsoleWriting)
								Console.WriteLine($"Cached embedded assembly [{assembly.FullName}]");

							if (_cachedAssemblies.TryAdd(assembly.FullName, new CachedAssembly(assembly, CachedAssemblyType.Embedded)))
								_assemblyShortNames.TryAdd(assembly.GetName().Name, assembly.FullName);

							CacheEmbeddedAssemblies(assembly, assemblyLocation, suppressConsoleWriting);
						}
					}
				}
				catch (Exception ex)
				{
					Debug.Assert(false, ex.ToString());
					continue;
				}
			}
		}
	}
}
