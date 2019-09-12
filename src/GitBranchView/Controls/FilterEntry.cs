using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using GitBranchView.Forms;
using GitBranchView.Model;

namespace GitBranchView.Controls
{
	public partial class FilterEntry : UserControl
	{
		public class ActionEventArgs : EventArgs
		{
			public ActionEventArgs(EntryAction action)
			{
				Action = action;
			}

			public EntryAction Action { get; }
		}

		public class ChangedEventArgs : EventArgs
		{
			public ChangedEventArgs(FilterType fromType, FilterType toType)
			{
				FromType = fromType;
				ToType = toType;
			}

			public FilterType FromType { get; }
			public FilterType ToType { get; }
		}

		public event EventHandler<ActionEventArgs> Action;
		public event EventHandler<ChangedEventArgs> Changed;

		public FilterEntry(RootPathFilter filter)
		{
			InitializeComponent();
			Filter = filter;
			UpdateInfo();
		}

		public RootPathFilter Filter { get; private set; }

		private bool IsHighlight => Filter.Type == FilterType.Highlight;

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
			RaiseActionEvent(EntryAction.Delete);
		}

		private void ButtonMoveUp_Click(object sender, EventArgs e)
		{
			RaiseActionEvent(EntryAction.MoveUp);
		}

		private void ButtonMoveDown_Click(object sender, EventArgs e)
		{
			RaiseActionEvent(EntryAction.MoveDown);
		}

		private void LinkLabelEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			using (FilterForm filterForm = new FilterForm(Filter.Clone()))
			{
				if (filterForm.ShowDialog(this) != DialogResult.OK)
					return;

				FilterType fromType = Filter.Type;
				FilterType toType = filterForm.Filter.Type;

				Filter = filterForm.Filter;
				UpdateInfo();
				Invalidate();

				RaiseChangedEvent(fromType, toType);
			}
		}

		private void UpdateInfo()
		{
			labelTypeAndTarget.Text = string.Join(" | ", Filter.Target.GetFlagValues().Select(x => x.ToString()).Append(Filter.Type.ToString()));
			labelTypeAndTarget.Left = Width - labelTypeAndTarget.Width - labelTypeAndTarget.Margin.Right;
			labelFilter.Text = Filter.Filter;
			labelFilter.Width = labelTypeAndTarget.Left - labelTypeAndTarget.Margin.Left - labelFilter.Margin.Right - labelFilter.Left;
		}

		private void RaiseActionEvent(EntryAction action)
		{
			Action?.BeginInvoke(this, new ActionEventArgs(action), null, null);
		}

		private void RaiseChangedEvent(FilterType fromType, FilterType toType)
		{
			Changed?.BeginInvoke(this, new ChangedEventArgs(fromType, toType), null, null);
		}
	}
}
