using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crownwood.Magic.Forms;

namespace MultiRemoteDesktopClient
{
    public partial class PopupMDIContainer : Form
    {
        public PopupMDIContainer()
        {
            InitializeComponent();
            InitializeControls();
            InitializeControlEvents();
        }

        public void InitializeControls()
        {
        }

        public void InitializeControlEvents()
        {
            this.tabMDIChild.SelectionChanged += new EventHandler(tabMDIChild_SelectionChanged);
        }

        void tabMDIChild_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        public void PopIn(ref Panel panel, Form parentForm, string title)
        {
            Form f = new Form();
            f.MdiParent = this;
            f.Size = new Size(640, 480);
            f.Show();
            f.Text = parentForm.Text;
            //panel.Parent = f;

            CreateTab(ref panel, title);

            parentForm.WindowState = FormWindowState.Minimized;
        }

        public void PopOut(ref Panel panel, RdpClientWindow parentForm)
        {
            //panel.Dock = DockStyle.Fill;
            //panel.Parent = parentForm;

            IntPtr handle = panel.Handle;
            DestroyTab(handle, parentForm);

            parentForm.WindowState = FormWindowState.Normal;
        }

        void CreateTab(ref Panel panel, string text)
        {
            Crownwood.Magic.Controls.TabPage newPage = new Crownwood.Magic.Controls.TabPage(text, panel);
            newPage.Tag = panel.Handle;


            tabMDIChild.TabPages.Add(newPage);
        }

        void DestroyTab(IntPtr PanelHandle, RdpClientWindow parentForm)
        {
            Crownwood.Magic.Controls.TabPage thisPage = null;
            // destroy tag
            foreach (Crownwood.Magic.Controls.TabPage tabpage in tabMDIChild.TabPages)
            {
                if ((IntPtr)tabpage.Tag == PanelHandle)
                {
                    Panel p = (Panel)tabpage.Control;
                    p.Visible = true;
                    p.Dock = DockStyle.Fill;
                    p.Parent = parentForm;

                    Application.DoEvents();

                    thisPage = tabpage;

                    break;
                }
            }

            if (thisPage != null)
            {
                tabMDIChild.TabPages.Remove(thisPage);
            }

            // check if this form can be closed
            if (tabMDIChild.TabPages.Count == 0)
            {
                //this.Close();
            }
        }
    }
}
