using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiRemoteDesktopClient.Controls
{
    public partial class RDThumbnail : UserControl
    {
        string _title = string.Empty;
        IntPtr _mdichild_parentHandle = IntPtr.Zero;
        Image _tnImage = null;

        public RDThumbnail()
        {
            InitializeComponent();
            panelDrawing.Dock = DockStyle.Fill;
            panelDrawing.BringToFront();
        }

        public string Title
        {
            set
            {
                this._title = value;
                lblTitle.Text = this._title;
            }
            get { return this._title; }
        }

        public Image RDImage
        {
            set
            {
                this._tnImage = value;
                panelDrawing.BackgroundImage = this._tnImage;
            }
            get { return this._tnImage; }
        }

        public IntPtr MDIChild_Handle
        {
            set
            {
                this._mdichild_parentHandle = value;
            }
            get { return this._mdichild_parentHandle; }
        }
    }
}
