namespace GitBranchView
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemVersion = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.labelInfo = new System.Windows.Forms.Label();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.panelScroll = new System.Windows.Forms.Panel();
			this.buttonClose = new System.Windows.Forms.Button();
			this.pictureBoxTitle = new System.Windows.Forms.PictureBox();
			this.pictureBoxBackground = new System.Windows.Forms.PictureBox();
			this.contextMenuStrip.SuspendLayout();
			this.panelScroll.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTitle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).BeginInit();
			this.pictureBoxBackground.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Git Branch View";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseUp);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemVersion,
            this.toolStripSeparator3,
            this.toolStripMenuItemSettings,
            this.toolStripSeparator2,
            this.toolStripMenuItemExit});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(126, 82);
			// 
			// toolStripMenuItemVersion
			// 
			this.toolStripMenuItemVersion.Enabled = false;
			this.toolStripMenuItemVersion.Name = "toolStripMenuItemVersion";
			this.toolStripMenuItemVersion.Size = new System.Drawing.Size(125, 22);
			this.toolStripMenuItemVersion.Text = "Version ...";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(122, 6);
			// 
			// toolStripMenuItemSettings
			// 
			this.toolStripMenuItemSettings.Name = "toolStripMenuItemSettings";
			this.toolStripMenuItemSettings.Size = new System.Drawing.Size(125, 22);
			this.toolStripMenuItemSettings.Text = "Settings...";
			this.toolStripMenuItemSettings.Click += new System.EventHandler(this.ToolStripMenuItemSettings_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(122, 6);
			// 
			// toolStripMenuItemExit
			// 
			this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
			this.toolStripMenuItemExit.Size = new System.Drawing.Size(125, 22);
			this.toolStripMenuItemExit.Text = "Exit";
			this.toolStripMenuItemExit.Click += new System.EventHandler(this.ToolStripMenuItemExit_Click);
			// 
			// labelInfo
			// 
			this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelInfo.Location = new System.Drawing.Point(0, 44);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(267, 13);
			this.labelInfo.TabIndex = 1;
			this.labelInfo.Text = "No root folder selected.";
			this.labelInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel.BackColor = System.Drawing.SystemColors.Window;
			this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(267, 117);
			this.flowLayoutPanel.TabIndex = 2;
			// 
			// panelScroll
			// 
			this.panelScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelScroll.AutoScroll = true;
			this.panelScroll.Controls.Add(this.flowLayoutPanel);
			this.panelScroll.Location = new System.Drawing.Point(0, 33);
			this.panelScroll.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.panelScroll.Name = "panelScroll";
			this.panelScroll.Size = new System.Drawing.Size(267, 117);
			this.panelScroll.TabIndex = 3;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.BackColor = System.Drawing.Color.Transparent;
			this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonClose.FlatAppearance.BorderSize = 0;
			this.buttonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
			this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
			this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonClose.Image = global::GitBranchView.Properties.Resources.close;
			this.buttonClose.Location = new System.Drawing.Point(253, 0);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(21, 21);
			this.buttonClose.TabIndex = 6;
			this.buttonClose.UseVisualStyleBackColor = false;
			this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
			// 
			// pictureBoxTitle
			// 
			this.pictureBoxTitle.BackColor = System.Drawing.Color.Transparent;
			this.pictureBoxTitle.Image = global::GitBranchView.Properties.Resources.title;
			this.pictureBoxTitle.Location = new System.Drawing.Point(13, 4);
			this.pictureBoxTitle.Name = "pictureBoxTitle";
			this.pictureBoxTitle.Size = new System.Drawing.Size(118, 13);
			this.pictureBoxTitle.TabIndex = 7;
			this.pictureBoxTitle.TabStop = false;
			// 
			// pictureBoxBackground
			// 
			this.pictureBoxBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
			this.pictureBoxBackground.Controls.Add(this.pictureBoxTitle);
			this.pictureBoxBackground.Controls.Add(this.buttonClose);
			this.pictureBoxBackground.Location = new System.Drawing.Point(-8, -1);
			this.pictureBoxBackground.Name = "pictureBoxBackground";
			this.pictureBoxBackground.Size = new System.Drawing.Size(283, 21);
			this.pictureBoxBackground.TabIndex = 8;
			this.pictureBoxBackground.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(267, 152);
			this.ControlBox = false;
			this.Controls.Add(this.labelInfo);
			this.Controls.Add(this.panelScroll);
			this.Controls.Add(this.pictureBoxBackground);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.contextMenuStrip.ResumeLayout(false);
			this.panelScroll.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTitle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).EndInit();
			this.pictureBoxBackground.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.Label labelInfo;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
		private System.Windows.Forms.Panel panelScroll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemVersion;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSettings;
		private System.Windows.Forms.PictureBox pictureBoxTitle;
		private System.Windows.Forms.PictureBox pictureBoxBackground;
	}
}

