﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using ToolComponents.Core.Extensions;

namespace GitBranchView
{
	public partial class SettingsForm : Form
	{
		public class ChangedEventArgs : EventArgs
		{
			public ChangedEventArgs(bool rootFoldersChanged, bool rootFoldersHighlightChanged)
			{
				RootFoldersChanged = rootFoldersChanged;
				RootFoldersHighlightChanged = rootFoldersHighlightChanged;
			}

			public bool RootFoldersChanged { get; }
			public bool RootFoldersHighlightChanged { get; }
		}

		private const string WINDOWS_RUN_REGISTRY_KEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

		private readonly Label _widthHolder = new Label { Height = 1, Text = string.Empty };
		private readonly IDictionary<Settings.Root, List<FilterEntry>> _rootFilters;
		private bool _lastStartWithWindows;
		private bool _isTerminating;
		private bool _rootFoldersChanged;
		private bool _rootFoldersHighlightChanged;

		public event EventHandler<ChangedEventArgs> SettingsChanged; 

		public SettingsForm()
		{
			InitializeComponent();
			_rootFilters = new Dictionary<Settings.Root, List<FilterEntry>>();
		}

		public void ShowForm()
		{
			LoadSettings();

			if ((int) Opacity == 0)
				Opacity = 1;

			Show();
			try { NativeMethods.SetForegroundWindow(Handle); } catch { ; }
		}

		public void TerminateForm()
		{
			_isTerminating = true;
		}

		private void HideForm()
		{
			Hide();
			Opacity = 0;
		}

		private void RaiseSettingsChangedEvent(bool rootFoldersChanged, bool rootFoldersHighlightChanged)
		{
			Task.Run(() => SettingsChanged?.Invoke(this, new ChangedEventArgs(rootFoldersChanged, rootFoldersHighlightChanged)));
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			MinimumSize = Size;
		}

		private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_isTerminating)
				return;

			e.Cancel = true;
			Hide();
		}

		private void ButtonBrowseGitExePath_Click(object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog
				{
					Title = "Locate Git executable...",
					CheckFileExists = true,
					Filter = "Git Executable (git.exe)|git.exe",
					Multiselect = false,
					FileName = textBoxGitExePath.Text
				};

			if (fileDialog.ShowDialog() != DialogResult.OK)
				return;

			textBoxGitExePath.Text = fileDialog.FileName;
		}

		private void ButtonBrowseLinkCommandPath_Click(object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog
				{
					Title = "Select an executable...",
					CheckFileExists = true,
					Filter = "Executable Files (*.exe)|*.exe",
					Multiselect = false,
					InitialDirectory = Path.GetDirectoryName(textBoxLinkCommandPath.Text),
					FileName = textBoxLinkCommandPath.Text
				};

			if (fileDialog.ShowDialog() != DialogResult.OK)
				return;

			textBoxLinkCommandPath.Text = fileDialog.FileName;
		}

		private void ComboBoxRootPaths_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxRootPaths.SelectedItem == null)
				return;

			Settings.Root selectedRoot = (Settings.Root) comboBoxRootPaths.SelectedItem;
			Settings.Default.SelectedRootPath = selectedRoot.Path;

			ResetFlowLayoutPanel();
			flowLayoutPanelRootFolderFilters.Controls.AddRange(_rootFilters[selectedRoot].ToArray<Control>());
			UpdateFlowLayoutPanelSize();
		}

		private void ButtonRootPathAdd_Click(object sender, EventArgs e)
		{
			Settings.Root selectedRoot = (Settings.Root) comboBoxRootPaths.SelectedItem;

			FolderBrowserDialog folderDialog = new FolderBrowserDialog
				{
					Description = "Select a root folder...",
					SelectedPath = selectedRoot?.Path
				};

			if (folderDialog.ShowDialog() != DialogResult.OK)
				return;

			Settings.Root root = new Settings.Root { Path = folderDialog.SelectedPath };
			_rootFilters.Add(root, new List<FilterEntry>());
			comboBoxRootPaths.Items.Add(root);
			comboBoxRootPaths.SelectedItem = root;
			_rootFoldersChanged = true;
		}

		private void ButtonRootPathRemove_Click(object sender, EventArgs e)
		{
			if (comboBoxRootPaths.SelectedItem == null)
				return;

			Settings.Root selectedRoot = (Settings.Root) comboBoxRootPaths.SelectedItem;

			if (MessageBox.Show("Are you sure you want to remove the selected root folder?", "Git Branch View", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				ResetFlowLayoutPanel();
				comboBoxRootPaths.Items.Remove(selectedRoot);
				_rootFilters[selectedRoot].ForEach(DisposeFilterEntry);
				_rootFilters.Remove(selectedRoot);
				_rootFoldersChanged = true;
			}
		}

		private void FlowLayoutPanelRootFolderFilters_Resize(object sender, EventArgs e)
		{
			_widthHolder.Width = flowLayoutPanelRootFolderFilters.Width;
		}

		private void ButtonFilterAdd_Click(object sender, EventArgs e)
		{
			if (comboBoxRootPaths.SelectedItem == null)
				return;

			Settings.Root selectedRoot = (Settings.Root) comboBoxRootPaths.SelectedItem;

			using (FilterForm filterForm = new FilterForm())
			{
				if (filterForm.ShowDialog(this) != DialogResult.OK)
					return;

				FilterEntry filterEntry = CreateFilterEntry(filterForm.Filter);
				_rootFilters[selectedRoot].Add(filterEntry);
				flowLayoutPanelRootFolderFilters.Controls.Add(filterEntry);
				UpdateFlowLayoutPanelSize();
				_rootFoldersChanged = _rootFoldersChanged | filterEntry.Filter.Type != Settings.FilterType.Highlight;
				_rootFoldersHighlightChanged = _rootFoldersHighlightChanged | filterEntry.Filter.Type == Settings.FilterType.Highlight;
			}
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			SaveSettings();
			HideForm();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			HideForm();
		}

		private void ButtonApply_Click(object sender, EventArgs e)
		{
			SaveSettings();
		}

		private void FilterEntry_Action(object sender, FilterEntry.ActionEventArgs e)
		{
			if (!(sender is FilterEntry filterEntry))
				return;

			_rootFoldersChanged = _rootFoldersChanged | filterEntry.Filter.Type != Settings.FilterType.Highlight;
			_rootFoldersHighlightChanged = _rootFoldersHighlightChanged | filterEntry.Filter.Type == Settings.FilterType.Highlight;

			this.InvokeIfRequired(() =>
				{
					if (comboBoxRootPaths.SelectedItem == null)
						return;

					Settings.Root selectedRoot = (Settings.Root) comboBoxRootPaths.SelectedItem;

					switch (e.Action)
					{
						case FilterEntryAction.Delete:
							flowLayoutPanelRootFolderFilters.Controls.Remove(filterEntry);
							_rootFilters[selectedRoot].Remove(filterEntry);
							DisposeFilterEntry(filterEntry);
							UpdateFlowLayoutPanelSize();
							break;
						case FilterEntryAction.MoveUp:
							MoveFilterEntry(selectedRoot, filterEntry, -1);
							break;
						case FilterEntryAction.MoveDown:
							MoveFilterEntry(selectedRoot, filterEntry, 1);
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
				});
		}

		private void FilterEntry_Changed(object sender, FilterEntry.ChangedEventArgs e)
		{
			_rootFoldersChanged = _rootFoldersChanged | e.FromType != Settings.FilterType.Highlight | e.ToType != Settings.FilterType.Highlight;
			_rootFoldersHighlightChanged = _rootFoldersHighlightChanged | e.FromType == Settings.FilterType.Highlight | e.ToType == Settings.FilterType.Highlight;
		}

		private void UpdateFlowLayoutPanelSize()
		{
			flowLayoutPanelRootFolderFilters.Height = flowLayoutPanelRootFolderFilters.Controls.Cast<Control>().Sum(x => x.Height + x.Margin.Vertical);
		}

		private void ResetFlowLayoutPanel()
		{
			flowLayoutPanelRootFolderFilters.Controls.Clear();
			flowLayoutPanelRootFolderFilters.Controls.Add(_widthHolder);
			_widthHolder.Width = flowLayoutPanelRootFolderFilters.Width;
		}

		private FilterEntry CreateFilterEntry(Settings.RootPathFilter filter)
		{
			FilterEntry filterEntry = new FilterEntry(filter);
			filterEntry.Action += FilterEntry_Action;
			filterEntry.Changed += FilterEntry_Changed;
			return filterEntry;
		}

		private void DisposeFilterEntry(FilterEntry filterEntry)
		{
			filterEntry.Action -= FilterEntry_Action;
			filterEntry.Changed -= FilterEntry_Changed;
			filterEntry.Dispose();
		}

		private void MoveFilterEntry(Settings.Root root, FilterEntry filterEntry, int adjustment)
		{
			if (adjustment == 0)
				return;

			int index = _rootFilters[root].IndexOf(filterEntry);

			if (index < 0)
				return;

			int newIndex = index + adjustment;
			newIndex = newIndex < 0 ? 0 : newIndex;
			newIndex = newIndex >= _rootFilters[root].Count ? _rootFilters[root].Count - 1 : newIndex;
			flowLayoutPanelRootFolderFilters.Controls.SetChildIndex(filterEntry, newIndex + 1);
			_rootFilters[root].RemoveAt(index);
			_rootFilters[root].Insert(newIndex, filterEntry);
		}

		private void LoadSettings()
		{
			textBoxLinkCommandPath.Text = Settings.Default.CommandPath;
			textBoxLinkCommandArgs.Text = Settings.Default.CommandArgs;
			checkBoxCloseOnLostFocus.Checked = Settings.Default.CloseOnLostFocus;
			checkBoxStartWithWindows.Checked = _lastStartWithWindows = Settings.Default.StartWithWindows;
			checkBoxEnableLogging.Checked = Settings.Default.EnableLogging;
			textBoxGitExePath.Text = Settings.Default.GitPath;
			checkBoxGitEnableRemoteBranchLookup.Checked = Settings.Default.EnableRemoteBranchLookup;

			foreach (Settings.Root root in _rootFilters.Keys)
				_rootFilters[root].ForEach(DisposeFilterEntry);

			_rootFilters.Clear();

			foreach (Settings.Root root in Settings.Default.Roots.Select(x => x.Clone()))
				_rootFilters.Add(root, root.Filters.Select(CreateFilterEntry).ToList());

			comboBoxRootPaths.Items.Clear();
			comboBoxRootPaths.Items.AddRange(_rootFilters.Keys.ToArray<object>());

			if (comboBoxRootPaths.Items.Count > 0)
			{
				comboBoxRootPaths.SelectedItem = comboBoxRootPaths.Items
					.Cast<Settings.Root>()
					.FirstOrDefault(root => string.Equals(root.Path, Settings.Default.SelectedRootPath, StringComparison.CurrentCultureIgnoreCase)) ?? comboBoxRootPaths.Items[0];
			}
		}

		private void SaveSettings()
		{
			Settings.Default.CommandPath = textBoxLinkCommandPath.Text;
			Settings.Default.CommandArgs = textBoxLinkCommandArgs.Text;
			Settings.Default.CloseOnLostFocus = checkBoxCloseOnLostFocus.Checked;
			Settings.Default.StartWithWindows = checkBoxStartWithWindows.Checked;
			Settings.Default.EnableLogging = checkBoxEnableLogging.Checked;
			Settings.Default.GitPath = textBoxGitExePath.Text;
			Settings.Default.EnableRemoteBranchLookup = checkBoxGitEnableRemoteBranchLookup.Checked;

			Settings.Default.Roots = _rootFilters.Keys
				.Select(root =>
					{
						List<FilterEntry> filterEntries = _rootFilters[root];
						root = Settings.Default.Roots.FirstOrDefault(x => x.Id == root.Id) ?? root;
						root.Filters = filterEntries.Select(entry => entry.Filter).ToList();
						return root;
					})
				.ToList();

			Settings.Default.Save();

			if (_lastStartWithWindows != Settings.Default.StartWithWindows)
			{
				SetWindowsStartupTrigger(Settings.Default.StartWithWindows ? Application.ExecutablePath : null);
				_lastStartWithWindows = Settings.Default.StartWithWindows;
			}

			RaiseSettingsChangedEvent(_rootFoldersChanged, _rootFoldersHighlightChanged);
			_rootFoldersChanged = _rootFoldersHighlightChanged = false;
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
	}
}