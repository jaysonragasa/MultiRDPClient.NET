namespace MultiRemoteDesktopClient
{
    partial class RdpClientWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            try // cannot access a disposed object;
            {
                base.Dispose(disposing);
            }
            catch (System.Exception ex)
            {
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RdpClientWindow));
            this.tmrSC = new System.Windows.Forms.Timer(this.components);
            this.rdpPanelBase = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnSndKey_TaskManager = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnConnect = new System.Windows.Forms.ToolStripButton();
            this.btnDisconnect = new System.Windows.Forms.ToolStripButton();
            this.btnReconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFullscreen = new System.Windows.Forms.ToolStripButton();
            this.btnFitToScreen = new System.Windows.Forms.ToolStripDropDownButton();
            this.m_FTS_FitToScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FTS_Strech = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPopout_in = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rdpClient = new AxMSTSCLib.AxMsRdpClient2();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdpClient)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrSC
            // 
            this.tmrSC.Interval = 2000;
            // 
            // rdpPanelBase
            // 
            this.rdpPanelBase.Location = new System.Drawing.Point(463, 28);
            this.rdpPanelBase.Name = "rdpPanelBase";
            this.rdpPanelBase.Size = new System.Drawing.Size(144, 103);
            this.rdpPanelBase.TabIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator2,
            this.btnConnect,
            this.btnDisconnect,
            this.btnReconnect,
            this.toolStripSeparator1,
            this.btnFullscreen,
            this.btnFitToScreen,
            this.btnSettings,
            this.toolStripSeparator3,
            this.btnPopout_in});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(619, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSndKey_TaskManager});
            this.toolStripDropDownButton1.Image = global::MultiRemoteDesktopClient.Properties.Resources.keyboard_16;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(89, 22);
            this.toolStripDropDownButton1.Text = "Send Keys";
            this.toolStripDropDownButton1.Visible = false;
            // 
            // btnSndKey_TaskManager
            // 
            this.btnSndKey_TaskManager.Image = global::MultiRemoteDesktopClient.Properties.Resources.sysmon_16;
            this.btnSndKey_TaskManager.Name = "btnSndKey_TaskManager";
            this.btnSndKey_TaskManager.Size = new System.Drawing.Size(159, 22);
            this.btnSndKey_TaskManager.Text = "CLTR+ALT+DEL";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnConnect
            // 
            this.btnConnect.Image = global::MultiRemoteDesktopClient.Properties.Resources.mstscax_dll_I345e_0409_16;
            this.btnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(72, 22);
            this.btnConnect.Text = "&Connect";
            this.btnConnect.ToolTipText = "Connect to server";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDisconnect.Image = global::MultiRemoteDesktopClient.Properties.Resources.Shutdown_16;
            this.btnDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(23, 22);
            this.btnDisconnect.Text = "&Disconnect";
            this.btnDisconnect.ToolTipText = "Disconnect to server";
            // 
            // btnReconnect
            // 
            this.btnReconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReconnect.Image = global::MultiRemoteDesktopClient.Properties.Resources.Restart_16;
            this.btnReconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReconnect.Name = "btnReconnect";
            this.btnReconnect.Size = new System.Drawing.Size(23, 22);
            this.btnReconnect.Text = "&Reconnect";
            this.btnReconnect.ToolTipText = "Restart the connection";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFullscreen
            // 
            this.btnFullscreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFullscreen.Image = global::MultiRemoteDesktopClient.Properties.Resources.ehSSO_dll_I00dc_0409_16;
            this.btnFullscreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFullscreen.Name = "btnFullscreen";
            this.btnFullscreen.Size = new System.Drawing.Size(23, 22);
            this.btnFullscreen.Text = "&Full Screen";
            // 
            // btnFitToScreen
            // 
            this.btnFitToScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFitToScreen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_FTS_FitToScreen,
            this.m_FTS_Strech});
            this.btnFitToScreen.Image = global::MultiRemoteDesktopClient.Properties.Resources.fit_16;
            this.btnFitToScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFitToScreen.Name = "btnFitToScreen";
            this.btnFitToScreen.Size = new System.Drawing.Size(29, 22);
            this.btnFitToScreen.Text = "Fit to &Window";
            // 
            // m_FTS_FitToScreen
            // 
            this.m_FTS_FitToScreen.Name = "m_FTS_FitToScreen";
            this.m_FTS_FitToScreen.Size = new System.Drawing.Size(148, 22);
            this.m_FTS_FitToScreen.Text = "Fit to Window";
            // 
            // m_FTS_Strech
            // 
            this.m_FTS_Strech.Name = "m_FTS_Strech";
            this.m_FTS_Strech.Size = new System.Drawing.Size(148, 22);
            this.m_FTS_Strech.Tag = "0";
            this.m_FTS_Strech.Text = "Stretch";
            // 
            // btnSettings
            // 
            this.btnSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSettings.Image = global::MultiRemoteDesktopClient.Properties.Resources.filemgmt_dll_I00ec_0409_16;
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(69, 22);
            this.btnSettings.Text = "&Settings";
            this.btnSettings.ToolTipText = "Show connection settings";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPopout_in
            // 
            this.btnPopout_in.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnPopout_in.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPopout_in.Image = global::MultiRemoteDesktopClient.Properties.Resources.pop_out_16;
            this.btnPopout_in.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPopout_in.Name = "btnPopout_in";
            this.btnPopout_in.Size = new System.Drawing.Size(23, 22);
            this.btnPopout_in.Tag = "0";
            this.btnPopout_in.Text = "Pop out";
            this.btnPopout_in.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 401);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(619, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(38, 17);
            this.lblStatus.Text = "Idle ...";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.rdpClient);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(619, 376);
            this.panel2.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(204, 184);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(3, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(198, 159);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // rdpClient
            // 
            this.rdpClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdpClient.Enabled = true;
            this.rdpClient.Location = new System.Drawing.Point(0, 0);
            this.rdpClient.Name = "rdpClient";
            this.rdpClient.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rdpClient.OcxState")));
            this.rdpClient.Size = new System.Drawing.Size(619, 376);
            this.rdpClient.TabIndex = 0;
            // 
            // RdpClientWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 423);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.rdpPanelBase);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RdpClientWindow";
            this.Tag = "Remote Desktop Client - {0}@{1}[{2}]";
            this.Text = "Remote Desktop Client";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdpClient)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrSC;
        private System.Windows.Forms.Panel rdpPanelBase;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem btnSndKey_TaskManager;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnConnect;
        private System.Windows.Forms.ToolStripButton btnDisconnect;
        private System.Windows.Forms.ToolStripButton btnReconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFullscreen;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnPopout_in;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        public AxMSTSCLib.AxMsRdpClient2 rdpClient;
        private System.Windows.Forms.ToolStripDropDownButton btnFitToScreen;
        private System.Windows.Forms.ToolStripMenuItem m_FTS_FitToScreen;
        private System.Windows.Forms.ToolStripMenuItem m_FTS_Strech;
    }
}

