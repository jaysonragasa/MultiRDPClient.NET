using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MultiRemoteDesktopClient
{
    partial class RemoteDesktopClient
    {
        private int _lastPanelWidth = 0;

        /// <summary>
        /// The Index of the selected item in our ListView which is the lvServerLists.
        /// </summary>
        private int _selIndex = 0;

        void InitializeServerListEvents()
        {
            this.lvServerLists.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(lvServerLists_ItemSelectionChanged);
            this.lvServerLists.DoubleClick += new EventHandler(lvServerLists_DoubleClick);
            this.lvServerLists.MouseDown += new MouseEventHandler(lvServerLists_MouseDown);

            this.tlvServerLists.AfterSelect += new TreeViewEventHandler(tlvServerLists_AfterSelect);
            this.tlvServerLists.DoubleClick += new EventHandler(tlvServerLists_DoubleClick);
            this.tlvServerLists.MouseDown += new MouseEventHandler(tlvServerLists_MouseDown);

            this.btnHideServerLists.Click += new EventHandler(btnHideServerLists_Click);
            this.btnPinServerLists.Click += new EventHandler(btnPinServerLists_Click);
            this.tabServerLists.Click += new EventHandler(tabServerLists_Click);

            #region server list context menu
            {
                this.lvServerListsContextMenu_NewClient.Click += new EventHandler(ShowNewForm);
                this.lvServerListsContextMenu_DeleteClient.Click += new EventHandler(btnDelete_Click);
                this.lvServerListsContextMenu_EditClientSettings.Click += new EventHandler(OpenSettingsWindow);
                this.lvServerListsContextMenu_ConnectAll.Click += new EventHandler(toolbar_ConnectAll_Click);
            }
            #endregion
        }

        void tabServerLists_Click(object sender, EventArgs e)
        {
            if (tabstripLeftPanel.SelectedTab == tabServerLists)
            {
                panelServerLists.Enabled = true;
                panelServerLists.Visible = true;
            }
        }


        void lvServerLists_DoubleClick(object sender, EventArgs e)
        {
            this.Connect();
        }

        void lvServerLists_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this._selIndex = e.ItemIndex;
        }

        void lvServerLists_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo lvhi = lvServerLists.HitTest(new Point(e.X, e.Y));

            if (lvhi.Item != null)
            {
                Database.ServerDetails sd = (Database.ServerDetails)lvhi.Item.Tag;

                status_TextStatus.Text = sd.ServerName + " - " + sd.Server;

                CommonTools.Node n = tlvServerLists.FindNode(sd.ServerName, false);
                if (n != null)
                {
                    tlvServerLists.FocusedNode = n;
                }
            }
            else
            {
                status_TextStatus.Text = string.Empty;
            }
        }

        CommonTools.Node thisSelectedNode = null;

        void tlvServerLists_DoubleClick(object sender, EventArgs e)
        {
            if (thisSelectedNode != null)
            {
                if (!thisSelectedNode.Key.Contains("gid")) // we don't need parent node / group
                {
                    this.Connect();
                }
            }
        }

        void tlvServerLists_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // :P TreeViewEventArgs for System.Windows.Forms.TreeNode ??

            // no implementation of CommonTools.TreeNode

            // let's just be safe.. this control is buggy :P
            if (tlvServerLists.NodesSelection == null) { return; }

            // just get the first node selection
            CommonTools.Node n = tlvServerLists.NodesSelection[0];

            ListViewItem thisItem = this.lvServerLists.FindItemWithText(n[0].ToString());

            if (thisItem != null)
            {
                this._selIndex = thisItem.Index;
            }
        }

        void tlvServerLists_MouseDown(object sender, MouseEventArgs e)
        {
            thisSelectedNode = tlvServerLists.CalcHitNode(new Point(e.X, e.Y));

            if (thisSelectedNode != null)
            {
                if (!thisSelectedNode.Key.Contains("gid")) // we don't need parent node / group
                {
                    Database.ServerDetails sd = (Database.ServerDetails)thisSelectedNode.Tag;
                    status_TextStatus.Text = sd.ServerName + " - " + sd.Server;
                    this.tlvch.EnableControls(true);

                    ListViewItem thisItem = this.lvServerLists.FindItemWithText(thisSelectedNode[0].ToString());

                    if (thisItem != null)
                    {
                        // select the item on our listview too
                        thisItem.Selected = true;
                    }
                }
                else
                {
                    // disable all unnecessary controls
                    this.tlvch.EnableControls(false);

                    // ok .. we like our ConnectAll button and Menu strip item to be enabled 
                    // so we can use it when we selected our parent node / group
                    // let's just enable them
                    toolbar_ConnectAll.Enabled = true;
                    lvServerListsContextMenu_ConnectAll.Enabled = true;

                    // and we have to let our "this._selIndex" at least know
                    // where to start so when we click ConnectAll in a parent node,
                    // it has a way to check.

                    // say the first item in a listview group.
                    // remeber, we are basing our events on listview instead of the treeview
                    // to prevent confusion
                    
                    //if (thisSelectedNode.HasChildren)
                    if (thisSelectedNode.Nodes.Count != 0)
                    {
                        // get the first child node
                        CommonTools.Node n = thisSelectedNode.Nodes.FirstNode;

                        // and match that in our listview
                        ListViewItem item = lvServerLists.FindItemWithText(n[0].ToString(), false, 0, true);
                        if (item != null)
                        {
                            // ok so we have it.
                            this._selIndex = item.Index;
                        }

                        item = null;
                        n = null;
                    }
                    else
                    {
                        // no child nodes
                        // let's just disable ConnectAll buttons and menu items again
                        // disable all unnecessary controls
                        this.tlvch.EnableControls(false);
                    }
                }
            }
            else
            {
                // disable all unnecessary controls
                // if nothing is selected
                this.tlvch.EnableControls(false);
            }
        }

        void btnHideServerLists_Click(object sender, EventArgs e)
        {
            //this._lastPanelWidth = this.panelServerLists.Width;

            this.panelServerLists.Enabled = false;
            this.panelServerLists.Visible = false;
        }

        void btnPinServerLists_Click(object sender, EventArgs e)
        {
            DockStyle dstyle = (DockStyle)panelServerLists.Tag;
            if (dstyle == DockStyle.Left)
            {
                GlobalHelper.controlHideShow.Enable = true;
                btnPinServerLists.Image = Properties.Resources.pin_out;
                SetupServerListPanel(DockStyle.None);
            }
            else
            {
                GlobalHelper.controlHideShow.Enable = false;
                btnPinServerLists.Image = Properties.Resources.pin_in;
                SetupServerListPanel(DockStyle.Left);
            }
        }
    }
}
