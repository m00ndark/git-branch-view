using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GitBranchView
{
	public enum FilterEntryAction
	{
		Delete,
		MoveUp,
		MoveDown
	}

	public partial class FilterEntry : UserControl
	{
		public class ActionEventArgs : EventArgs
		{
			public ActionEventArgs(FilterEntryAction action)
			{
				Action = action;
			}

			public FilterEntryAction Action { get; }
		}

		public class ChangedEventArgs : EventArgs
		{
			public ChangedEventArgs(Settings.FilterType fromType, Settings.FilterType toType)
			{
				FromType = fromType;
				ToType = toType;
			}

			public Settings.FilterType FromType { get; }
			public Settings.FilterType ToType { get; }
		}

		public event EventHandler<ActionEventArgs> Action;
		public event EventHandler<ChangedEventArgs> Changed;

		public FilterEntry(Settings.RootPathFilter filter)
		{
			InitializeComponent();
			Filter = filter;
			UpdateInfo();
		}

		public Settings.RootPathFilter Filter { get; private set; }

		private bool IsHighlight => Filter.Type == Settings.FilterType.Highlight;

		private void FilterEntry_Load(object sender, EventArgs e)
		{
			Anchor = AnchorStyles.Left | AnchorStyles.Right;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			Color color = IsHighlight ? (Color) Filter.Color : SystemColors.Control;
			Rectangle rectangle = new Rectangle(linkLabelEdit.Left - 5, 1, Width - linkLabelEdit.Left - 2, Height - 3);

			if (IsHighlight)
			{
				using (Brush brush = new SolidBrush(color))
					e.Graphics.FillRectangle(brush, rectangle);
			}
			else
			{
				using (Pen pen = new Pen(color))
					e.Graphics.DrawRectangle(pen, rectangle);
			}
		}

		private void ButtonDelete_Click(object sender, EventArgs e)
		{
			RaiseActionEvent(FilterEntryAction.Delete);
		}

		private void ButtonMoveUp_Click(object sender, EventArgs e)
		{
			RaiseActionEvent(FilterEntryAction.MoveUp);
		}

		private void ButtonMoveDown_Click(object sender, EventArgs e)
		{
			RaiseActionEvent(FilterEntryAction.MoveDown);
		}

		private void LinkLabelEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			using (FilterForm filterForm = new FilterForm(Filter.Clone()))
			{
				if (filterForm.ShowDialog(this) != DialogResult.OK)
					return;

				Settings.FilterType fromType = Filter.Type;
				Settings.FilterType toType = filterForm.Filter.Type;

				Filter = filterForm.Filter;
				UpdateInfo();
				Invalidate();

				RaiseChangedEvent(fromType, toType);
			}
		}

		private void UpdateInfo()
		{
			labelType.Text = Filter.Type.ToString();
			labelType.Left = Width - labelType.Width - labelType.Margin.Right;
			labelFilter.Text = Filter.Filter;
		}

		private void RaiseActionEvent(FilterEntryAction action)
		{
			Action?.BeginInvoke(this, new ActionEventArgs(action), null, null);
		}

		private void RaiseChangedEvent(Settings.FilterType fromType, Settings.FilterType toType)
		{
			Changed?.BeginInvoke(this, new ChangedEventArgs(fromType, toType), null, null);
		}
	}
}
