using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using ToolComponents.Core.Logging;

namespace GitBranchView
{
	public static class Git
	{
		private const int NO_EXIT_CODE = -1;

		public static bool TryGetBranch(Settings.Root root, string path, out string branch, out int trackedChanges, out int untrackedChanges, out string[] errors)
		{
			List<string> errorLines = new List<string>();

			// try get branch
			(bool Success, string Output, string Error) result = ExecProcess(root, Settings.Default.GitPath, "symbolic-ref --short HEAD", path);

			if (!result.Success)
			{
				// try get tag
				result = ExecProcess(root, Settings.Default.GitPath, "describe --tags --exact-match", path);
			}

			if (!result.Success)
			{
				// try get hash
				result = ExecProcess(root, Settings.Default.GitPath, "rev-parse --short HEAD", path);
			}

			bool success = result.Success;
			branch = success ? result.Output.Trim() : "<unknown>";
			errorLines.AddRange(result.Error.LineSplit());

			result = ExecProcess(root, Settings.Default.GitPath, "status --short", path);
			string[] changes = result.Success ? result.Output.LineSplit(StringSplitOptions.RemoveEmptyEntries) : null;
			trackedChanges = changes?.Count(x => !x.StartsWith("??")) ?? -1;
			untrackedChanges = changes?.Count(x => x.StartsWith("??")) ?? -1;
			errorLines.AddRange(Enumerable.Repeat(string.Empty, 2).Concat(result.Error.LineSplit()));

			errors = errorLines.All(string.IsNullOrWhiteSpace) ? null : errorLines.ToArray();
			return success;
		}

		public static bool BranchExistRemote(Settings.Root root, string path, string branch, out string[] errors)
		{
			(bool Success, string Output, string Error) result = ExecProcess(root, Settings.Default.GitPath, $"ls-remote --heads --tags origin {branch}", path);

			errors = result.Error?.LineSplit();
			return result.Success && result.Output.LineSplit().Any(line => line.Trim().EndsWith(branch));
		}

		public static bool Checkout(Settings.Root root, string path, string branch)
		{
			return ExecProcess(root, Settings.Default.GitPath, $"checkout {branch}", path).Success;
		}

		public static bool Pull(Settings.Root root, string path)
		{
			return ExecProcess(root, Settings.Default.GitPath, "pull", path).Success;
		}

		public static bool ResetHard(Settings.Root root, string path)
		{
			return ExecProcess(root, Settings.Default.GitPath, "reset --hard", path).Success;
		}

		public static bool CleanAll(Settings.Root root, string path)
		{
			return ExecProcess(root, Settings.Default.GitPath, "clean -fdx", path).Success;
		}

		private static (bool Success, string Output, string Error) ExecProcess(Settings.Root root, string filePath, string args, string workingDir)
		{
			Guid execId = Guid.NewGuid();
			Stopwatch stopwatch = Stopwatch.StartNew();

			int exitCode = int.MinValue;
			string output = null, error = null;
			string[] environment = null;

			try
			{
				string command = $"{Path.GetFileNameWithoutExtension(filePath)} {args}";

				if (Settings.Default.EnableLogging)
					Logger.Add($"Executing command {execId}: {workingDir} > {command}");

				using (Process process = new Process
					{
						StartInfo =
							{
								FileName = filePath,
								Arguments = args,
								WorkingDirectory = workingDir,
								CreateNoWindow = true,
								UseShellExecute = false,
								RedirectStandardOutput = true,
								RedirectStandardError = true
							}
				})
				{
					process.Start();
					process.WaitForExit();

					exitCode = process.ExitCode;
					output = process.StandardOutput.ReadToEnd();
					error = process.StandardError.ReadToEnd();
					environment = process.StartInfo.EnvironmentVariables
						.Cast<DictionaryEntry>()
						.Select(x => $"{x.Key}={x.Value}")
						.OrderBy(x => x)
						.ToArray();

					bool success = exitCode == 0;
					return (success, success ? output : null, !success ? $"{command}\n\n{error}" : null);
				}
			}
			finally
			{
				if (Settings.Default.EnableLogging)
				{
					Logger.Add($"Command {execId} terminated, duration: {stopwatch.Elapsed}");
					Dump(execId, root, filePath, args, workingDir, exitCode, output, error, environment);
				}
			}
		}

		[SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
		private static void Dump(Guid execId, Settings.Root root, string filePath, string args, string workingDir, int exitCode, string output, string error, string[] environment)
		{
			string commandName = Path.GetFileNameWithoutExtension(filePath);

			string dumpPath = Path.Combine(Path.GetDirectoryName(Logger.LogFolder), "CommandDump", root.Id.ToString(), workingDir.RelativeTo(root));
			string dumpFilePath = Path.Combine(dumpPath, $"{commandName}_{DateTime.Now:yyyyMMddHHmmssfff}.txt");

			Directory.CreateDirectory(dumpPath);

			Logger.Add($"Writing command output for {execId} to {dumpFilePath}");

			string[] outputLines = output.LineSplit().Where(x => !string.IsNullOrEmpty(x)).ToArray();
			string[] errorLines = error.LineSplit().Where(x => !string.IsNullOrEmpty(x)).ToArray();

			File.WriteAllLines(dumpFilePath, FormatDump(new[]
				{
					new[] { "COMMAND", $"{filePath} {args}" },
					new[] { "WORKING DIRECTORY", workingDir },
					new[] { "ENVIRONMENT VARIABLES" }.Concat(environment.Length > 0 ? environment : new[] { "<none>" }),
					new[] { "EXIT CODE", exitCode != NO_EXIT_CODE ? exitCode.ToString() : "<none>" },
					new[] { "OUTPUT" }.Concat(outputLines.Length > 0 ? outputLines : new[] { "<empty>" }),
					new[] { "ERROR" }.Concat(errorLines.Length > 0 ? errorLines : new[] { "<empty>" })
				}), Encoding.UTF8);
		}

		private static IEnumerable<string> FormatDump(IReadOnlyList<IEnumerable<string>> dumpItems)
		{
			for (int i = 0; i < dumpItems.Count; i++)
			{
				if (i > 0)
					yield return string.Empty;

				foreach (string item in dumpItems[i].Select((item, j) => j > 0 ? $"  {item}" : item))
					yield return item;
			}
		}

		private static string[] LineSplit(this string str, StringSplitOptions options = StringSplitOptions.None)
		{
			return str?.Split(new[] { '\n' }, options) ?? new string[0];
		}
	}
}
