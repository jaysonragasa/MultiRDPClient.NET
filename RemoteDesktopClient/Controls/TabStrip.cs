/*
 * TabStrip Control
 * http://www.codeproject.com/KB/tabs/tabstrips.aspx
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;
using VisualStyles = System.Windows.Forms.VisualStyles;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Windows.Forms.Design;

namespace Messir.Windows.Forms
{
    public class SelectedTabChangedEventArgs : EventArgs
    {
        public readonly TabStripButton SelectedTab;

        public SelectedTabChangedEventArgs(TabStripButton tab)
        {
            SelectedTab = tab;
        }

    }

    /// <summary>
    /// Represents a TabStrip control
    /// </summary>
    public class TabStrip : ToolStrip
    {
        private TabStripRenderer myRenderer = new TabStripRenderer();
        protected TabStripButton mySelTab;
        DesignerVerb insPage = null;

        public TabStrip()
            : base()
        {
            InitControl();
        }

        public TabStrip(params TabStripButton[] buttons)
            : base(buttons)
        {
            InitControl();
        }

        protected void InitControl()
        {
            base.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            base.Renderer = myRenderer;
            myRenderer.RenderMode = this.RenderStyle;
            insPage = new DesignerVerb("Insert tab page", new EventHandler(OnInsertPageClicked));
        }

        public override ISite Site
        {
            get
            {
                ISite site = base.Site;
                if (site != null && site.DesignMode)
                {
                    IContainer comp = site.Container;
                    if (comp != null)
                    {
                        IDesignerHost host = comp as IDesignerHost;
                        if (host != null)
                        {
                            IDesigner designer = host.GetDesigner(site.Component);
                            if (designer != null && !designer.Verbs.Contains(insPage))
                                designer.Verbs.Add(insPage);
                        }
                    }
                }
                return site;
            }
            set
            {
                base.Site = value;
            }
        }

        protected void OnInsertPageClicked(object sender, EventArgs e)
        {
            ISite site = base.Site;
            if (site != null && site.DesignMode)
            {
                IContainer container = site.Container;
                if (container != null)
                {
                    TabStripButton btn = new TabStripButton();
                    container.Add(btn);
                    btn.Text = btn.Name;
                }
            }
        }

        /// <summary>
        /// Gets custom renderer for TabStrip. Set operation has no effect
        /// </summary>
        public new ToolStripRenderer Renderer
        {
            get { return myRenderer; }
            set { base.Renderer = myRenderer; }
        }

        /// <summary>
        /// Gets or sets layout style for TabStrip control
        /// </summary>
        public new ToolStripLayoutStyle LayoutStyle
        {
            get { return base.LayoutStyle; }
            set
            {
                switch (value)
                {
                    case ToolStripLayoutStyle.StackWithOverflow:
                    case ToolStripLayoutStyle.HorizontalStackWithOverflow:
                    case ToolStripLayoutStyle.VerticalStackWithOverflow:
                        base.LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;
                        break;
                    case ToolStripLayoutStyle.Table:
                        base.LayoutStyle = ToolStripLayoutStyle.Table;
                        break;
                    case ToolStripLayoutStyle.Flow:
                        base.LayoutStyle = ToolStripLayoutStyle.Flow;
                        break;
                    default:
                        base.LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("Use RenderStyle instead")]
        [Browsable(false)]
        public new ToolStripRenderMode RenderMode
        {
            get { return base.RenderMode; }
            set { RenderStyle = value; }
        }

        /// <summary>
        /// Gets or sets render style for TabStrip, use it instead of 
        /// </summary>
        [Category("Appearance")]
        [Description("Gets or sets render style for TabStrip. You should use this property instead of RenderMode.")]
        public ToolStripRenderMode RenderStyle
        {
            get { return myRenderer.RenderMode; }
            set
            {
                myRenderer.RenderMode = value;
                this.Invalidate();
            }
        }

        protected override Padding DefaultPadding
        {
            get
            {
                return Padding.Empty;
            }
        }

        [Browsable(false)]
        public new Padding Padding
        {
            get { return DefaultPadding; }
            set { }
        }

        /// <summary>
        /// Gets or sets if control should use system visual styles for painting items
        /// </summary>
        [Category("Appearance")]
        [Description("Specifies if TabStrip should use system visual styles for painting items")]
        public bool UseVisualStyles
        {
            get { return myRenderer.UseVS; }
            set
            {
                myRenderer.UseVS = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets if TabButtons should be drawn flipped
        /// </summary>
        [Category("Appearance")]
        [Description("Specifies if TabButtons should be drawn flipped (for right- and bottom-aligned TabStrips)")]
        public bool FlipButtons
        {
            get { return myRenderer.Mirrored; }
            set
            {
                myRenderer.Mirrored = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets currently selected tab
        /// </summary>
        public TabStripButton SelectedTab
        {
            get { return mySelTab; }
            set
            {
                if (value == null)
                    return;
                if (mySelTab == value)
                    return;
                if (value.Owner != this)
                    throw new ArgumentException("Cannot select TabButtons that do not belong to this TabStrip");
                OnItemClicked(new ToolStripItemClickedEventArgs(value));
            }
        }

        public event EventHandler<SelectedTabChangedEventArgs> SelectedTabChanged;

        protected void OnTabSelected(TabStripButton tab)
        {
            this.Invalidate();
            if (SelectedTabChanged != null)
                SelectedTabChanged(this, new SelectedTabChangedEventArgs(tab));
        }

        protected override void OnItemAdded(ToolStripItemEventArgs e)
        {
            base.OnItemAdded(e);
            if (e.Item is TabStripButton)
                SelectedTab = (TabStripButton)e.Item;
        }

        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            TabStripButton clickedBtn = e.ClickedItem as TabStripButton;
            if (clickedBtn != null)
            {
                this.SuspendLayout();
                mySelTab = clickedBtn;
                this.ResumeLayout();
                OnTabSelected(clickedBtn);
            }
            base.OnItemClicked(e);
        }

    }

    /// <summary>
    /// Represents a renderer class for TabStrip control
    /// </summary>
    internal class TabStripRenderer : ToolStripRenderer
    {
        private const int selOffset = 2;

        private ToolStripRenderer currentRenderer = null;
        private ToolStripRenderMode renderMode = ToolStripRenderMode.Custom;
        private bool mirrored = false;
        private bool useVS = Application.RenderWithVisualStyles;

        /// <summary>
        /// Gets or sets render mode for this renderer
        /// </summary>
        public ToolStripRenderMode RenderMode
        {
            get { return renderMode; }
            set
            {
                renderMode = value;
                switch (renderMode)
                {
                    case ToolStripRenderMode.Professional:
                        currentRenderer = new ToolStripProfessionalRenderer();
                        break;
                    case ToolStripRenderMode.System:
                        currentRenderer = new ToolStripSystemRenderer();
                        break;
                    default:
                        currentRenderer = null;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets whether to mirror background
        /// </summary>
        /// <remarks>Use false for left and top positions, true for right and bottom</remarks>
        public bool Mirrored
        {
            get { return mirrored; }
            set { mirrored = value; }
        }

        /// <summary>
        /// Returns if visual styles should be applied for drawing
        /// </summary>
        public bool UseVS
        {
            get { return useVS; }
            set
            {
                if (value && !Application.RenderWithVisualStyles)
                    return;
                useVS = value;
            }
        }

        protected override void Initialize(ToolStrip ts)
        {
            base.Initialize(ts);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            Color c = SystemColors.AppWorkspace;
            if (UseVS)
            {
                VisualStyleRenderer rndr = new VisualStyleRenderer(VisualStyleElement.Tab.Pane.Normal);
                c = rndr.GetColor(ColorProperty.BorderColorHint);
            }

            using (Pen p = new Pen(c))
            using (Pen p2 = new Pen(e.BackColor))
            {
                Rectangle r = e.ToolStrip.Bounds;
                int x1 = (Mirrored) ? 0 : r.Width - 1 - e.ToolStrip.Padding.Horizontal;
                int y1 = (Mirrored) ? 0 : r.Height - 1;
                if (e.ToolStrip.Orientation == Orientation.Horizontal)
                    e.Graphics.DrawLine(p, 0, y1, r.Width, y1);
                else
                {
                    e.Graphics.DrawLine(p, x1, 0, x1, r.Height);
                    if (!Mirrored)
                        for (int i = x1 + 1; i < r.Width; i++)
                            e.Graphics.DrawLine(p2, i, 0, i, r.Height);
                }
                foreach (ToolStripItem x in e.ToolStrip.Items)
                {
                    if (x.IsOnOverflow) continue;
                    TabStripButton btn = x as TabStripButton;
                    if (btn == null) continue;
                    Rectangle rc = btn.Bounds;
                    int x2 = (Mirrored) ? rc.Left : rc.Right;
                    int y2 = (Mirrored) ? rc.Top : rc.Bottom - 1;
                    int addXY = (Mirrored) ? 0 : 1;
                    if (e.ToolStrip.Orientation == Orientation.Horizontal)
                    {
                        e.Graphics.DrawLine(p, rc.Left, y2, rc.Right, y2);
                        if (btn.Checked) e.Graphics.DrawLine(p2, rc.Left + 2 - addXY, y2, rc.Right - 2 - addXY, y2);
                    }
                    else
                    {
                        e.Graphics.DrawLine(p, x2, rc.Top, x2, rc.Bottom);
                        if (btn.Checked) e.Graphics.DrawLine(p2, x2, rc.Top + 2 - addXY, x2, rc.Bottom - 2 - addXY);
                    }
                }
            }
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawToolStripBackground(e);
            else
                base.OnRenderToolStripBackground(e);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            TabStrip tabs = e.ToolStrip as TabStrip;
            TabStripButton tab = e.Item as TabStripButton;
            if (tabs == null || tab == null)
            {
                if (currentRenderer != null)
                    currentRenderer.DrawButtonBackground(e);
                else
                    base.OnRenderButtonBackground(e);
                return;
            }

            bool selected = tab.Checked;
            bool hovered = tab.Selected;
            int top = 0;
            int left = 0;
            int width = tab.Bounds.Width - 1;
            int height = tab.Bounds.Height - 1;
            Rectangle drawBorder;


            if (UseVS)
            {
                if (tabs.Orientation == Orientation.Horizontal)
                {
                    if (!selected)
                    {
                        top = selOffset;
                        height -= (selOffset - 1);
                    }
                    else
                        top = 1;
                    drawBorder = new Rectangle(0, 0, width, height);
                }
                else
                {
                    if (!selected)
                    {
                        left = selOffset;
                        width -= (selOffset - 1);
                    }
                    else
                        left = 1;
                    drawBorder = new Rectangle(0, 0, height, width);
                }
                using (Bitmap b = new Bitmap(drawBorder.Width, drawBorder.Height))
                {
                    VisualStyleElement el = VisualStyleElement.Tab.TabItem.Normal;
                    if (selected)
                        el = VisualStyleElement.Tab.TabItem.Pressed;
                    if (hovered)
                        el = VisualStyleElement.Tab.TabItem.Hot;
                    if (!tab.Enabled)
                        el = VisualStyleElement.Tab.TabItem.Disabled;

                    if (!selected || hovered) drawBorder.Width++; else drawBorder.Height++;

                    using (Graphics gr = Graphics.FromImage(b))
                    {
                        VisualStyleRenderer rndr = new VisualStyleRenderer(el);
                        rndr.DrawBackground(gr, drawBorder);

                        if (tabs.Orientation == Orientation.Vertical)
                        {
                            if (Mirrored)
                                b.RotateFlip(RotateFlipType.Rotate270FlipXY);
                            else
                                b.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        }
                        else
                        {
                            if (Mirrored)
                                b.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        }
                        if (Mirrored)
                        {
                            left = tab.Bounds.Width - b.Width - left;
                            top = tab.Bounds.Height - b.Height - top;
                        }
                        g.DrawImage(b, left, top);
                    }
                }
            }
            else
            {
                if (tabs.Orientation == Orientation.Horizontal)
                {
                    if (!selected)
                    {
                        top = selOffset;
                        height -= (selOffset - 1);
                    }
                    else
                        top = 1;
                    if (Mirrored)
                    {
                        left = 1;
                        top = 0;
                    }
                    else
                        top++;
                    width--;
                }
                else
                {
                    if (!selected)
                    {
                        left = selOffset;
                        width--;
                    }
                    else
                        left = 1;
                    if (Mirrored)
                    {
                        left = 0;
                        top = 1;
                    }
                }
                height--;
                drawBorder = new Rectangle(left, top, width, height);

                using (GraphicsPath gp = new GraphicsPath())
                {
                    if (Mirrored && tabs.Orientation == Orientation.Horizontal)
                    {
                        gp.AddLine(drawBorder.Left, drawBorder.Top, drawBorder.Left, drawBorder.Bottom - 2);
                        gp.AddArc(drawBorder.Left, drawBorder.Bottom - 3, 2, 2, 90, 90);
                        gp.AddLine(drawBorder.Left + 2, drawBorder.Bottom, drawBorder.Right - 2, drawBorder.Bottom);
                        gp.AddArc(drawBorder.Right - 2, drawBorder.Bottom - 3, 2, 2, 0, 90);
                        gp.AddLine(drawBorder.Right, drawBorder.Bottom - 2, drawBorder.Right, drawBorder.Top);
                    }
                    else if (!Mirrored && tabs.Orientation == Orientation.Horizontal)
                    {
                        gp.AddLine(drawBorder.Left, drawBorder.Bottom, drawBorder.Left, drawBorder.Top + 2);
                        gp.AddArc(drawBorder.Left, drawBorder.Top + 1, 2, 2, 180, 90);
                        gp.AddLine(drawBorder.Left + 2, drawBorder.Top, drawBorder.Right - 2, drawBorder.Top);
                        gp.AddArc(drawBorder.Right - 2, drawBorder.Top + 1, 2, 2, 270, 90);
                        gp.AddLine(drawBorder.Right, drawBorder.Top + 2, drawBorder.Right, drawBorder.Bottom);
                    }
                    else if (Mirrored && tabs.Orientation == Orientation.Vertical)
                    {
                        gp.AddLine(drawBorder.Left, drawBorder.Top, drawBorder.Right - 2, drawBorder.Top);
                        gp.AddArc(drawBorder.Right - 2, drawBorder.Top + 1, 2, 2, 270, 90);
                        gp.AddLine(drawBorder.Right, drawBorder.Top + 2, drawBorder.Right, drawBorder.Bottom - 2);
                        gp.AddArc(drawBorder.Right - 2, drawBorder.Bottom - 3, 2, 2, 0, 90);
                        gp.AddLine(drawBorder.Right - 2, drawBorder.Bottom, drawBorder.Left, drawBorder.Bottom);
                    }
                    else
                    {
                        gp.AddLine(drawBorder.Right, drawBorder.Top, drawBorder.Left + 2, drawBorder.Top);
                        gp.AddArc(drawBorder.Left, drawBorder.Top + 1, 2, 2, 180, 90);
                        gp.AddLine(drawBorder.Left, drawBorder.Top + 2, drawBorder.Left, drawBorder.Bottom - 2);
                        gp.AddArc(drawBorder.Left, drawBorder.Bottom - 3, 2, 2, 90, 90);
                        gp.AddLine(drawBorder.Left + 2, drawBorder.Bottom, drawBorder.Right, drawBorder.Bottom);
                    }

                    if (selected || hovered)
                    {
                        Color fill = (hovered) ? Color.WhiteSmoke : Color.White;
                        if (renderMode == ToolStripRenderMode.Professional)
                        {
                            fill = (hovered) ? ProfessionalColors.ButtonCheckedGradientBegin : ProfessionalColors.ButtonCheckedGradientEnd;
                            using (LinearGradientBrush br = new LinearGradientBrush(tab.ContentRectangle, fill, ProfessionalColors.ButtonCheckedGradientMiddle, LinearGradientMode.Vertical))
                                g.FillPath(br, gp);
                        }
                        else
                            using (SolidBrush br = new SolidBrush(fill))
                                g.FillPath(br, gp);
                    }
                    using (Pen p = new Pen((selected) ? ControlPaint.Dark(SystemColors.AppWorkspace) : SystemColors.AppWorkspace))
                        g.DrawPath(p, gp);
                }
            }

        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            Rectangle rc = e.ImageRectangle;
            TabStripButton btn = e.Item as TabStripButton;
            if (btn != null)
            {
                int delta = ((Mirrored) ? -1 : 1) * ((btn.Checked) ? 1 : selOffset);
                if (e.ToolStrip.Orientation == Orientation.Horizontal)
                    rc.Offset((Mirrored) ? 2 : 1, delta + ((Mirrored) ? 1 : 0));
                else
                    rc.Offset(delta + 2, 0);
            }
            ToolStripItemImageRenderEventArgs x =
                new ToolStripItemImageRenderEventArgs(e.Graphics, e.Item, e.Image, rc);
            if (currentRenderer != null)
                currentRenderer.DrawItemImage(x);
            else
                base.OnRenderItemImage(x);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            Rectangle rc = e.TextRectangle;
            TabStripButton btn = e.Item as TabStripButton;
            Color c = e.TextColor;
            Font f = e.TextFont;
            if (btn != null)
            {
                int delta = ((Mirrored) ? -1 : 1) * ((btn.Checked) ? 1 : selOffset);
                if (e.ToolStrip.Orientation == Orientation.Horizontal)
                    rc.Offset((Mirrored) ? 2 : 1, delta + ((Mirrored) ? 1 : -1));
                else
                    rc.Offset(delta + 2, 0);
                if (btn.Selected)
                    c = btn.HotTextColor;
                else if (btn.Checked)
                    c = btn.SelectedTextColor;
                if (btn.Checked)
                    f = btn.SelectedFont;
            }
            ToolStripItemTextRenderEventArgs x =
                new ToolStripItemTextRenderEventArgs(e.Graphics, e.Item, e.Text, rc, c, f, e.TextFormat);
            x.TextDirection = e.TextDirection;
            if (currentRenderer != null)
                currentRenderer.DrawItemText(x);
            else
                base.OnRenderItemText(x);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawArrow(e);
            else
                base.OnRenderArrow(e);
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawDropDownButtonBackground(e);
            else
                base.OnRenderDropDownButtonBackground(e);
        }

        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawGrip(e);
            else
                base.OnRenderGrip(e);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawImageMargin(e);
            else
                base.OnRenderImageMargin(e);
        }

        protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawItemBackground(e);
            else
                base.OnRenderItemBackground(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawItemCheck(e);
            else
                base.OnRenderItemCheck(e);
        }

        protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawLabelBackground(e);
            else
                base.OnRenderLabelBackground(e);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawMenuItemBackground(e);
            else
                base.OnRenderMenuItemBackground(e);
        }

        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawOverflowButtonBackground(e);
            else
                base.OnRenderOverflowButtonBackground(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawSeparator(e);
            else
                base.OnRenderSeparator(e);
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawSplitButton(e);
            else
                base.OnRenderSplitButtonBackground(e);
        }

        protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawStatusStripSizingGrip(e);
            else
                base.OnRenderStatusStripSizingGrip(e);
        }

        protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawToolStripContentPanelBackground(e);
            else
                base.OnRenderToolStripContentPanelBackground(e);
        }

        protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawToolStripPanelBackground(e);
            else
                base.OnRenderToolStripPanelBackground(e);
        }

        protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
        {
            if (currentRenderer != null)
                currentRenderer.DrawToolStripStatusLabelBackground(e);
            else
                base.OnRenderToolStripStatusLabelBackground(e);
        }
    }

    /// <summary>
    /// Represents a TabButton for TabStrip control
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class TabStripButton : ToolStripButton
    {
        public TabStripButton() : base() { InitButton(); }
        public TabStripButton(Image image) : base(image) { InitButton(); }
        public TabStripButton(string text) : base(text) { InitButton(); }
        public TabStripButton(string text, Image image) : base(text, image) { InitButton(); }
        public TabStripButton(string Text, Image Image, EventHandler Handler) : base(Text, Image, Handler) { InitButton(); }
        public TabStripButton(string Text, Image Image, EventHandler Handler, string name) : base(Text, Image, Handler, name) { InitButton(); }

        private void InitButton()
        {
            m_SelectedFont = this.Font;
        }

        public override Size GetPreferredSize(Size constrainingSize)
        {
            Size sz = base.GetPreferredSize(constrainingSize);
            if (this.Owner != null && this.Owner.Orientation == Orientation.Vertical)
            {
                sz.Width += 3;
                sz.Height += 10;
            }
            return sz;
        }

        protected override Padding DefaultMargin
        {
            get
            {
                return new Padding(0);
            }
        }

        [Browsable(false)]
        public new Padding Margin
        {
            get { return base.Margin; }
            set { }
        }

        [Browsable(false)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { }
        }

        private Color m_HotTextColor = Control.DefaultForeColor;

        [Category("Appearance")]
        [Description("Text color when TabButton is highlighted")]
        public Color HotTextColor
        {
            get { return m_HotTextColor; }
            set { m_HotTextColor = value; }
        }

        private Color m_SelectedTextColor = Control.DefaultForeColor;

        [Category("Appearance")]
        [Description("Text color when TabButton is selected")]
        public Color SelectedTextColor
        {
            get { return m_SelectedTextColor; }
            set { m_SelectedTextColor = value; }
        }

        private Font m_SelectedFont;

        [Category("Appearance")]
        [Description("Font when TabButton is selected")]
        public Font SelectedFont
        {
            get { return (m_SelectedFont == null) ? this.Font : m_SelectedFont; }
            set { m_SelectedFont = value; }
        }

        [Browsable(false)]
        [DefaultValue(false)]
        public new bool Checked
        {
            get { return IsSelected; }
            set { }
        }

        /// <summary>
        /// Gets or sets if this TabButton is currently selected
        /// </summary>
        [Browsable(false)]
        public bool IsSelected
        {
            get
            {
                TabStrip owner = Owner as TabStrip;
                if (owner != null)
                    return (this == owner.SelectedTab);
                return false;
            }
            set
            {
                if (value == false) return;
                TabStrip owner = Owner as TabStrip;
                if (owner == null) return;
                owner.SelectedTab = this;
            }
        }

        protected override void OnOwnerChanged(EventArgs e)
        {
            if (Owner != null && !(Owner is TabStrip))
                throw new Exception("Cannot add TabStripButton to " + Owner.GetType().Name);
            base.OnOwnerChanged(e);
        }

    }

}
