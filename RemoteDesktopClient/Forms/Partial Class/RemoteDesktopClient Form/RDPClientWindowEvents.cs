using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MultiRemoteDesktopClient
{
    partial class RemoteDesktopClient
    {
        void clientWin_ServerSettingsChanged(object sender, Database.ServerDetails sd, int ListIndex)
        {
            ListViewItem item = lvServerLists.Items[ListIndex];

            if (item != null)
            {
                item.Text = sd.ServerName;
                item.SubItems[1].Text = sd.Server;
                item.SubItems[2].Text = sd.Description;
                item.Tag = sd;
            }
        }

        void clientWin_OnFormShown(object sender, EventArgs e, int ListIndex, IntPtr Handle)
        {
            RdpClientWindow rcw = (RdpClientWindow)sender;

            Crownwood.Magic.Controls.TabPage tabMDI = new Crownwood.Magic.Controls.TabPage(); //(rcw.Text, rcw.toolStrip1);
            tabMDI.Title = lvServerLists.Items[ListIndex].Text;
            tabMDI.ImageIndex = 0;
            tabMDI.Tag = rcw.Handle; rcw = null;
            tabMDI.Selected = true;

            tabMDIChild.TabPages.Add(tabMDI);
            GlobalHelper.MDIChildrens = this.MdiChildren;
        }

        void clientWin_OnFormClosing(object sender, FormClosingEventArgs e, int ListIndex, IntPtr Handle)
        {
            lvServerLists.Items[ListIndex].ImageIndex = 1;

            foreach (Crownwood.Magic.Controls.TabPage tabMDI in tabMDIChild.TabPages)
            {
                if ((IntPtr)tabMDI.Tag == Handle)
                {
                    tabMDIChild.TabPages.Remove(tabMDI);
                    break;
                }
            }

            GlobalHelper.MDIChildrens = this.MdiChildren;
        }

        void clientWin_OnFormActivated(object sender, EventArgs e, int ListIndex, IntPtr Handle)
        {
            foreach (Crownwood.Magic.Controls.TabPage tabMDI in tabMDIChild.TabPages)
            {
                if ((IntPtr)tabMDI.Tag == Handle)
                {
                    tabMDI.Selected = true;
                    break;
                }
            }
        }

        void clientWin_Disconnected(object sender, AxMSTSCLib.IMsTscAxEvents_OnDisconnectedEvent e, int ListIndex)
        {
            lvServerLists.Items[ListIndex].ImageIndex = 1;
            tabMDIChild.SelectedTab.ImageIndex = 1;
        }

        void clientWin_Connected(object sender, EventArgs e, int ListIndex)
        {
            //lvServerLists.Items[ListIndex].ImageIndex = 0;
            //tabMDIChild.SelectedTab.ImageIndex = 0;
        }

        void clientWin_LoginComplete(object sender, EventArgs e, int ListIndex)
        {
            lvServerLists.Items[ListIndex].ImageIndex = 0;
            tabMDIChild.SelectedTab.ImageIndex = 0;
        }

        void clientWin_Connecting(object sender, EventArgs e, int ListIndex)
        {
            lvServerLists.Items[ListIndex].ImageIndex = 2;
        }
    }
}
