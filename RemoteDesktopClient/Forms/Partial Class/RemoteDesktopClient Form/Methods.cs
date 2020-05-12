using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MultiRemoteDesktopClient
{
    partial class RemoteDesktopClient
    {
        public void GetServerLists()
        {
            tlvServerLists.Nodes.Clear();
            lvServerLists.Items.Clear();
            lvServerLists.Groups.Clear();

            GetGroups();

            GlobalHelper.dbServers.Read();

            foreach (Database.ServerDetails sd in GlobalHelper.dbServers.ArrayListServers)
            {
                // add items to ListView
                ListViewItem item = new ListViewItem(sd.ServerName);
                item.SubItems.Add(sd.Server);
                item.SubItems.Add(sd.Description);
                item.ImageIndex = 1;
                item.Tag = sd;
                item.Group = lvServerLists.Groups["gid" + sd.GroupID.ToString()];

                lvServerLists.Items.Add(item);

                // add items to TreeListView
                object[] o = {
                                 sd.ServerName,
                                 sd.Server,
                                 sd.Description
                             };
                CommonTools.Node n = new CommonTools.Node(o);
                n.Tag = sd;
                tlvServerLists.Nodes["gid" + sd.GroupID.ToString()].Nodes.Add(sd.UID, n);
                tlvServerLists.Nodes["gid" + sd.GroupID.ToString()].ExpandAll();
            }

            FixListViewColumn();
        }

        public void GetGroups()
        {
            GlobalHelper.dbGroups.Read();
            
            foreach (Database.GroupDetails gd in GlobalHelper.dbGroups.ArrayListGroups)
            {
                // add groups to ListView
                ListViewGroup lvg = new ListViewGroup("gid" + gd.GroupID.ToString(), gd.GroupName);
                lvServerLists.Groups.Add(lvg);

                // add parent node to TreeListView
                CommonTools.Node n = new CommonTools.Node(gd.GroupName);
                n.MakeVisible();
                this.tlvServerLists.Nodes.Add("gid" + gd.GroupID.ToString(), n);
            }
        }

        public void FixListViewColumn()
        {
            object x = new object();

            lock (x) // force to resize the listview columns
            {
                foreach (ColumnHeader ch in this.lvServerLists.Columns)
                {
                    ch.Width = -2;
                }
            }
        }

        public RdpClientWindow GetClientWindowByTitleParams(params object[] args)
        {
            RdpClientWindow ret = null;

            string formTitlePattern = "Remote Desktop Client - {0}@{1}[{2}]";
            string formTitle = string.Format(formTitlePattern, args);

            foreach (RdpClientWindow f in this.MdiChildren)
            {
                if (f.Text == formTitle)
                {
                    ret = f;
                }
            }

            return ret;
        }

        void DisconnectAll()
        {
            foreach (RdpClientWindow f in this.MdiChildren)
            {
                f.Disconnect();
            }
        }

        private void ShowMe()
        {
            
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            TopMost = top;
        }

        public void SetupServerListPanel(DockStyle dock)
        {
            panelServerLists.Dock = dock;
            panelServerLists.Tag = dock;

            if (dock == DockStyle.None)
            {
                panelServerLists.Top = tabstripLeftPanel.Top;
                panelServerLists.Left = tabstripLeftPanel.Left + tabstripLeftPanel.Width;
                panelServerLists.Height = tabstripLeftPanel.Height;
                panelServerLists.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;

                panelMDIToolbars.BringToFront();
                panelServerLists.BringToFront();
            }
            else if (dock == DockStyle.Left)
            {
                panelServerLists.Dock = dock;
                panelServerLists.BringToFront();
                panelMDIToolbars.BringToFront();
            }
        }

        public void ConnectByServerName(string server_name)
        {
            ListViewItem item = this.lvServerLists.FindItemWithText(server_name, false, 0, true);
            if (item != null)
            {   
                this._selIndex = item.Index;
                Connect();
            }
        }

        public void Connect()
        {
            object x = new object();

            lock (x)
            {
                Database.ServerDetails sd = (Database.ServerDetails)lvServerLists.Items[this._selIndex].Tag;

                bool canCreateNewForm = true;
                string formTitlePattern = "Remote Desktop Client - {0}@{1}[{2}]";
                string formTitle = string.Format(formTitlePattern, sd.Username, sd.ServerName, sd.Server);

                foreach (Form f in this.MdiChildren)
                {
                    if (f.Text == formTitle)
                    {
                        f.Activate();
                        canCreateNewForm = false;
                        break;
                    }
                }

                if (canCreateNewForm)
                {
                    RdpClientWindow clientWin = new RdpClientWindow(sd, this);
                    clientWin.Connected += new Connected(clientWin_Connected);
                    clientWin.Connecting += new Connecting(clientWin_Connecting);
                    clientWin.LoginComplete += new LoginComplete(clientWin_LoginComplete);
                    clientWin.Disconnected += new Disconnected(clientWin_Disconnected);
                    clientWin.OnFormShown += new OnFormShown(clientWin_OnFormShown);
                    clientWin.OnFormClosing += new OnFormClosing(clientWin_OnFormClosing);
                    clientWin.OnFormActivated += new OnFormActivated(clientWin_OnFormActivated);
                    clientWin.ServerSettingsChanged += new ServerSettingsChanged(clientWin_ServerSettingsChanged);
                    clientWin.Text = formTitle;
                    clientWin.MdiParent = this;
                    System.Diagnostics.Debug.WriteLine(this.Handle);
                    clientWin.ListIndex = this._selIndex;
                    clientWin.Show();
                    clientWin.BringToFront();
                    clientWin.Connect();
                }
            }
        }

        /// <summary>
        /// Connect all items in a group.
        /// </summary>
        public void GroupConnectAll()
        {
            // hmmm... let's just rely on our ListView

            // check what group are we at
            ListViewGroup thisGroup = this.lvServerLists.Items[this._selIndex].Group;

            // connect all items in the group
            foreach (ListViewItem thisItem in thisGroup.Items)
            {
                this._selIndex = thisItem.Index;
                this.Connect();
            }
        }

        public void GroupConnectAll(string groupname)
        {
            bool foundAGroup = false;

            foreach (ListViewGroup group in this.lvServerLists.Groups)
            {
                System.Diagnostics.Debug.WriteLine(group.Header + ", " + groupname);
                if (group.Header == groupname)
                {
                    // check if we have items in a group
                    if (group.Items.Count != 0)
                    {
                        // so let's just get the first item
                        this._selIndex = group.Items[0].Index;

                        // and connect all items in the group
                        GroupConnectAll();

                        foundAGroup = true;

                        break;
                    }
                }
            }

            if (!foundAGroup)
            {
                MessageBox.Show("No server's found on associated on this group '" + groupname + "'", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Welcome()
        {
            DialogResult dr;

            Database.Database db = new Database.Database();

            if (!GlobalHelper.appSettings.IsAppConfigExists())
            {
                dr = MessageBox.Show(
                    "Looks like it's your first time to use Multi Remote Desktop Client .Net!\r\n\r\nThe application created a default password for you called \"pass\".\r\nDo you like to update your password now?",
                    this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );

                if (dr == DialogResult.Yes)
                {
                    // call our toolbar_Configuration event method.
                    this.toolbar_Configuration_Click(this.toolbar_Configuration, null);
                }

                // create our new database schema and default datas
                db.ResetDatabase();
                
            }

            db.Delete(false);
            db = null;
        }

        public bool AskPassword(object sender)
        {
            if (GlobalHelper.appSettings.Settings.Password == string.Empty)
            {
                return false;
            }

            PasswordWindow pw = new PasswordWindow();
            DialogResult dr = pw.ShowDialog(this);

            if (dr == DialogResult.Cancel)
            {
                //lock(this)
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }

            return true;
        }
    }
}
