using System;

namespace GitBranchView
{
	public class ToolStripItemTag
	{
		public ToolStripItemTag(EventHandler clickEventHandler, object value = null)
		{
			ClickEventHandler = clickEventHandler;
			Value = value;
		}

		public EventHandler ClickEventHandler { get; set; }
		public object Value { get; set; }
	}
}
