using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitBranchView.Controls;
using GitBranchView.Model;
using Microsoft.Win32;
using ToolComponents.Core.Extensions;

namespace GitBranchView.Forms
{
	public enum EntryAction
	{
		Delete,
		MoveUp,
		MoveDown
	}

	public partial class SettingsForm : Form
	{
		public class ChangedEventArgs : EventArgs
		{
			public ChangedEventArgs(bool quickLaunchFilesChanged, bool repositoryLinkBehaviorChanged, bool gitCommandsChanged, bool rootFoldersChanged, bool rootFoldersHighlightChanged)
			{
				QuickLaunchFilesChanged = quickLaunchFilesChanged;
				RepositoryLinkBehaviorChanged = repositoryLinkBehaviorChanged;
				GitCommandsChanged = gitCommandsChanged;
				RootFoldersChanged = rootFoldersChanged;
				RootFoldersHighlightChanged = rootFoldersHighlightChanged;
			}

			public bool QuickLaunchFilesChanged { get; }
			public bool RepositoryLinkBehaviorChanged { get; }
			public bool GitCommandsChanged { get; }
			public bool RootFoldersChanged { get; }
			public bool RootFoldersHighlightChanged { get; }
		}

		private const string WINDOWS_RUN_REGISTRY_KEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

		private readonly Label _widthHolder1 = new Label { Height = 1, Text = string.Empty, Margin = Padding.Empty };
		private readonly Label _widthHolder2 = new Label { Height = 1, Text = string.Empty, Margin = Padding.Empty };
		private readonly List<GitCommandEntry> _gitCommands;
		private readonly IDictionary<Root, List<FilterEntry>> _rootFilters;
		private bool _lastStartWithWindows;
		private bool _isTerminating;
		private bool _quickLaunchFilesChanged;
		private bool _repositoryLinkBehaviorChanged;
		private bool _gitCommandsChanged;
		private bool _rootFoldersChanged;
		private bool _rootFoldersHighlightChanged;

		public event EventHandler<ChangedEventArgs> SettingsChanged; 

		public SettingsForm()
		{
			InitializeComponent();
			_gitCommands = new List<GitCommandEntry>();
			_rootFilters = new Dictionary<Root, List<FilterEntry>>();
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

		private void RaiseSettingsChangedEvent(bool quickLaunchFilesChanged, bool repositoryLinkBehaviorChanged, bool gitCommandsChanged, bool rootFoldersChanged, bool rootFoldersHighlightChanged)
		{
			Task.Run(() => SettingsChanged?.Invoke(this, new ChangedEventArgs(quickLaunchFilesChanged, repositoryLinkBehaviorChanged, gitCommandsChanged, rootFoldersChanged, rootFoldersHighlightChanged)));
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
			HideForm();
		}

		private void ButtonCustomCommandPathBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog
				{
					Title = "Select an executable...",
					CheckFileExists = true,
					Filter = "Executable Files (*.exe)|*.exe",
					Multiselect = false,
					InitialDirectory = Path.GetDirectoryName(textBoxCustomCommandPath.Text),
					FileName = textBoxCustomCommandPath.Text
				};

			if (fileDialog.ShowDialog() != DialogResult.OK)
				return;

			textBoxCustomCommandPath.Text = fileDialog.FileName;
		}

		private void TextBoxQuickLaunchFilesFilter_TextChanged(object sender, EventArgs e)
		{
			_quickLaunchFilesChanged = true;
		}

		private void RadioButtonQuickLaunchFilesDoNotGroup_CheckedChanged(object sender, EventArgs e)
		{
			_quickLaunchFilesChanged = true;
		}

		private void RadioButtonQuickLaunchFilesGroupByExtension_CheckedChanged(object sender, EventArgs e)
		{
			_quickLaunchFilesChanged = true;
		}

		private void RadioButtonQuickLaunchFilesGroupByPath_CheckedChanged(object sender, EventArgs e)
		{
			_quickLaunchFilesChanged = true;
		}

		private void CheckBoxQuickLaunchFilesShowFrequent_CheckedChanged(object sender, EventArgs e)
		{
			_quickLaunchFilesChanged = true;
			EnableControls();
		}

		private void NumericUpDownQuickLaunchFilesFrequentCount_ValueChanged(object sender, EventArgs e)
		{
			_quickLaunchFilesChanged = true;
		}

		private void RadioButtonGitRepositoryLinkExecuteCustomCommand_CheckedChanged(object sender, EventArgs e)
		{
			_repositoryLinkBehaviorChanged = true;
			EnableControls();
		}

		private void RadioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile_CheckedChanged(object sender, EventArgs e)
		{
			_repositoryLinkBehaviorChanged = true;
			EnableControls();
		}

		private void TextBoxCustomCommandName_TextChanged(object sender, EventArgs e)
		{
			_repositoryLinkBehaviorChanged = true;
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

		private void FlowLayoutPanelGitContextMenuCommands_Resize(object sender, EventArgs e)
		{
			_widthHolder1.Width = flowLayoutPanelGitContextMenuCommands.Width;
		}

		private void ButtonGitContextMenuCommandsAdd_Click(object sender, EventArgs e)
		{
			GitCommandEntry gitCommandEntry = CreateGitCommandEntry(new GitContextMenuCommand());
			_gitCommands.Add(gitCommandEntry);
			flowLayoutPanelGitContextMenuCommands.Controls.Add(gitCommandEntry);
			UpdateFlowLayoutPanelSize(flowLayoutPanelGitContextMenuCommands);
			_gitCommandsChanged = true;
		}

		private void ComboBoxRootPaths_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxRootPaths.SelectedItem == null)
				return;

			Root selectedRoot = (Root) comboBoxRootPaths.SelectedItem;
			Settings.Default.SelectedRootPath = selectedRoot.Path;

			ResetFlowLayoutPanel(flowLayoutPanelRootPathFilters, _widthHolder2);
			flowLayoutPanelRootPathFilters.Controls.AddRange(_rootFilters[selectedRoot].ToArray<Control>());
			UpdateFlowLayoutPanelSize(flowLayoutPanelRootPathFilters);
		}

		private void ButtonRootPathAdd_Click(object sender, EventArgs e)
		{
			Root selectedRoot = (Root) comboBoxRootPaths.SelectedItem;

			FolderBrowserDialog folderDialog = new FolderBrowserDialog
				{
					Description = "Select a root folder...",
					SelectedPath = selectedRoot?.Path
				};

			if (folderDialog.ShowDialog() != DialogResult.OK)
				return;

			Root root = new Root { Path = folderDialog.SelectedPath };
			_rootFilters.Add(root, new List<FilterEntry>());
			comboBoxRootPaths.Items.Add(root);
			comboBoxRootPaths.SelectedItem = root;
			_rootFoldersChanged = true;
		}

		private void ButtonRootPathRemove_Click(object sender, EventArgs e)
		{
			if (comboBoxRootPaths.SelectedItem == null)
				return;

			Root selectedRoot = (Root) comboBoxRootPaths.SelectedItem;

			if (Program.ShowQuestion("Are you sure you want to remove the selected root folder?") == DialogResult.Yes)
			{
				ResetFlowLayoutPanel(flowLayoutPanelRootPathFilters, _widthHolder2);
				comboBoxRootPaths.Items.Remove(selectedRoot);
				_rootFilters[selectedRoot].ForEach(DisposeFilterEntry);
				_rootFilters.Remove(selectedRoot);
				_rootFoldersChanged = true;
			}
		}

		private void FlowLayoutPanelRootPathFilters_Resize(object sender, EventArgs e)
		{
			_widthHolder2.Width = flowLayoutPanelRootPathFilters.Width;
		}

		private void ButtonRootPathFilterAdd_Click(object sender, EventArgs e)
		{
			if (comboBoxRootPaths.SelectedItem == null)
				return;

			Root selectedRoot = (Root) comboBoxRootPaths.SelectedItem;

			using (FilterForm filterForm = new FilterForm())
			{
				if (filterForm.ShowDialog(this) != DialogResult.OK)
					return;

				FilterEntry filterEntry = CreateFilterEntry(filterForm.Filter);
				_rootFilters[selectedRoot].Add(filterEntry);
				flowLayoutPanelRootPathFilters.Controls.Add(filterEntry);
				UpdateFlowLayoutPanelSize(flowLayoutPanelRootPathFilters);
				_rootFoldersChanged |= filterEntry.Filter.Type != FilterType.Highlight;
				_rootFoldersHighlightChanged |= filterEntry.Filter.Type == FilterType.Highlight;
			}
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			if (!SettingsAreValid())
				return;

			SaveSettings();
			HideForm();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			HideForm();
		}

		private void ButtonApply_Click(object sender, EventArgs e)
		{
			if (!SettingsAreValid())
				return;

			SaveSettings();
			LoadSettings();
		}

		private void GitCommandEntry_Action(object sender, GitCommandEntry.ActionEventArgs e)
		{
			if (!(sender is GitCommandEntry gitCommandEntry))
				return;

			_gitCommandsChanged = true;

			this.InvokeIfRequired(() =>
				{
					switch (e.Action)
					{
						case EntryAction.Delete:
							flowLayoutPanelGitContextMenuCommands.Controls.Remove(gitCommandEntry);
							_gitCommands.Remove(gitCommandEntry);
							DisposeGitCommandEntry(gitCommandEntry);
							UpdateFlowLayoutPanelSize(flowLayoutPanelGitContextMenuCommands);
							break;
						case EntryAction.MoveUp:
							MoveGitCommandEntry(gitCommandEntry, -1);
							break;
						case EntryAction.MoveDown:
							MoveGitCommandEntry(gitCommandEntry, 1);
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
				});
		}

		private void GitCommandEntry_Changed(object sender, EventArgs e)
		{
			_gitCommandsChanged = true;
		}

		private void FilterEntry_Action(object sender, FilterEntry.ActionEventArgs e)
		{
			if (!(sender is FilterEntry filterEntry))
				return;

			_rootFoldersChanged |= filterEntry.Filter.Type != FilterType.Highlight;
			_rootFoldersHighlightChanged |= filterEntry.Filter.Type == FilterType.Highlight;

			this.InvokeIfRequired(() =>
				{
					if (comboBoxRootPaths.SelectedItem == null)
						return;

					Root selectedRoot = (Root) comboBoxRootPaths.SelectedItem;

					switch (e.Action)
					{
						case EntryAction.Delete:
							flowLayoutPanelRootPathFilters.Controls.Remove(filterEntry);
							_rootFilters[selectedRoot].Remove(filterEntry);
							DisposeFilterEntry(filterEntry);
							UpdateFlowLayoutPanelSize(flowLayoutPanelRootPathFilters);
							break;
						case EntryAction.MoveUp:
							MoveFilterEntry(selectedRoot, filterEntry, -1);
							break;
						case EntryAction.MoveDown:
							MoveFilterEntry(selectedRoot, filterEntry, 1);
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
				});
		}

		private void FilterEntry_Changed(object sender, FilterEntry.ChangedEventArgs e)
		{
			_rootFoldersChanged = _rootFoldersChanged | e.FromType != FilterType.Highlight | e.ToType != FilterType.Highlight;
			_rootFoldersHighlightChanged = _rootFoldersHighlightChanged | e.FromType == FilterType.Highlight | e.ToType == FilterType.Highlight;
		}

		private static void UpdateFlowLayoutPanelSize(FlowLayoutPanel flowLayoutPanel)
		{
			flowLayoutPanel.Height = flowLayoutPanel.Controls.Cast<Control>().Sum(x => x.Height + x.Margin.Vertical);
		}

		private static void ResetFlowLayoutPanel(FlowLayoutPanel flowLayoutPanel, Label widthHolder)
		{
			flowLayoutPanel.Controls.Clear();
			flowLayoutPanel.Controls.Add(widthHolder);
			widthHolder.Width = flowLayoutPanel.Width;
		}

		private void UpdateGitCommands()
		{
			ResetFlowLayoutPanel(flowLayoutPanelGitContextMenuCommands, _widthHolder1);
			flowLayoutPanelGitContextMenuCommands.Controls.AddRange(_gitCommands.ToArray<Control>());
			UpdateFlowLayoutPanelSize(flowLayoutPanelGitContextMenuCommands);
		}

		private GitCommandEntry CreateGitCommandEntry(GitContextMenuCommand gitCommand)
		{
			GitCommandEntry gitCommandEntry = new GitCommandEntry(gitCommand);
			gitCommandEntry.Action += GitCommandEntry_Action;
			gitCommandEntry.Changed += GitCommandEntry_Changed;
			return gitCommandEntry;
		}

		private void DisposeGitCommandEntry(GitCommandEntry gitCommandEntry)
		{
			gitCommandEntry.Action -= GitCommandEntry_Action;
			gitCommandEntry.Changed -= GitCommandEntry_Changed;
			gitCommandEntry.Dispose();
		}

		private void MoveGitCommandEntry(GitCommandEntry gitCommandEntry, int adjustment)
		{
			if (adjustment == 0)
				return;

			int index = _gitCommands.IndexOf(gitCommandEntry);

			if (index < 0)
				return;

			int newIndex = index + adjustment;
			newIndex = newIndex < 0 ? 0 : newIndex;
			newIndex = newIndex >= _gitCommands.Count ? _gitCommands.Count - 1 : newIndex;
			flowLayoutPanelGitContextMenuCommands.Controls.SetChildIndex(gitCommandEntry, newIndex + 1);
			_gitCommands.RemoveAt(index);
			_gitCommands.Insert(newIndex, gitCommandEntry);
		}

		private FilterEntry CreateFilterEntry(RootPathFilter filter)
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

		private void MoveFilterEntry(Root root, FilterEntry filterEntry, int adjustment)
		{
			if (adjustment == 0)
				return;

			int index = _rootFilters[root].IndexOf(filterEntry);

			if (index < 0)
				return;

			int newIndex = index + adjustment;
			newIndex = newIndex < 0 ? 0 : newIndex;
			newIndex = newIndex >= _rootFilters[root].Count ? _rootFilters[root].Count - 1 : newIndex;
			flowLayoutPanelRootPathFilters.Controls.SetChildIndex(filterEntry, newIndex + 1);
			_rootFilters[root].RemoveAt(index);
			_rootFilters[root].Insert(newIndex, filterEntry);
		}

		private void EnableControls(bool enable = true)
		{
			numericUpDownQuickLaunchFilesFrequentCount.Enabled = enable && checkBoxQuickLaunchFilesShowFrequent.Checked;
			labelCustomCommandName.Enabled = enable && radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.Checked;
			textBoxCustomCommandName.Enabled = enable && radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.Checked;
		}

		private bool SettingsAreValid()
		{
			if (!textBoxQuickLaunchFilesFilter.Text.IsValidRegex(allowEmpty: true))
			{
				Program.ShowError("Quick launch files filter not a valid regular expression!");
				return false;
			}

			if (string.IsNullOrWhiteSpace(textBoxCustomCommandName.Text))
			{
				Program.ShowError("Custom command name cannot be empty!");
				return false;
			}

			return true;
		}

		private void LoadSettings()
		{
			textBoxCustomCommandPath.Text = Settings.Default.CommandPath;
			textBoxCustomCommandArgs.Text = Settings.Default.CommandArgs;
			textBoxCustomCommandName.Text = Settings.Default.CommandName;
			textBoxQuickLaunchFilesFilter.Text = Settings.Default.QuickLaunchFilesFilter;
			radioButtonQuickLaunchFilesDoNotGroup.Checked = Settings.Default.QuickLaunchFilesGrouping == QuickLaunchFilesGrouping.None;
			radioButtonQuickLaunchFilesGroupByExtension.Checked = Settings.Default.QuickLaunchFilesGrouping == QuickLaunchFilesGrouping.ByExtension;
			radioButtonQuickLaunchFilesGroupByPath.Checked = Settings.Default.QuickLaunchFilesGrouping == QuickLaunchFilesGrouping.ByPath;
			checkBoxQuickLaunchFilesShowFrequent.Checked = Settings.Default.ShowFrequentQuickLaunchFiles;
			numericUpDownQuickLaunchFilesFrequentCount.Value = Settings.Default.FrequentQuickLaunchFilesCount;
			radioButtonGitRepositoryLinkExecuteCustomCommand.Checked = Settings.Default.RepositoryLinkBehavior == RepositoryLinkBehavior.ExecuteCustomCommand;
			radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.Checked = Settings.Default.RepositoryLinkBehavior == RepositoryLinkBehavior.LaunchSelectedQuickLaunchFile;
			checkBoxCloseOnLostFocus.Checked = Settings.Default.CloseOnLostFocus;
			checkBoxStartWithWindows.Checked = _lastStartWithWindows = Settings.Default.StartWithWindows;
			checkBoxEnableLogging.Checked = Settings.Default.EnableLogging;
			textBoxGitExePath.Text = Settings.Default.GitPath;
			checkBoxGitShowCommandOutput.Checked = Settings.Default.ShowGitCommandOutput;
			checkBoxGitEnableRemoteBranchLookup.Checked = Settings.Default.EnableRemoteBranchLookup;
			checkBoxGitExcludeLfsRepositories.Checked = Settings.Default.ExcludeLfsRepositories;

			_gitCommands.ForEach(DisposeGitCommandEntry);
			_gitCommands.Clear();
			_gitCommands.AddRange(Settings.Default.GitContextMenuCommands.Select(x => x.Clone()).Select(CreateGitCommandEntry));

			UpdateGitCommands();

			foreach (Root root in _rootFilters.Keys)
				_rootFilters[root].ForEach(DisposeFilterEntry);

			_rootFilters.Clear();

			foreach (Root root in Settings.Default.Roots.Select(x => x.Clone()))
				_rootFilters.Add(root, root.Filters.Select(CreateFilterEntry).ToList());

			comboBoxRootPaths.Items.Clear();
			comboBoxRootPaths.Items.AddRange(_rootFilters.Keys.ToArray<object>());

			if (comboBoxRootPaths.Items.Count > 0)
			{
				comboBoxRootPaths.SelectedItem = comboBoxRootPaths.Items
					.Cast<Root>()
					.FirstOrDefault(root => string.Equals(root.Path, Settings.Default.SelectedRootPath, StringComparison.CurrentCultureIgnoreCase)) ?? comboBoxRootPaths.Items[0];
			}
		}

		private void SaveSettings()
		{
			_rootFoldersChanged |= Settings.Default.EnableRemoteBranchLookup != checkBoxGitEnableRemoteBranchLookup.Checked;

			Settings.Default.CommandPath = textBoxCustomCommandPath.Text;
			Settings.Default.CommandArgs = textBoxCustomCommandArgs.Text;
			Settings.Default.CommandName = textBoxCustomCommandName.Text;
			Settings.Default.QuickLaunchFilesFilter = textBoxQuickLaunchFilesFilter.Text;
			Settings.Default.QuickLaunchFilesGrouping = radioButtonQuickLaunchFilesGroupByExtension.Checked
				? QuickLaunchFilesGrouping.ByExtension
				: radioButtonQuickLaunchFilesGroupByPath.Checked
					? QuickLaunchFilesGrouping.ByPath
					: QuickLaunchFilesGrouping.None;
			Settings.Default.ShowFrequentQuickLaunchFiles = checkBoxQuickLaunchFilesShowFrequent.Checked;
			Settings.Default.FrequentQuickLaunchFilesCount = (int)numericUpDownQuickLaunchFilesFrequentCount.Value;
			Settings.Default.RepositoryLinkBehavior = radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.Checked
				? RepositoryLinkBehavior.LaunchSelectedQuickLaunchFile
				: RepositoryLinkBehavior.ExecuteCustomCommand;
			Settings.Default.CloseOnLostFocus = checkBoxCloseOnLostFocus.Checked;
			Settings.Default.StartWithWindows = checkBoxStartWithWindows.Checked;
			Settings.Default.EnableLogging = checkBoxEnableLogging.Checked;
			Settings.Default.GitPath = textBoxGitExePath.Text;
			Settings.Default.ShowGitCommandOutput = checkBoxGitShowCommandOutput.Checked;
			Settings.Default.EnableRemoteBranchLookup = checkBoxGitEnableRemoteBranchLookup.Checked;
			Settings.Default.ExcludeLfsRepositories = checkBoxGitExcludeLfsRepositories.Checked;

			Settings.Default.GitContextMenuCommands = _gitCommands
				.Select(gitCommandEntry => gitCommandEntry.GitCommand)
				.Select(gitCommand =>
					{
						if (gitCommand.Command?.StartsWith("git ", StringComparison.CurrentCultureIgnoreCase) ?? false)
							gitCommand.Command = gitCommand.Command.Substring(4);

						return gitCommand;
					})
				.Where(gitCommand => !string.IsNullOrWhiteSpace(gitCommand.Caption) && !string.IsNullOrWhiteSpace(gitCommand.Command))
				.ToList();

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

			RaiseSettingsChangedEvent(_quickLaunchFilesChanged, _repositoryLinkBehaviorChanged, _gitCommandsChanged, _rootFoldersChanged, _rootFoldersHighlightChanged);
			_quickLaunchFilesChanged = _repositoryLinkBehaviorChanged = _gitCommandsChanged = _rootFoldersChanged = _rootFoldersHighlightChanged = false;
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
