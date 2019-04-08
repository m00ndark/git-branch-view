using System.Drawing;

namespace GitBranchView.Model
{
	public class HighlightColor
	{
		public HighlightColor(Color color)
		{
			A = color.A;
			R = color.R;
			G = color.G;
			B = color.B;
		}

		public byte A { get; set; }
		public byte R { get; set; }
		public byte G { get; set; }
		public byte B { get; set; }

		public static implicit operator Color(HighlightColor color)
			=> Color.FromArgb(color.A, color.R, color.G, color.B);

		public static implicit operator HighlightColor(Color color)
			=> new HighlightColor(color);
	}
}
