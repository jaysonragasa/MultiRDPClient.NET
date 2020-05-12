using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AxMSTSCLib;
using Win32APIs;

namespace MultiRemoteDesktopClient
{
    public delegate void Connected(object sender, EventArgs e, int ListIndex);
    public delegate void Connecting(object sender, EventArgs e, int ListIndex);
    public delegate void LoginComplete(object sender, EventArgs e, int ListIndex);
    public delegate void Disconnected(object sender, AxMSTSCLib.IMsTscAxEvents_OnDisconnectedEvent e, int ListIndex);
    public delegate void OnFormClosing(object sender, FormClosingEventArgs e, int ListIndex, IntPtr Handle);
    public delegate void OnFormActivated(object sender, EventArgs e, int ListIndex, IntPtr Handle);
    public delegate void OnFormShown(object sender, EventArgs e, int ListIndex, IntPtr Handle);
    public delegate void ServerSettingsChanged(object sender, Database.ServerDetails sd, int ListIndex);

    public partial class RdpClientWindow : Form
    {
        public event Connected Connected;
        public event Connecting Connecting;
        public event LoginComplete LoginComplete;
        public event Disconnected Disconnected;
        public event OnFormClosing OnFormClosing;
        public event OnFormActivated OnFormActivated;
        public event OnFormShown OnFormShown;
        public event ServerSettingsChanged ServerSettingsChanged;

        public Database.ServerDetails _sd;

        // used to easly locate in Server lists (RemoteDesktopClient)
        private int _listIndex = 0;

        private bool _isFitToWindow = false;

        public RdpClientWindow(Database.ServerDetails sd, Form parent)
        {
            InitializeComponent();
            InitializeControl(sd);
            InitializeControlEvents();

            this.MdiParent = parent;
            this.Visible = true;
        }

        public void InitializeControl(Database.ServerDetails sd)
        {
            GlobalHelper.infoWin.AddControl(new object[] {
                btnFitToScreen
            });

            this._sd = sd;

            rdpClient.Server = sd.Server;
            rdpClient.UserName = sd.Username;
            //rdpClient.Domain = sd.dom
            rdpClient.AdvancedSettings2.ClearTextPassword = sd.Password;
            rdpClient.ColorDepth = sd.ColorDepth;
            rdpClient.DesktopWidth = sd.DesktopWidth;
            rdpClient.DesktopHeight = sd.DesktopHeight;
            rdpClient.FullScreen = sd.Fullscreen;
            

            // this fixes the rdp control locking issue
            // when lossing its focus
            //rdpClient.AdvancedSettings3.ContainerHandledFullScreen = -1;
            //rdpClient.AdvancedSettings3.DisplayConnectionBar = true;
            //rdpClient.FullScreen = true;
            //rdpClient.AdvancedSettings3.SmartSizing = true;
            //rdpClient.AdvancedSettings3.PerformanceFlags = 0x00000100;

            //rdpClient.AdvancedSettings2.allowBackgroundInput = -1;
            rdpClient.AdvancedSettings2.AcceleratorPassthrough = -1;
            rdpClient.AdvancedSettings2.Compress = -1;
            rdpClient.AdvancedSettings2.BitmapPersistence = -1;
            rdpClient.AdvancedSettings2.BitmapPeristence = -1;
            //rdpClient.AdvancedSettings2.BitmapCacheSize = 512;
            rdpClient.AdvancedSettings2.CachePersistenceActive = -1;


            // custom port
            if (sd.Port != 0)
            {
                rdpClient.AdvancedSettings2.RDPPort = sd.Port;
            }

            btnConnect.Enabled = false;

            panel1.Visible = false;
            tmrSC.Enabled = false;
        }

        public void InitializeControlEvents()
        {
            this.Shown += new EventHandler(RdpClientWindow_Shown);
            this.FormClosing += new FormClosingEventHandler(RdpClientWindow_FormClosing);

            btnDisconnect.Click += new EventHandler(ToolbarButtons_Click);
            btnConnect.Click += new EventHandler(ToolbarButtons_Click);
            btnReconnect.Click += new EventHandler(ToolbarButtons_Click);
            btnSettings.Click += new EventHandler(ToolbarButtons_Click);
            btnFullscreen.Click += new EventHandler(ToolbarButtons_Click);
            m_FTS_FitToScreen.Click += new EventHandler(ToolbarButtons_Click);
            m_FTS_Strech.Click += new EventHandler(ToolbarButtons_Click);
            btnPopout_in.Click += new EventHandler(btnPopout_in_Click);

            this.rdpClient.OnConnecting += new EventHandler(rdpClient_OnConnecting);
            this.rdpClient.OnConnected += new EventHandler(rdpClient_OnConnected);
            this.rdpClient.OnLoginComplete += new EventHandler(rdpClient_OnLoginComplete);
            this.rdpClient.OnDisconnected += new AxMSTSCLib.IMsTscAxEvents_OnDisconnectedEventHandler(rdpClient_OnDisconnected);

            btnSndKey_TaskManager.Click += new EventHandler(SendKeys_Button_Click);

            tmrSC.Tick += new EventHandler(tmrSC_Tick);
        }

        public const int WM_LEAVING_FULLSCREEN = 0x4ff;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x21)  // mouse click
            {
                this.rdpClient.Focus();
            }
            else if (m.Msg == WM_LEAVING_FULLSCREEN)
            {
            }

            base.WndProc(ref m);
        }

        PopupMDIContainer popupmdi = null;
        void btnPopout_in_Click(object sender, EventArgs e)
        {
            // we just can't move our entire form
            // into different window because of the ActiveX error
            // crying out about the Windowless control.

            if (int.Parse(btnPopout_in.Tag.ToString()) == 0)
            {
                popupmdi = new PopupMDIContainer();
                popupmdi.Show();
                popupmdi.PopIn(ref rdpPanelBase, this, this._sd.ServerName);

                btnPopout_in.Image = Properties.Resources.pop_in_16;
                btnPopout_in.Tag = 1;
            }
            else if (int.Parse(btnPopout_in.Tag.ToString()) == 1)
            {
                popupmdi.PopOut(ref rdpPanelBase, this);

                btnPopout_in.Image = Properties.Resources.pop_out_16;
                btnPopout_in.Tag = 0;
            }
        }

        #region EVENT: Send Keys
        void SendKeys_Button_Click(object sender, EventArgs e)
        {
            rdpClient.Focus();

            if (sender == btnSndKey_TaskManager)
            {
                //SendKeys.Send("(^%)");
                SendKeys.Send("(^%{END})");
            }

            //rdpClient.AdvancedSettings2.HotKeyCtrlAltDel;
        }
        #endregion

        #region EVENT: RDP Client

        void rdpClient_OnDisconnected(object sender, AxMSTSCLib.IMsTscAxEvents_OnDisconnectedEvent e)
        {
            Status("Disconnected from " + this._sd.Server);

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;

            { // check connection status on output
                System.Diagnostics.Debug.WriteLine("OnDisconnected " + rdpClient.Connected);
            }

            if (Disconnected != null)
            {
                Disconnected(this, e, this._listIndex);
            }
        }

        void rdpClient_OnLoginComplete(object sender, EventArgs e)
        {
            Status("Loged in using " + this._sd.Username + " user account");

            { // check connection status on output
                System.Diagnostics.Debug.WriteLine("OnLoginComplete " + rdpClient.Connected);
            }

            if (LoginComplete != null)
            {
                LoginComplete(this, e, this._listIndex);
            }
        }

        void rdpClient_OnConnected(object sender, EventArgs e)
        {
            Status("Connected to " + this._sd.Server);

            { // check connection status on output
                System.Diagnostics.Debug.WriteLine("OnConnected " + rdpClient.Connected);
            }

            if (Connected != null)
            {
                Connected(this, e, this._listIndex);
            }
        }

        void rdpClient_OnConnecting(object sender, EventArgs e)
        {
            Status("Connecting to " + this._sd.Server);

            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;

            { // check connection status on output
                System.Diagnostics.Debug.WriteLine("OnConnecting " + rdpClient.Connected);
            }

            if (Connecting != null)
            {
                Connecting(this, e, this._listIndex);
            }
        }

        #endregion

        #region EVENT: server settings window

        Rectangle ssw_GetClientWindowSize()
        {
            return rdpClient.RectangleToScreen(rdpClient.ClientRectangle);
        }

        void ssw_ApplySettings(object sender, Database.ServerDetails sd)
        {
            this._sd = sd;

            MessageBox.Show("This will restart your connection", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            Reconnect(true, false, false);
        }

        #endregion

        #region EVENT: other form controls

        void tmrSC_Tick(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = GetCurrentScreen();
        }

        void ToolbarButtons_Click(object sender, EventArgs e)
        {
            if (sender == btnDisconnect)
            {
                Disconnect();
            }
            else if (sender == btnConnect)
            {
                Connect();
            }
            else if (sender == btnReconnect)
            {
                Reconnect(false, this._isFitToWindow, false);
            }
            else if (sender == btnSettings)
            {
                ServerSettingsWindow ssw = new ServerSettingsWindow(this._sd);

                ssw.ApplySettings += new ApplySettings(ssw_ApplySettings);
                ssw.GetClientWindowSize += new GetClientWindowSize(ssw_GetClientWindowSize);
                ssw.ShowDialog();

                this._sd = ssw.CurrentServerSettings();

                if (ServerSettingsChanged != null)
                {
                    ServerSettingsChanged(sender, this._sd, this._listIndex);
                }
            }
            else if (sender == btnFullscreen)
            {
                DialogResult dr = MessageBox.Show("You are about to enter in Fullscreen mode.\r\nBy default, the remote desktop resolution will be the same as what you see on the window.\r\n\r\nWould you like to resize it automatically based on your screen resolution though it will be permanent as soon as you leave in Fullscreen.\r\n\r\nNote: This will reconnect.", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    Reconnect(false, false, true);
                }
                else
                {
                    rdpClient.FullScreen = true;
                }
            }
            else if (sender == m_FTS_FitToScreen)
            {
                DialogResult dr = MessageBox.Show("This will resize the server resolution based on this current client window size, though it will not affect you current settings.\r\n\r\nDo you want to continue?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dr == DialogResult.OK)
                {
                    Reconnect(true, true, false);
                }
            }
            else if (sender == m_FTS_Strech)
            {
                if (int.Parse(m_FTS_Strech.Tag.ToString()) == 0)
                {
                    rdpClient.AdvancedSettings3.SmartSizing = true;
                    m_FTS_Strech.Text = "Don't Stretch";
                    m_FTS_Strech.Tag = 1;
                }
                else
                {
                    rdpClient.AdvancedSettings3.SmartSizing = false;
                    m_FTS_Strech.Text = "Stretch";
                    m_FTS_Strech.Tag = 0;
                }
            }
        }

        void RdpClientWindow_Shown(object sender, EventArgs e)
        {
            if (OnFormShown != null)
            {
                OnFormShown(this, e, this._listIndex, this.Handle);
            }

            // stretch RD view
            ToolbarButtons_Click(m_FTS_Strech, null);
        }

        void RdpClientWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to close this window?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                Disconnect();
                rdpClient.Dispose();

                if (OnFormClosing != null)
                {
                    OnFormClosing(this, e, this._listIndex, this.Handle);
                }

                Dispose();
            }
            else
            {
                e.Cancel = true;
            }
        }

        void RdpClientWindow_Activated(object sender, EventArgs e)
        {
            this.rdpClient.Focus();

            if (OnFormActivated != null)
            {
                OnFormActivated(this, e, this._listIndex, this.Handle);
            }
        }

        #endregion

        #region METHOD: s

        public void Connect()
        {
            Status("Starting ...");
            rdpClient.Connect();
        }

        public void Disconnect()
        {
            Status("Disconnecting ...");
            rdpClient.DisconnectedText = "Disconnected";

            if (rdpClient.Connected != 0)
            {
                rdpClient.Disconnect();
            }
        }

        public void Reconnect(bool hasChanges, bool isFitToWindow, bool isFullscreen)
        {
            Disconnect();

            Status("Waiting for the server to properly disconnect ...");

            // wait for the server to properly disconnect
            while (rdpClient.Connected != 0)
            {
                System.Threading.Thread.Sleep(1000);
                Application.DoEvents();
            }

            Status("Reconnecting ...");

            if (hasChanges)
            {
                rdpClient.Server = this._sd.Server;
                rdpClient.UserName = this._sd.Username;
                rdpClient.AdvancedSettings2.ClearTextPassword = this._sd.Password;
                rdpClient.ColorDepth = this._sd.ColorDepth;

                this._isFitToWindow = isFitToWindow;

                if (isFitToWindow)
                {
                    rdpClient.DesktopWidth = this.rdpClient.Width;
                    rdpClient.DesktopHeight = this.rdpClient.Height;
                }
                else
                {
                    rdpClient.DesktopWidth = this._sd.DesktopWidth;
                    rdpClient.DesktopHeight = this._sd.DesktopHeight;
                }

                rdpClient.FullScreen = this._sd.Fullscreen;
            }

            if (isFullscreen)
            {
                rdpClient.DesktopWidth = Screen.PrimaryScreen.Bounds.Width;
                rdpClient.DesktopHeight = Screen.PrimaryScreen.Bounds.Height;

                rdpClient.FullScreen = true;
            }

            Connect();
        }

        public Image GetCurrentScreen()
        {
            return APIs.ControlToImage.GetControlScreenshot(this.panel2);
        }

        private void Status(string stat)
        {
            lblStatus.Text = stat;
        }

        #endregion

        #region PROPERTY

        public int ListIndex
        {
            get
            {
                return this._listIndex;
            }
            set
            {
                this._listIndex = value;
            }
        }

        #endregion
    }
}