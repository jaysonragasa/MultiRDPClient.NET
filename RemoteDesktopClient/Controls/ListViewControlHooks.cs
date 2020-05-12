#define emptylist_indicator
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiRemoteDesktopClient.Controls
{
    public class ListViewEx : ListView
    {
        Object[] _objCtrl_EmptyListItem;
        Object[] _objCtrl_ItemSelection;

        public void AddControlForEmptyListItem(Object[] ctrl)
        {
            this._objCtrl_EmptyListItem = ctrl;
            EnableControls(false, this._objCtrl_EmptyListItem);
        }

        public void AddControlForItemSelection(Object[] ctrl)
        {
            this._objCtrl_ItemSelection = ctrl;
        }

        private void EnableControls(bool enable, Object[] ctrl)
        {
            if (ctrl != null)
            {
                foreach (Object o in ctrl)
                {
                    Type t = o.GetType();
                    System.Reflection.PropertyInfo pi = t.GetProperty("Enabled");

                    if (pi.CanWrite)
                    {
                        pi.SetValue(o, enable, null);
                    }
                }
            }
        } 

        // some of the codes here are took in
#if emptylist_indicator
        #region Indicating an empty ListView in C# - http://www.codeproject.com/KB/list/extlistviewarticle.aspx
        // and I modified to support some of my future code feature

        public ListViewEx()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
        }
 
        private bool _gridLines = false;

        /// 
        /// Handle GridLines on our own because base.GridLines has to be switched on
        /// and off depending on the amount of items in the ListView.
        /// 
        [DefaultValue(false)]
        public new bool GridLines
        {
            get { return _gridLines; }
            set
            {
                if (_gridLines != value)
                {
                    _gridLines = value;
                    Invalidate();
                }
            }
        }
         
        private string _noItemsMessage = "There are no items to show in this view";

        /// 
        /// To be able to localize the message it must not be hardcoded
        /// 
        [DefaultValue("There are no items to show in this view")]
        public string NoItemsMessage
        {
	        get {  return _noItemsMessage; }
	        set { _noItemsMessage = value; Invalidate(); }    
        }
         
        const int WM_ERASEBKGND = 0x14;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 20)
            {
                #region Handle drawing of "no items" message
                if (Items.Count == 0 && Columns.Count > 0)
                {
                    if (this.GridLines)
                    {
                        base.GridLines = false;
                    }

                    int w = 0;
                    foreach (ColumnHeader h in this.Columns)
                        w += h.Width;

                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    Rectangle rc = new Rectangle(0, (int)(this.Font.Height * 1.5), w, this.Height);

                    using (Graphics g = this.CreateGraphics())
                    {
                        g.FillRectangle(SystemBrushes.Window, 0, 0, this.Width, this.Height);
                        g.DrawString(NoItemsMessage, this.Font, SystemBrushes.ControlText, rc, sf);
                    }

                    EnableControls(false, this._objCtrl_EmptyListItem);
                }
                else
                {
                    //EnableControls(true, this._objCtrl_EmptyListItem);

                    base.GridLines = this.GridLines;
                }
                #endregion
            }
        }

        #endregion
#endif
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                ListViewHitTestInfo lvhi = HitTest(e.X, e.Y);

                if (lvhi.Item == null)
                {
                    EnableControls(false, this._objCtrl_ItemSelection);
                }
                else
                {
                    EnableControls(true, this._objCtrl_ItemSelection);
                }
            }

            base.OnMouseDown(e);
        }
    }
}