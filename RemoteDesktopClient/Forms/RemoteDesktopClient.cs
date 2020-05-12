using System;
using System.Drawing;
using System.Windows.Forms;

namespace MultiRemoteDesktopClient
{
    public partial class RemoteDesktopClient : Form
    {
        MultiRemoteDesktopClient.Controls.TreeListViewControlHooks tlvch;
        FormWindowState _lastWindowState;

        public void Initialize()
        {
            // let's just setup everyting
            // before running some arguments
            InitializeComponent();
            InitializeControl();
            InitializeControlEvents();
        }

        public RemoteDesktopClient()
        {
            Initialize();

            Welcome();
            if (!AskPassword(this))
            {
                toobar_Lock.Enabled = false;
            }
        }

        public void DoArguments(string[] args)
        {
            /*
             * List of valid arguments:
             * 
             * /sname <server name>
             * - connect to existing server by Server Name
             * 
             * /gname <group name>
             * - Connect to multiple server inside a group by providing a Group Name
            */

            string args_server_name = string.Empty;
            string args_group_name = string.Empty;
            CommandLine.Utility.Arguments a = new CommandLine.Utility.Arguments(args);
            
            if (a["sname"] != null)
            {
                this.ConnectByServerName(a["sname"]);
            }

            if (a["gname"] != null)
            {
                this.GroupConnectAll(a["gname"]);
            }
        }

        public void InitializeControl()
        {
            //dont show the Pin button for a while
            btnPinServerLists.Visible = false;
            // set initial panel dock style
            panelServerLists.Tag = DockStyle.None;
            // simulate clicking to Pin Button
            btnPinServerLists_Click(btnPinServerLists, null);

            #region views
            {
                this.m_View_SLIV_Details.Tag = ServerListViews.Details;
                this.m_View_SLIV_Tile.Tag = ServerListViews.Tile;
                this.m_View_SLIV_Tree.Tag = ServerListViews.Tree;
                this.toolbar_SLIV_Details.Tag = ServerListViews.Details;
                this.toolbar_SLIV_Tile.Tag = ServerListViews.Tile;
                this.toolbar_SLIV_Tree.Tag = ServerListViews.Tree;
            }
            #endregion

            #region Informatin Window
            {
                GlobalHelper.infoWin.EnableInformationWindow = !GlobalHelper.appSettings.Settings.HideInformationPopupWindow;
                GlobalHelper.infoWin.AddControl(new object[] {
                    this.lvServerLists,
                    this.tlvServerLists,
                    this.toolbar_EditSettings
                });
            }
            #endregion

            #region listview server list control hooks
            {
                lvServerLists.AddControlForEmptyListItem(new object[] {
                    toolbar_DeleteClient,
                    toolbar_EditSettings,
                    toolbar_ConnectAll,
                    m_Edit_DeleteClient,
                    m_File_EditSettings
                });

                lvServerLists.AddControlForItemSelection(new object[] {
                    toolbar_DeleteClient,
                    toolbar_EditSettings,
                    toolbar_ConnectAll,
                    m_Edit_DeleteClient,
                    m_File_EditSettings,
                    lvServerListsContextMenu_DeleteClient,
                    lvServerListsContextMenu_EditClientSettings,
                    lvServerListsContextMenu_ConnectAll
                });
            }
            #endregion

            #region tree listview control hooks
            {
                tlvch = new MultiRemoteDesktopClient.Controls.TreeListViewControlHooks(ref this.tlvServerLists);

                tlvch.AddControlForEmptyListItem(new object[] {
                    toolbar_DeleteClient,
                    toolbar_EditSettings,
                    toolbar_ConnectAll,
                    m_Edit_DeleteClient,
                    m_File_EditSettings
                });

                tlvch.AddControlForItemSelection(new object[] {
                    toolbar_DeleteClient,
                    toolbar_EditSettings,
                    toolbar_ConnectAll,
                    m_Edit_DeleteClient,
                    m_File_EditSettings,
                    lvServerListsContextMenu_DeleteClient,
                    lvServerListsContextMenu_EditClientSettings,
                    lvServerListsContextMenu_ConnectAll
                });
            }
            #endregion

            #region treelistview columns
            // TreeListView's Design time support is so buggy and usually deletes the columns
            tlvServerLists.Columns.AddRange(new CommonTools.TreeListColumn[] {
                new CommonTools.TreeListColumn("server_name", "Server Name", 50),
                new CommonTools.TreeListColumn("server", "Server ", 50),
                new CommonTools.TreeListColumn("descr", "Description", 50)
            });
            tlvServerLists.Columns["server_name"].AutoSize = true;
            tlvServerLists.Columns["server_name"].AutoSizeRatio = 100;
            tlvServerLists.Columns["server"].AutoSize = true;
            tlvServerLists.Columns["server"].AutoSizeRatio = 50;
            #endregion

            this._lastPanelWidth = this.panelServerLists.Width;

            // change server list view
            IconViews(this.toolbar_SLIV_Tree, null);

            // show thumbnail form;
            //RDThumbnailsWindow rdtnwin = new RDThumbnailsWindow();
            //rdtnwin.Show();
        }

        public void InitializeControlEvents()
        {
            this.Shown += new EventHandler(RemoteDesktopClient_Shown);
            this.FormClosing += new FormClosingEventHandler(RemoteDesktopClient_FormClosing);
            this.SizeChanged += new EventHandler(RemoteDesktopClient_SizeChanged);
            this.m_Help_About.Click += new EventHandler(aboutToolStripMenuItem_Click);

            #region splitter
            {
                this.splitter.MouseDown += new MouseEventHandler(splitter_MouseDown);
                this.splitter.MouseMove += new MouseEventHandler(splitter_MouseMove);
                this.splitter.MouseUp += new MouseEventHandler(splitter_MouseUp);
            }
            #endregion

            #region main toolbar events
            {
                InitializeEvent_MainToolbars();
            }
            #endregion

            #region server lists events
            {
                InitializeServerListEvents();
            }
            #endregion

            #region mdi tabs
            {
                this.tabMDIChild.SelectionChanged += new EventHandler(tabMDIChild_SelectionChanged);
                this.tabMDIChild.ClosePressed += new EventHandler(tabMDIChild_ClosePressed);
            }
            #endregion

            #region system tray
            {
                systray.DoubleClick += new EventHandler(systray_DoubleClick);
            }
            #endregion
        }

        #region default events

        void RemoteDesktopClient_Shown(object sender, EventArgs e)
        {
            GetServerLists();

            DoArguments(Environment.GetCommandLineArgs());

            NotificationContextMenu ncm = new NotificationContextMenu();
            ncm.OnDisconnect_Clicked += new DelegateDisconnectEvent(btnDCAll_Click);
            ncm.OnConfiguration_Clicked += new DelegateConfigurationEvent(toolbar_Configuration_Click);
            ncm.OnLock_Clicked += new DelegateLockEvent(toobar_Lock_Click);
            ncm.OnServer_Clicked += new DelegateServerEvent(ncm_OnServer_Clicked);

            this.systray.ContextMenuStrip = ncm;
            //CommonTools.Node n = tlvServerLists.FindNode("92.48.83.65", true);
            //if (n != null)
            //{
            //    MessageBox.Show(n[0].ToString());
            //}
        }

        // check RemoteDesktopClient_Shown(object sender, EventArgs e)
        void ncm_OnServer_Clicked(object sender, EventArgs e, Database.ServerDetails server_details)
        {
            ListViewItem thisItem = lvServerLists.FindItemWithText(server_details.ServerName, false, 0);
            if (thisItem != null)
            {
                this._selIndex = thisItem.Index;
                this.Connect();
            }
        }

        void RemoteDesktopClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to exit", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow aw = new AboutWindow();
            aw.ShowDialog();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region show/hide toolbar and stats events

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        #endregion

        #region client window layout

        private void LayoutMdi_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mItem = (ToolStripMenuItem)sender;

            LayoutMdi((MdiLayout)int.Parse(mItem.Tag.ToString()));
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        #endregion

        #region mdi tabs

        void tabMDIChild_SelectionChanged(object sender, EventArgs e)
        {
            if (tabMDIChild.SelectedTab == null) { return; }

            foreach (RdpClientWindow f in this.MdiChildren)
            {
                if ((IntPtr)tabMDIChild.SelectedTab.Tag == f.Handle)
                {
                    f.Activate();
                    f.rdpClient.Focus();

                    break;
                }
            }
        }

        void tabMDIChild_ClosePressed(object sender, EventArgs e)
        {
            this.ActiveMdiChild.Close();
        }

        #endregion

        #region splitter stuff
        // we have to programmatically move the splitter inside the server lists panel
        // because we have a floating panel which was the server lists panel
        int splitX = 0;

        void splitter_MouseDown(object sender, MouseEventArgs e)
        {
            splitX = e.X;
            splitter.BackColor = Color.FromKnownColor(KnownColor.ActiveCaption);
        }

        void splitter_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                splitter.Left += e.X - splitX;
                panelServerLists.Width = splitter.Left + splitter.Width;
            }
        }
        void splitter_MouseUp(object sender, MouseEventArgs e)
        {
            splitter.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        #endregion

        void RemoteDesktopClient_SizeChanged(object sender, EventArgs e)
        {
            // we only want the Maximize and Normal state of this window
            if (this.WindowState != FormWindowState.Minimized)
            {
                this._lastWindowState = this.WindowState;
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                if (GlobalHelper.appSettings.Settings.HideWhenMinimized)
                {
                    this.Visible = false;
                }
            }
        }

        void systray_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = this._lastWindowState;
            this.Activate();
            this.BringToFront();
        }

        #endregion
    }
}
