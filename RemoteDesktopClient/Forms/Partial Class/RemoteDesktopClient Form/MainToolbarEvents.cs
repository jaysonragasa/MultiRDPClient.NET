using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MultiRemoteDesktopClient
{
    partial class RemoteDesktopClient
    {
        public void InitializeEvent_MainToolbars()
        {
            #region import / export buttons
            {
                this.toolbar_ImportRDP.Click += new EventHandler(ImportExportRDPFile_Button_Click);
                this.toolbar_ExportRDP.Click += new EventHandler(ImportExportRDPFile_Button_Click);
                this.m_File_ImportRDP.Click += new EventHandler(ImportExportRDPFile_Button_Click);
                this.m_File_ExportRDP.Click += new EventHandler(ImportExportRDPFile_Button_Click);
            }
            #endregion

            #region icon views
            {
                this.m_View_SLIV_Details.Click += new EventHandler(IconViews);
                this.m_View_SLIV_Tile.Click += new EventHandler(IconViews);
                this.m_View_SLIV_Tree.Click += new EventHandler(IconViews);
                this.toolbar_SLIV_Details.Click += new EventHandler(IconViews);
                this.toolbar_SLIV_Tile.Click += new EventHandler(IconViews);
                this.toolbar_SLIV_Tree.Click += new EventHandler(IconViews);
            }
            #endregion

            this.m_Edit_DeleteClient.Click += new EventHandler(btnDelete_Click);
            this.toolbar_DisconnectAll.Click += new EventHandler(btnDCAll_Click);
            this.toolbar_DeleteClient.Click += new EventHandler(btnDelete_Click);
            this.toolbar_ConnectAll.Click += new EventHandler(toolbar_ConnectAll_Click);

            this.m_Tools_Configuration.Click += new EventHandler(toolbar_Configuration_Click);
            this.toolbar_Configuration.Click += new EventHandler(toolbar_Configuration_Click);

            this.m_Edit_ManageGroups.Click += new EventHandler(toolbar_ManageGroups_Click);
            this.toolbar_ManageGroups.Click += new EventHandler(toolbar_ManageGroups_Click);

            this.m_File_Lock.Click += new EventHandler(toobar_Lock_Click);
            this.toobar_Lock.Click += new EventHandler(toobar_Lock_Click);
        }

        void toobar_Lock_Click(object sender, EventArgs e)
        {
            this.AskPassword(this);
        }

        void toolbar_ManageGroups_Click(object sender, EventArgs e)
        {
            GroupManagerWindow gmw = new GroupManagerWindow();
            gmw.ShowDialog(this);

            this.GetServerLists();
        }

        void toolbar_Configuration_Click(object sender, EventArgs e)
        {
            ConfigurationWindow f = new ConfigurationWindow();
            f.ShowDialog();

            if (GlobalHelper.appSettings.Settings.Password == string.Empty)
            {
                toobar_Lock.Enabled = false;
            }
            else
            {
                toobar_Lock.Enabled = true;
            }

            // apply some global settings after reconfiguring
            GlobalHelper.infoWin.EnableInformationWindow = !GlobalHelper.appSettings.Settings.HideInformationPopupWindow;
        }

        void toolbar_ConnectAll_Click(object sender, EventArgs e)
        {
            this.GroupConnectAll();
        }

        void IconViews(object sender, EventArgs e)
        {
            ServerListViews slView = (ServerListViews)((ToolStripMenuItem)sender).Tag;

            splitter.Size = new Size(3, panelServerLists.Height);
            splitter.Location = new Point(panelServerLists.Width - splitter.Width, 0);

            if (slView != ServerListViews.Tree)
            {
                this.lvServerLists.View = (View)((ToolStripMenuItem)sender).Tag;

                // move the control
                this.lvServerLists.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                this.lvServerLists.Location = new Point(0, lblServerListsPanelTitle.Top + lblServerListsPanelTitle.Height);
                this.lvServerLists.Size = new Size(panelServerLists.Width - splitter.Width, panelServerLists.Height - this.lvServerLists.Top);
                this.lvServerLists.BringToFront();
            }
            else if (slView == ServerListViews.Tree)
            {
                this.tlvServerLists.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                this.tlvServerLists.Location = new Point(0, lblServerListsPanelTitle.Top + lblServerListsPanelTitle.Height);
                this.tlvServerLists.Size = new Size(panelServerLists.Width-splitter.Width, panelServerLists.Height - this.tlvServerLists.Top);
                this.tlvServerLists.BringToFront();
            }

            splitter.BringToFront();

            m_View_SLIV.Image = ((ToolStripMenuItem)sender).Image;
            toolbar_SLIV.Image = ((ToolStripMenuItem)sender).Image;

            FixListViewColumn();
        }

        void ImportExportRDPFile_Button_Click(object sender, EventArgs e)
        {
            if (sender == toolbar_ImportRDP || sender == m_File_ImportRDP)
            {
                ImportWindow iw = new ImportWindow();
                iw.ShowDialog();

                GetServerLists();
            }
            else if (sender == toolbar_ExportRDP || sender == m_File_ExportRDP)
            {
                ExportWindow ew = new ExportWindow(ref this.lvServerLists);
                ew.ShowDialog();
            }
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            ServerSettingsWindow ssw = new ServerSettingsWindow();
            ssw.ShowDialog();

            GetServerLists();
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            Database.ServerDetails sd = (Database.ServerDetails)lvServerLists.Items[this._selIndex].Tag;

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this server " + sd.ServerName + " from the server list", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                GlobalHelper.dbServers.DeleteByID(sd.UID);

                GetServerLists();
            }
        }

        void btnDCAll_Click(object sender, EventArgs e)
        {
            DisconnectAll();
        }

        private void OpenSettingsWindow(object sender, EventArgs e)
        {
            Database.ServerDetails sd = (Database.ServerDetails)lvServerLists.Items[this._selIndex].Tag;
            
            ServerSettingsWindow ssw = new ServerSettingsWindow(sd);
            ssw.ApplySettings += new ApplySettings(ssw_ApplySettings);
            ssw.GetClientWindowSize += new GetClientWindowSize(ssw_GetClientWindowSize);
            ssw.ShowDialog();

            GetServerLists();
        }

        #region settings window event (check private void OpenSettingsWindow(object sender, EventArgs e) event)

        Rectangle ssw_GetClientWindowSize()
        {
            Database.ServerDetails sd = (Database.ServerDetails)lvServerLists.Items[this._selIndex].Tag;
            RdpClientWindow rdpClientWin = GetClientWindowByTitleParams(sd.Username, sd.ServerName, sd.Server);

            if (rdpClientWin != null)
            {
                return rdpClientWin.rdpClient.RectangleToScreen(rdpClientWin.rdpClient.ClientRectangle);
            }
            else
            {
                MessageBox.Show("The relative RDP Client Window for this server does not exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Rectangle r = new Rectangle(0, 0, sd.DesktopWidth, sd.DesktopHeight);
                return r;
            }
        }

        void ssw_ApplySettings(object sender, Database.ServerDetails sd)
        {
            RdpClientWindow rdpClientWin = GetClientWindowByTitleParams(sd.Username, sd.ServerName, sd.Server);

            if (rdpClientWin != null)
            {
                rdpClientWin._sd = sd;
                rdpClientWin.Reconnect(true, false, false);
            }
            else
            {
                MessageBox.Show("The relative RDP Client Window for this server does not exists.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion
    }
}
