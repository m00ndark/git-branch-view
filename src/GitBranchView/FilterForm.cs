using System;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GitBranchView
{
	public partial class FilterForm : Form
	{
		private readonly string _text;

		public FilterForm(Settings.RootPathFilter filter = null)
		{
			InitializeComponent();
			Filter = filter ?? new Settings.RootPathFilter();

			comboBoxType.Items.AddRange(Enum.GetValues(typeof(Settings.FilterType)).Cast<object>().ToArray());

			comboBoxType.SelectedItem = filter?.Type ?? Settings.FilterType.Exclude;
			textBoxFilter.Text = filter?.Filter ?? string.Empty;
			_text = filter == null ? "New Filter" : "Edit Filter";
		}

		public Settings.RootPathFilter Filter { get; }

		private void FilterForm_Load(object sender, EventArgs e)
		{
			MinimumSize = Size;
			Text = _text;
		}

		private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.FilterType? type = (Settings.FilterType?) comboBoxType.SelectedItem;

			if (type == Settings.FilterType.Highlight)
				UpdateColor();

			labelColor.Enabled = type == Settings.FilterType.Highlight;
			buttonColor.Enabled = type == Settings.FilterType.Highlight;
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
				MessageBox.Show("Filter type not selected!", Program.GIT_BRANCH_VIEW, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!IsValidRegex(textBoxFilter.Text))
			{
				MessageBox.Show("Filter not a valid regular expression!", Program.GIT_BRANCH_VIEW, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Filter.Type = (Settings.FilterType) comboBoxType.SelectedItem;
			Filter.Filter = textBoxFilter.Text;

			DialogResult = DialogResult.OK;
			Close();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private static bool IsValidRegex(string pattern)
		{
			if (string.IsNullOrWhiteSpace(pattern))
				return false;

			try
			{
				Regex.IsMatch("", pattern);
			}
			catch (ArgumentException)
			{
				return false;
			}

			return true;
		}
	}
}
