namespace MultiRemoteDesktopClient
{
    partial class PasswordWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordWindow));
            this.label3 = new System.Windows.Forms.Label();
            this.txPassword = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupboxCAPTCHA = new System.Windows.Forms.GroupBox();
            this.btnRenewCAPTCHA = new System.Windows.Forms.Button();
            this.captcha1 = new CAPTCHA.Captcha();
            this.label5 = new System.Windows.Forms.Label();
            this.txCaptcha = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupboxCAPTCHA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.captcha1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // txPassword
            // 
            this.txPassword.Location = new System.Drawing.Point(71, 85);
            this.txPassword.Name = "txPassword";
            this.txPassword.Size = new System.Drawing.Size(263, 22);
            this.txPassword.TabIndex = 3;
            this.txPassword.UseSystemPasswordChar = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(259, 291);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(178, 291);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "&Start";
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(307, 26);
            this.label4.TabIndex = 2;
            this.label4.Text = "You have entered an incorrect password for 3 times so you\r\nneed to be verified if" +
                " you are Human.";
            // 
            // groupboxCAPTCHA
            // 
            this.groupboxCAPTCHA.Controls.Add(this.btnRenewCAPTCHA);
            this.groupboxCAPTCHA.Controls.Add(this.captcha1);
            this.groupboxCAPTCHA.Controls.Add(this.label4);
            this.groupboxCAPTCHA.Controls.Add(this.label5);
            this.groupboxCAPTCHA.Controls.Add(this.txCaptcha);
            this.groupboxCAPTCHA.Location = new System.Drawing.Point(12, 123);
            this.groupboxCAPTCHA.Name = "groupboxCAPTCHA";
            this.groupboxCAPTCHA.Size = new System.Drawing.Size(322, 157);
            this.groupboxCAPTCHA.TabIndex = 6;
            this.groupboxCAPTCHA.TabStop = false;
            this.groupboxCAPTCHA.Text = "CAPTCHA Verification";
            this.groupboxCAPTCHA.Visible = false;
            // 
            // btnRenewCAPTCHA
            // 
            this.btnRenewCAPTCHA.FlatAppearance.BorderSize = 0;
            this.btnRenewCAPTCHA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenewCAPTCHA.Image = global::MultiRemoteDesktopClient.Properties.Resources.Refresh;
            this.btnRenewCAPTCHA.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRenewCAPTCHA.Location = new System.Drawing.Point(241, 48);
            this.btnRenewCAPTCHA.Name = "btnRenewCAPTCHA";
            this.btnRenewCAPTCHA.Size = new System.Drawing.Size(72, 48);
            this.btnRenewCAPTCHA.TabIndex = 5;
            this.btnRenewCAPTCHA.Text = "Renew";
            this.btnRenewCAPTCHA.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRenewCAPTCHA.UseVisualStyleBackColor = true;
            // 
            // captcha1
            // 
            this.captcha1.Image = ((System.Drawing.Image)(resources.GetObject("captcha1.Image")));
            this.captcha1.Location = new System.Drawing.Point(10, 48);
            this.captcha1.Name = "captcha1";
            this.captcha1.RandomText = "O4ns#&mL";
            this.captcha1.Size = new System.Drawing.Size(231, 48);
            this.captcha1.TabIndex = 4;
            this.captcha1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Type the above characters.";
            // 
            // txCaptcha
            // 
            this.txCaptcha.Location = new System.Drawing.Point(10, 126);
            this.txCaptcha.Name = "txCaptcha";
            this.txCaptcha.Size = new System.Drawing.Size(303, 22);
            this.txCaptcha.TabIndex = 3;
            this.txCaptcha.UseSystemPasswordChar = true;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::MultiRemoteDesktopClient.Properties.Resources.RDP_Blank_header;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(346, 70);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(68, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Welcome to Multi Remote Desktop Client .NET\r\nPlease enter your password to start " +
                "this application.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(66, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Password";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::MultiRemoteDesktopClient.Properties.Resources.password_48;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // PasswordWindow
            // 
            this.AcceptButton = this.btnGo;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(346, 326);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupboxCAPTCHA);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password";
            this.groupboxCAPTCHA.ResumeLayout(false);
            this.groupboxCAPTCHA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.captcha1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txPassword;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupboxCAPTCHA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txCaptcha;
        private CAPTCHA.Captcha captcha1;
        private System.Windows.Forms.Button btnRenewCAPTCHA;
    }
}