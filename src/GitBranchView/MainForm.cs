using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolComponents.Core.Extensions;

namespace GitBranchView
{
	public partial class MainForm : Form
	{
		private readonly SettingsForm _settingsForm;
		private DateTime _formClosedAt;
		private bool _keepOpenOnce;
		private bool _isUpdatingRootFolders;
		private Point _lastShowPosition;

		public MainForm()
		{
			InitializeComponent();
			contextMenuStrip.Renderer = new ToolStripMenuRenderer();
			_settingsForm = new SettingsForm();
			_settingsForm.SettingsChanged += SettingsForm_SettingsChanged;
			_formClosedAt = DateTime.MinValue;
			_keepOpenOnce = false;
			_isUpdatingRootFolders = false;
		}

		private void SettingsForm_SettingsChanged(object sender, EventArgs e)
		{
			this.InvokeIfRequired(() =>
				{
					UpdateButtons();
					UpdateRootFolders();
				});
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			toolStripMenuItemVersion.Text = Assembly.GetExecutingAssembly().GetBuildVersion();

			HideForm();
			UpdateButtons();
			UpdateRootFolders();
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
			{
				if (ModifierKeys == Keys.Control)
					TrySetClipboardText(Assembly.GetExecutingAssembly().GetBuildVersion());

				return;
			}

			if (ModifierKeys == Keys.Control)
			{
				_keepOpenOnce = true;
				UpdateButtons();
			}

			if ((int) Opacity == 0 && _formClosedAt.AddMilliseconds(500) < DateTime.Now)
			{
				_lastShowPosition = MousePosition;
				SetLocation();
				Opacity = 1;
				Show();
			}

			try { NativeMethods.SetForegroundWindow(Handle); } catch { ; }
		}

		private void ToolStripMenuItemSettings_Click(object sender, EventArgs e)
		{
			_settingsForm.ShowForm();
		}

		private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			_settingsForm.TerminateForm();
			Application.Exit();
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

		private void HideForm()
		{
			Hide();
			Opacity = 0;
			_formClosedAt = DateTime.Now;
		}

		private void SetLocation()
		{
			Location = new Point(
				Math.Min(_lastShowPosition.X - Size.Width / 2, Screen.PrimaryScreen.WorkingArea.Width - Size.Width - 8),
				Math.Max(Screen.PrimaryScreen.WorkingArea.Top, Screen.PrimaryScreen.WorkingArea.Height - Size.Height - 8));
		}

		private void UpdateButtons()
		{
			buttonClose.Visible = !Settings.Default.CloseOnLostFocus || _keepOpenOnce;
		}

		private async void UpdateRootFolders()
		{
			try
			{
				_isUpdatingRootFolders = true;

				foreach (RootEntry rootEntry in flowLayoutPanel.Controls.OfType<RootEntry>())
					rootEntry.SizeChanged -= RootEntry_SizeChanged;

				flowLayoutPanel.Controls.Clear();
				labelInfo.Visible = !Settings.Default.Roots.Any();

				List<RootEntry> rootEntries = Settings.Default.Roots
					.OrderBy(root => root.Path)
					.Select(root => new RootEntry(root))
					.ToList();

				rootEntries.ForEach(rootEntry => rootEntry.UpdateSize());
				UpdateSize(rootEntries);
				flowLayoutPanel.Controls.AddRange(rootEntries.ToArray<Control>());
				rootEntries.FirstOrDefault()?.Focus();

				rootEntries.ForEach(rootEntry => rootEntry.SizeChanged += RootEntry_SizeChanged);

				await Task.WhenAll(rootEntries.Select(rootEntry => rootEntry.UpdateFolders()));
			}
			finally
			{
				_isUpdatingRootFolders = false;
			}
		}

		private void RootEntry_SizeChanged(object sender, EventArgs e)
		{
			if (!(sender is RootEntry changedRootEntry))
				return;

			List<RootEntry> rootEntries = flowLayoutPanel.Controls.OfType<RootEntry>().ToList();

			if (!_isUpdatingRootFolders)
			{
				foreach (RootEntry rootEntry in rootEntries)
				{
					if (rootEntry == changedRootEntry)
						continue;

					rootEntry.SizeChanged -= RootEntry_SizeChanged;
					rootEntry.UpdateSize();
					rootEntry.SizeChanged += RootEntry_SizeChanged;
				}
			}

			UpdateSize(rootEntries);
		}

		private void UpdateSize(List<RootEntry> rootEntries)
		{
			UpdateSize(rootEntries.Select(x => (x.Size, x.Margin)).ToArray());
			SetLocation();

			foreach (RootEntry rootEntry in rootEntries)
			{
				if (rootEntry.Width == flowLayoutPanel.Width)
					continue;

				rootEntry.Width = flowLayoutPanel.Width;
				rootEntry.UpdateWidth();
			}
		}

		private void UpdateSize(ICollection<(Size Size, Padding Margin)> controlDimensions)
		{
			const int MIN_WIDTH = 290, MIN_HEIGHT = 170;

			MinimumSize = MaximumSize = new Size(0, 0);

			Size newSize;
			int totalControlsHeight = controlDimensions?.Sum(x => x.Size.Height + x.Margin.Vertical) ?? 0;

			if (controlDimensions != null && controlDimensions.Count > 0)
			{
				int maxControlWidth = controlDimensions.Max(x => x.Size.Width + x.Margin.Horizontal);
				newSize = new Size(Math.Max(MIN_WIDTH, Width - panelScroll.Width + maxControlWidth),
					Math.Min(Height - panelScroll.Height + totalControlsHeight, Screen.PrimaryScreen.WorkingArea.Height));
			}
			else
			{
				newSize = new Size(MIN_WIDTH, MIN_HEIGHT);
			}

			if (newSize.Width != Size.Width || newSize.Height != Size.Height)
			{
				bool flowLayoutPanelHeightChanged = false;
				if (totalControlsHeight < flowLayoutPanel.Height)
				{
					flowLayoutPanel.Size = new Size(flowLayoutPanel.Width, totalControlsHeight);
					flowLayoutPanelHeightChanged = true;
				}

				Size = newSize;

				if (!flowLayoutPanelHeightChanged)
					flowLayoutPanel.Size = new Size(flowLayoutPanel.Width, totalControlsHeight);
			}

			MinimumSize = MaximumSize = Size;
			SetLocation();
		}

		public static void TrySetClipboardText(string text)
		{
			int attempt = 0;
			while (attempt++ < 10)
			{
				try
				{
					Clipboard.SetText(text);
					break;
				}
				catch
				{
					;
				}
			}
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
