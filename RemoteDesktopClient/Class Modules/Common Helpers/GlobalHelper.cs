using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Database;

namespace MultiRemoteDesktopClient
{
    public enum ServerListViews
    {
        // Summary:
        //     Each item appears as a full-sized icon with a label below it.
        LargeIcon = 0,
        //
        // Summary:
        //     Each item appears on a separate line with further information about each
        //     item arranged in columns. The left-most column contains a small icon and
        //     label, and subsequent columns contain sub items as specified by the application.
        //     A column displays a header which can display a caption for the column. The
        //     user can resize each column at run time.
        Details = 1,
        //
        // Summary:
        //     Each item appears as a small icon with a label to its right.
        SmallIcon = 2,
        //
        // Summary:
        //     Each item appears as a small icon with a label to its right. Items are arranged
        //     in columns with no column headers.
        List = 3,
        //
        // Summary:
        //     Each item appears as a full-sized icon with the item label and subitem information
        //     to the right of it. The subitem information that appears is specified by
        //     the application. This view is available only on Windows XP and the Windows
        //     Server 2003 family. On earlier operating systems, this value is ignored and
        //     the System.Windows.Forms.ListView control displays in the System.Windows.Forms.View.LargeIcon
        //     view.
        Tile = 4,
        //
        // Summary:
        //     viewed as Tree
        Tree = 5
    }

    public class GlobalHelper
    {
        public static Servers dbServers = new Servers();
        public static Groups dbGroups = new Groups();
        public static ApplicationSettings appSettings = new ApplicationSettings();

        public static ControlHideShow controlHideShow = new ControlHideShow();
        public static Form[] MDIChildrens;
        

        public static LiveInformationBox.InfoWindow infoWin = new LiveInformationBox.InfoWindow(
            System.IO.Path.Combine(Application.StartupPath, "XMLInfoFile.xml"),
            LiveInformationBox.InfoWindow.WindowPositions.BOTTOM_RIGHT
        );

        public static void PopulateGroupsDropDown(ComboBox cb, string selected_text)
        {
            cb.Items.Clear();

            dbGroups.Read();

            foreach (Database.GroupDetails gd in dbGroups.ArrayListGroups)
            {
                cb.Items.Add(gd.GroupName);
            }

            cb.Items.Add("( Add new Group ) ...");

            if (selected_text != string.Empty)
            {
                cb.SelectedIndex = cb.FindStringExact(selected_text);
            }
        }
    }
}
