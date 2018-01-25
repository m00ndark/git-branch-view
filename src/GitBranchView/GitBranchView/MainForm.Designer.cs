﻿namespace GitBranchView
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
			this.toolStripMenuItemSelectRootFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSelectGitExePath = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSelectLinkCommand = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.labelRootPath = new System.Windows.Forms.Label();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.panelScroll = new System.Windows.Forms.Panel();
			this.groupBoxSeparator = new System.Windows.Forms.GroupBox();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemStartWithWindows = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			this.panelScroll.SuspendLayout();
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
            this.toolStripMenuItemSelectRootFolder,
            this.toolStripMenuItemSelectGitExePath,
            this.toolStripMenuItemSelectLinkCommand,
            this.toolStripSeparator1,
            this.toolStripMenuItemStartWithWindows,
            this.toolStripSeparator2,
            this.toolStripMenuItemExit});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(219, 148);
			// 
			// toolStripMenuItemSelectRootFolder
			// 
			this.toolStripMenuItemSelectRootFolder.Name = "toolStripMenuItemSelectRootFolder";
			this.toolStripMenuItemSelectRootFolder.Size = new System.Drawing.Size(218, 22);
			this.toolStripMenuItemSelectRootFolder.Text = "Select Root Folder...";
			this.toolStripMenuItemSelectRootFolder.Click += new System.EventHandler(this.ToolStripMenuItemSelectRootFolder_Click);
			// 
			// toolStripMenuItemSelectGitExePath
			// 
			this.toolStripMenuItemSelectGitExePath.Name = "toolStripMenuItemSelectGitExePath";
			this.toolStripMenuItemSelectGitExePath.Size = new System.Drawing.Size(218, 22);
			this.toolStripMenuItemSelectGitExePath.Text = "Select Git Executable Path...";
			this.toolStripMenuItemSelectGitExePath.Click += new System.EventHandler(this.ToolStripMenuItemSelectGitExePath_Click);
			// 
			// toolStripMenuItemSelectLinkCommand
			// 
			this.toolStripMenuItemSelectLinkCommand.Name = "toolStripMenuItemSelectLinkCommand";
			this.toolStripMenuItemSelectLinkCommand.Size = new System.Drawing.Size(218, 22);
			this.toolStripMenuItemSelectLinkCommand.Text = "Select Link Command...";
			this.toolStripMenuItemSelectLinkCommand.Click += new System.EventHandler(this.ToolStripMenuItemSelectLinkCommand_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(215, 6);
			// 
			// toolStripMenuItemExit
			// 
			this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
			this.toolStripMenuItemExit.Size = new System.Drawing.Size(218, 22);
			this.toolStripMenuItemExit.Text = "Exit";
			this.toolStripMenuItemExit.Click += new System.EventHandler(this.ToolStripMenuItemExit_Click);
			// 
			// labelRootPath
			// 
			this.labelRootPath.AutoSize = true;
			this.labelRootPath.Location = new System.Drawing.Point(3, 2);
			this.labelRootPath.Name = "labelRootPath";
			this.labelRootPath.Size = new System.Drawing.Size(129, 13);
			this.labelRootPath.TabIndex = 1;
			this.labelRootPath.Text = "No root folder selected.";
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel.BackColor = System.Drawing.SystemColors.Window;
			this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(254, 72);
			this.flowLayoutPanel.TabIndex = 2;
			// 
			// panelScroll
			// 
			this.panelScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelScroll.AutoScroll = true;
			this.panelScroll.Controls.Add(this.flowLayoutPanel);
			this.panelScroll.Location = new System.Drawing.Point(0, 29);
			this.panelScroll.Name = "panelScroll";
			this.panelScroll.Size = new System.Drawing.Size(254, 72);
			this.panelScroll.TabIndex = 3;
			// 
			// groupBoxSeparator
			// 
			this.groupBoxSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxSeparator.Location = new System.Drawing.Point(-3, 16);
			this.groupBoxSeparator.Name = "groupBoxSeparator";
			this.groupBoxSeparator.Size = new System.Drawing.Size(269, 105);
			this.groupBoxSeparator.TabIndex = 4;
			this.groupBoxSeparator.TabStop = false;
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRefresh.BackColor = System.Drawing.SystemColors.Window;
			this.buttonRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonRefresh.Image = global::GitBranchView.Properties.Resources.refresh;
			this.buttonRefresh.Location = new System.Drawing.Point(229, -1);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(20, 20);
			this.buttonRefresh.TabIndex = 5;
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(215, 6);
			// 
			// toolStripMenuItemStartWithWindows
			// 
			this.toolStripMenuItemStartWithWindows.Name = "toolStripMenuItemStartWithWindows";
			this.toolStripMenuItemStartWithWindows.Size = new System.Drawing.Size(218, 22);
			this.toolStripMenuItemStartWithWindows.Text = "Start With Windows";
			this.toolStripMenuItemStartWithWindows.Click += new System.EventHandler(this.ToolStripMenuItemStartWithWindows_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(254, 104);
			this.ControlBox = false;
			this.Controls.Add(this.buttonRefresh);
			this.Controls.Add(this.panelScroll);
			this.Controls.Add(this.groupBoxSeparator);
			this.Controls.Add(this.labelRootPath);
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
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSelectRootFolder;
		private System.Windows.Forms.Label labelRootPath;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSelectGitExePath;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
		private System.Windows.Forms.Panel panelScroll;
		private System.Windows.Forms.GroupBox groupBoxSeparator;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSelectLinkCommand;
		private System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStartWithWindows;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}

