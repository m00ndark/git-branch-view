using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolComponents.Core.Extensions;

namespace GitBranchView.Forms
{
	public enum OutputFormTitle
	{
		Output,
		Error
	}

	public interface IOutputHandler
	{
		void AddLine(string line);
	}

	public partial class OutputForm : Form, IOutputHandler
	{
		public OutputForm(
			bool showInTaskbar = false,
			OutputFormTitle title = OutputFormTitle.Output)
		{
			InitializeComponent();
			ShowInTaskbar = showInTaskbar;
			Title = title;
		}

		public OutputForm(
			IEnumerable<string> errorOutput,
			bool showInTaskbar = false,
			OutputFormTitle title = OutputFormTitle.Error) : this(showInTaskbar, title)
		{
			ErrorOutput = errorOutput;
		}

		public OutputForm(
			Func<IOutputHandler, Task<(bool Success, string[] Errors)>> operation,
			bool outputIsProgress = false,
			bool showInTaskbar = false,
			OutputFormTitle title = OutputFormTitle.Output) : this(showInTaskbar, title)
		{
			Operation = operation;
			OutputIsProgress = outputIsProgress;
		}

		public IEnumerable<string> ErrorOutput { get; }
		public Func<IOutputHandler, Task<(bool Success, string[] Errors)>> Operation { get; }
		public (bool Success, string[] Errors) OperationResult { get; private set; }
		public bool OutputIsProgress { get; }
		public OutputFormTitle Title { get; }

		public void AddLine(string line)
		{
			this.InvokeIfRequired(() =>
				{
					if (!OutputIsProgress)
					{
						textBoxDetails.AppendText(line + Environment.NewLine);
						return;
					}

					if (string.IsNullOrWhiteSpace(line))
						return;

					line = line.StartsWith("\u001b[K") ? line.Substring(3) : line;
					string initialText = @"^([^\d]*)".GetCachedRegex().Match(line).Value;
					string similarLine = @"^(.*)\r{1}$"
						.GetCachedRegex(RegexOptions.Multiline)
						.Matches(textBoxDetails.Text)
						.Cast<Match>()
						.Select(match => match.Groups[1].Value)
						.LastOrDefault(x => x.StartsWith(initialText));

					if (similarLine != null)
					{
						textBoxDetails.Text = textBoxDetails.Text.Replace(similarLine, line);
					}
					else
					{
						textBoxDetails.AppendText(line + Environment.NewLine);
					}
				});
		}

		private void OutputForm_Load(object sender, EventArgs e)
		{
			Text = $"{Program.GIT_BRANCH_VIEW} - {Title}";

			if (ErrorOutput != null)
			{
				textBoxDetails.Text = string.Join(Environment.NewLine, ErrorOutput);
			}
			else if (Operation != null)
			{
				Task.Run(ExecuteOperation);
			}

			buttonClose.Focus();
		}

		private async void ExecuteOperation()
		{
			await this.InvokeIfRequired(async () =>
				{
					buttonClose.Enabled = false;
					OperationResult = await Operation.Invoke(this);
					buttonClose.Enabled = true;
				});
		}

		private void ButtonClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
