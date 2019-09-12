using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitBranchView.Model;
using GitBranchView.Properties;
using ToolComponents.Core.Extensions;

namespace GitBranchView.Controls
{
	public partial class RootEntry : UserControl
	{
		private bool _isUpdatingFolders = false;

		public event EventHandler ExpandedCollapsed;

		public RootEntry(Root root)
		{
			Root = root;
			InitializeComponent();
		}

		public Root Root { get; }

		private void RaiseExpandedCollapsed()
		{
			ExpandedCollapsed?.Invoke(this, EventArgs.Empty);
		}

		private void PictureBoxExpandCollapse_MouseDown(object sender, MouseEventArgs e)
		{
			if (_isUpdatingFolders)
				return;

			Root.Expanded = !Root.Expanded;
			pictureBoxExpandCollapse.Image = Root.Expanded ? Resources.expanded : Resources.collapsed;
			buttonRefresh.Visible = Root.Expanded;
			UpdateSize();
			RaiseExpandedCollapsed();
		}

		private async void ButtonRefresh_Click(object sender, EventArgs e)
		{
			await UpdateFolders();
		}

		public async Task UpdateFolders()
		{
			try
			{
				_isUpdatingFolders = true;

				foreach (FolderEntry folderEntry in flowLayoutPanel.Controls.OfType<FolderEntry>())
					folderEntry.WidthChanged -= FolderEntry_WidthChanged;

				flowLayoutPanel.Controls.Clear();
				labelInfo.Text = "Scanning...";
				labelInfo.Visible = true;
				buttonRefresh.Visible = false;

				if (!Settings.Default.GitPath.GitPathIsValid(out string error) || !Root.IsValid(out error))
				{
					labelInfo.Text = error;
					return;
				}

				pictureBoxExpandCollapse.Image = Root.Expanded ? Resources.expanded : Resources.collapsed;
				labelRootPath.Text = Root.Path;

				Stopwatch stopwatch = Stopwatch.StartNew();

				ConcurrentBag<(string Path, string Branch, int TrackedChanges, int UntrackedChanges, string[] Errors)> gitRepositories = new ConcurrentBag<(string, string, int, int, string[])>();

				await Task.Run(() =>
					{
						Parallel.ForEach(Root.Path.ScanFolder(), folder =>
							{
								Git.TryGetBranch(Root, folder, out string branch, out int trackedChanges, out int untrackedChanges, out string[] errors);

								if (!Root.ShouldInclude(folder, branch))
									return;

								gitRepositories.Add((folder, branch, trackedChanges, untrackedChanges, errors));

								this.InvokeIfRequired(() =>
									{
										int repositoriesCount = gitRepositories.Count;
										labelInfo.Text = $"Scanning; found {repositoriesCount} {(repositoriesCount == 1 ? "repository" : "repositories")}...";
										Application.DoEvents();
									});
							});
					});

				labelInfo.Text = $"Rendering {gitRepositories.Count} {(gitRepositories.Count == 1 ? "repository" : "repositories")}...";
				Application.DoEvents();

				List<FolderEntry> folderEntries = gitRepositories
					.OrderBy(x => x.Path)
					.Select(x => new FolderEntry(Root, x.Path, x.Branch, x.TrackedChanges, x.UntrackedChanges, x.Errors))
					.ToList();

				folderEntries.ForEach(folderEntry => folderEntry.WidthChanged += FolderEntry_WidthChanged);

				flowLayoutPanel.Controls.AddRange(folderEntries.ToArray<Control>());
				UpdateSize(folderEntries);
				labelInfo.Visible = false;
				buttonRefresh.Visible = Root.Expanded;
				folderEntries.FirstOrDefault()?.Focus();

				Debug.WriteLine($"{Root.Path}\t{stopwatch.Elapsed}");
			}
			finally
			{
				_isUpdatingFolders = false;
			}
		}

		private void FolderEntry_WidthChanged(object sender, EventArgs e)
		{
			if (!(sender is FolderEntry changedFolderEntry))
				return;

			if (changedFolderEntry.Width != flowLayoutPanel.Width)
				UpdateSize();
		}

		public void UpdateSize()
		{
			List<FolderEntry> folderEntries = flowLayoutPanel.Controls.OfType<FolderEntry>().ToList();
			UpdateSize(folderEntries);
		}

		private void UpdateSize(List<FolderEntry> folderEntries)
		{
			int headingWidth = labelRootPath.Left + labelRootPath.Width + labelRootPath.Margin.Horizontal + buttonRefresh.Width;
			UpdateSize(folderEntries.Select(x => (x.Size, x.Margin)).ToArray(), headingWidth);
			UpdateWidth(folderEntries);
		}

		private void UpdateSize(ICollection<(Size Size, Padding Margin)> controlDimensions, int headingWidth)
		{
			const int MIN_WIDTH = 270, MIN_HEIGHT = 50;

			if (controlDimensions != null && controlDimensions.Count > 0)
			{
				int totalControlsHeight = controlDimensions.Sum(x => x.Size.Height + x.Margin.Vertical);
				int maxControlWidth = Math.Max(headingWidth, controlDimensions.Max(x => x.Size.Width + x.Margin.Horizontal));
				Size newSize = new Size(maxControlWidth, (Root.Expanded ? flowLayoutPanel.Top + totalControlsHeight : flowLayoutPanel.Top) + 1);
				if (newSize.Width != Size.Width || newSize.Height != Size.Height)
					Size = newSize;
			}
			else
				Size = new Size(Math.Max(MIN_WIDTH, headingWidth), MIN_HEIGHT);
		}

		public void UpdateWidth()
		{
			List<FolderEntry> folderEntries = flowLayoutPanel.Controls.OfType<FolderEntry>().ToList();
			UpdateWidth(folderEntries);
		}

		private void UpdateWidth(List<FolderEntry> folderEntries)
		{
			folderEntries.ForEach(folderEntry =>
				{
					if (folderEntry.Width != flowLayoutPanel.Width)
						folderEntry.Width = flowLayoutPanel.Width;
				});
		}

		public void HighlightChanged()
		{
			foreach (FolderEntry folderEntry in flowLayoutPanel.Controls.OfType<FolderEntry>())
			{
				folderEntry.HighlightChanged();
			}
		}

		public void GitContextMenuCommandsChanged()
		{
			foreach (FolderEntry folderEntry in flowLayoutPanel.Controls.OfType<FolderEntry>())
			{
				folderEntry.GitContextMenuCommandsChanged();
			}
		}
	}
}
