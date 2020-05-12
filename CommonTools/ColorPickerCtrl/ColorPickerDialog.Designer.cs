namespace CommonTools
{
	partial class ColorPickerDialog
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
			this.m_tabControl = new System.Windows.Forms.TabControl();
			this.m_colorWheelTabPage = new System.Windows.Forms.TabPage();
			this.m_knownColorsTabPage = new System.Windows.Forms.TabPage();
			this.m_colorList = new CommonTools.ColorListBox();
			this.m_systemColorsTabPage = new System.Windows.Forms.TabPage();
			this.m_systemColorList = new CommonTools.SystemColorListBox();
			this.m_cancel = new System.Windows.Forms.Button();
			this.m_ok = new System.Windows.Forms.Button();
			this.m_tabControl.SuspendLayout();
			this.m_knownColorsTabPage.SuspendLayout();
			this.m_systemColorsTabPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_tabControl
			// 
			this.m_tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_tabControl.Controls.Add(this.m_colorWheelTabPage);
			this.m_tabControl.Controls.Add(this.m_knownColorsTabPage);
			this.m_tabControl.Controls.Add(this.m_systemColorsTabPage);
			this.m_tabControl.Location = new System.Drawing.Point(4, 5);
			this.m_tabControl.Name = "m_tabControl";
			this.m_tabControl.SelectedIndex = 0;
			this.m_tabControl.Size = new System.Drawing.Size(527, 282);
			this.m_tabControl.TabIndex = 1;
			this.m_tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnSelected);
			// 
			// m_colorWheelTabPage
			// 
			this.m_colorWheelTabPage.Location = new System.Drawing.Point(4, 22);
			this.m_colorWheelTabPage.Name = "m_colorWheelTabPage";
			this.m_colorWheelTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.m_colorWheelTabPage.Size = new System.Drawing.Size(519, 256);
			this.m_colorWheelTabPage.TabIndex = 0;
			this.m_colorWheelTabPage.Text = "Colors";
			this.m_colorWheelTabPage.UseVisualStyleBackColor = true;
			// 
			// m_knownColorsTabPage
			// 
			this.m_knownColorsTabPage.Controls.Add(this.m_colorList);
			this.m_knownColorsTabPage.Location = new System.Drawing.Point(4, 22);
			this.m_knownColorsTabPage.Name = "m_knownColorsTabPage";
			this.m_knownColorsTabPage.Size = new System.Drawing.Size(519, 256);
			this.m_knownColorsTabPage.TabIndex = 1;
			this.m_knownColorsTabPage.Text = "Known Colors";
			this.m_knownColorsTabPage.UseVisualStyleBackColor = true;
			// 
			// m_colorList
			// 
			this.m_colorList.ColumnWidth = 170;
			this.m_colorList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_colorList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.m_colorList.FormattingEnabled = true;
			this.m_colorList.IntegralHeight = false;
			this.m_colorList.ItemHeight = 25;
			this.m_colorList.Location = new System.Drawing.Point(0, 0);
			this.m_colorList.MultiColumn = true;
			this.m_colorList.Name = "m_colorList";
			this.m_colorList.SelectedColor = System.Drawing.Color.AliceBlue;
			this.m_colorList.Size = new System.Drawing.Size(519, 256);
			this.m_colorList.TabIndex = 0;
			this.m_colorList.SelectedIndexChanged += new System.EventHandler(this.OnItemChanged);
			// 
			// m_systemColorsTabPage
			// 
			this.m_systemColorsTabPage.Controls.Add(this.m_systemColorList);
			this.m_systemColorsTabPage.Location = new System.Drawing.Point(4, 22);
			this.m_systemColorsTabPage.Name = "m_systemColorsTabPage";
			this.m_systemColorsTabPage.Size = new System.Drawing.Size(519, 256);
			this.m_systemColorsTabPage.TabIndex = 2;
			this.m_systemColorsTabPage.Text = "System Colors";
			this.m_systemColorsTabPage.UseVisualStyleBackColor = true;
			// 
			// m_systemColorList
			// 
			this.m_systemColorList.ColumnWidth = 170;
			this.m_systemColorList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_systemColorList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.m_systemColorList.FormattingEnabled = true;
			this.m_systemColorList.IntegralHeight = false;
			this.m_systemColorList.ItemHeight = 25;
			this.m_systemColorList.Location = new System.Drawing.Point(0, 0);
			this.m_systemColorList.MultiColumn = true;
			this.m_systemColorList.Name = "m_systemColorList";
			this.m_systemColorList.SelectedColor = System.Drawing.SystemColors.ActiveBorder;
			this.m_systemColorList.Size = new System.Drawing.Size(519, 256);
			this.m_systemColorList.TabIndex = 0;
			this.m_systemColorList.SelectedIndexChanged += new System.EventHandler(this.OnItemChanged);
			// 
			// m_cancel
			// 
			this.m_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_cancel.Location = new System.Drawing.Point(452, 293);
			this.m_cancel.Name = "m_cancel";
			this.m_cancel.Size = new System.Drawing.Size(75, 23);
			this.m_cancel.TabIndex = 2;
			this.m_cancel.Text = "&Cancel";
			this.m_cancel.UseVisualStyleBackColor = true;
			// 
			// m_ok
			// 
			this.m_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_ok.Location = new System.Drawing.Point(371, 293);
			this.m_ok.Name = "m_ok";
			this.m_ok.Size = new System.Drawing.Size(75, 23);
			this.m_ok.TabIndex = 2;
			this.m_ok.Text = "&OK";
			this.m_ok.UseVisualStyleBackColor = true;
			// 
			// ColorPickerDialog
			// 
			this.AcceptButton = this.m_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.m_cancel;
			this.ClientSize = new System.Drawing.Size(534, 326);
			this.Controls.Add(this.m_ok);
			this.Controls.Add(this.m_cancel);
			this.Controls.Add(this.m_tabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ColorPickerDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Color Picker";
			this.m_tabControl.ResumeLayout(false);
			this.m_knownColorsTabPage.ResumeLayout(false);
			this.m_systemColorsTabPage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl m_tabControl;
		private System.Windows.Forms.TabPage m_knownColorsTabPage;
		private System.Windows.Forms.TabPage m_colorWheelTabPage;
		private System.Windows.Forms.Button m_ok;
		private System.Windows.Forms.Button m_cancel;
		private System.Windows.Forms.TabPage m_systemColorsTabPage;
		private CommonTools.ColorListBox m_colorList;
		private CommonTools.SystemColorListBox m_systemColorList;
	}
}

