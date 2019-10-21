namespace GitBranchView.Forms
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
			this.tableLayoutPanelGeneral = new System.Windows.Forms.TableLayoutPanel();
			this.groupBoxGitRepositoryLink = new System.Windows.Forms.GroupBox();
			this.textBoxCustomCommandName = new System.Windows.Forms.TextBox();
			this.labelCustomCommandName = new System.Windows.Forms.Label();
			this.labelGitRepositoryLinkWhenLinkIsClicked = new System.Windows.Forms.Label();
			this.radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile = new System.Windows.Forms.RadioButton();
			this.radioButtonGitRepositoryLinkExecuteCustomCommand = new System.Windows.Forms.RadioButton();
			this.groupBoxQuickLaunchFiles = new System.Windows.Forms.GroupBox();
			this.radioButtonQuickLaunchFilesGroupByPath = new System.Windows.Forms.RadioButton();
			this.labelQuickLaunchFilesGroupBy = new System.Windows.Forms.Label();
			this.radioButtonQuickLaunchFilesGroupByExtension = new System.Windows.Forms.RadioButton();
			this.radioButtonQuickLaunchFilesDoNotGroup = new System.Windows.Forms.RadioButton();
			this.textBoxQuickLaunchFilesFilter = new System.Windows.Forms.TextBox();
			this.labelQuickLaunchFilesFilter = new System.Windows.Forms.Label();
			this.checkBoxEnableLogging = new System.Windows.Forms.CheckBox();
			this.checkBoxStartWithWindows = new System.Windows.Forms.CheckBox();
			this.checkBoxCloseOnLostFocus = new System.Windows.Forms.CheckBox();
			this.groupBoxCustomCommand = new System.Windows.Forms.GroupBox();
			this.labelCustomCommandInfo = new System.Windows.Forms.Label();
			this.textBoxCustomCommandArgs = new System.Windows.Forms.TextBox();
			this.labelCustomCommandArgs = new System.Windows.Forms.Label();
			this.textBoxCustomCommandPath = new System.Windows.Forms.TextBox();
			this.buttonCustomCommandPathBrowse = new System.Windows.Forms.Button();
			this.labelCustomCommandPath = new System.Windows.Forms.Label();
			this.tabPageGit = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelGit = new System.Windows.Forms.TableLayoutPanel();
			this.panelGit2 = new System.Windows.Forms.Panel();
			this.checkBoxGitExcludeLfsRepositories = new System.Windows.Forms.CheckBox();
			this.panelGit1 = new System.Windows.Forms.Panel();
			this.checkBoxGitShowCommandOutput = new System.Windows.Forms.CheckBox();
			this.checkBoxGitEnableRemoteBranchLookup = new System.Windows.Forms.CheckBox();
			this.groupBoxGitContextMenuCommands = new System.Windows.Forms.GroupBox();
			this.panelScrollGitContextMenuCommands = new System.Windows.Forms.Panel();
			this.flowLayoutPanelGitContextMenuCommands = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonGitContextMenuCommandsAdd = new System.Windows.Forms.Button();
			this.groupBoxGitExe = new System.Windows.Forms.GroupBox();
			this.textBoxGitExePath = new System.Windows.Forms.TextBox();
			this.buttonBrowseGitExePath = new System.Windows.Forms.Button();
			this.labelGitExePath = new System.Windows.Forms.Label();
			this.tabPageRootFolders = new System.Windows.Forms.TabPage();
			this.groupBoxRootPathFilters = new System.Windows.Forms.GroupBox();
			this.panelScrollRootPathFilters = new System.Windows.Forms.Panel();
			this.flowLayoutPanelRootPathFilters = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonRootPathFilterAdd = new System.Windows.Forms.Button();
			this.buttonRootPathRemove = new System.Windows.Forms.Button();
			this.buttonRootPathAdd = new System.Windows.Forms.Button();
			this.comboBoxRootPaths = new System.Windows.Forms.ComboBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
			this.buttonApply = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.tabPageGeneral.SuspendLayout();
			this.tableLayoutPanelGeneral.SuspendLayout();
			this.groupBoxGitRepositoryLink.SuspendLayout();
			this.groupBoxQuickLaunchFiles.SuspendLayout();
			this.groupBoxCustomCommand.SuspendLayout();
			this.tabPageGit.SuspendLayout();
			this.tableLayoutPanelGit.SuspendLayout();
			this.panelGit2.SuspendLayout();
			this.panelGit1.SuspendLayout();
			this.groupBoxGitContextMenuCommands.SuspendLayout();
			this.panelScrollGitContextMenuCommands.SuspendLayout();
			this.groupBoxGitExe.SuspendLayout();
			this.tabPageRootFolders.SuspendLayout();
			this.groupBoxRootPathFilters.SuspendLayout();
			this.panelScrollRootPathFilters.SuspendLayout();
			this.tableLayoutPanelButtons.SuspendLayout();
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
			this.tabControl.Size = new System.Drawing.Size(688, 415);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageGeneral
			// 
			this.tabPageGeneral.Controls.Add(this.tableLayoutPanelGeneral);
			this.tabPageGeneral.Controls.Add(this.checkBoxEnableLogging);
			this.tabPageGeneral.Controls.Add(this.checkBoxStartWithWindows);
			this.tabPageGeneral.Controls.Add(this.checkBoxCloseOnLostFocus);
			this.tabPageGeneral.Controls.Add(this.groupBoxCustomCommand);
			this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabPageGeneral.Name = "tabPageGeneral";
			this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(6);
			this.tabPageGeneral.Size = new System.Drawing.Size(680, 389);
			this.tabPageGeneral.TabIndex = 0;
			this.tabPageGeneral.Text = "General";
			this.tabPageGeneral.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelGeneral
			// 
			this.tableLayoutPanelGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelGeneral.ColumnCount = 2;
			this.tableLayoutPanelGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelGeneral.Controls.Add(this.groupBoxGitRepositoryLink, 1, 0);
			this.tableLayoutPanelGeneral.Controls.Add(this.groupBoxQuickLaunchFiles, 0, 0);
			this.tableLayoutPanelGeneral.Location = new System.Drawing.Point(9, 115);
			this.tableLayoutPanelGeneral.Name = "tableLayoutPanelGeneral";
			this.tableLayoutPanelGeneral.RowCount = 1;
			this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelGeneral.Size = new System.Drawing.Size(662, 133);
			this.tableLayoutPanelGeneral.TabIndex = 13;
			// 
			// groupBoxGitRepositoryLink
			// 
			this.groupBoxGitRepositoryLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxGitRepositoryLink.Controls.Add(this.textBoxCustomCommandName);
			this.groupBoxGitRepositoryLink.Controls.Add(this.labelCustomCommandName);
			this.groupBoxGitRepositoryLink.Controls.Add(this.labelGitRepositoryLinkWhenLinkIsClicked);
			this.groupBoxGitRepositoryLink.Controls.Add(this.radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile);
			this.groupBoxGitRepositoryLink.Controls.Add(this.radioButtonGitRepositoryLinkExecuteCustomCommand);
			this.groupBoxGitRepositoryLink.Location = new System.Drawing.Point(337, 0);
			this.groupBoxGitRepositoryLink.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.groupBoxGitRepositoryLink.Name = "groupBoxGitRepositoryLink";
			this.groupBoxGitRepositoryLink.Size = new System.Drawing.Size(325, 133);
			this.groupBoxGitRepositoryLink.TabIndex = 12;
			this.groupBoxGitRepositoryLink.TabStop = false;
			this.groupBoxGitRepositoryLink.Text = "Git Repository Link";
			// 
			// textBoxCustomCommandName
			// 
			this.textBoxCustomCommandName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCustomCommandName.Location = new System.Drawing.Point(53, 97);
			this.textBoxCustomCommandName.Name = "textBoxCustomCommandName";
			this.textBoxCustomCommandName.Size = new System.Drawing.Size(228, 22);
			this.textBoxCustomCommandName.TabIndex = 9;
			this.textBoxCustomCommandName.TextChanged += new System.EventHandler(this.TextBoxCustomCommandName_TextChanged);
			// 
			// labelCustomCommandName
			// 
			this.labelCustomCommandName.AutoSize = true;
			this.labelCustomCommandName.Location = new System.Drawing.Point(50, 81);
			this.labelCustomCommandName.Name = "labelCustomCommandName";
			this.labelCustomCommandName.Size = new System.Drawing.Size(219, 13);
			this.labelCustomCommandName.TabIndex = 12;
			this.labelCustomCommandName.Text = "Custom command name in context menu:";
			// 
			// labelGitRepositoryLinkWhenLinkIsClicked
			// 
			this.labelGitRepositoryLinkWhenLinkIsClicked.AutoSize = true;
			this.labelGitRepositoryLinkWhenLinkIsClicked.Location = new System.Drawing.Point(6, 24);
			this.labelGitRepositoryLinkWhenLinkIsClicked.Name = "labelGitRepositoryLinkWhenLinkIsClicked";
			this.labelGitRepositoryLinkWhenLinkIsClicked.Size = new System.Drawing.Size(118, 13);
			this.labelGitRepositoryLinkWhenLinkIsClicked.TabIndex = 9;
			this.labelGitRepositoryLinkWhenLinkIsClicked.Text = "When link is clicked...";
			// 
			// radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile
			// 
			this.radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.AutoSize = true;
			this.radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.Location = new System.Drawing.Point(20, 60);
			this.radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.Name = "radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile";
			this.radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.Size = new System.Drawing.Size(195, 17);
			this.radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.TabIndex = 11;
			this.radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.TabStop = true;
			this.radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.Text = "Launch selected quick launch file";
			this.radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.UseVisualStyleBackColor = true;
			this.radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile.CheckedChanged += new System.EventHandler(this.RadioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile_CheckedChanged);
			// 
			// radioButtonGitRepositoryLinkExecuteCustomCommand
			// 
			this.radioButtonGitRepositoryLinkExecuteCustomCommand.AutoSize = true;
			this.radioButtonGitRepositoryLinkExecuteCustomCommand.Location = new System.Drawing.Point(20, 41);
			this.radioButtonGitRepositoryLinkExecuteCustomCommand.Name = "radioButtonGitRepositoryLinkExecuteCustomCommand";
			this.radioButtonGitRepositoryLinkExecuteCustomCommand.Size = new System.Drawing.Size(157, 17);
			this.radioButtonGitRepositoryLinkExecuteCustomCommand.TabIndex = 10;
			this.radioButtonGitRepositoryLinkExecuteCustomCommand.TabStop = true;
			this.radioButtonGitRepositoryLinkExecuteCustomCommand.Text = "Execute custom command";
			this.radioButtonGitRepositoryLinkExecuteCustomCommand.UseVisualStyleBackColor = true;
			this.radioButtonGitRepositoryLinkExecuteCustomCommand.CheckedChanged += new System.EventHandler(this.RadioButtonGitRepositoryLinkExecuteCustomCommand_CheckedChanged);
			// 
			// groupBoxQuickLaunchFiles
			// 
			this.groupBoxQuickLaunchFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxQuickLaunchFiles.Controls.Add(this.radioButtonQuickLaunchFilesGroupByPath);
			this.groupBoxQuickLaunchFiles.Controls.Add(this.labelQuickLaunchFilesGroupBy);
			this.groupBoxQuickLaunchFiles.Controls.Add(this.radioButtonQuickLaunchFilesGroupByExtension);
			this.groupBoxQuickLaunchFiles.Controls.Add(this.radioButtonQuickLaunchFilesDoNotGroup);
			this.groupBoxQuickLaunchFiles.Controls.Add(this.textBoxQuickLaunchFilesFilter);
			this.groupBoxQuickLaunchFiles.Controls.Add(this.labelQuickLaunchFilesFilter);
			this.groupBoxQuickLaunchFiles.Location = new System.Drawing.Point(0, 0);
			this.groupBoxQuickLaunchFiles.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
			this.groupBoxQuickLaunchFiles.Name = "groupBoxQuickLaunchFiles";
			this.groupBoxQuickLaunchFiles.Size = new System.Drawing.Size(325, 133);
			this.groupBoxQuickLaunchFiles.TabIndex = 8;
			this.groupBoxQuickLaunchFiles.TabStop = false;
			this.groupBoxQuickLaunchFiles.Text = "Quick Launch Files";
			// 
			// radioButtonQuickLaunchFilesGroupByPath
			// 
			this.radioButtonQuickLaunchFilesGroupByPath.AutoSize = true;
			this.radioButtonQuickLaunchFilesGroupByPath.Location = new System.Drawing.Point(20, 107);
			this.radioButtonQuickLaunchFilesGroupByPath.Name = "radioButtonQuickLaunchFilesGroupByPath";
			this.radioButtonQuickLaunchFilesGroupByPath.Size = new System.Drawing.Size(92, 17);
			this.radioButtonQuickLaunchFilesGroupByPath.TabIndex = 15;
			this.radioButtonQuickLaunchFilesGroupByPath.TabStop = true;
			this.radioButtonQuickLaunchFilesGroupByPath.Text = "Relative path";
			this.radioButtonQuickLaunchFilesGroupByPath.UseVisualStyleBackColor = true;
			this.radioButtonQuickLaunchFilesGroupByPath.CheckedChanged += new System.EventHandler(this.RadioButtonQuickLaunchFilesGroupByPath_CheckedChanged);
			// 
			// labelQuickLaunchFilesGroupBy
			// 
			this.labelQuickLaunchFilesGroupBy.AutoSize = true;
			this.labelQuickLaunchFilesGroupBy.Location = new System.Drawing.Point(6, 52);
			this.labelQuickLaunchFilesGroupBy.Name = "labelQuickLaunchFilesGroupBy";
			this.labelQuickLaunchFilesGroupBy.Size = new System.Drawing.Size(188, 13);
			this.labelQuickLaunchFilesGroupBy.TabIndex = 12;
			this.labelQuickLaunchFilesGroupBy.Text = "Group by (if more than one group):";
			// 
			// radioButtonQuickLaunchFilesGroupByExtension
			// 
			this.radioButtonQuickLaunchFilesGroupByExtension.AutoSize = true;
			this.radioButtonQuickLaunchFilesGroupByExtension.Location = new System.Drawing.Point(20, 88);
			this.radioButtonQuickLaunchFilesGroupByExtension.Name = "radioButtonQuickLaunchFilesGroupByExtension";
			this.radioButtonQuickLaunchFilesGroupByExtension.Size = new System.Drawing.Size(75, 17);
			this.radioButtonQuickLaunchFilesGroupByExtension.TabIndex = 14;
			this.radioButtonQuickLaunchFilesGroupByExtension.TabStop = true;
			this.radioButtonQuickLaunchFilesGroupByExtension.Text = "Extension";
			this.radioButtonQuickLaunchFilesGroupByExtension.UseVisualStyleBackColor = true;
			this.radioButtonQuickLaunchFilesGroupByExtension.CheckedChanged += new System.EventHandler(this.RadioButtonQuickLaunchFilesGroupByExtension_CheckedChanged);
			// 
			// radioButtonQuickLaunchFilesDoNotGroup
			// 
			this.radioButtonQuickLaunchFilesDoNotGroup.AutoSize = true;
			this.radioButtonQuickLaunchFilesDoNotGroup.Location = new System.Drawing.Point(20, 69);
			this.radioButtonQuickLaunchFilesDoNotGroup.Name = "radioButtonQuickLaunchFilesDoNotGroup";
			this.radioButtonQuickLaunchFilesDoNotGroup.Size = new System.Drawing.Size(96, 17);
			this.radioButtonQuickLaunchFilesDoNotGroup.TabIndex = 13;
			this.radioButtonQuickLaunchFilesDoNotGroup.TabStop = true;
			this.radioButtonQuickLaunchFilesDoNotGroup.Text = "Do not group";
			this.radioButtonQuickLaunchFilesDoNotGroup.UseVisualStyleBackColor = true;
			this.radioButtonQuickLaunchFilesDoNotGroup.CheckedChanged += new System.EventHandler(this.RadioButtonQuickLaunchFilesDoNotGroup_CheckedChanged);
			// 
			// textBoxQuickLaunchFilesFilter
			// 
			this.textBoxQuickLaunchFilesFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxQuickLaunchFilesFilter.Location = new System.Drawing.Point(78, 21);
			this.textBoxQuickLaunchFilesFilter.Name = "textBoxQuickLaunchFilesFilter";
			this.textBoxQuickLaunchFilesFilter.Size = new System.Drawing.Size(238, 22);
			this.textBoxQuickLaunchFilesFilter.TabIndex = 9;
			this.textBoxQuickLaunchFilesFilter.TextChanged += new System.EventHandler(this.TextBoxQuickLaunchFilesFilter_TextChanged);
			// 
			// labelQuickLaunchFilesFilter
			// 
			this.labelQuickLaunchFilesFilter.AutoSize = true;
			this.labelQuickLaunchFilesFilter.Location = new System.Drawing.Point(6, 24);
			this.labelQuickLaunchFilesFilter.Name = "labelQuickLaunchFilesFilter";
			this.labelQuickLaunchFilesFilter.Size = new System.Drawing.Size(68, 13);
			this.labelQuickLaunchFilesFilter.TabIndex = 5;
			this.labelQuickLaunchFilesFilter.Text = "Regex filter:";
			// 
			// checkBoxEnableLogging
			// 
			this.checkBoxEnableLogging.AutoSize = true;
			this.checkBoxEnableLogging.Location = new System.Drawing.Point(9, 310);
			this.checkBoxEnableLogging.Name = "checkBoxEnableLogging";
			this.checkBoxEnableLogging.Size = new System.Drawing.Size(105, 17);
			this.checkBoxEnableLogging.TabIndex = 7;
			this.checkBoxEnableLogging.Text = "Enable logging";
			this.checkBoxEnableLogging.UseVisualStyleBackColor = true;
			// 
			// checkBoxStartWithWindows
			// 
			this.checkBoxStartWithWindows.AutoSize = true;
			this.checkBoxStartWithWindows.Location = new System.Drawing.Point(9, 287);
			this.checkBoxStartWithWindows.Name = "checkBoxStartWithWindows";
			this.checkBoxStartWithWindows.Size = new System.Drawing.Size(211, 17);
			this.checkBoxStartWithWindows.TabIndex = 6;
			this.checkBoxStartWithWindows.Text = "Start this application with Windows";
			this.checkBoxStartWithWindows.UseVisualStyleBackColor = true;
			// 
			// checkBoxCloseOnLostFocus
			// 
			this.checkBoxCloseOnLostFocus.AutoSize = true;
			this.checkBoxCloseOnLostFocus.Location = new System.Drawing.Point(9, 264);
			this.checkBoxCloseOnLostFocus.Name = "checkBoxCloseOnLostFocus";
			this.checkBoxCloseOnLostFocus.Size = new System.Drawing.Size(418, 17);
			this.checkBoxCloseOnLostFocus.TabIndex = 5;
			this.checkBoxCloseOnLostFocus.Text = "Close on lost focus (temporarily disabled when opened using Ctrl+Left click)";
			this.checkBoxCloseOnLostFocus.UseVisualStyleBackColor = true;
			// 
			// groupBoxCustomCommand
			// 
			this.groupBoxCustomCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxCustomCommand.Controls.Add(this.labelCustomCommandInfo);
			this.groupBoxCustomCommand.Controls.Add(this.textBoxCustomCommandArgs);
			this.groupBoxCustomCommand.Controls.Add(this.labelCustomCommandArgs);
			this.groupBoxCustomCommand.Controls.Add(this.textBoxCustomCommandPath);
			this.groupBoxCustomCommand.Controls.Add(this.buttonCustomCommandPathBrowse);
			this.groupBoxCustomCommand.Controls.Add(this.labelCustomCommandPath);
			this.groupBoxCustomCommand.Location = new System.Drawing.Point(9, 9);
			this.groupBoxCustomCommand.Name = "groupBoxCustomCommand";
			this.groupBoxCustomCommand.Size = new System.Drawing.Size(662, 100);
			this.groupBoxCustomCommand.TabIndex = 4;
			this.groupBoxCustomCommand.TabStop = false;
			this.groupBoxCustomCommand.Text = "Custom Command";
			// 
			// labelCustomCommandInfo
			// 
			this.labelCustomCommandInfo.AutoSize = true;
			this.labelCustomCommandInfo.Location = new System.Drawing.Point(75, 74);
			this.labelCustomCommandInfo.Name = "labelCustomCommandInfo";
			this.labelCustomCommandInfo.Size = new System.Drawing.Size(299, 13);
			this.labelCustomCommandInfo.TabIndex = 8;
			this.labelCustomCommandInfo.Text = "(use <path> as a placeholder for the Git repository path)";
			// 
			// textBoxCustomCommandArgs
			// 
			this.textBoxCustomCommandArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCustomCommandArgs.Location = new System.Drawing.Point(78, 49);
			this.textBoxCustomCommandArgs.Name = "textBoxCustomCommandArgs";
			this.textBoxCustomCommandArgs.Size = new System.Drawing.Size(540, 22);
			this.textBoxCustomCommandArgs.TabIndex = 4;
			// 
			// labelCustomCommandArgs
			// 
			this.labelCustomCommandArgs.AutoSize = true;
			this.labelCustomCommandArgs.Location = new System.Drawing.Point(6, 52);
			this.labelCustomCommandArgs.Name = "labelCustomCommandArgs";
			this.labelCustomCommandArgs.Size = new System.Drawing.Size(66, 13);
			this.labelCustomCommandArgs.TabIndex = 3;
			this.labelCustomCommandArgs.Text = "Arguments:";
			// 
			// textBoxCustomCommandPath
			// 
			this.textBoxCustomCommandPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCustomCommandPath.Location = new System.Drawing.Point(78, 21);
			this.textBoxCustomCommandPath.Name = "textBoxCustomCommandPath";
			this.textBoxCustomCommandPath.Size = new System.Drawing.Size(540, 22);
			this.textBoxCustomCommandPath.TabIndex = 1;
			// 
			// buttonCustomCommandPathBrowse
			// 
			this.buttonCustomCommandPathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCustomCommandPathBrowse.Location = new System.Drawing.Point(624, 20);
			this.buttonCustomCommandPathBrowse.Name = "buttonCustomCommandPathBrowse";
			this.buttonCustomCommandPathBrowse.Size = new System.Drawing.Size(32, 24);
			this.buttonCustomCommandPathBrowse.TabIndex = 2;
			this.buttonCustomCommandPathBrowse.Text = "...";
			this.buttonCustomCommandPathBrowse.UseVisualStyleBackColor = true;
			this.buttonCustomCommandPathBrowse.Click += new System.EventHandler(this.ButtonCustomCommandPathBrowse_Click);
			// 
			// labelCustomCommandPath
			// 
			this.labelCustomCommandPath.AutoSize = true;
			this.labelCustomCommandPath.Location = new System.Drawing.Point(6, 24);
			this.labelCustomCommandPath.Name = "labelCustomCommandPath";
			this.labelCustomCommandPath.Size = new System.Drawing.Size(33, 13);
			this.labelCustomCommandPath.TabIndex = 0;
			this.labelCustomCommandPath.Text = "Path:";
			// 
			// tabPageGit
			// 
			this.tabPageGit.Controls.Add(this.tableLayoutPanelGit);
			this.tabPageGit.Controls.Add(this.groupBoxGitContextMenuCommands);
			this.tabPageGit.Controls.Add(this.groupBoxGitExe);
			this.tabPageGit.Location = new System.Drawing.Point(4, 22);
			this.tabPageGit.Name = "tabPageGit";
			this.tabPageGit.Padding = new System.Windows.Forms.Padding(6);
			this.tabPageGit.Size = new System.Drawing.Size(680, 389);
			this.tabPageGit.TabIndex = 2;
			this.tabPageGit.Text = "Git";
			this.tabPageGit.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelGit
			// 
			this.tableLayoutPanelGit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelGit.ColumnCount = 2;
			this.tableLayoutPanelGit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelGit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelGit.Controls.Add(this.panelGit2, 1, 0);
			this.tableLayoutPanelGit.Controls.Add(this.panelGit1, 0, 0);
			this.tableLayoutPanelGit.Location = new System.Drawing.Point(9, 340);
			this.tableLayoutPanelGit.Name = "tableLayoutPanelGit";
			this.tableLayoutPanelGit.RowCount = 1;
			this.tableLayoutPanelGit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelGit.Size = new System.Drawing.Size(661, 40);
			this.tableLayoutPanelGit.TabIndex = 8;
			// 
			// panelGit2
			// 
			this.panelGit2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelGit2.Controls.Add(this.checkBoxGitExcludeLfsRepositories);
			this.panelGit2.Location = new System.Drawing.Point(330, 0);
			this.panelGit2.Margin = new System.Windows.Forms.Padding(0);
			this.panelGit2.Name = "panelGit2";
			this.panelGit2.Size = new System.Drawing.Size(331, 40);
			this.panelGit2.TabIndex = 1;
			// 
			// checkBoxGitExcludeLfsRepositories
			// 
			this.checkBoxGitExcludeLfsRepositories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxGitExcludeLfsRepositories.AutoSize = true;
			this.checkBoxGitExcludeLfsRepositories.Location = new System.Drawing.Point(0, 0);
			this.checkBoxGitExcludeLfsRepositories.Name = "checkBoxGitExcludeLfsRepositories";
			this.checkBoxGitExcludeLfsRepositories.Size = new System.Drawing.Size(286, 17);
			this.checkBoxGitExcludeLfsRepositories.TabIndex = 8;
			this.checkBoxGitExcludeLfsRepositories.Text = "Exclude repositories using Git LFS from folder scan";
			this.checkBoxGitExcludeLfsRepositories.UseVisualStyleBackColor = true;
			// 
			// panelGit1
			// 
			this.panelGit1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelGit1.Controls.Add(this.checkBoxGitShowCommandOutput);
			this.panelGit1.Controls.Add(this.checkBoxGitEnableRemoteBranchLookup);
			this.panelGit1.Location = new System.Drawing.Point(0, 0);
			this.panelGit1.Margin = new System.Windows.Forms.Padding(0);
			this.panelGit1.Name = "panelGit1";
			this.panelGit1.Size = new System.Drawing.Size(330, 40);
			this.panelGit1.TabIndex = 0;
			// 
			// checkBoxGitShowCommandOutput
			// 
			this.checkBoxGitShowCommandOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxGitShowCommandOutput.AutoSize = true;
			this.checkBoxGitShowCommandOutput.Location = new System.Drawing.Point(0, 0);
			this.checkBoxGitShowCommandOutput.Name = "checkBoxGitShowCommandOutput";
			this.checkBoxGitShowCommandOutput.Size = new System.Drawing.Size(258, 17);
			this.checkBoxGitShowCommandOutput.TabIndex = 7;
			this.checkBoxGitShowCommandOutput.Text = "Show output for user initiated git commands";
			this.checkBoxGitShowCommandOutput.UseVisualStyleBackColor = true;
			// 
			// checkBoxGitEnableRemoteBranchLookup
			// 
			this.checkBoxGitEnableRemoteBranchLookup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxGitEnableRemoteBranchLookup.AutoSize = true;
			this.checkBoxGitEnableRemoteBranchLookup.Location = new System.Drawing.Point(0, 23);
			this.checkBoxGitEnableRemoteBranchLookup.Name = "checkBoxGitEnableRemoteBranchLookup";
			this.checkBoxGitEnableRemoteBranchLookup.Size = new System.Drawing.Size(179, 17);
			this.checkBoxGitEnableRemoteBranchLookup.TabIndex = 6;
			this.checkBoxGitEnableRemoteBranchLookup.Text = "Enable remote branch lookup";
			this.checkBoxGitEnableRemoteBranchLookup.UseVisualStyleBackColor = true;
			// 
			// groupBoxGitContextMenuCommands
			// 
			this.groupBoxGitContextMenuCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxGitContextMenuCommands.Controls.Add(this.panelScrollGitContextMenuCommands);
			this.groupBoxGitContextMenuCommands.Controls.Add(this.buttonGitContextMenuCommandsAdd);
			this.groupBoxGitContextMenuCommands.Location = new System.Drawing.Point(9, 74);
			this.groupBoxGitContextMenuCommands.Name = "groupBoxGitContextMenuCommands";
			this.groupBoxGitContextMenuCommands.Size = new System.Drawing.Size(662, 251);
			this.groupBoxGitContextMenuCommands.TabIndex = 5;
			this.groupBoxGitContextMenuCommands.TabStop = false;
			this.groupBoxGitContextMenuCommands.Text = "Context Menu Commands";
			// 
			// panelScrollGitContextMenuCommands
			// 
			this.panelScrollGitContextMenuCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelScrollGitContextMenuCommands.AutoScroll = true;
			this.panelScrollGitContextMenuCommands.BackColor = System.Drawing.Color.Transparent;
			this.panelScrollGitContextMenuCommands.Controls.Add(this.flowLayoutPanelGitContextMenuCommands);
			this.panelScrollGitContextMenuCommands.Location = new System.Drawing.Point(6, 21);
			this.panelScrollGitContextMenuCommands.Name = "panelScrollGitContextMenuCommands";
			this.panelScrollGitContextMenuCommands.Size = new System.Drawing.Size(615, 224);
			this.panelScrollGitContextMenuCommands.TabIndex = 9;
			// 
			// flowLayoutPanelGitContextMenuCommands
			// 
			this.flowLayoutPanelGitContextMenuCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelGitContextMenuCommands.BackColor = System.Drawing.Color.Transparent;
			this.flowLayoutPanelGitContextMenuCommands.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanelGitContextMenuCommands.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanelGitContextMenuCommands.Name = "flowLayoutPanelGitContextMenuCommands";
			this.flowLayoutPanelGitContextMenuCommands.Size = new System.Drawing.Size(615, 224);
			this.flowLayoutPanelGitContextMenuCommands.TabIndex = 3;
			this.flowLayoutPanelGitContextMenuCommands.Resize += new System.EventHandler(this.FlowLayoutPanelGitContextMenuCommands_Resize);
			// 
			// buttonGitContextMenuCommandsAdd
			// 
			this.buttonGitContextMenuCommandsAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonGitContextMenuCommandsAdd.BackColor = System.Drawing.SystemColors.Window;
			this.buttonGitContextMenuCommandsAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonGitContextMenuCommandsAdd.FlatAppearance.BorderSize = 0;
			this.buttonGitContextMenuCommandsAdd.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace;
			this.buttonGitContextMenuCommandsAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonGitContextMenuCommandsAdd.Image = global::GitBranchView.Properties.Resources.add;
			this.buttonGitContextMenuCommandsAdd.Location = new System.Drawing.Point(630, 25);
			this.buttonGitContextMenuCommandsAdd.Name = "buttonGitContextMenuCommandsAdd";
			this.buttonGitContextMenuCommandsAdd.Size = new System.Drawing.Size(21, 21);
			this.buttonGitContextMenuCommandsAdd.TabIndex = 8;
			this.buttonGitContextMenuCommandsAdd.UseVisualStyleBackColor = true;
			this.buttonGitContextMenuCommandsAdd.Click += new System.EventHandler(this.ButtonGitContextMenuCommandsAdd_Click);
			// 
			// groupBoxGitExe
			// 
			this.groupBoxGitExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
			// tabPageRootFolders
			// 
			this.tabPageRootFolders.Controls.Add(this.groupBoxRootPathFilters);
			this.tabPageRootFolders.Controls.Add(this.buttonRootPathRemove);
			this.tabPageRootFolders.Controls.Add(this.buttonRootPathAdd);
			this.tabPageRootFolders.Controls.Add(this.comboBoxRootPaths);
			this.tabPageRootFolders.Location = new System.Drawing.Point(4, 22);
			this.tabPageRootFolders.Name = "tabPageRootFolders";
			this.tabPageRootFolders.Padding = new System.Windows.Forms.Padding(6);
			this.tabPageRootFolders.Size = new System.Drawing.Size(680, 389);
			this.tabPageRootFolders.TabIndex = 1;
			this.tabPageRootFolders.Text = "Root Folders";
			this.tabPageRootFolders.UseVisualStyleBackColor = true;
			// 
			// groupBoxRootPathFilters
			// 
			this.groupBoxRootPathFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxRootPathFilters.Controls.Add(this.panelScrollRootPathFilters);
			this.groupBoxRootPathFilters.Controls.Add(this.buttonRootPathFilterAdd);
			this.groupBoxRootPathFilters.Location = new System.Drawing.Point(9, 36);
			this.groupBoxRootPathFilters.Name = "groupBoxRootPathFilters";
			this.groupBoxRootPathFilters.Size = new System.Drawing.Size(662, 344);
			this.groupBoxRootPathFilters.TabIndex = 4;
			this.groupBoxRootPathFilters.TabStop = false;
			this.groupBoxRootPathFilters.Text = "Filters";
			// 
			// panelScrollRootPathFilters
			// 
			this.panelScrollRootPathFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelScrollRootPathFilters.AutoScroll = true;
			this.panelScrollRootPathFilters.BackColor = System.Drawing.Color.Transparent;
			this.panelScrollRootPathFilters.Controls.Add(this.flowLayoutPanelRootPathFilters);
			this.panelScrollRootPathFilters.Location = new System.Drawing.Point(6, 21);
			this.panelScrollRootPathFilters.Name = "panelScrollRootPathFilters";
			this.panelScrollRootPathFilters.Size = new System.Drawing.Size(623, 317);
			this.panelScrollRootPathFilters.TabIndex = 7;
			// 
			// flowLayoutPanelRootPathFilters
			// 
			this.flowLayoutPanelRootPathFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelRootPathFilters.BackColor = System.Drawing.Color.Transparent;
			this.flowLayoutPanelRootPathFilters.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanelRootPathFilters.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanelRootPathFilters.Name = "flowLayoutPanelRootPathFilters";
			this.flowLayoutPanelRootPathFilters.Size = new System.Drawing.Size(623, 317);
			this.flowLayoutPanelRootPathFilters.TabIndex = 3;
			this.flowLayoutPanelRootPathFilters.Resize += new System.EventHandler(this.FlowLayoutPanelRootPathFilters_Resize);
			// 
			// buttonRootPathFilterAdd
			// 
			this.buttonRootPathFilterAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRootPathFilterAdd.BackColor = System.Drawing.SystemColors.Window;
			this.buttonRootPathFilterAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonRootPathFilterAdd.FlatAppearance.BorderSize = 0;
			this.buttonRootPathFilterAdd.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace;
			this.buttonRootPathFilterAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonRootPathFilterAdd.Image = global::GitBranchView.Properties.Resources.add;
			this.buttonRootPathFilterAdd.Location = new System.Drawing.Point(633, 22);
			this.buttonRootPathFilterAdd.Name = "buttonRootPathFilterAdd";
			this.buttonRootPathFilterAdd.Size = new System.Drawing.Size(21, 21);
			this.buttonRootPathFilterAdd.TabIndex = 6;
			this.buttonRootPathFilterAdd.UseVisualStyleBackColor = true;
			this.buttonRootPathFilterAdd.Click += new System.EventHandler(this.ButtonRootPathFilterAdd_Click);
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
			// tableLayoutPanelButtons
			// 
			this.tableLayoutPanelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelButtons.ColumnCount = 3;
			this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
			this.tableLayoutPanelButtons.Controls.Add(this.buttonOK, 0, 0);
			this.tableLayoutPanelButtons.Controls.Add(this.buttonCancel, 1, 0);
			this.tableLayoutPanelButtons.Controls.Add(this.buttonApply, 2, 0);
			this.tableLayoutPanelButtons.Location = new System.Drawing.Point(9, 433);
			this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
			this.tableLayoutPanelButtons.RowCount = 1;
			this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelButtons.Size = new System.Drawing.Size(688, 23);
			this.tableLayoutPanelButtons.TabIndex = 7;
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
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(706, 468);
			this.Controls.Add(this.tableLayoutPanelButtons);
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
			this.tableLayoutPanelGeneral.ResumeLayout(false);
			this.groupBoxGitRepositoryLink.ResumeLayout(false);
			this.groupBoxGitRepositoryLink.PerformLayout();
			this.groupBoxQuickLaunchFiles.ResumeLayout(false);
			this.groupBoxQuickLaunchFiles.PerformLayout();
			this.groupBoxCustomCommand.ResumeLayout(false);
			this.groupBoxCustomCommand.PerformLayout();
			this.tabPageGit.ResumeLayout(false);
			this.tableLayoutPanelGit.ResumeLayout(false);
			this.panelGit2.ResumeLayout(false);
			this.panelGit2.PerformLayout();
			this.panelGit1.ResumeLayout(false);
			this.panelGit1.PerformLayout();
			this.groupBoxGitContextMenuCommands.ResumeLayout(false);
			this.panelScrollGitContextMenuCommands.ResumeLayout(false);
			this.groupBoxGitExe.ResumeLayout(false);
			this.groupBoxGitExe.PerformLayout();
			this.tabPageRootFolders.ResumeLayout(false);
			this.groupBoxRootPathFilters.ResumeLayout(false);
			this.panelScrollRootPathFilters.ResumeLayout(false);
			this.tableLayoutPanelButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageGeneral;
		private System.Windows.Forms.TabPage tabPageRootFolders;
		private System.Windows.Forms.GroupBox groupBoxCustomCommand;
		private System.Windows.Forms.TextBox textBoxCustomCommandPath;
		private System.Windows.Forms.Button buttonCustomCommandPathBrowse;
		private System.Windows.Forms.Label labelCustomCommandPath;
		private System.Windows.Forms.TextBox textBoxCustomCommandArgs;
		private System.Windows.Forms.Label labelCustomCommandArgs;
		private System.Windows.Forms.Label labelCustomCommandInfo;
		private System.Windows.Forms.CheckBox checkBoxStartWithWindows;
		private System.Windows.Forms.CheckBox checkBoxCloseOnLostFocus;
		private System.Windows.Forms.Button buttonRootPathRemove;
		private System.Windows.Forms.Button buttonRootPathAdd;
		private System.Windows.Forms.ComboBox comboBoxRootPaths;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRootPathFilters;
		private System.Windows.Forms.GroupBox groupBoxRootPathFilters;
		private System.Windows.Forms.Button buttonRootPathFilterAdd;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
		private System.Windows.Forms.Panel panelScrollRootPathFilters;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.CheckBox checkBoxEnableLogging;
		private System.Windows.Forms.TabPage tabPageGit;
		private System.Windows.Forms.CheckBox checkBoxGitEnableRemoteBranchLookup;
		private System.Windows.Forms.GroupBox groupBoxGitContextMenuCommands;
		private System.Windows.Forms.GroupBox groupBoxGitExe;
		private System.Windows.Forms.TextBox textBoxGitExePath;
		private System.Windows.Forms.Button buttonBrowseGitExePath;
		private System.Windows.Forms.Label labelGitExePath;
		private System.Windows.Forms.Panel panelScrollGitContextMenuCommands;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelGitContextMenuCommands;
		private System.Windows.Forms.Button buttonGitContextMenuCommandsAdd;
		private System.Windows.Forms.GroupBox groupBoxQuickLaunchFiles;
		private System.Windows.Forms.Label labelQuickLaunchFilesFilter;
		private System.Windows.Forms.RadioButton radioButtonGitRepositoryLinkLaunchSelectedQuickLaunchFile;
		private System.Windows.Forms.RadioButton radioButtonGitRepositoryLinkExecuteCustomCommand;
		private System.Windows.Forms.Label labelGitRepositoryLinkWhenLinkIsClicked;
		private System.Windows.Forms.GroupBox groupBoxGitRepositoryLink;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGeneral;
		private System.Windows.Forms.TextBox textBoxCustomCommandName;
		private System.Windows.Forms.Label labelCustomCommandName;
		private System.Windows.Forms.TextBox textBoxQuickLaunchFilesFilter;
		private System.Windows.Forms.RadioButton radioButtonQuickLaunchFilesGroupByPath;
		private System.Windows.Forms.Label labelQuickLaunchFilesGroupBy;
		private System.Windows.Forms.RadioButton radioButtonQuickLaunchFilesGroupByExtension;
		private System.Windows.Forms.RadioButton radioButtonQuickLaunchFilesDoNotGroup;
		private System.Windows.Forms.CheckBox checkBoxGitShowCommandOutput;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGit;
		private System.Windows.Forms.Panel panelGit2;
		private System.Windows.Forms.Panel panelGit1;
		private System.Windows.Forms.CheckBox checkBoxGitExcludeLfsRepositories;
	}
}