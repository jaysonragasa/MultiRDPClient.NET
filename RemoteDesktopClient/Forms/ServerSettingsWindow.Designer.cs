namespace MultiRemoteDesktopClient
{
    partial class ServerSettingsWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerSettingsWindow));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddGroup = new System.Windows.Forms.ComboBox();
            this.txDescription = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txDomain = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txPort = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txServername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureColor = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbColor = new System.Windows.Forms.TrackBar();
            this.lblColorDepth = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetClientWinS = new System.Windows.Forms.Button();
            this.cbFullscreen = new System.Windows.Forms.CheckBox();
            this.txHeight = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txWidth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblResolution = new System.Windows.Forms.Label();
            this.tbDeskSize = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.txComputer = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbColor)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDeskSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 76);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(387, 311);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(379, 285);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txComputer);
            this.groupBox1.Controls.Add(this.ddGroup);
            this.groupBox1.Controls.Add(this.txDescription);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txDomain);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txPassword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txUsername);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txPort);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txServername);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 273);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logon Settings";
            // 
            // ddGroup
            // 
            this.ddGroup.FormattingEnabled = true;
            this.ddGroup.Location = new System.Drawing.Point(160, 236);
            this.ddGroup.Name = "ddGroup";
            this.ddGroup.Size = new System.Drawing.Size(194, 21);
            this.ddGroup.TabIndex = 6;
            // 
            // txDescription
            // 
            this.txDescription.Location = new System.Drawing.Point(67, 180);
            this.txDescription.Multiline = true;
            this.txDescription.Name = "txDescription";
            this.txDescription.Size = new System.Drawing.Size(287, 50);
            this.txDescription.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(64, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Description";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(64, 239);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Group";
            // 
            // txDomain
            // 
            this.txDomain.Location = new System.Drawing.Point(160, 133);
            this.txDomain.Name = "txDomain";
            this.txDomain.Size = new System.Drawing.Size(194, 22);
            this.txDomain.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Domain";
            // 
            // txPassword
            // 
            this.txPassword.Location = new System.Drawing.Point(160, 105);
            this.txPassword.Name = "txPassword";
            this.txPassword.Size = new System.Drawing.Size(194, 22);
            this.txPassword.TabIndex = 3;
            this.txPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Password";
            // 
            // txUsername
            // 
            this.txUsername.Location = new System.Drawing.Point(160, 77);
            this.txUsername.Name = "txUsername";
            this.txUsername.Size = new System.Drawing.Size(194, 22);
            this.txUsername.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Username";
            // 
            // txPort
            // 
            this.txPort.Location = new System.Drawing.Point(309, 49);
            this.txPort.Name = "txPort";
            this.txPort.Size = new System.Drawing.Size(45, 22);
            this.txPort.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(275, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Computer";
            // 
            // txServername
            // 
            this.txServername.Location = new System.Drawing.Point(160, 21);
            this.txServername.Name = "txServername";
            this.txServername.Size = new System.Drawing.Size(194, 22);
            this.txServername.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Name";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MultiRemoteDesktopClient.Properties.Resources.logon_settings_48;
            this.pictureBox1.Location = new System.Drawing.Point(11, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(379, 285);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Display";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureColor);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.tbColor);
            this.groupBox3.Controls.Add(this.lblColorDepth);
            this.groupBox3.Location = new System.Drawing.Point(6, 180);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(367, 99);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Colors";
            // 
            // pictureColor
            // 
            this.pictureColor.BackgroundImage = global::MultiRemoteDesktopClient.Properties.Resources.color_16_48;
            this.pictureColor.Location = new System.Drawing.Point(11, 21);
            this.pictureColor.Name = "pictureColor";
            this.pictureColor.Size = new System.Drawing.Size(48, 48);
            this.pictureColor.TabIndex = 2;
            this.pictureColor.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(65, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Choose color settings";
            // 
            // tbColor
            // 
            this.tbColor.LargeChange = 1;
            this.tbColor.Location = new System.Drawing.Point(65, 37);
            this.tbColor.Maximum = 4;
            this.tbColor.Minimum = 1;
            this.tbColor.Name = "tbColor";
            this.tbColor.Size = new System.Drawing.Size(170, 45);
            this.tbColor.TabIndex = 10;
            this.tbColor.Value = 2;
            // 
            // lblColorDepth
            // 
            this.lblColorDepth.AutoSize = true;
            this.lblColorDepth.Location = new System.Drawing.Point(248, 40);
            this.lblColorDepth.Name = "lblColorDepth";
            this.lblColorDepth.Size = new System.Drawing.Size(101, 13);
            this.lblColorDepth.TabIndex = 4;
            this.lblColorDepth.Text = "High Color (16 bit)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGetClientWinS);
            this.groupBox2.Controls.Add(this.cbFullscreen);
            this.groupBox2.Controls.Add(this.txHeight);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txWidth);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.lblResolution);
            this.groupBox2.Controls.Add(this.tbDeskSize);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(367, 168);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Desktop Size";
            // 
            // btnGetClientWinS
            // 
            this.btnGetClientWinS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGetClientWinS.Location = new System.Drawing.Point(251, 112);
            this.btnGetClientWinS.Name = "btnGetClientWinS";
            this.btnGetClientWinS.Size = new System.Drawing.Size(110, 36);
            this.btnGetClientWinS.TabIndex = 10;
            this.btnGetClientWinS.Text = "Get client window size";
            this.btnGetClientWinS.UseVisualStyleBackColor = true;
            // 
            // cbFullscreen
            // 
            this.cbFullscreen.AutoSize = true;
            this.cbFullscreen.Location = new System.Drawing.Point(251, 69);
            this.cbFullscreen.Name = "cbFullscreen";
            this.cbFullscreen.Size = new System.Drawing.Size(78, 17);
            this.cbFullscreen.TabIndex = 7;
            this.cbFullscreen.Text = "Fullscreen";
            this.cbFullscreen.UseVisualStyleBackColor = true;
            // 
            // txHeight
            // 
            this.txHeight.Location = new System.Drawing.Point(122, 133);
            this.txHeight.Name = "txHeight";
            this.txHeight.Size = new System.Drawing.Size(116, 22);
            this.txHeight.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(65, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Height";
            // 
            // txWidth
            // 
            this.txWidth.Location = new System.Drawing.Point(122, 105);
            this.txWidth.Name = "txWidth";
            this.txWidth.Size = new System.Drawing.Size(116, 22);
            this.txWidth.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(65, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Width";
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(248, 49);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(99, 13);
            this.lblResolution.TabIndex = 4;
            this.lblResolution.Text = "1120 by 700 pixels";
            // 
            // tbDeskSize
            // 
            this.tbDeskSize.LargeChange = 1;
            this.tbDeskSize.Location = new System.Drawing.Point(68, 46);
            this.tbDeskSize.Maximum = 7;
            this.tbDeskSize.Minimum = 1;
            this.tbDeskSize.Name = "tbDeskSize";
            this.tbDeskSize.Size = new System.Drawing.Size(170, 45);
            this.tbDeskSize.TabIndex = 6;
            this.tbDeskSize.Value = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(65, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(182, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "You can enter a custom resolution";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(65, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(173, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Choose the size of your desktop";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::MultiRemoteDesktopClient.Properties.Resources.imageres_dll_I006e_0409_48;
            this.pictureBox2.Location = new System.Drawing.Point(11, 21);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(318, 392);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(237, 392);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "color_full_48.ico");
            this.imageList1.Images.SetKeyName(1, "color_16_48.ico");
            this.imageList1.Images.SetKeyName(2, "color_8_48.ico");
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::MultiRemoteDesktopClient.Properties.Resources.RDC_Header;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(399, 70);
            this.panel1.TabIndex = 0;
            // 
            // txComputer
            // 
            this.txComputer.FormattingEnabled = true;
            this.txComputer.Location = new System.Drawing.Point(160, 49);
            this.txComputer.Name = "txComputer";
            this.txComputer.Size = new System.Drawing.Size(109, 21);
            this.txComputer.TabIndex = 7;
            // 
            // ServerSettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(399, 421);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerSettingsWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server Settings";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbColor)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDeskSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txServername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txDomain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.TrackBar tbDeskSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txHeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cbFullscreen;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureColor;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar tbColor;
        private System.Windows.Forms.Label lblColorDepth;
        private System.Windows.Forms.Button btnGetClientWinS;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox ddGroup;
        private System.Windows.Forms.TextBox txPort;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox txComputer;
    }
}