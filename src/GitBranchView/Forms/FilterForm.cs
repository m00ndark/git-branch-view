using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GitBranchView.Model;

namespace GitBranchView.Forms
{
	public partial class FilterForm : Form
	{
		private readonly string _text;

		public FilterForm(RootPathFilter filter = null)
		{
			InitializeComponent();
			Filter = filter ?? new RootPathFilter();

			comboBoxType.Items.AddRange(Enum.GetValues(typeof(FilterType)).Cast<object>().ToArray());

			comboBoxType.SelectedItem = filter?.Type ?? FilterType.Exclude;
			checkBoxTargetPath.Checked = filter?.Target.HasFlag(FilterTargets.Path) ?? false;
			checkBoxTargetBranch.Checked = filter?.Target.HasFlag(FilterTargets.Branch) ?? false;
			textBoxFilter.Text = filter?.Filter ?? string.Empty;
			_text = filter == null ? "New Filter" : "Edit Filter";
		}

		public RootPathFilter Filter { get; }

		private void FilterForm_Load(object sender, EventArgs e)
		{
			MinimumSize = Size;
			Text = _text;
		}

		private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			FilterType? type = (FilterType?) comboBoxType.SelectedItem;

			if (type == FilterType.Highlight)
				UpdateColor();

			labelColor.Enabled = type == FilterType.Highlight;
			buttonColor.Enabled = type == FilterType.Highlight;
		}

		private void ButtonColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog
				{
					Color = Filter.Color,
					AllowFullOpen = true,
					AnyColor = true,
					FullOpen = true
				};

			if (colorDialog.ShowDialog(this) != DialogResult.OK)
				return;

			Filter.Color = colorDialog.Color;
			UpdateColor();
		}

		private void UpdateColor()
		{
			buttonColor.BackColor = Filter.Color;
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			if (comboBoxType.SelectedItem == null)
			{
				Program.ShowError("Filter type not selected!");
				return;
			}

			if (!checkBoxTargetPath.Checked && !checkBoxTargetBranch.Checked)
			{
				Program.ShowError("Filter target not selected!");
				return;
			}

			if (!textBoxFilter.Text.IsValidRegex())
			{
				Program.ShowError("Filter not a valid regular expression!");
				return;
			}

			Filter.Type = (FilterType) comboBoxType.SelectedItem;
			Filter.Target = (checkBoxTargetPath.Checked ? FilterTargets.Path : FilterTargets.None)
				| (checkBoxTargetBranch.Checked ? FilterTargets.Branch : FilterTargets.None);
			Filter.Filter = textBoxFilter.Text;

			DialogResult = DialogResult.OK;
			Close();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
