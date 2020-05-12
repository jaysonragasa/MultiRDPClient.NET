namespace MultiRemoteDesktopClient.Controls
{
    partial class RDThumbnail
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
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelDrawing = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnConnect = new System.Windows.Forms.ToolStripButton();
            this.btnDisconnect = new System.Windows.Forms.ToolStripButton();
            this.btnReconnect = new System.Windows.Forms.ToolStripButton();
            this.btnFullscreen = new System.Windows.Forms.ToolStripButton();
            this.btnFitToScreen = new System.Windows.Forms.ToolStripDropDownButton();
            this.m_FTS_FitToScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FTS_Strech = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 19);
            this.panel1.TabIndex = 0;
            // 
            // panelDrawing
            // 
            this.panelDrawing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelDrawing.Location = new System.Drawing.Point(6, 56);
            this.panelDrawing.Name = "panelDrawing";
            this.panelDrawing.Size = new System.Drawing.Size(238, 185);
            this.panelDrawing.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(3, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(35, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "label1";
            // 
            // panelToolbar
            // 
            this.panelToolbar.Controls.Add(this.toolStrip1);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Location = new System.Drawing.Point(0, 19);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(250, 31);
            this.panelToolbar.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnect,
            this.btnDisconnect,
            this.btnReconnect,
            this.btnFullscreen,
            this.toolStripSeparator1,
            this.btnFitToScreen,
            this.btnSettings});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(250, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            this.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSettings.Image = global::MultiRemoteDesktopClient.Properties.Resources.filemgmt_dll_I00ec_0409_16;
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(23, 22);
            this.btnSettings.Text = "&Settings";
            this.btnSettings.ToolTipText = "Show connection settings";
            // 
            // RDThumbnail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDrawing);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.panel1);
            this.Name = "RDThumbnail";
            this.Size = new System.Drawing.Size(250, 247);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelDrawing;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnConnect;
        private System.Windows.Forms.ToolStripButton btnDisconnect;
        private System.Windows.Forms.ToolStripButton btnReconnect;
        private System.Windows.Forms.ToolStripButton btnFullscreen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton btnFitToScreen;
        private System.Windows.Forms.ToolStripMenuItem m_FTS_FitToScreen;
        private System.Windows.Forms.ToolStripMenuItem m_FTS_Strech;
        private System.Windows.Forms.ToolStripButton btnSettings;
    }
}
