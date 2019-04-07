namespace GitBranchView
{
	partial class SettingsForm
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageGeneral = new System.Windows.Forms.TabPage();
			this.checkBoxEnableLogging = new System.Windows.Forms.CheckBox();
			this.checkBoxStartWithWindows = new System.Windows.Forms.CheckBox();
			this.checkBoxCloseOnLostFocus = new System.Windows.Forms.CheckBox();
			this.groupBoxLinkCommand = new System.Windows.Forms.GroupBox();
			this.labelInfo = new System.Windows.Forms.Label();
			this.textBoxLinkCommandArgs = new System.Windows.Forms.TextBox();
			this.labelLinkCommandArgs = new System.Windows.Forms.Label();
			this.textBoxLinkCommandPath = new System.Windows.Forms.TextBox();
			this.buttonBrowseLinkCommandPath = new System.Windows.Forms.Button();
			this.labelLinkCommandPath = new System.Windows.Forms.Label();
			this.tabPageRootFolders = new System.Windows.Forms.TabPage();
			this.groupBoxFilters = new System.Windows.Forms.GroupBox();
			this.panelScroll = new System.Windows.Forms.Panel();
			this.flowLayoutPanelRootFolderFilters = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonFilterAdd = new System.Windows.Forms.Button();
			this.buttonRootPathRemove = new System.Windows.Forms.Button();
			this.buttonRootPathAdd = new System.Windows.Forms.Button();
			this.comboBoxRootPaths = new System.Windows.Forms.ComboBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.buttonApply = new System.Windows.Forms.Button();
			this.tabPageGit = new System.Windows.Forms.TabPage();
			this.groupBoxGitExe = new System.Windows.Forms.GroupBox();
			this.textBoxGitExePath = new System.Windows.Forms.TextBox();
			this.buttonBrowseGitExePath = new System.Windows.Forms.Button();
			this.labelGitExePath = new System.Windows.Forms.Label();
			this.groupBoxGitContextMenuCommands = new System.Windows.Forms.GroupBox();
			this.checkBoxGitEnableRemoteBranchLookup = new System.Windows.Forms.CheckBox();
			this.tabControl.SuspendLayout();
			this.tabPageGeneral.SuspendLayout();
			this.groupBoxLinkCommand.SuspendLayout();
			this.tabPageRootFolders.SuspendLayout();
			this.groupBoxFilters.SuspendLayout();
			this.panelScroll.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.tabPageGit.SuspendLayout();
			this.groupBoxGitExe.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPageGeneral);
			this.tabControl.Controls.Add(this.tabPageGit);
			this.tabControl.Controls.Add(this.tabPageRootFolders);
			this.tabControl.Location = new System.Drawing.Point(9, 9);
			this.tabControl.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(688, 392);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageGeneral
			// 
			this.tabPageGeneral.Controls.Add(this.checkBoxEnableLogging);
			this.tabPageGeneral.Controls.Add(this.checkBoxStartWithWindows);
			this.tabPageGeneral.Controls.Add(this.checkBoxCloseOnLostFocus);
			this.tabPageGeneral.Controls.Add(this.groupBoxLinkCommand);
			this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabPageGeneral.Name = "tabPageGeneral";
			this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(6);
			this.tabPageGeneral.Size = new System.Drawing.Size(680, 366);
			this.tabPageGeneral.TabIndex = 0;
			this.tabPageGeneral.Text = "General";
			this.tabPageGeneral.UseVisualStyleBackColor = true;
			// 
			// checkBoxEnableLogging
			// 
			this.checkBoxEnableLogging.AutoSize = true;
			this.checkBoxEnableLogging.Location = new System.Drawing.Point(9, 170);
			this.checkBoxEnableLogging.Name = "checkBoxEnableLogging";
			this.checkBoxEnableLogging.Size = new System.Drawing.Size(105, 17);
			this.checkBoxEnableLogging.TabIndex = 7;
			this.checkBoxEnableLogging.Text = "Enable logging";
			this.checkBoxEnableLogging.UseVisualStyleBackColor = true;
			// 
			// checkBoxStartWithWindows
			// 
			this.checkBoxStartWithWindows.AutoSize = true;
			this.checkBoxStartWithWindows.Location = new System.Drawing.Point(9, 147);
			this.checkBoxStartWithWindows.Name = "checkBoxStartWithWindows";
			this.checkBoxStartWithWindows.Size = new System.Drawing.Size(211, 17);
			this.checkBoxStartWithWindows.TabIndex = 6;
			this.checkBoxStartWithWindows.Text = "Start this application with Windows";
			this.checkBoxStartWithWindows.UseVisualStyleBackColor = true;
			// 
			// checkBoxCloseOnLostFocus
			// 
			this.checkBoxCloseOnLostFocus.AutoSize = true;
			this.checkBoxCloseOnLostFocus.Location = new System.Drawing.Point(9, 124);
			this.checkBoxCloseOnLostFocus.Name = "checkBoxCloseOnLostFocus";
			this.checkBoxCloseOnLostFocus.Size = new System.Drawing.Size(326, 17);
			this.checkBoxCloseOnLostFocus.TabIndex = 5;
			this.checkBoxCloseOnLostFocus.Text = "Close on lost focus (temporarily disabled by Ctrl+Left click)";
			this.checkBoxCloseOnLostFocus.UseVisualStyleBackColor = true;
			// 
			// groupBoxLinkCommand
			// 
			this.groupBoxLinkCommand.Controls.Add(this.labelInfo);
			this.groupBoxLinkCommand.Controls.Add(this.textBoxLinkCommandArgs);
			this.groupBoxLinkCommand.Controls.Add(this.labelLinkCommandArgs);
			this.groupBoxLinkCommand.Controls.Add(this.textBoxLinkCommandPath);
			this.groupBoxLinkCommand.Controls.Add(this.buttonBrowseLinkCommandPath);
			this.groupBoxLinkCommand.Controls.Add(this.labelLinkCommandPath);
			this.groupBoxLinkCommand.Location = new System.Drawing.Point(9, 9);
			this.groupBoxLinkCommand.Name = "groupBoxLinkCommand";
			this.groupBoxLinkCommand.Size = new System.Drawing.Size(662, 100);
			this.groupBoxLinkCommand.TabIndex = 4;
			this.groupBoxLinkCommand.TabStop = false;
			this.groupBoxLinkCommand.Text = "Link Command";
			// 
			// labelInfo
			// 
			this.labelInfo.AutoSize = true;
			this.labelInfo.Location = new System.Drawing.Point(42, 74);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(299, 13);
			this.labelInfo.TabIndex = 8;
			this.labelInfo.Text = "(use <path> as a placeholder for the Git repository path)";
			// 
			// textBoxLinkCommandArgs
			// 
			this.textBoxLinkCommandArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLinkCommandArgs.Location = new System.Drawing.Point(45, 49);
			this.textBoxLinkCommandArgs.Name = "textBoxLinkCommandArgs";
			this.textBoxLinkCommandArgs.Size = new System.Drawing.Size(573, 22);
			this.textBoxLinkCommandArgs.TabIndex = 4;
			// 
			// labelLinkCommandArgs
			// 
			this.labelLinkCommandArgs.AutoSize = true;
			this.labelLinkCommandArgs.Location = new System.Drawing.Point(6, 52);
			this.labelLinkCommandArgs.Name = "labelLinkCommandArgs";
			this.labelLinkCommandArgs.Size = new System.Drawing.Size(33, 13);
			this.labelLinkCommandArgs.TabIndex = 3;
			this.labelLinkCommandArgs.Text = "Args:";
			// 
			// textBoxLinkCommandPath
			// 
			this.textBoxLinkCommandPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLinkCommandPath.Location = new System.Drawing.Point(45, 21);
			this.textBoxLinkCommandPath.Name = "textBoxLinkCommandPath";
			this.textBoxLinkCommandPath.Size = new System.Drawing.Size(573, 22);
			this.textBoxLinkCommandPath.TabIndex = 1;
			// 
			// buttonBrowseLinkCommandPath
			// 
			this.buttonBrowseLinkCommandPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseLinkCommandPath.Location = new System.Drawing.Point(624, 20);
			this.buttonBrowseLinkCommandPath.Name = "buttonBrowseLinkCommandPath";
			this.buttonBrowseLinkCommandPath.Size = new System.Drawing.Size(32, 24);
			this.buttonBrowseLinkCommandPath.TabIndex = 2;
			this.buttonBrowseLinkCommandPath.Text = "...";
			this.buttonBrowseLinkCommandPath.UseVisualStyleBackColor = true;
			this.buttonBrowseLinkCommandPath.Click += new System.EventHandler(this.ButtonBrowseLinkCommandPath_Click);
			// 
			// labelLinkCommandPath
			// 
			this.labelLinkCommandPath.AutoSize = true;
			this.labelLinkCommandPath.Location = new System.Drawing.Point(6, 24);
			this.labelLinkCommandPath.Name = "labelLinkCommandPath";
			this.labelLinkCommandPath.Size = new System.Drawing.Size(33, 13);
			this.labelLinkCommandPath.TabIndex = 0;
			this.labelLinkCommandPath.Text = "Path:";
			// 
			// tabPageRootFolders
			// 
			this.tabPageRootFolders.Controls.Add(this.groupBoxFilters);
			this.tabPageRootFolders.Controls.Add(this.buttonRootPathRemove);
			this.tabPageRootFolders.Controls.Add(this.buttonRootPathAdd);
			this.tabPageRootFolders.Controls.Add(this.comboBoxRootPaths);
			this.tabPageRootFolders.Location = new System.Drawing.Point(4, 22);
			this.tabPageRootFolders.Name = "tabPageRootFolders";
			this.tabPageRootFolders.Padding = new System.Windows.Forms.Padding(6);
			this.tabPageRootFolders.Size = new System.Drawing.Size(680, 366);
			this.tabPageRootFolders.TabIndex = 1;
			this.tabPageRootFolders.Text = "Root Folders";
			this.tabPageRootFolders.UseVisualStyleBackColor = true;
			// 
			// groupBoxFilters
			// 
			this.groupBoxFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxFilters.Controls.Add(this.panelScroll);
			this.groupBoxFilters.Controls.Add(this.buttonFilterAdd);
			this.groupBoxFilters.Location = new System.Drawing.Point(9, 36);
			this.groupBoxFilters.Name = "groupBoxFilters";
			this.groupBoxFilters.Size = new System.Drawing.Size(662, 321);
			this.groupBoxFilters.TabIndex = 4;
			this.groupBoxFilters.TabStop = false;
			this.groupBoxFilters.Text = "Filters";
			// 
			// panelScroll
			// 
			this.panelScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelScroll.AutoScroll = true;
			this.panelScroll.BackColor = System.Drawing.Color.Transparent;
			this.panelScroll.Controls.Add(this.flowLayoutPanelRootFolderFilters);
			this.panelScroll.Location = new System.Drawing.Point(6, 21);
			this.panelScroll.Name = "panelScroll";
			this.panelScroll.Size = new System.Drawing.Size(623, 294);
			this.panelScroll.TabIndex = 7;
			// 
			// flowLayoutPanelRootFolderFilters
			// 
			this.flowLayoutPanelRootFolderFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelRootFolderFilters.BackColor = System.Drawing.Color.Transparent;
			this.flowLayoutPanelRootFolderFilters.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanelRootFolderFilters.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanelRootFolderFilters.Name = "flowLayoutPanelRootFolderFilters";
			this.flowLayoutPanelRootFolderFilters.Size = new System.Drawing.Size(623, 294);
			this.flowLayoutPanelRootFolderFilters.TabIndex = 3;
			this.flowLayoutPanelRootFolderFilters.Resize += new System.EventHandler(this.FlowLayoutPanelRootFolderFilters_Resize);
			// 
			// buttonFilterAdd
			// 
			this.buttonFilterAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFilterAdd.BackColor = System.Drawing.SystemColors.Window;
			this.buttonFilterAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonFilterAdd.FlatAppearance.BorderSize = 0;
			this.buttonFilterAdd.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace;
			this.buttonFilterAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonFilterAdd.Image = global::GitBranchView.Properties.Resources.add;
			this.buttonFilterAdd.Location = new System.Drawing.Point(635, 21);
			this.buttonFilterAdd.Name = "buttonFilterAdd";
			this.buttonFilterAdd.Size = new System.Drawing.Size(21, 21);
			this.buttonFilterAdd.TabIndex = 6;
			this.buttonFilterAdd.UseVisualStyleBackColor = true;
			this.buttonFilterAdd.Click += new System.EventHandler(this.ButtonFilterAdd_Click);
			// 
			// buttonRootPathRemove
			// 
			this.buttonRootPathRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRootPathRemove.Location = new System.Drawing.Point(596, 8);
			this.buttonRootPathRemove.Name = "buttonRootPathRemove";
			this.buttonRootPathRemove.Size = new System.Drawing.Size(75, 23);
			this.buttonRootPathRemove.TabIndex = 2;
			this.buttonRootPathRemove.Text = "Remove";
			this.buttonRootPathRemove.UseVisualStyleBackColor = true;
			this.buttonRootPathRemove.Click += new System.EventHandler(this.ButtonRootPathRemove_Click);
			// 
			// buttonRootPathAdd
			// 
			this.buttonRootPathAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRootPathAdd.Location = new System.Drawing.Point(515, 8);
			this.buttonRootPathAdd.Name = "buttonRootPathAdd";
			this.buttonRootPathAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonRootPathAdd.TabIndex = 1;
			this.buttonRootPathAdd.Text = "Add...";
			this.buttonRootPathAdd.UseVisualStyleBackColor = true;
			this.buttonRootPathAdd.Click += new System.EventHandler(this.ButtonRootPathAdd_Click);
			// 
			// comboBoxRootPaths
			// 
			this.comboBoxRootPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxRootPaths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxRootPaths.FormattingEnabled = true;
			this.comboBoxRootPaths.Location = new System.Drawing.Point(9, 9);
			this.comboBoxRootPaths.Name = "comboBoxRootPaths";
			this.comboBoxRootPaths.Size = new System.Drawing.Size(500, 21);
			this.comboBoxRootPaths.Sorted = true;
			this.comboBoxRootPaths.TabIndex = 0;
			this.comboBoxRootPaths.SelectedIndexChanged += new System.EventHandler(this.ComboBoxRootPaths_SelectedIndexChanged);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(226, 0);
			this.buttonOK.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.buttonCancel.Location = new System.Drawing.Point(307, 0);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
			this.tableLayoutPanel.Controls.Add(this.buttonOK, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.buttonCancel, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.buttonApply, 2, 0);
			this.tableLayoutPanel.Location = new System.Drawing.Point(9, 410);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(688, 23);
			this.tableLayoutPanel.TabIndex = 7;
			// 
			// buttonApply
			// 
			this.buttonApply.Location = new System.Drawing.Point(385, 0);
			this.buttonApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(75, 23);
			this.buttonApply.TabIndex = 7;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
			// 
			// tabPageGit
			// 
			this.tabPageGit.Controls.Add(this.checkBoxGitEnableRemoteBranchLookup);
			this.tabPageGit.Controls.Add(this.groupBoxGitContextMenuCommands);
			this.tabPageGit.Controls.Add(this.groupBoxGitExe);
			this.tabPageGit.Location = new System.Drawing.Point(4, 22);
			this.tabPageGit.Name = "tabPageGit";
			this.tabPageGit.Padding = new System.Windows.Forms.Padding(6);
			this.tabPageGit.Size = new System.Drawing.Size(680, 366);
			this.tabPageGit.TabIndex = 2;
			this.tabPageGit.Text = "Git";
			this.tabPageGit.UseVisualStyleBackColor = true;
			// 
			// groupBoxGitExe
			// 
			this.groupBoxGitExe.Controls.Add(this.textBoxGitExePath);
			this.groupBoxGitExe.Controls.Add(this.buttonBrowseGitExePath);
			this.groupBoxGitExe.Controls.Add(this.labelGitExePath);
			this.groupBoxGitExe.Location = new System.Drawing.Point(9, 9);
			this.groupBoxGitExe.Name = "groupBoxGitExe";
			this.groupBoxGitExe.Size = new System.Drawing.Size(662, 59);
			this.groupBoxGitExe.TabIndex = 4;
			this.groupBoxGitExe.TabStop = false;
			this.groupBoxGitExe.Text = "Executable";
			// 
			// textBoxGitExePath
			// 
			this.textBoxGitExePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxGitExePath.Location = new System.Drawing.Point(45, 21);
			this.textBoxGitExePath.Name = "textBoxGitExePath";
			this.textBoxGitExePath.Size = new System.Drawing.Size(573, 22);
			this.textBoxGitExePath.TabIndex = 1;
			// 
			// buttonBrowseGitExePath
			// 
			this.buttonBrowseGitExePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseGitExePath.Location = new System.Drawing.Point(624, 20);
			this.buttonBrowseGitExePath.Name = "buttonBrowseGitExePath";
			this.buttonBrowseGitExePath.Size = new System.Drawing.Size(32, 24);
			this.buttonBrowseGitExePath.TabIndex = 2;
			this.buttonBrowseGitExePath.Text = "...";
			this.buttonBrowseGitExePath.UseVisualStyleBackColor = true;
			this.buttonBrowseGitExePath.Click += new System.EventHandler(this.ButtonBrowseGitExePath_Click);
			// 
			// labelGitExePath
			// 
			this.labelGitExePath.AutoSize = true;
			this.labelGitExePath.Location = new System.Drawing.Point(6, 24);
			this.labelGitExePath.Name = "labelGitExePath";
			this.labelGitExePath.Size = new System.Drawing.Size(33, 13);
			this.labelGitExePath.TabIndex = 0;
			this.labelGitExePath.Text = "Path:";
			// 
			// groupBoxGitContextMenuCommands
			// 
			this.groupBoxGitContextMenuCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxGitContextMenuCommands.Location = new System.Drawing.Point(9, 74);
			this.groupBoxGitContextMenuCommands.Name = "groupBoxGitContextMenuCommands";
			this.groupBoxGitContextMenuCommands.Size = new System.Drawing.Size(662, 251);
			this.groupBoxGitContextMenuCommands.TabIndex = 5;
			this.groupBoxGitContextMenuCommands.TabStop = false;
			this.groupBoxGitContextMenuCommands.Text = "Context Menu Commands";
			// 
			// checkBoxGitEnableRemoteBranchLookup
			// 
			this.checkBoxGitEnableRemoteBranchLookup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxGitEnableRemoteBranchLookup.AutoSize = true;
			this.checkBoxGitEnableRemoteBranchLookup.Location = new System.Drawing.Point(9, 340);
			this.checkBoxGitEnableRemoteBranchLookup.Name = "checkBoxGitEnableRemoteBranchLookup";
			this.checkBoxGitEnableRemoteBranchLookup.Size = new System.Drawing.Size(179, 17);
			this.checkBoxGitEnableRemoteBranchLookup.TabIndex = 6;
			this.checkBoxGitEnableRemoteBranchLookup.Text = "Enable remote branch lookup";
			this.checkBoxGitEnableRemoteBranchLookup.UseVisualStyleBackColor = true;
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(706, 445);
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.tabControl);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Git Branch View Settings";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.tabControl.ResumeLayout(false);
			this.tabPageGeneral.ResumeLayout(false);
			this.tabPageGeneral.PerformLayout();
			this.groupBoxLinkCommand.ResumeLayout(false);
			this.groupBoxLinkCommand.PerformLayout();
			this.tabPageRootFolders.ResumeLayout(false);
			this.groupBoxFilters.ResumeLayout(false);
			this.panelScroll.ResumeLayout(false);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tabPageGit.ResumeLayout(false);
			this.tabPageGit.PerformLayout();
			this.groupBoxGitExe.ResumeLayout(false);
			this.groupBoxGitExe.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageGeneral;
		private System.Windows.Forms.TabPage tabPageRootFolders;
		private System.Windows.Forms.GroupBox groupBoxLinkCommand;
		private System.Windows.Forms.TextBox textBoxLinkCommandPath;
		private System.Windows.Forms.Button buttonBrowseLinkCommandPath;
		private System.Windows.Forms.Label labelLinkCommandPath;
		private System.Windows.Forms.TextBox textBoxLinkCommandArgs;
		private System.Windows.Forms.Label labelLinkCommandArgs;
		private System.Windows.Forms.Label labelInfo;
		private System.Windows.Forms.CheckBox checkBoxStartWithWindows;
		private System.Windows.Forms.CheckBox checkBoxCloseOnLostFocus;
		private System.Windows.Forms.Button buttonRootPathRemove;
		private System.Windows.Forms.Button buttonRootPathAdd;
		private System.Windows.Forms.ComboBox comboBoxRootPaths;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRootFolderFilters;
		private System.Windows.Forms.GroupBox groupBoxFilters;
		private System.Windows.Forms.Button buttonFilterAdd;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Panel panelScroll;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.CheckBox checkBoxEnableLogging;
		private System.Windows.Forms.TabPage tabPageGit;
		private System.Windows.Forms.CheckBox checkBoxGitEnableRemoteBranchLookup;
		private System.Windows.Forms.GroupBox groupBoxGitContextMenuCommands;
		private System.Windows.Forms.GroupBox groupBoxGitExe;
		private System.Windows.Forms.TextBox textBoxGitExePath;
		private System.Windows.Forms.Button buttonBrowseGitExePath;
		private System.Windows.Forms.Label labelGitExePath;
	}
}