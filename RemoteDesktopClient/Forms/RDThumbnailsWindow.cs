using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiRemoteDesktopClient
{
    public partial class RDThumbnailsWindow : Form
    {
        public RDThumbnailsWindow()
        {
            InitializeComponent();
            InitializeControls();
            InitializeControlEvents();
        }

        void InitializeControls()
        {
            timerMDIChildMonitor.Enabled = true;
        }

        void InitializeControlEvents()
        {
            timerMDIChildMonitor.Tick += new EventHandler(timerMDIChildMonitor_Tick);
        }

        void timerMDIChildMonitor_Tick(object sender, EventArgs e)
        {
            if (GlobalHelper.MDIChildrens == null) { return; }
            System.Diagnostics.Debug.WriteLine(GlobalHelper.MDIChildrens.Count().ToString());
            foreach (RdpClientWindow f in GlobalHelper.MDIChildrens)
            {
                if (flowLayoutPanel1.Controls.Count == 0)
                {
                    CreateThumbnail(f);
                }
                else
                {
                    bool safeToCreateWindow = true;

                    foreach (MultiRemoteDesktopClient.Controls.RDThumbnail rdt in flowLayoutPanel1.Controls)
                    {
                        if (f.Handle == rdt.MDIChild_Handle)
                        {
                            // update the thumbnails if window found
                            UpdateThumbnail(f.GetCurrentScreen(), rdt);

                            safeToCreateWindow = false;
                        }
                    }

                    if (safeToCreateWindow)
                    {
                        CreateThumbnail(f);
                    }
                }
            }
        }

        void UpdateThumbnail(Image RDImage, MultiRemoteDesktopClient.Controls.RDThumbnail RDThumb)
        {
            RDThumb.RDImage = RDImage;
        }

        void CreateThumbnail(RdpClientWindow window)
        {
            MultiRemoteDesktopClient.Controls.RDThumbnail RDThumb = new MultiRemoteDesktopClient.Controls.RDThumbnail();
            RDThumb.Title = window.Text;
            RDThumb.RDImage = window.GetCurrentScreen();
            RDThumb.Visible = true;
            RDThumb.MDIChild_Handle = window.Handle;

            flowLayoutPanel1.Controls.Add(RDThumb);
        }
    }
}
