using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices; 

namespace CommonTools
{
	class Line : Control
	{
		AnchorStyles m_linePositions = AnchorStyles.Bottom;
		public AnchorStyles LinePositions
		{
			get { return m_linePositions; }
			set
			{
				m_linePositions = value;
				Invalidate();
			}
		}
		public Line()
		{
			TabStop = false;
		    SetStyle(ControlStyles.Selectable, false);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		    SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		}
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground(pevent);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint(e);
			Rectangle r = ClientRectangle;
			r.Inflate(-2,-2);
			Pen p = new Pen(ForeColor, 1);
			if ((int)(LinePositions & AnchorStyles.Left) > 0)
			{
				e.Graphics.DrawLine(p, r.Left-1, r.Top, r.Left-1, r.Bottom-1);
				e.Graphics.DrawLine(Pens.WhiteSmoke, r.Left, r.Top, r.Left, r.Bottom-1);
			}
			if ((int)(LinePositions & AnchorStyles.Right) > 0)
			{
				e.Graphics.DrawLine(p, r.Right, r.Top, r.Right, r.Bottom-1);
				e.Graphics.DrawLine(Pens.WhiteSmoke, r.Right+1, r.Top, r.Right+1, r.Bottom-1);
			}
			if ((int)(LinePositions & AnchorStyles.Top) > 0)
			{
				e.Graphics.DrawLine(p, r.Left, r.Top-1, r.Right-1, r.Top-1);
				e.Graphics.DrawLine(Pens.WhiteSmoke, r.Left, r.Top, r.Right-1, r.Top);
			}
			if ((int)(LinePositions & AnchorStyles.Bottom) > 0)
			{
				e.Graphics.DrawLine(p, r.Left, r.Bottom, r.Right-1, r.Bottom);
				e.Graphics.DrawLine(Pens.WhiteSmoke, r.Left, r.Bottom+1, r.Right-1, r.Bottom+1);
			}
			p.Dispose();
		}
	}
}
