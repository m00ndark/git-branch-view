namespace GitBranchView.Forms
{
	partial class CloneForm
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
			this.labelRepositoryUrl = new System.Windows.Forms.Label();
			this.textBoxRepositoryUrl = new System.Windows.Forms.TextBox();
			this.textBoxFolderName = new System.Windows.Forms.TextBox();
			this.labelFolderName = new System.Windows.Forms.Label();
			this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
			this.buttonClone = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelRootFolderHeader = new System.Windows.Forms.Label();
			this.labelRootFolder = new System.Windows.Forms.Label();
			this.pictureBoxProgress = new System.Windows.Forms.PictureBox();
			this.labelCloning = new System.Windows.Forms.Label();
			this.tableLayoutPanelProgress = new System.Windows.Forms.TableLayoutPanel();
			this.panelProgress = new System.Windows.Forms.Panel();
			this.tableLayoutPanelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgress)).BeginInit();
			this.tableLayoutPanelProgress.SuspendLayout();
			this.panelProgress.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelRepositoryUrl
			// 
			this.labelRepositoryUrl.AutoSize = true;
			this.labelRepositoryUrl.Location = new System.Drawing.Point(12, 37);
			this.labelRepositoryUrl.Name = "labelRepositoryUrl";
			this.labelRepositoryUrl.Size = new System.Drawing.Size(88, 13);
			this.labelRepositoryUrl.TabIndex = 0;
			this.labelRepositoryUrl.Text = "Repository URL:";
			// 
			// textBoxRepositoryUrl
			// 
			this.textBoxRepositoryUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRepositoryUrl.Location = new System.Drawing.Point(106, 34);
			this.textBoxRepositoryUrl.Name = "textBoxRepositoryUrl";
			this.textBoxRepositoryUrl.Size = new System.Drawing.Size(379, 22);
			this.textBoxRepositoryUrl.TabIndex = 1;
			this.textBoxRepositoryUrl.TextChanged += new System.EventHandler(this.TextBoxRepositoryUrl_TextChanged);
			this.textBoxRepositoryUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxRepositoryUrl_KeyDown);
			// 
			// textBoxFolderName
			// 
			this.textBoxFolderName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFolderName.Location = new System.Drawing.Point(106, 62);
			this.textBoxFolderName.Name = "textBoxFolderName";
			this.textBoxFolderName.Size = new System.Drawing.Size(379, 22);
			this.textBoxFolderName.TabIndex = 3;
			this.textBoxFolderName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxFolderName_KeyDown);
			// 
			// labelFolderName
			// 
			this.labelFolderName.AutoSize = true;
			this.labelFolderName.Location = new System.Drawing.Point(12, 65);
			this.labelFolderName.Name = "labelFolderName";
			this.labelFolderName.Size = new System.Drawing.Size(74, 13);
			this.labelFolderName.TabIndex = 2;
			this.labelFolderName.Text = "Folder name:";
			// 
			// tableLayoutPanelButtons
			// 
			this.tableLayoutPanelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelButtons.ColumnCount = 2;
			this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelButtons.Controls.Add(this.buttonClone, 0, 0);
			this.tableLayoutPanelButtons.Controls.Add(this.buttonCancel, 1, 0);
			this.tableLayoutPanelButtons.Location = new System.Drawing.Point(12, 102);
			this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
			this.tableLayoutPanelButtons.RowCount = 1;
			this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelButtons.Size = new System.Drawing.Size(473, 23);
			this.tableLayoutPanelButtons.TabIndex = 4;
			// 
			// buttonClone
			// 
			this.buttonClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClone.Location = new System.Drawing.Point(158, 0);
			this.buttonClone.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.buttonClone.Name = "buttonClone";
			this.buttonClone.Size = new System.Drawing.Size(75, 23);
			this.buttonClone.TabIndex = 0;
			this.buttonClone.Text = "Clone";
			this.buttonClone.UseVisualStyleBackColor = true;
			this.buttonClone.Click += new System.EventHandler(this.ButtonClone_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(239, 0);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// labelRootFolderHeader
			// 
			this.labelRootFolderHeader.AutoSize = true;
			this.labelRootFolderHeader.Location = new System.Drawing.Point(12, 9);
			this.labelRootFolderHeader.Name = "labelRootFolderHeader";
			this.labelRootFolderHeader.Size = new System.Drawing.Size(69, 13);
			this.labelRootFolderHeader.TabIndex = 5;
			this.labelRootFolderHeader.Text = "Root folder:";
			// 
			// labelRootFolder
			// 
			this.labelRootFolder.AutoSize = true;
			this.labelRootFolder.Location = new System.Drawing.Point(103, 9);
			this.labelRootFolder.Name = "labelRootFolder";
			this.labelRootFolder.Size = new System.Drawing.Size(45, 13);
			this.labelRootFolder.TabIndex = 6;
			this.labelRootFolder.Text = "<root>";
			// 
			// pictureBoxProgress
			// 
			this.pictureBoxProgress.Image = global::GitBranchView.Properties.Resources.progress_large;
			this.pictureBoxProgress.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxProgress.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.pictureBoxProgress.Name = "pictureBoxProgress";
			this.pictureBoxProgress.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxProgress.TabIndex = 0;
			this.pictureBoxProgress.TabStop = false;
			// 
			// labelCloning
			// 
			this.labelCloning.Location = new System.Drawing.Point(35, 0);
			this.labelCloning.Name = "labelCloning";
			this.labelCloning.Size = new System.Drawing.Size(57, 32);
			this.labelCloning.TabIndex = 1;
			this.labelCloning.Text = "Cloning...";
			this.labelCloning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanelProgress
			// 
			this.tableLayoutPanelProgress.ColumnCount = 1;
			this.tableLayoutPanelProgress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelProgress.Controls.Add(this.panelProgress, 0, 0);
			this.tableLayoutPanelProgress.Location = new System.Drawing.Point(12, 93);
			this.tableLayoutPanelProgress.Name = "tableLayoutPanelProgress";
			this.tableLayoutPanelProgress.RowCount = 1;
			this.tableLayoutPanelProgress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelProgress.Size = new System.Drawing.Size(473, 32);
			this.tableLayoutPanelProgress.TabIndex = 8;
			this.tableLayoutPanelProgress.Visible = false;
			// 
			// panelProgress
			// 
			this.panelProgress.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.panelProgress.Controls.Add(this.pictureBoxProgress);
			this.panelProgress.Controls.Add(this.labelCloning);
			this.panelProgress.Location = new System.Drawing.Point(190, 0);
			this.panelProgress.Margin = new System.Windows.Forms.Padding(0);
			this.panelProgress.Name = "panelProgress";
			this.panelProgress.Size = new System.Drawing.Size(92, 32);
			this.panelProgress.TabIndex = 9;
			// 
			// CloneForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(497, 137);
			this.Controls.Add(this.labelRootFolder);
			this.Controls.Add(this.labelRootFolderHeader);
			this.Controls.Add(this.tableLayoutPanelButtons);
			this.Controls.Add(this.textBoxFolderName);
			this.Controls.Add(this.labelFolderName);
			this.Controls.Add(this.textBoxRepositoryUrl);
			this.Controls.Add(this.labelRepositoryUrl);
			this.Controls.Add(this.tableLayoutPanelProgress);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CloneForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Git Branch View - Clone";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloneForm_FormClosing);
			this.Load += new System.EventHandler(this.CloneForm_Load);
			this.tableLayoutPanelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgress)).EndInit();
			this.tableLayoutPanelProgress.ResumeLayout(false);
			this.panelProgress.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelRepositoryUrl;
		private System.Windows.Forms.TextBox textBoxRepositoryUrl;
		private System.Windows.Forms.TextBox textBoxFolderName;
		private System.Windows.Forms.Label labelFolderName;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
		private System.Windows.Forms.Button buttonClone;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelRootFolderHeader;
		private System.Windows.Forms.Label labelRootFolder;
		private System.Windows.Forms.PictureBox pictureBoxProgress;
		private System.Windows.Forms.Label labelCloning;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProgress;
		private System.Windows.Forms.Panel panelProgress;
	}
}