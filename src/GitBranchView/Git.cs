using System;
using System.Diagnostics;
using System.Linq;

namespace GitBranchView
{
	public static class Git
	{
		public static bool TryGetBranch(string path, out string branch, out int trackedChanges, out int untrackedChanges)
		{
			Debug.WriteLine($"Querying for branch and status: {path}");

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
			string[] changes = result.Success ? result.Output.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries) : null;
			trackedChanges = changes?.Count(x => !x.StartsWith("??")) ?? -1;
			untrackedChanges = changes?.Count(x => x.StartsWith("??")) ?? -1;

			return success;
		}

		public static bool Checkout(string path, string branch)
		{
			Debug.WriteLine($"Checking out {branch}: {path}");

			return ExecProcess(Settings.Default.GitPath, $"checkout {branch}", path).Success;
		}

		private static (bool Success, string Output) ExecProcess(string fileName, string args, string workingDir)
		{
			using (Process process = new Process
				{
					StartInfo =
						{
							FileName = fileName,
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
	}
}
