using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;

namespace CommonTools
{
	public class VisualStyleItemBackground // can't find system provided visual style for this.
	{
		[StructLayout(LayoutKind.Sequential)]
		public class RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
			public RECT()
			{
			}

			public RECT(Rectangle r)
			{
				this.left = r.X;
				this.top = r.Y;
				this.right = r.Right;
				this.bottom = r.Bottom;
			}
			/*
			public COMRECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}

			public static COMRECT FromXYWH(int x, int y, int width, int height)
			{
				return new COMRECT(x, y, x + width, y + height);
			}

			public override string ToString()
			{
				return string.Concat(new object[] { "Left = ", this.left, " Top ", this.top, " Right = ", this.right, " Bottom = ", this.bottom });
			}
			 * */
		}

		[DllImport("uxtheme.dll", CharSet=CharSet.Auto)]
		public static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int partId, int stateId, [In] RECT pRect, [In] RECT pClipRect);
 

		[DllImport("uxtheme.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr OpenThemeData(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszClassList);
 
		[DllImport("uxtheme.dll", CharSet=CharSet.Auto)]
		public static extern int CloseThemeData(IntPtr hTheme);

		//http://www.ookii.org/misc/vsstyle.h
		//http://msdn2.microsoft.com/en-us/library/bb773210(VS.85).aspx
		enum ITEMSTATES
		{
			LBPSI_HOT = 1,
			LBPSI_HOTSELECTED = 2,
			LBPSI_SELECTED = 3,
			LBPSI_SELECTEDNOTFOCUS = 4,
		};

		enum LISTBOXPARTS
		{
			LBCP_BORDER_HSCROLL = 1,
			LBCP_BORDER_HVSCROLL = 2,
			LBCP_BORDER_NOSCROLL = 3,
			LBCP_BORDER_VSCROLL = 4,
			LBCP_ITEM = 5,
		};



		public enum Style
		{
			Normal,
			Inactive,	// when not focused
		}
		Style m_style;
		public VisualStyleItemBackground(Style style)
		{
			m_style = style;
		}
		public void DrawBackground(Control owner, Graphics dc, Rectangle r)
		{
			IntPtr themeHandle = OpenThemeData(owner.Handle, "Explorer");
			if (themeHandle != IntPtr.Zero)
			{
				DrawThemeBackground(themeHandle, dc.GetHdc(), (int)LISTBOXPARTS.LBCP_ITEM, (int)ITEMSTATES.LBPSI_SELECTED, new RECT(r), new RECT(r));
				dc.ReleaseHdc();
				CloseThemeData(themeHandle);
				return;
			}





			Color selectedColor = Color.FromArgb(150, 220, 250);
			if (m_style == Style.Inactive)
				selectedColor = Color.FromArgb(217, 217, 217);

			Pen pen = new Pen(selectedColor);
			GraphicsPath path = new GraphicsPath();
			path.AddLine(r.Left + 2, r.Top, r.Right - 2, r.Top);
			path.AddLine(r.Right, r.Top + 2, r.Right, r.Bottom - 2);
			path.AddLine(r.Right - 2, r.Bottom, r.Left + 2, r.Bottom);
			path.AddLine(r.Left, r.Bottom - 2, r.Left, r.Top + 2);
			path.CloseFigure();
			dc.DrawPath(pen, path);

			r.Inflate(-1, -1);
			LinearGradientBrush brush = new LinearGradientBrush(r, Color.White, Color.FromArgb(90, selectedColor), 90);
			dc.FillRectangle(brush, r);
			// for some reason in some cases the 'white' end of the gradient brush is drawn with the starting color
			// therefore this redraw of the 'top' line of the rectangle
			dc.DrawLine(Pens.White, r.Left + 1, r.Top, r.Right - 1, r.Top);

			pen.Dispose();
			brush.Dispose();
			path.Dispose();
		}
	}

	class TreeListPainter
	{
		static Pen m_gridLinePen = SystemPens.Control;
		public static Pen GridLinePen
		{
			get { return m_gridLinePen; }
		}
	}
	public class CellPainter
	{
		protected TreeListView m_owner;
		public CellPainter(TreeListView owner)
		{
			m_owner = owner;
		}
		public virtual void DrawSelectionBackground(Graphics dc, Rectangle nodeRect, Node node)
		{
			if (m_owner.NodesSelection.Contains(node) || m_owner.FocusedNode == node)
			{
				if (!Application.RenderWithVisualStyles)
				{
					// have to fill the solid background only before the node is painted
					dc.FillRectangle(SystemBrushes.FromSystemColor(SystemColors.Highlight), nodeRect);
				}
				else
				{
					// have to draw the transparent background after the node is painted
					VisualStyleItemBackground.Style style = VisualStyleItemBackground.Style.Normal;
					if (m_owner.Focused == false)
						style = VisualStyleItemBackground.Style.Inactive;
					VisualStyleItemBackground rendere = new VisualStyleItemBackground(style);
					rendere.DrawBackground(m_owner, dc, nodeRect);
				}
			}
			if (m_owner.Focused && (m_owner.FocusedNode == node))
			{
				nodeRect.Height += 1;
				nodeRect.Inflate(-1,-1);
				ControlPaint.DrawFocusRectangle(dc, nodeRect);
			}
		}
		public virtual void PaintCell(Graphics dc, 
			Rectangle cellRect, 
			Node node, 
			TreeListColumn column, 
			TreeList.TextFormatting format, 
			object data)
		{
			if (format.BackColor != Color.Transparent)
			{
				Rectangle r = cellRect;
				r.X = column.CalculatedRect.X;
				r.Width = column.CalculatedRect.Width;
				SolidBrush brush = new SolidBrush(format.BackColor);
				dc.FillRectangle(brush, r);
				brush.Dispose();
			}
			if (data != null)
			{
				cellRect = CommonTools.Util.AdjustRectangle(cellRect, format.Padding);
				//dc.DrawRectangle(Pens.Black, cellRect);

				Color color = format.ForeColor;
                if (m_owner.FocusedNode == node && Application.RenderWithVisualStyles == false)
                {
                    color = SystemColors.HighlightText;
                }
				TextFormatFlags flags= TextFormatFlags.EndEllipsis | format.GetFormattingFlags();
				TextRenderer.DrawText(dc, data.ToString(), m_owner.Font, cellRect, color, flags);
			}
		}
		public virtual void PaintCellPlusMinus(Graphics dc, Rectangle glyphRect, Node node)
		{
			if (!Application.RenderWithVisualStyles)
			{
				return;
			}

			VisualStyleElement element = VisualStyleElement.TreeView.Glyph.Closed;
			if (node.Expanded)
				element = VisualStyleElement.TreeView.Glyph.Opened;

			if (VisualStyleRenderer.IsElementDefined(element))
			{
				VisualStyleRenderer renderer = new VisualStyleRenderer(element);
				renderer.DrawBackground(dc, glyphRect);
			}
		}
	}
	public class ColumnHeaderPainter
	{
		public virtual void DrawHeaderFiller(Graphics dc, Rectangle r)
		{
			if (!Application.RenderWithVisualStyles)
			{
				ControlPaint.DrawButton(dc, r, ButtonState.Flat);
				return;
			}
			VisualStyleElement element = VisualStyleElement.Header.Item.Normal;
			if (VisualStyleRenderer.IsElementDefined(element))
			{
				VisualStyleRenderer renderer = new VisualStyleRenderer(element);
				renderer.DrawBackground(dc, r);
			}
		}
		public virtual void DrawHeader(Graphics dc, Rectangle cellRect, TreeListColumn column, TreeList.TextFormatting format, bool isHot)
		{
			if (!Application.RenderWithVisualStyles)
			{
				ControlPaint.DrawButton(dc, cellRect, ButtonState.Flat);
				return;
			}
			VisualStyleElement element = VisualStyleElement.Header.Item.Normal;
			if (isHot)
				element = VisualStyleElement.Header.Item.Hot;
			if (VisualStyleRenderer.IsElementDefined(element))
			{
				VisualStyleRenderer renderer = new VisualStyleRenderer(element);
				renderer.DrawBackground(dc, cellRect);

				if (format.BackColor != Color.Transparent)
				{
					SolidBrush brush = new SolidBrush(format.BackColor);
					dc.FillRectangle(brush, cellRect);
					brush.Dispose();
				}
				cellRect = CommonTools.Util.AdjustRectangle(cellRect, format.Padding);
				//dc.DrawRectangle(Pens.Black, cellRect);

				Color color = format.ForeColor;
				TextFormatFlags flags= TextFormatFlags.EndEllipsis | format.GetFormattingFlags();
				TextRenderer.DrawText(dc, column.Caption, column.Font, cellRect, color, flags);
			}
		}
		public virtual void DrawVerticalGridLines(TreeListColumnCollection columns, Graphics dc, Rectangle r, int hScrollOffset)
		{
			foreach (TreeListColumn col in columns.VisibleColumns)
			{
				int rightPos = col.CalculatedRect.Right - hScrollOffset;
				if (rightPos < 0)
					continue;
				dc.DrawLine(TreeListPainter.GridLinePen, rightPos, r.Top, rightPos, r.Bottom);
			}
		}
	}
	public class RowPainter
	{
		public void DrawHeader(Graphics dc, Rectangle r, bool isHot)
		{
			if (!Application.RenderWithVisualStyles)
			{
				ControlPaint.DrawButton(dc, r, ButtonState.Flat);
				return;
			}

			VisualStyleElement element = VisualStyleElement.Header.Item.Normal;
			if (isHot)
				element = VisualStyleElement.Header.Item.Hot;
			if (VisualStyleRenderer.IsElementDefined(element))
			{
				VisualStyleRenderer renderer = new VisualStyleRenderer(element);
				renderer.DrawBackground(dc, r);
			}
		}
		public void DrawHorizontalGridLine(Graphics dc, Rectangle r)
		{
			dc.DrawLine(TreeListPainter.GridLinePen, r.Left, r.Bottom, r.Right, r.Bottom);
		}
	}
}
