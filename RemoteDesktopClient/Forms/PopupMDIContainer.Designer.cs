namespace MultiRemoteDesktopClient
{
    partial class PopupMDIContainer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupMDIContainer));
            this.tabMDIChild = new Crownwood.Magic.Controls.TabControl();
            this.SuspendLayout();
            // 
            // tabMDIChild
            // 
            this.tabMDIChild.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiDocument;
            this.tabMDIChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMDIChild.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways;
            this.tabMDIChild.IDEPixelBorder = false;
            this.tabMDIChild.Location = new System.Drawing.Point(0, 0);
            this.tabMDIChild.Name = "tabMDIChild";
            this.tabMDIChild.Size = new System.Drawing.Size(624, 444);
            this.tabMDIChild.TabIndex = 0;
            // 
            // PopupMDIContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 444);
            this.Controls.Add(this.tabMDIChild);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "PopupMDIContainer";
            this.Text = "PopupMDIContainer";
            this.ResumeLayout(false);

        }

        #endregion

        private Crownwood.Magic.Controls.TabControl tabMDIChild;

    }
}