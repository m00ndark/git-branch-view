using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GitBranchView
{
	public static class Git
	{
		public static bool TryGetBranch(string path, out string branch, out int trackedChanges, out int untrackedChanges)
		{
			// try get branch
			(bool Success, string Output) result = ExecProcess(Settings.Default.GitPath, "symbolic-ref --short HEAD", path);

			if (!result.Success)
			{
				// try get tag
				result = ExecProcess(Settings.Default.GitPath, "describe --tags --exact-match", path);
			}

			if (!result.Success)
			{
				// try get hash
				result = ExecProcess(Settings.Default.GitPath, "rev-parse --short HEAD", path);
			}

			bool success = result.Success;
			branch = success ? result.Output.Trim() : null;

			result = ExecProcess(Settings.Default.GitPath, "status --short", path);
			string[] changes = result.Success ? result.Output.LineSplit() : null;
			trackedChanges = changes?.Count(x => !x.StartsWith("??")) ?? -1;
			untrackedChanges = changes?.Count(x => x.StartsWith("??")) ?? -1;

			return success;
		}

		public static bool BranchExistRemote(string path, string branch)
		{
			(bool Success, string Output) result = ExecProcess(Settings.Default.GitPath, $"ls-remote --heads --tags origin {branch}", path);

			return result.Success && result.Output.LineSplit().Any(line => line.Trim().EndsWith(branch));
		}

		public static bool Checkout(string path, string branch)
		{
			return ExecProcess(Settings.Default.GitPath, $"checkout {branch}", path).Success;
		}

		public static bool Pull(string path)
		{
			return ExecProcess(Settings.Default.GitPath, "pull", path).Success;
		}

		public static bool ResetHard(string path)
		{
			return ExecProcess(Settings.Default.GitPath, "reset --hard", path).Success;
		}

		public static bool CleanAll(string path)
		{
			return ExecProcess(Settings.Default.GitPath, "clean -fdx", path).Success;
		}

		private static (bool Success, string Output) ExecProcess(string filePath, string args, string workingDir)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();

			try
			{
				Debug.WriteLine($"{workingDir} > {Path.GetFileNameWithoutExtension(filePath)} {args}");

				using (Process process = new Process
					{
						StartInfo =
							{
								FileName = filePath,
								Arguments = args,
								WorkingDirectory = workingDir,
								CreateNoWindow = true,
								RedirectStandardOutput = true,
								UseShellExecute = false
							}
					})
				{
					process.Start();
					process.WaitForExit();
					bool success = process.ExitCode == 0;
					return (success, success ? process.StandardOutput.ReadToEnd() : null);
				}
			}
			finally
			{
				Debug.WriteLine($"{workingDir} > {Path.GetFileNameWithoutExtension(filePath)} {args} >> {stopwatch.Elapsed}");
			}
		}

		private static string[] LineSplit(this string str)
		{
			return str.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
