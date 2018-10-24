using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitBranchView
{
	public partial class RootEntry : UserControl
	{
		public event EventHandler ExpandedCollapsed;

		public RootEntry(Settings.Root root)
		{
			Root = root;
			InitializeComponent();
		}

		public Settings.Root Root { get; }

		private void RaiseExpandedCollapsed()
		{
			ExpandedCollapsed?.Invoke(this, EventArgs.Empty);
		}

		private void PictureBoxExpandCollapse_MouseDown(object sender, MouseEventArgs e)
		{
			Root.Expanded = !Root.Expanded;
			pictureBoxExpandCollapse.Image = Root.Expanded ? Properties.Resources.expanded : Properties.Resources.collapsed;
			UpdateSize();
			RaiseExpandedCollapsed();
		}

		private void ButtonRefresh_Click(object sender, EventArgs e)
		{
			UpdateFolders();
		}

		public void UpdateFolders()
		{
			foreach (FolderEntry folderEntry in flowLayoutPanel.Controls.OfType<FolderEntry>())
				folderEntry.WidthChanged -= FolderEntry_WidthChanged;

			flowLayoutPanel.Controls.Clear();
			labelError.Visible = false;

			if (!Settings.Default.GitPath.GitPathIsValid(out string error) || !Root.IsValid(out error))
			{
				labelError.Text = error;
				labelError.Visible = true;
				return;
			}

			pictureBoxExpandCollapse.Image = Root.Expanded ? Properties.Resources.expanded : Properties.Resources.collapsed;
			labelRootPath.Text = Root.Path;

			ConcurrentBag<(string Path, string Branch, int TrackedChanges, int UntrackedChanges)> gitRepositories = new ConcurrentBag<(string, string, int, int)>();

			Parallel.ForEach(Root.Path.ScanFolder(), folder =>
				{
					if (Git.TryGetBranch(folder, out string branch, out int trackedChanges, out int untrackedChanges))
						gitRepositories.Add((folder, branch, trackedChanges, untrackedChanges));
				});

			List<FolderEntry> folderEntries = gitRepositories
				.Where(x => Root.ShouldInclude(x.Path))
				.OrderBy(x => x.Path)
				.Select(x => new FolderEntry(Root, x.Path, x.Branch, x.TrackedChanges, x.UntrackedChanges))
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
				UpdateSize();
		}

		public void UpdateSize()
		{
			List<FolderEntry> folderEntries = flowLayoutPanel.Controls.OfType<FolderEntry>().ToList();
			folderEntries.ForEach(folderEntry => folderEntry.UpdateSize());
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
			const int MIN_WIDTH = 270, MIN_HEIGHT = 120;

			if (controlDimensions != null && controlDimensions.Count > 0)
			{
				int totalControlsHeight = controlDimensions.Sum(x => x.Size.Height + x.Margin.Vertical);
				int maxControlWidth = Math.Max(headingWidth, controlDimensions.Max(x => x.Size.Width + x.Margin.Horizontal));
				Size newSize = new Size(Root.Expanded ? maxControlWidth : MIN_WIDTH, (Root.Expanded ? flowLayoutPanel.Top + totalControlsHeight : flowLayoutPanel.Top) + 1);
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
			folderEntries.ForEach(folderEntry => folderEntry.Width = flowLayoutPanel.Width);
		}
	}
}
