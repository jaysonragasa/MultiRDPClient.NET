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
    public partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            InitializeComponent();

            richTextBox1.Text = @"
Remote Network Technology
http://dev.remotenetworktechnology.com/ts/rdpfile.htm
- Thanks for sharing the knowledge about how the RDP setting keys works.

Terminal Services Team Blog
http://blogs.msdn.com/ts/archive/2008/09/02/specifying-the-ts-client-start-location-on-the-virtual-desktop.aspx
- Thanks for ""winposstr"" explanation

Building Secure ASP.NET Applications: Authentication, Authorization, and Secure Communication
http://msdn.microsoft.com/en-us/library/aa302402.aspx#secnetht07_topic4
- Thanks for the DataProtection class! works great for RDP Password.
- Modified it a bit to support passing a description in CryptProtectData and UncryptProtectData.

Remko Weijnen
http://www.remkoweijnen.nl/blog/2007/10/18/how-rdp-passwords-are-encrypted/#comment-900
- Thanks for the ""psw"" idea!

Symmetric key encryption and decryption using Rijndael algorithm
http://www.obviex.com/samples/Encryption.aspx
- Thanks for that algorithm!

TreeListView
http://www.codeproject.com/KB/tree/TreeWithColumns.aspx
- Thanks for that control! I loved it so much that's why I have to fix some of your bugs :)

Crownwood Magic Library
http://www.codeproject.com/KB/tabs/magictabcontrol.aspx
- Thanks for the Custom Tab Control

TabStrip Control
http://www.codeproject.com/KB/tabs/tabstrips.aspx
- Thanks for the Horizontal Tab Control. Though I haven't implemented yet due to some bug from that control.

And thanks to all those who shared their ideas.

NOTE:
Modification and Implementation are all done by Me to produce the expected result.

Multi Remote Desktop Client .NET
Is licensed under: Microsoft Public License (Ms-PL)
http://www.microsoft.com/opensource/licenses.mspx
";
        }
    }
}
