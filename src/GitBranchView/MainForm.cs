using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GitBranchView
{
	public partial class MainForm : Form
	{
		private const string WINDOWS_RUN_REGISTRY_KEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

		private DateTime _formClosedAt;
		private bool _keepOpenOnce;
		private Point _lastShowPosition;

		public MainForm()
		{
			InitializeComponent();
			contextMenuStrip.Renderer = new ToolStripMenuRenderer();
			_formClosedAt = DateTime.MinValue;
			_keepOpenOnce = false;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			toolStripMenuItemVersion.Text = Assembly.GetExecutingAssembly().GetBuildVersion();
			toolStripMenuItemCloseOnLostFocus.Checked = Settings.Default.CloseOnLostFocus;
			toolStripMenuItemStartWithWindows.Checked = Settings.Default.StartWithWindows;

			HideForm();
			UpdateButtons();
			UpdateFolders();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.Save();
		}

		private void MainForm_Deactivate(object sender, EventArgs e)
		{
			if (Settings.Default.CloseOnLostFocus && !_keepOpenOnce && (int) Opacity == 1)
				HideForm();
		}

		private void NotifyIcon_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			if ((int) Opacity != 0 || _formClosedAt.AddMilliseconds(500) >= DateTime.Now)
				return;

			if (ModifierKeys == Keys.Control)
			{
				_keepOpenOnce = true;
				UpdateButtons();
			}

			ShowForm();
		}

		private void ToolStripMenuItemSelectRootFolder_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderDialog = new FolderBrowserDialog
				{
					Description = "Select a root folder...",
					SelectedPath = Settings.Default.RootPath
				};

			if (folderDialog.ShowDialog() != DialogResult.OK)
				return;

			Settings.Default.RootPath = folderDialog.SelectedPath;
			Settings.Default.Save();

			UpdateFolders();
		}

		private void ToolStripMenuItemSelectGitExePath_Click(object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog
				{
					Title = "Locate Git executable...",
					CheckFileExists = true,
					Filter = "Git Executable (git.exe)|git.exe",
					Multiselect = false,
					FileName = Settings.Default.GitPath
				};

			if (fileDialog.ShowDialog() != DialogResult.OK)
				return;

			Settings.Default.GitPath = fileDialog.FileName;
			Settings.Default.Save();

			UpdateFolders();
		}

		private void ToolStripMenuItemSelectLinkCommand_Click(object sender, EventArgs e)
		{
			using (CommandForm commandForm = new CommandForm(Settings.Default.CommandPath, Settings.Default.CommandArgs))
			{
				if (commandForm.ShowDialog(this) != DialogResult.OK)
					return;

				Settings.Default.CommandPath = commandForm.CommandPath;
				Settings.Default.CommandArgs = commandForm.CommandArgs;
				Settings.Default.Save();
			}
		}

		private void ToolStripMenuItemCloseOnLostFocus_Click(object sender, EventArgs e)
		{
			Settings.Default.CloseOnLostFocus = !Settings.Default.CloseOnLostFocus;
			Settings.Default.Save();
			UpdateButtons();
			toolStripMenuItemCloseOnLostFocus.Checked = Settings.Default.CloseOnLostFocus;
		}

		private void ToolStripMenuItemStartWithWindows_Click(object sender, EventArgs e)
		{
			Settings.Default.StartWithWindows = !Settings.Default.StartWithWindows;
			Settings.Default.Save();
			SetWindowsStartupTrigger(Settings.Default.StartWithWindows ? Application.ExecutablePath : null);
			toolStripMenuItemStartWithWindows.Checked = Settings.Default.StartWithWindows;
		}

		private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void ButtonRefresh_Click(object sender, EventArgs e)
		{
			UpdateFolders();
		}

		private void ButtonClose_Click(object sender, EventArgs e)
		{
			if (_keepOpenOnce)
			{
				_keepOpenOnce = false;
				UpdateButtons();
			}

			HideForm();
		}

		private void ShowForm()
		{
			if ((int) Opacity == 0)
			{
				_lastShowPosition = MousePosition;
				SetLocation();
				Opacity = 1;
			}
			Show();
			try { NativeMethods.SetForegroundWindow(Handle); } catch { ; }
		}

		private void HideForm()
		{
			Hide();
			Opacity = 0;
			_formClosedAt = DateTime.Now;
		}

		private void SetLocation()
		{
			Location = new Point(Math.Min(_lastShowPosition.X - Size.Width / 2, Screen.PrimaryScreen.WorkingArea.Width - Size.Width - 8),
				Math.Max(Screen.PrimaryScreen.WorkingArea.Top, Screen.PrimaryScreen.WorkingArea.Height - Size.Height - 8));
		}

		private void UpdateButtons()
		{
			if (Settings.Default.CloseOnLostFocus && !_keepOpenOnce)
			{
				buttonClose.Visible = false;
				buttonRefresh.Left = buttonClose.Left;
			}
			else
			{
				buttonClose.Visible = true;
				buttonRefresh.Left = buttonClose.Left - 23;
			}
		}

		private void UpdateFolders()
		{
			string rootPath = Settings.Default.RootPath;
			string gitPath = Settings.Default.GitPath;

			foreach (FolderEntry folderEntry in flowLayoutPanel.Controls.OfType<FolderEntry>())
				folderEntry.WidthChanged -= FolderEntry_WidthChanged;

			flowLayoutPanel.Controls.Clear();

			if (string.IsNullOrEmpty(rootPath))
			{
				labelRootPath.Text = "No root folder selected.";
				return;
			}

			if (!Directory.Exists(rootPath))
			{
				labelRootPath.Text = $"Root folder '{rootPath}' does not exist.";
				return;
			}

			if (string.IsNullOrEmpty(gitPath))
			{
				labelRootPath.Text = "Path to Git executable not selected.";
				return;
			}

			if (!File.Exists(gitPath))
			{
				labelRootPath.Text = $"Path to Git executable '{gitPath}' does not exist.";
				return;
			}

			labelRootPath.Text = rootPath;

			ConcurrentBag<(string Path, string Branch, int TrackedChanges, int UntrackedChanges)> gitRepositories = new ConcurrentBag<(string, string, int, int)>();

			Parallel.ForEach(ScanFolder(rootPath), folder =>
				{
					if (Git.TryGetBranch(folder, out string branch, out int trackedChanges, out int untrackedChanges))
						gitRepositories.Add((folder, branch, trackedChanges, untrackedChanges));
				});

			List<FolderEntry> folderEntries = gitRepositories
				.OrderBy(x => x.Path)
				.Select(x => new FolderEntry(rootPath, x.Path, x.Branch, x.TrackedChanges, x.UntrackedChanges))
				.ToList();

			folderEntries.ForEach(folderEntry => folderEntry.WidthChanged += FolderEntry_WidthChanged);

			UpdateSize(folderEntries);
			flowLayoutPanel.Controls.AddRange(folderEntries.ToArray<Control>());
			folderEntries.FirstOrDefault()?.Focus();
		}

		private void FolderEntry_WidthChanged(object sender, EventArgs e)
		{
			if (!(sender is FolderEntry changedFolderEntry))
				return;

			if (changedFolderEntry.Width != flowLayoutPanel.Width)
			{
				List<FolderEntry> folderEntries = flowLayoutPanel.Controls.OfType<FolderEntry>().ToList();
				folderEntries.ForEach(folderEntry => folderEntry.UpdateSize());
				UpdateSize(folderEntries);
				SetLocation();
			}
		}

		private void UpdateSize(List<FolderEntry> folderEntries)
		{
			UpdateSize(folderEntries.Select(x => x.Size).ToArray());
			folderEntries.ForEach(folderEntry => folderEntry.Width = flowLayoutPanel.Width);
		}

		private void UpdateSize(ICollection<Size> controlSizes)
		{
			const int MIN_WIDTH = 270, MIN_HEIGHT = 120;
			MinimumSize = MaximumSize = new Size(0, 0);

			if (controlSizes != null && controlSizes.Count > 0)
			{
				int totalControlsHeight = controlSizes.Sum(size => size.Height);
				int maxControlWidth = controlSizes.Max(size => size.Width);
				Size = new Size(Math.Max(MIN_WIDTH, Width - panelScroll.Width + maxControlWidth),
					Math.Min(Height - panelScroll.Height + totalControlsHeight, Screen.PrimaryScreen.WorkingArea.Height));
				flowLayoutPanel.Size = new Size(Math.Max(MIN_WIDTH, maxControlWidth), totalControlsHeight);
			}
			else
				Size = new Size(MIN_WIDTH, MIN_HEIGHT);

			MinimumSize = MaximumSize = Size;
			Location = new Point(Math.Min(Location.X, Screen.PrimaryScreen.WorkingArea.Width - Size.Width - 8),
				Math.Max(Screen.PrimaryScreen.WorkingArea.Top, Screen.PrimaryScreen.WorkingArea.Height - Size.Height - 8));
		}

		private static IEnumerable<string> ScanFolder(string path)
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

		public static void SetWindowsStartupTrigger(string executablePath)
		{
			const string appName = "Git Branch View";
			RegistryKey key = Registry.CurrentUser.OpenSubKey(WINDOWS_RUN_REGISTRY_KEY, true);

			if (key == null)
				return;

			if (string.IsNullOrEmpty(executablePath))
				key.DeleteValue(appName, false);
			else
				key.SetValue(appName, $"\"{executablePath}\"", RegistryValueKind.String);

			key.Close();
		}

		private class ToolStripMenuRenderer : ToolStripProfessionalRenderer
		{
			protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
			{
				if (e.Item.Enabled)
					base.OnRenderMenuItemBackground(e);
			}

			protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
			{
				if (e.Item.Enabled)
					base.OnRenderMenuItemBackground(e);
			}
		}
	}
}
