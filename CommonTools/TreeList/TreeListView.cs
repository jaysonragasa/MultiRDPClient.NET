using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace CommonTools
{
	[Designer(typeof(TreeListViewDesigner))]
	public class TreeListView : Control, ISupportInitialize
	{
		public event TreeViewEventHandler AfterSelect;
		protected virtual void OnAfterSelect(Node node)
		{
			raiseAfterSelect(node);
		}
		protected virtual void raiseAfterSelect(Node node)
		{
            if (AfterSelect != null)
            {
                AfterSelect(this, new TreeViewEventArgs(null));
            }
		}

		public delegate void NotifyBeforeExpandHandler(Node node, bool isExpanding);
		public event NotifyBeforeExpandHandler NotifyBeforeExpand;
		public virtual void OnNotifyBeforeExpand(Node node, bool isExpanding)
		{
			raiseNotifyBeforeExpand(node, isExpanding);
		}
		protected virtual void raiseNotifyBeforeExpand(Node node, bool isExpanding)
		{
			if (NotifyBeforeExpand != null)
				NotifyBeforeExpand(node, isExpanding);
		}

		public delegate void NotifyAfterHandler(Node node, bool isExpanding);
		public event NotifyAfterHandler NotifyAfterExpand;
		public virtual void OnNotifyAfterExpand(Node node, bool isExpanded)
		{
			raiseNotifyAfterExpand(node, isExpanded);
		}
		protected virtual void raiseNotifyAfterExpand(Node node, bool isExpanded)
		{
			if (NotifyAfterExpand != null)
				NotifyAfterExpand(node, isExpanded);
		}

		TreeListViewNodes			m_nodes;
		TreeListColumnCollection	m_columns;
		TreeList.RowSetting			m_rowSetting;
		TreeList.ViewSetting		m_viewSetting;

		[Category("Columns")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeListColumnCollection Columns
		{
			get { return m_columns; }
		}

		[Category("Options")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeList.CollumnSetting ColumnsOptions
		{
			get { return m_columns.Options; }
		}

		[Category("Options")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeList.RowSetting RowOptions
		{
			get { return m_rowSetting; }
		}
		
		[Category("Options")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeList.ViewSetting ViewOptions
		{
			get { return m_viewSetting; }
		}

		[Category("Behavior")]
		[DefaultValue(typeof(bool), "True")]
		public bool MultiSelect
		{
			get { return m_multiSelect; }
			set { m_multiSelect = value; }
		}

		[DefaultValue(typeof(Color), "Window")]
		public new Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}
		public ImageList Images
		{
			get { return m_images; }
			set { m_images = value; }
		}

		//[Browsable(false)]
		public TreeListViewNodes Nodes
		{
			get { return m_nodes; }
		}
		public TreeListView()
		{
			this.DoubleBuffered = true;
			this.BackColor = SystemColors.Window;
			this.TabStop = true;

			m_rowPainter = new RowPainter();
			m_cellPainter = new CellPainter(this);

			m_nodes = new TreeListViewNodes(this);
			m_columns = new TreeListColumnCollection(this);
			m_rowSetting = new TreeList.RowSetting(this);
			m_viewSetting = new TreeList.ViewSetting(this);
			AddScroolBars();
		}
		public void RecalcLayout()
		{
			if (m_firstVisibleNode == null)
				m_firstVisibleNode = Nodes.FirstNode;
			if (Nodes.Count == 0)
				m_firstVisibleNode = null;

			UpdateScrollBars();
			int vscroll = VScrollValue();
			if (vscroll == 0)
				m_firstVisibleNode = Nodes.FirstNode;
			else
				m_firstVisibleNode = NodeCollection.GetNextNode(Nodes.FirstNode, vscroll);
			Invalidate();
		}
		void AddScroolBars()
		{
			// I was not able to get the wanted behavior by using ScrollableControl with AutoScroll enabled.
			// horizontal scrolling is ok to do it by pixels, but for vertical I want to maintain the headers
			// and only scroll the rows.
			// I was not able to manually overwrite the vscroll bar handling to get this behavior, instead I opted for
			// custom implementation of scrollbars

			// to get the 'filler' between hscroll and vscroll I dock scroll + filler in a panel
			m_hScroll = new HScrollBar();
			m_hScroll.Scroll += new ScrollEventHandler(OnHScroll);
			m_hScroll.Dock = DockStyle.Fill;

			m_vScroll = new VScrollBar();
			m_vScroll.Scroll += new ScrollEventHandler(OnVScroll);
			m_vScroll.Dock = DockStyle.Right;

			m_hScrollFiller = new Panel();
			m_hScrollFiller.BackColor = Color.Transparent;
			m_hScrollFiller.Size = new Size(m_vScroll.Width-1, m_hScroll.Height);
			m_hScrollFiller.Dock = DockStyle.Right;
			
			Controls.Add(m_vScroll);

			m_hScrollPanel = new Panel();
			m_hScrollPanel.Height = m_hScroll.Height;
			m_hScrollPanel.Dock = DockStyle.Bottom;
			m_hScrollPanel.Controls.Add(m_hScroll);
			m_hScrollPanel.Controls.Add(m_hScrollFiller);
			Controls.Add(m_hScrollPanel);
		}
		
		VScrollBar	m_vScroll;
		HScrollBar	m_hScroll;
		Panel		m_hScrollFiller;
		Panel		m_hScrollPanel;
		bool		m_multiSelect = true;
		Node		m_firstVisibleNode = null;
		ImageList	m_images = null;

		RowPainter m_rowPainter;
		CellPainter m_cellPainter;
		[Browsable(false)]
		public CellPainter CellPainter
		{
			get { return m_cellPainter; }
			set { m_cellPainter = value; }
		}

		TreeListColumn	m_resizingColumn;
		int m_resizingColumnScrollOffset;

		NodesSelection	m_nodesSelection = new NodesSelection();
		Node			m_focusedNode = null;
		[Browsable(false)]
		public NodesSelection NodesSelection
		{
			get { return m_nodesSelection; }
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Node FocusedNode
		{
			get { return m_focusedNode; }
			set
			{
				Node curNode = FocusedNode;
                
				if (object.ReferenceEquals(curNode, value))
					return;
                //if (MultiSelect == false)
                //    NodesSelection.Clear();

                NodesSelection.Clear();
                NodesSelection.Add(value);
				
				int oldrow = NodeCollection.GetVisibleNodeIndex(curNode);
				int newrow = NodeCollection.GetVisibleNodeIndex(value);
				
				m_focusedNode = value;
				OnAfterSelect(value);
				InvalidateRow(oldrow);
				InvalidateRow(newrow);
				EnsureVisible(m_focusedNode);
			}
		}
		public void EnsureVisible(Node node)
		{
			int screenvisible = MaxVisibleRows() - 1;
			int visibleIndex = NodeCollection.GetVisibleNodeIndex(node);
			if (visibleIndex < VScrollValue())
			{
				SetVScrollValue(visibleIndex);
			}
			if (visibleIndex > VScrollValue() + screenvisible)
			{
				SetVScrollValue(visibleIndex - screenvisible);
			}
		}
		public Node CalcHitNode(Point mousepoint)
		{
			int hitrow = CalcHitRow(mousepoint);
			if (hitrow < 0)
				return null;
			return NodeCollection.GetNextNode(m_firstVisibleNode, hitrow);
		}
		public Node GetHitNode()
		{
			return CalcHitNode(PointToClient(Control.MousePosition));
		}
		public CommonTools.HitInfo CalcColumnHit(Point mousepoint)
		{
			return Columns.CalcHitInfo(mousepoint, HScrollValue());
		}
		public bool HitTestScrollbar(Point mousepoint)
		{
			if (m_hScroll.Visible && mousepoint.Y >= ClientRectangle.Height - m_hScroll.Height)
				return true;
			return false;
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
			{
				Columns.RecalcVisibleColumsRect();
				UpdateScrollBars();
			}
		}
		protected virtual void BeforeShowContextMenu()
		{
		}
		protected void InvalidateRow(int absoluteRowIndex)
		{
			int visibleRowIndex = absoluteRowIndex - VScrollValue();
			Rectangle r = CalcRowRecangle(visibleRowIndex);
			if (r != Rectangle.Empty)
			{
				r.Inflate(1,1);
				Invalidate(r);
			}
		}

		void OnVScroll(object sender, ScrollEventArgs e)
		{
			int diff = e.NewValue - e.OldValue;
			//assumedScrollPos += diff;
			if (e.NewValue == 0)
			{
				m_firstVisibleNode = Nodes.FirstNode;
				diff = 0;
			}
			m_firstVisibleNode = NodeCollection.GetNextNode(m_firstVisibleNode, diff);
			Invalidate();
		}
		void OnHScroll(object sender, ScrollEventArgs e)
		{
			Invalidate();
		}
		void SetVScrollValue(int value)
		{
			if (value < 0)
				value = 0;
			int max = m_vScroll.Maximum - m_vScroll.LargeChange + 1;
			if (value > max)
				value = max;

			if ((value >= 0 && value <= max) && (value != m_vScroll.Value))
			{
				ScrollEventArgs e = new ScrollEventArgs(ScrollEventType.ThumbPosition, m_vScroll.Value, value, ScrollOrientation.VerticalScroll);
				// setting the scroll value does not cause a Scroll event
				m_vScroll.Value = value;
				// so we have to fake it
				OnVScroll(m_vScroll, e);
			}
		}
		int VScrollValue()
		{
			if (m_vScroll.Visible == false)
				return 0;
			return m_vScroll.Value;
		}
		int HScrollValue()
		{
			if (m_hScroll.Visible == false)
				return 0;
			return m_hScroll.Value;
		}
		void UpdateScrollBars()
		{
			if (ClientRectangle.Width < 0)
				return;
			int maxvisiblerows = MaxVisibleRows();
			int totalrows = Nodes.VisibleNodeCount;
			if (maxvisiblerows > totalrows)
			{
				m_vScroll.Visible = false;
				SetVScrollValue(0);
			}
			else
			{
				m_vScroll.Visible = true;
				m_vScroll.SmallChange = 1;
				m_vScroll.LargeChange = maxvisiblerows;
				m_vScroll.Minimum = 0;
				m_vScroll.Maximum = totalrows-1;

				int maxscrollvalue = m_vScroll.Maximum - m_vScroll.LargeChange;
				if (maxscrollvalue < m_vScroll.Value)
					SetVScrollValue(maxscrollvalue);
			}

			if (ClientRectangle.Width > MinWidth())
			{
				m_hScrollPanel.Visible = false;
				m_hScroll.Value = 0;
			}	
			else
			{
				m_hScroll.Minimum = 0;
				m_hScroll.Maximum = MinWidth();
				m_hScroll.SmallChange = 5;
				m_hScroll.LargeChange = ClientRectangle.Width;
				m_hScrollFiller.Visible = m_vScroll.Visible;
				m_hScrollPanel.Visible = true;
			}
		}
		int m_hotrow = -1;
		int CalcHitRow(Point mousepoint)
		{
			if (mousepoint.Y <= Columns.Options.HeaderHeight)
				return -1;
			return (mousepoint.Y - Columns.Options.HeaderHeight) / RowOptions.ItemHeight;
		}
		int VisibleRowToYPoint(int visibleRowIndex)
		{
			return Columns.Options.HeaderHeight + (visibleRowIndex * RowOptions.ItemHeight);
		}
		Rectangle CalcRowRecangle(int visibleRowIndex)
		{
			Rectangle r = ClientRectangle;
			r.Y = VisibleRowToYPoint(visibleRowIndex);
			if (r.Top < Columns.Options.HeaderHeight || r.Top > ClientRectangle.Height)
				return Rectangle.Empty;
			r.Height = RowOptions.ItemHeight;
			return r;
		}

		void MultiSelectAdd(Node clickedNode, Keys modifierKeys)
		{
			if (Control.ModifierKeys == Keys.None)
			{
				foreach (Node node in NodesSelection)
				{
					int newrow = NodeCollection.GetVisibleNodeIndex(node);
					InvalidateRow(newrow);
				}
				NodesSelection.Clear();
				NodesSelection.Add(clickedNode);
			}
			if (Control.ModifierKeys == Keys.Shift)
			{
				if (NodesSelection.Count == 0)
					NodesSelection.Add(clickedNode);
				else
				{
					int startrow = NodeCollection.GetVisibleNodeIndex(NodesSelection[0]);
					int currow = NodeCollection.GetVisibleNodeIndex(clickedNode);
					if (currow > startrow)
					{
						Node startingNode = NodesSelection[0];
						NodesSelection.Clear();
						foreach (Node node in NodeCollection.ForwardNodeIterator(startingNode, clickedNode, true))
							NodesSelection.Add(node);
						Invalidate();
					}
					if (currow < startrow)
					{
						Node startingNode = NodesSelection[0];
						NodesSelection.Clear();
						foreach (Node node in NodeCollection.ReverseNodeIterator(startingNode, clickedNode, true))
							NodesSelection.Add(node);
						Invalidate();
					}
				}
			}
			if (Control.ModifierKeys == Keys.Control)
			{
				if (NodesSelection.Contains(clickedNode))
					NodesSelection.Remove(clickedNode);
				else
					NodesSelection.Add(clickedNode);
			}
			InvalidateRow(NodeCollection.GetVisibleNodeIndex(clickedNode));
			FocusedNode = clickedNode;
		}
		internal event MouseEventHandler AfterResizingColumn;
		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Point mousePoint = new Point(e.X, e.Y);
				Node clickedNode = CalcHitNode(mousePoint);
				if (clickedNode != null && Columns.Count > 0)
				{
                    int clickedRow = CalcHitRow(mousePoint);
                    Rectangle glyphRect = GetPlusMinusRectangle(clickedNode, Columns.VisibleColumns[0], clickedRow);
                    if (clickedNode.HasChildren && glyphRect != Rectangle.Empty && glyphRect.Contains(mousePoint))
                        clickedNode.Expanded = !clickedNode.Expanded;

                    if (MultiSelect)
                    {
                        MultiSelectAdd(clickedNode, Control.ModifierKeys);
                    }
                    else
                        FocusedNode = clickedNode;
				}
			}
			base.OnMouseClick(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (m_resizingColumn != null)
			{
				int left = m_resizingColumn.CalculatedRect.Left - m_resizingColumnScrollOffset;
				int width = e.X - left;
				if (width < 10)
					width = 10;
				m_resizingColumn.Width = width;
				Columns.RecalcVisibleColumsRect(true);
				Invalidate();
				return;
			}

			TreeListColumn hotcol = null;
			CommonTools.HitInfo info = Columns.CalcHitInfo(new Point(e.X, e.Y), HScrollValue());
			if ((int)(info.HitType & HitInfo.eHitType.kColumnHeader) > 0)
				hotcol = info.Column;
			if ((int)(info.HitType & HitInfo.eHitType.kColumnHeaderResize) > 0)
				Cursor = Cursors.VSplit;
			else
				Cursor = Cursors.Arrow;

			SetHotColumn(hotcol, true);

			int vScrollOffset = VScrollValue();
			
			int newhotrow = -1;
			if (hotcol == null)
			{
				int row = (e.Y - Columns.Options.HeaderHeight) / RowOptions.ItemHeight;
				newhotrow = row + vScrollOffset;
			}
			if (newhotrow != m_hotrow)
			{
				InvalidateRow(m_hotrow);
				m_hotrow = newhotrow;
				InvalidateRow(m_hotrow);
			}
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			SetHotColumn(null, false);
		}
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			int value = m_vScroll.Value - (e.Delta * SystemInformation.MouseWheelScrollLines / 120);
			if (m_vScroll.Visible)
				SetVScrollValue(value);
			base.OnMouseWheel(e);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.Focus();
			if (e.Button == MouseButtons.Right)
			{
				Point mousePoint = new Point(e.X, e.Y);
				Node clickedNode = CalcHitNode(mousePoint);
				if (clickedNode != null)
				{
					// if multi select the selection is cleard if clicked node is not in selection
					if (MultiSelect)
					{
						if (NodesSelection.Contains(clickedNode) == false)
							MultiSelectAdd(clickedNode, Control.ModifierKeys);
					}

					FocusedNode = clickedNode;
					Invalidate();
				}
				BeforeShowContextMenu();
			}

			if (e.Button == MouseButtons.Left)
			{
				CommonTools.HitInfo info = Columns.CalcHitInfo(new Point(e.X, e.Y), HScrollValue());
				if ((int)(info.HitType & HitInfo.eHitType.kColumnHeaderResize) > 0)
				{
					m_resizingColumn = info.Column;
					m_resizingColumnScrollOffset = HScrollValue();
					return;
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (m_resizingColumn != null)
			{
				m_resizingColumn = null;
				Columns.RecalcVisibleColumsRect();
				UpdateScrollBars();
				Invalidate();
				if (AfterResizingColumn != null)
					AfterResizingColumn(this, e);
			}
			base.OnMouseUp(e);
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			Point mousePoint = new Point(e.X, e.Y);
			Node clickedNode = CalcHitNode(mousePoint);
			if (clickedNode != null && clickedNode.HasChildren)
				clickedNode.Expanded = !clickedNode.Expanded;
		}

		// Somewhere I read that it could be risky to do any handling in GetFocus / LostFocus. 
		// The reason is that it will throw exception incase you make a call which recreates the windows handle (e.g. 
		// change the border style. Instead one should always use OnEnter and OnLeave instead. That is why I'm using
		// OnEnter and OnLeave instead, even though I'm only doing Invalidate.
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			Invalidate();
		}
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			Invalidate();
		}

		void SetHotColumn(TreeListColumn col, bool ishot)
		{
			int scrolloffset = HScrollValue();
			if (col != m_hotColumn)
			{
				if (m_hotColumn != null)
				{
					m_hotColumn.ishot = false;
					Rectangle r = m_hotColumn.CalculatedRect;
					r.X -= scrolloffset;
					Invalidate(r);
				}
				m_hotColumn = col;
				if (m_hotColumn != null)
				{
					m_hotColumn.ishot = ishot;
					Rectangle r = m_hotColumn.CalculatedRect;
					r.X -= scrolloffset;
					Invalidate(r);
				}
			}
		}
		internal int RowHeaderWidth()
		{
			if (RowOptions.ShowHeader)
				return RowOptions.HeaderWidth;
			return 0;
		}
		int MinWidth()
		{
			return RowHeaderWidth() + Columns.ColumnsWidth;
		}
		int MaxVisibleRows(out int remainder)
		{
			remainder = 0;
			if (ClientRectangle.Height < 0)
				return 0;
			int height = ClientRectangle.Height - Columns.Options.HeaderHeight;
			//return (int) Math.Ceiling((double)(ClientRectangle.Height - Columns.HeaderHeight) / (double)Nodes.ItemHeight); 
			remainder = (ClientRectangle.Height - Columns.Options.HeaderHeight) % RowOptions.ItemHeight ;
			return (ClientRectangle.Height - Columns.Options.HeaderHeight) / RowOptions.ItemHeight ;
		}
		int MaxVisibleRows()
		{
			int unused;
			return MaxVisibleRows(out unused);
		}
		public void BeginUpdate()
		{
			m_nodes.BeginUpdate();
		}
		public void EndUpdate()
		{
			m_nodes.EndUpdate();
			RecalcLayout();
		}
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams p = base.CreateParams;
				p.Style &= ~(int)CommonTools.WinUtil.WS_BORDER;
				p.ExStyle &= ~(int)CommonTools.WinUtil.WS_EX_CLIENTEDGE;
				switch (ViewOptions.BorderStyle)
				{
					case BorderStyle.Fixed3D:
						p.ExStyle |= (int)CommonTools.WinUtil.WS_EX_CLIENTEDGE;
						break;
					case BorderStyle.FixedSingle:
						p.Style |= (int)CommonTools.WinUtil.WS_BORDER;
						break;
					default:
						break;
				}
				return p;
			}
		}
		
		TreeListColumn m_hotColumn = null;

		object GetDataDesignMode(Node node, TreeListColumn column)
		{
			string id = string.Empty;
			while (node != null)
			{
				id = node.Owner.GetNodeIndex(node).ToString() + ":" + id;
				node = node.Parent;
			}
			return "<temp>" + id;
		}
		protected virtual object GetData(Node node, TreeListColumn column)
		{
			if (node[column.Index] != null)
				return node[column.Index];
			return null;
		}
		public new Rectangle ClientRectangle
		{
			get
			{
				Rectangle r = base.ClientRectangle;
				if (m_vScroll.Visible)
					r.Width -= m_vScroll.Width+1;
				if (m_hScroll.Visible)
					r.Height -= m_hScroll.Height+1;
				return r;
			}
		}

		protected virtual CommonTools.TreeList.TextFormatting GetFormatting(CommonTools.Node node, CommonTools.TreeListColumn column)
		{
			return column.CellFormat;
		}
		protected virtual void PaintCell(Graphics dc, Rectangle cellRect, Node node, TreeListColumn column)
		{
			if (this.DesignMode)
				CellPainter.PaintCell(dc, cellRect, node, column, GetFormatting(node, column), GetDataDesignMode(node, column));
			else
				CellPainter.PaintCell(dc, cellRect, node, column, GetFormatting(node, column), GetData(node, column));
		}
		protected virtual void PaintImage(Graphics dc, Rectangle imageRect, Node node, Image image)
		{
			if (image != null)
				dc.DrawImageUnscaled(image, imageRect);
		}
		protected virtual void PaintNode(Graphics dc, Rectangle rowRect, Node node, TreeListColumn[] visibleColumns, int visibleRowIndex)
		{
			CellPainter.DrawSelectionBackground(dc, rowRect, node);
			foreach (TreeListColumn col in visibleColumns)
			{
				if (col.CalculatedRect.Right - HScrollValue() < RowHeaderWidth())
					continue;

				Rectangle cellRect = rowRect;
				cellRect.X = col.CalculatedRect.X - HScrollValue();
				cellRect.Width = col.CalculatedRect.Width;

				if (col.VisibleIndex == 0)
				{
					int lineindet = 10;
					// add left margin
					cellRect.X += Columns.Options.LeftMargin;
					cellRect.Width -= Columns.Options.LeftMargin;

					// add indent size
					int indentSize = GetIndentSize(node) + 5;
					cellRect.X += indentSize;
					cellRect.Width -= indentSize;
					if (ViewOptions.ShowLine)
						PaintLines(dc, cellRect, node);
					cellRect.X += lineindet;
					cellRect.Width -= lineindet;

					Rectangle glyphRect = GetPlusMinusRectangle(node, col, visibleRowIndex);
					if (glyphRect != Rectangle.Empty && ViewOptions.ShowPlusMinus)
						CellPainter.PaintCellPlusMinus(dc, glyphRect, node);

					if (!ViewOptions.ShowLine && !ViewOptions.ShowPlusMinus)
					{
						cellRect.X -= (lineindet + 5);
						cellRect.Width += (lineindet + 5);
					}

					Image icon = GetNodeBitmap(node);
					if (icon != null)
					{
						// center the image vertically
						glyphRect.Y = cellRect.Y + (cellRect.Height / 2) - (icon.Height / 2);
						glyphRect.X = cellRect.X;
						glyphRect.Width = icon.Width;
						glyphRect.Height = icon.Height;


						PaintImage(dc, glyphRect, node, icon);
						cellRect.X += (glyphRect.Width + 2);
						cellRect.Width -= (glyphRect.Width + 2);
					}
					PaintCell(dc, cellRect, node, col);
				}
				else
				{
					PaintCell(dc, cellRect, node, col);
				}
			}
		}
		protected virtual void PaintLines(Graphics dc, Rectangle cellRect, Node node)
		{
			Pen pen = new Pen(Color.Gray);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

			int halfPoint = cellRect.Top + (cellRect.Height / 2);
			// line should start from center at first root node 
			if (node.Parent == null && node.PrevSibling == null)
			{
				cellRect.Y += (cellRect.Height / 2);
				cellRect.Height -= (cellRect.Height / 2);
			}
			if (node.NextSibling != null)	// draw full height line
				dc.DrawLine(pen, cellRect.X, cellRect.Top, cellRect.X, cellRect.Bottom);
			else
				dc.DrawLine(pen, cellRect.X, cellRect.Top, cellRect.X, halfPoint);
			dc.DrawLine(pen, cellRect.X, halfPoint, cellRect.X + 8, halfPoint);

			// now draw the lines for the parents sibling
			Node parent = node.Parent;
			while (parent != null)
			{
				cellRect.X -= ViewOptions.Indent;
				if (parent.NextSibling != null)
					dc.DrawLine(pen, cellRect.X, cellRect.Top, cellRect.X, cellRect.Bottom);
				parent = parent.Parent;
			}

			pen.Dispose();
		}
		protected virtual int GetIndentSize(Node node)
		{
			int indent = 0;
			Node parent = node.Parent;
			while (parent != null)
			{
				indent += ViewOptions.Indent;
				parent = parent.Parent;
			}
			return indent;
		}
		protected virtual Rectangle GetPlusMinusRectangle(Node node, TreeListColumn firstColumn, int visibleRowIndex)
		{
			if (node.HasChildren == false)
				return Rectangle.Empty;
			int hScrollOffset = HScrollValue();
			if (firstColumn.CalculatedRect.Right - hScrollOffset < RowHeaderWidth())
				return Rectangle.Empty;
			System.Diagnostics.Debug.Assert(firstColumn.VisibleIndex == 0);

			Rectangle glyphRect = firstColumn.CalculatedRect;
			glyphRect.X -= hScrollOffset;
			glyphRect.X += GetIndentSize(node);
			glyphRect.X += Columns.Options.LeftMargin;
			glyphRect.Width = 10;
			glyphRect.Y = VisibleRowToYPoint(visibleRowIndex);
			glyphRect.Height = RowOptions.ItemHeight;
			return glyphRect;
		}
		protected virtual Image GetNodeBitmap(Node node)
		{
			if (Images != null && node.ImageId >= 0 && node.ImageId < Images.Images.Count)
				return Images.Images[node.ImageId];
			return null;
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
            
			int hScrollOffset = HScrollValue();
			int remainder = 0;
			int visiblerows = MaxVisibleRows(out remainder);
			if (remainder > 0)
				visiblerows++;
			
			bool drawColumnHeaders = true;
			// draw columns
			if (drawColumnHeaders)
			{
				Rectangle headerRect = e.ClipRectangle;
				Columns.Draw(e.Graphics, headerRect, hScrollOffset);
			}
			// draw vertical grid lines
			if (ViewOptions.ShowGridLines)
			{
				// visible row count
				int remainRows = Nodes.VisibleNodeCount - m_vScroll.Value;
				if (visiblerows > remainRows)
					visiblerows = remainRows;
	
				Rectangle fullRect = ClientRectangle;
				if (drawColumnHeaders)
					fullRect.Y += Columns.Options.HeaderHeight;
				fullRect.Height = visiblerows * RowOptions.ItemHeight;
				Columns.Painter.DrawVerticalGridLines(Columns, e.Graphics, fullRect, hScrollOffset);
			}

			int visibleRowIndex = 0;
			TreeListColumn[] visibleColumns = this.Columns.VisibleColumns;
			int columnsWidth = Columns.ColumnsWidth;
			foreach (Node node in NodeCollection.ForwardNodeIterator(m_firstVisibleNode, true))
			{
				Rectangle rowRect = CalcRowRecangle(visibleRowIndex);
				if (rowRect == Rectangle.Empty || rowRect.Bottom <= e.ClipRectangle.Top || rowRect.Top >= e.ClipRectangle.Bottom)
				{
					if (visibleRowIndex > visiblerows)
						break;
					visibleRowIndex++;
					continue;
				}
				rowRect.X = RowHeaderWidth() - hScrollOffset;
				rowRect.Width = columnsWidth;

				// draw horizontal grid line for current node
				if (ViewOptions.ShowGridLines)
				{
					Rectangle r = rowRect;
					r.X = RowHeaderWidth();
					r.Width = columnsWidth - hScrollOffset;
					m_rowPainter.DrawHorizontalGridLine(e.Graphics, r);
				}

				// draw the current node
				PaintNode(e.Graphics, rowRect, node, visibleColumns, visibleRowIndex);
				
				// drow row header for current node
				Rectangle headerRect = rowRect;
				headerRect.X = 0;
				headerRect.Width = RowHeaderWidth();

				int absoluteRowIndex = visibleRowIndex + VScrollValue();
				headerRect.Width = RowHeaderWidth();
				m_rowPainter.DrawHeader(e.Graphics, headerRect, absoluteRowIndex == m_hotrow);
				
				visibleRowIndex++;
			}
		}

		protected override bool IsInputKey(Keys keyData)
		{
			if ((int)(keyData & Keys.Shift) > 0)
				return true;
			switch (keyData)
			{
				case Keys.Left:
				case Keys.Right:
				case Keys.Down:
				case Keys.Up:
				case Keys.PageUp:
				case Keys.PageDown:
				case Keys.Home:
				case Keys.End:
					return true;
			}
			return false;
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			Node newnode = null;
			if (e.KeyCode == Keys.PageUp)
			{
				int remainder = 0;
				int diff = MaxVisibleRows(out remainder)-1;
				newnode = NodeCollection.GetNextNode(FocusedNode, -diff);
				if (newnode == null)
					newnode = Nodes.FirstVisibleNode();
			}
			if (e.KeyCode == Keys.PageDown)
			{
				int remainder = 0;
				int diff = MaxVisibleRows(out remainder)-1;
				newnode = NodeCollection.GetNextNode(FocusedNode, diff);
				if (newnode == null)
					newnode = Nodes.LastVisibleNode(true);
			}

			if (e.KeyCode == Keys.Down)
			{
				newnode = NodeCollection.GetNextNode(FocusedNode, 1);
			}
			if (e.KeyCode == Keys.Up)
			{
				newnode = NodeCollection.GetNextNode(FocusedNode, -1);
			}
			if (e.KeyCode == Keys.Home)
			{
				newnode = Nodes.FirstNode;
			}
			if (e.KeyCode == Keys.End)
			{
				newnode = Nodes.LastVisibleNode(true);
			}
			if (e.KeyCode == Keys.Left)
			{
				if (FocusedNode != null)
				{
					if (FocusedNode.Expanded)
					{
						FocusedNode.Collapse();
						EnsureVisible(FocusedNode);
						return;
					}
					if (FocusedNode.Parent != null)
					{
						FocusedNode = FocusedNode.Parent;
						EnsureVisible(FocusedNode);
					}
				}
			}
			if (e.KeyCode == Keys.Right)
			{
				if (FocusedNode != null)
				{
					if (FocusedNode.Expanded == false && FocusedNode.HasChildren)
					{
						FocusedNode.Expand();
						EnsureVisible(FocusedNode);
						return;
					}
					if (FocusedNode.Expanded == true && FocusedNode.HasChildren)
					{
						FocusedNode = FocusedNode.Nodes.FirstNode;
						EnsureVisible(FocusedNode);
					}
				}
			}
			if (newnode != null)
			{
				if (MultiSelect)
				{
					// tree behavior is 
					// keys none,		the selected node is added as the focused and selected node
					// keys control,	only focused node is moved, the selected nodes collection is not modified
					// keys shift,		selection from first selected node to current node is done
					if (Control.ModifierKeys == Keys.Control)
						FocusedNode = newnode;
					else
						MultiSelectAdd(newnode, Control.ModifierKeys);
				}
				else
					FocusedNode = newnode;
				EnsureVisible(FocusedNode);
			}
			base.OnKeyDown(e);
		}

		internal void internalUpdateStyles()
		{
			base.UpdateStyles();
		}

		#region ISupportInitialize Members

		public void BeginInit()
		{
			Columns.BeginInit();
		}
		public void EndInit()
		{
			Columns.EndInit();
		}

		#endregion
		internal new bool DesignMode
		{
			get { return base.DesignMode; }
		}

        public CommonTools.Node FindNode(string text, bool searchFields)
        {
            CommonTools.Node ret = null;

            NodeFind(text, this.Nodes, searchFields, out ret);

            return ret;
        }

        // TreeListView recursion
        private void NodeFind(string text, CommonTools.NodeCollection cNodes, bool searchFields, out CommonTools.Node outNode)
        {
            bool found = false;
            outNode = null;

            foreach (Node n in NodeCollection.ForwardNodeIterator(this.Nodes[0], false))
            {
                for (int f = 0; f <= this.Columns.Count - 1; f++)
                {
                    if (n[f] == null) { continue; }

                    if (text == n[f].ToString())
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    outNode = n;
                    break;
                }
            }
        }
	}

	public class TreeListViewNodes : NodeCollection
	{
		TreeListView m_tree;
		bool m_isUpdating = false;
		public void BeginUpdate()
		{
			m_isUpdating = true;
		}
		public void EndUpdate()
		{
			m_isUpdating = false;
		}
		public TreeListViewNodes(TreeListView owner) : base(null)
		{
			m_tree = owner;
		}
		protected override void UpdateNodeCount(int oldvalue, int newvalue)
		{
			base.UpdateNodeCount(oldvalue, newvalue);
			if (!m_isUpdating)
				m_tree.RecalcLayout();
		}
		public override void Clear()
		{
			base.Clear();
			m_tree.RecalcLayout();
		}
		public override void NodetifyBeforeExpand(Node nodeToExpand, bool expanding)
		{
			if (!m_tree.DesignMode)
				m_tree.OnNotifyBeforeExpand(nodeToExpand, expanding);
		}
		public override void NodetifyAfterExpand(Node nodeToExpand, bool expanded)
		{
			m_tree.OnNotifyAfterExpand(nodeToExpand, expanded);
		}
		protected override int GetFieldIndex(string fieldname)
		{
			TreeListColumn col = m_tree.Columns[fieldname];
			if (col != null)
				return col.Index;
			return -1;
		}
	}
}
