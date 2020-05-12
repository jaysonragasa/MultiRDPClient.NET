using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using RDPFileReader;
using DataProtection;

namespace MultiRemoteDesktopClient
{
    public partial class ExportWindow : Form
    {
        OpenFileDialog ofd = null;

        public ExportWindow()
        {
            InitializeComponent();
            InitializeControls();
            InitializeControlEvents();
        }

        public ExportWindow(ref MultiRemoteDesktopClient.Controls.ListViewEx lv)
        {
            InitializeComponent();
            InitializeControls(ref lv);
            InitializeControlEvents();
        }

        public void InitializeControls()
        {

        }

        public void InitializeControls(ref MultiRemoteDesktopClient.Controls.ListViewEx lv)
        {
            foreach (ListViewItem thisItem in lv.Items)
            {
                ListViewItem item = new ListViewItem(thisItem.Text);
                item.SubItems.Add("OK");
                item.ImageIndex = 0;
                item.Tag = thisItem.Tag;

                lvRDPFiles.Items.Add(item);
            }
        }

        public void InitializeControlEvents()
        {
            this.Shown += new EventHandler(ExportWindow_Shown);
            this.btnStart.Click += new EventHandler(btnStart_Click);
        }

        void btnStart_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();

            if (fbd.SelectedPath != string.Empty)
            {
                foreach (ListViewItem thisItem in lvRDPFiles.Items)
                {
                    if (thisItem.Checked == true)
                    {
                        thisItem.SubItems[1].Text = "Importing...";

                        Database.ServerDetails sd = (Database.ServerDetails)thisItem.Tag;

                        RDPFile rdp = new RDPFile();
                        rdp.ScreenMode = 1;
                        rdp.DesktopWidth = sd.DesktopWidth;
                        rdp.DesktopHeight = sd.DesktopHeight;
                        rdp.SessionBPP = (RDPFile.SessionBPPs)sd.ColorDepth;

                        RDPFile.WindowsPosition winpos = new RDPFile.WindowsPosition();
                        RDPFile.RECT r = new RDPFile.RECT();
                        r.Top = 0;
                        r.Left = 0;
                        r.Width = sd.DesktopWidth;
                        r.Height = sd.DesktopHeight;
                        winpos.Rect = r;
                        winpos.WinState = RDPFile.WindowState.MAXMIZE;

                        rdp.WinPosStr = winpos;
                        rdp.FullAddress = sd.Server;
                        rdp.Compression = 1;
                        rdp.KeyboardHook = RDPFile.KeyboardHooks.ON_THE_REMOTE_COMPUTER;
                        rdp.AudioMode = RDPFile.AudioModes.BRING_TO_THIS_COMPUTER;
                        rdp.RedirectDrives = 0;
                        rdp.RedirectPrinters = 0;
                        rdp.RedirectComPorts = 0;
                        rdp.RedirectSmartCards = 0;
                        rdp.DisplayConnectionBar = 1;
                        rdp.AutoReconnectionEnabled = 1;
                        rdp.Username = sd.Username;
                        rdp.Domain = string.Empty;
                        rdp.AlternateShell = string.Empty;
                        rdp.ShellWorkingDirectory = string.Empty;

                        // what's with the ZERO ? 
                        // http://www.remkoweijnen.nl/blog/2008/03/02/how-rdp-passwords-are-encrypted-2/
                        rdp.Password = (sd.Password == string.Empty ? string.Empty : DataProtectionForRDPWrapper.Encrypt(sd.Password) + "0"); 

                        //System.Diagnostics.Debug.WriteLine(ss.Password);
                        rdp.DisableWallpaper = 1;
                        rdp.DisableFullWindowDrag = 1;
                        rdp.DisableMenuAnims = 1;
                        rdp.DisableThemes = 1;
                        rdp.DisableCursorSettings = 1;
                        rdp.BitmapCachePersistEnable = 1;

                        #region try exporting the file
                        {
                            try
                            {
                                rdp.Save(Path.Combine(fbd.SelectedPath, sd.ServerName + ".rdp"));

                                thisItem.SubItems[1].Text = "Done!";
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("An error occured while exporting the server '" + sd.ServerName + "' to RDP file format.\r\n\r\nError Message: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                System.Diagnostics.Debug.WriteLine(ex.Message + "\r\n" + ex.StackTrace);

                                continue;
                            }
                        }
                        #endregion
                    }
                }
            }
        }

        void ExportWindow_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
