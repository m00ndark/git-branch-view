using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitBranchView.Model;
using ToolComponents.Core.Logging;

namespace GitBranchView
{
	public static class Git
	{
		private const int NO_EXIT_CODE = -1;

		public static bool TryGetBranch(Root root, string path, out string branch, out int trackedChanges, out int untrackedChanges, out string[] errors)
		{
			List<string> errorLines = new List<string>();

			void AddErrors(string cmdLine, string[] cmdErrors)
			{
				cmdErrors = cmdErrors.PrependIfNotEmpty(cmdLine, string.Empty);
				errorLines.AddIfNotEmpty(cmdErrors, string.Empty);
			}

			// try get branch
			(bool Success, string CmdLine, string[] Output, string[] Error) result
				= ExecProcess(root, Settings.Default.GitPath, "symbolic-ref --short HEAD", path);

			if (!result.Success)
			{
				AddErrors(result.CmdLine, result.Error);

				// try get tag
				result = ExecProcess(root, Settings.Default.GitPath, "describe --tags --exact-match", path);
			}

			if (!result.Success)
			{
				AddErrors(result.CmdLine, result.Error);

				// try get hash
				result = ExecProcess(root, Settings.Default.GitPath, "rev-parse --short HEAD", path);
			}

			if (!result.Success)
			{
				AddErrors(result.CmdLine, result.Error);
			}

			bool success = result.Success;
			branch = success && result.Output.Any() ? result.Output.First().Trim() : "<unknown>";

			// try get changes
			result = ExecProcess(root, Settings.Default.GitPath, "status --short", path);
			string[] changes = result.Success ? result.Output.WithoutEmpty() : null;
			trackedChanges = changes?.Count(x => !x.StartsWith("??")) ?? -1;
			untrackedChanges = changes?.Count(x => x.StartsWith("??")) ?? -1;

			if (!result.Success)
			{
				AddErrors(result.CmdLine, result.Error);
			}

			errors = errorLines.NullIfEmpty();
			return success;
		}

		public static bool BranchExistRemote(Root root, string path, string branch, out string[] errors)
		{
			(bool success, string cmdLine, string[] output, string[] error)
				= ExecProcess(root, Settings.Default.GitPath, $"ls-remote --heads --tags origin {branch}", path);

			errors = success ? null : error.PrependIfNotEmpty(cmdLine, string.Empty);
			return success && output.Any(line => line.Trim().EndsWith(branch));
		}

		public static Task<(bool Success, string[] Errors)> ExecuteCommandAsync(
			Root root,
			string path,
			string command,
			Action<string> outputLineHandler = null)
		{
			return Task.Run(() =>
				{
					(bool success, string cmdLine, string[] _, string[] error)
						= ExecProcess(root, Settings.Default.GitPath, command, path, outputLineHandler);

					return (success, error.PrependIfNotEmpty(cmdLine, string.Empty));
				});
		}

		private static (bool Success, string CommandLine, string[] Output, string[] Error) ExecProcess(
			Root root,
			string filePath,
			string args,
			string workingDir,
			Action<string> outputLineHandler = null)
		{
			Guid execId = Guid.NewGuid();
			Stopwatch stopwatch = Stopwatch.StartNew();

			int exitCode = int.MinValue;
			List<string> output = new List<string>();
			List<string> error = new List<string>();
			string[] environment = null;

			void OnOutput(string data)
			{
				if (data == null) return;
				output.Add(data);
				outputLineHandler?.Invoke(data);
			}

			void OnError(string data)
			{
				if (data == null) return;
				error.Add(data);
				outputLineHandler?.Invoke(data);
			}

			try
			{
				string commandLine = $"{workingDir}> {Path.GetFileNameWithoutExtension(filePath)} {args}";

				if (Settings.Default.EnableLogging)
					Logger.Add($"Executing command {execId}: {commandLine}");

				outputLineHandler?.Invoke(commandLine + Environment.NewLine);

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
					process.OutputDataReceived += (sender, e) => OnOutput(e.Data);
					process.ErrorDataReceived += (sender, e) => OnError(e.Data);

					process.Start();
					process.BeginOutputReadLine();
					process.BeginErrorReadLine();
					process.WaitForExit();

					exitCode = process.ExitCode;
					environment = process.StartInfo.EnvironmentVariables
						.Cast<DictionaryEntry>()
						.Select(x => $"{x.Key}={x.Value}")
						.OrderBy(x => x)
						.ToArray();

					bool success = exitCode == 0;
					return (success, commandLine, output.ToArray(), error.ToArray());
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
		private static void Dump(Guid execId,
			Root root,
			string filePath,
			string args,
			string workingDir,
			int exitCode,
			IEnumerable<string> output,
			IEnumerable<string> error,
			string[] environment)
		{
			string commandName = Path.GetFileNameWithoutExtension(filePath);

			string dumpPath = Path.Combine(Path.GetDirectoryName(Logger.LogFolder), "CommandDump", root.Id.ToString(), workingDir.RelativeTo(root));
			string dumpFilePath = Path.Combine(dumpPath, $"{commandName}_{DateTime.Now:yyyyMMddHHmmssfff}.txt");

			Directory.CreateDirectory(dumpPath);

			Logger.Add($"Writing command output for {execId} to {dumpFilePath}");

			string[] outputLines = output.WithoutEmpty();
			string[] errorLines = error.WithoutEmpty();

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
