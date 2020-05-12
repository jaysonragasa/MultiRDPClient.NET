namespace CommonTools
{
	partial class PropertyDialog
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
			this.m_mainPanel = new System.Windows.Forms.Panel();
			this.m_rightPanel = new System.Windows.Forms.Panel();
			this.m_label = new System.Windows.Forms.Label();
			this.m_treeView = new System.Windows.Forms.TreeView();
			this.m_cancel = new System.Windows.Forms.Button();
			this.m_ok = new System.Windows.Forms.Button();
			this.m_viewPanel = new CommonTools.ViewMap();
			this.line1 = new CommonTools.Line();
			this.m_mainPanel.SuspendLayout();
			this.m_rightPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_mainPanel
			// 
			this.m_mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_mainPanel.Controls.Add(this.m_rightPanel);
			this.m_mainPanel.Controls.Add(this.m_treeView);
			this.m_mainPanel.Location = new System.Drawing.Point(4, 5);
			this.m_mainPanel.Name = "m_mainPanel";
			this.m_mainPanel.Size = new System.Drawing.Size(445, 186);
			this.m_mainPanel.TabIndex = 0;
			// 
			// m_rightPanel
			// 
			this.m_rightPanel.Controls.Add(this.m_label);
			this.m_rightPanel.Controls.Add(this.m_viewPanel);
			this.m_rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_rightPanel.Location = new System.Drawing.Point(178, 0);
			this.m_rightPanel.Name = "m_rightPanel";
			this.m_rightPanel.Size = new System.Drawing.Size(267, 186);
			this.m_rightPanel.TabIndex = 2;
			// 
			// m_label
			// 
			this.m_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_label.BackColor = System.Drawing.SystemColors.ControlDark;
			this.m_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_label.ForeColor = System.Drawing.SystemColors.Control;
			this.m_label.Location = new System.Drawing.Point(4, 0);
			this.m_label.Margin = new System.Windows.Forms.Padding(3);
			this.m_label.Name = "m_label";
			this.m_label.Size = new System.Drawing.Size(263, 20);
			this.m_label.TabIndex = 1;
			this.m_label.Text = "label1";
			this.m_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// m_treeView
			// 
			this.m_treeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.m_treeView.FullRowSelect = true;
			this.m_treeView.Location = new System.Drawing.Point(0, 0);
			this.m_treeView.Name = "m_treeView";
			this.m_treeView.Size = new System.Drawing.Size(178, 186);
			this.m_treeView.TabIndex = 0;
			this.m_treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterTreeSelect);
			// 
			// m_cancel
			// 
			this.m_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_cancel.Location = new System.Drawing.Point(374, 202);
			this.m_cancel.Name = "m_cancel";
			this.m_cancel.Size = new System.Drawing.Size(75, 23);
			this.m_cancel.TabIndex = 1;
			this.m_cancel.Text = "Cancel";
			this.m_cancel.UseVisualStyleBackColor = true;
			// 
			// m_ok
			// 
			this.m_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_ok.Location = new System.Drawing.Point(293, 202);
			this.m_ok.Name = "m_ok";
			this.m_ok.Size = new System.Drawing.Size(75, 23);
			this.m_ok.TabIndex = 1;
			this.m_ok.Text = "OK";
			this.m_ok.UseVisualStyleBackColor = true;
			// 
			// m_viewPanel
			// 
			this.m_viewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_viewPanel.CurKey = null;
			this.m_viewPanel.Location = new System.Drawing.Point(4, 26);
			this.m_viewPanel.Name = "m_viewPanel";
			this.m_viewPanel.Size = new System.Drawing.Size(263, 160);
			this.m_viewPanel.TabIndex = 2;
			// 
			// line1
			// 
			this.line1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.line1.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.line1.LinePositions = System.Windows.Forms.AnchorStyles.Top;
			this.line1.Location = new System.Drawing.Point(4, 196);
			this.line1.Name = "line1";
			this.line1.Size = new System.Drawing.Size(446, 10);
			this.line1.TabIndex = 2;
			this.line1.TabStop = false;
			this.line1.Text = "line1";
			// 
			// PropertyDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(454, 233);
			this.Controls.Add(this.m_ok);
			this.Controls.Add(this.m_cancel);
			this.Controls.Add(this.m_mainPanel);
			this.Controls.Add(this.line1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PropertyDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "PropertyDialog";
			this.m_mainPanel.ResumeLayout(false);
			this.m_rightPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel m_mainPanel;
		private System.Windows.Forms.Label m_label;
		private System.Windows.Forms.Panel m_rightPanel;
		private System.Windows.Forms.Button m_cancel;
		private System.Windows.Forms.Button m_ok;
		private ViewMap m_viewPanel;
		private Line line1;
		protected System.Windows.Forms.TreeView m_treeView;

	}
}