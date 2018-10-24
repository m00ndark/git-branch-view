using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBranchView
{
	public enum FilterEntryAction
	{
		Delete,
		MoveUp,
		MoveDown
	}

	public class FilterEntryEventArgs : EventArgs
	{
		public FilterEntryEventArgs(FilterEntryAction action)
		{
			Action = action;
		}

		public FilterEntryAction Action { get; }
	}
}
