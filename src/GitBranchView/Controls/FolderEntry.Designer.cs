namespace GitBranchView.Controls
{
	partial class FolderEntry
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.linkLabelFolder = new System.Windows.Forms.LinkLabel();
			this.labelBranch = new System.Windows.Forms.Label();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.labelChanges = new System.Windows.Forms.Label();
			this.buttonMore = new System.Windows.Forms.Button();
			this.linkLabelBranchError = new System.Windows.Forms.LinkLabel();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// linkLabelFolder
			// 
			this.linkLabelFolder.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelFolder.AutoSize = true;
			this.linkLabelFolder.BackColor = System.Drawing.Color.Transparent;
			this.linkLabelFolder.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelFolder.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelFolder.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelFolder.Location = new System.Drawing.Point(20, 3);
			this.linkLabelFolder.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
			this.linkLabelFolder.Name = "linkLabelFolder";
			this.linkLabelFolder.Size = new System.Drawing.Size(40, 13);
			this.linkLabelFolder.TabIndex = 0;
			this.linkLabelFolder.TabStop = true;
			this.linkLabelFolder.Text = "Folder";
			this.linkLabelFolder.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelPath_LinkClicked);
			// 
			// labelBranch
			// 
			this.labelBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelBranch.AutoSize = true;
			this.labelBranch.BackColor = System.Drawing.Color.Transparent;
			this.labelBranch.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.labelBranch.Location = new System.Drawing.Point(145, 3);
			this.labelBranch.Name = "labelBranch";
			this.labelBranch.Size = new System.Drawing.Size(43, 13);
			this.labelBranch.TabIndex = 1;
			this.labelBranch.Text = "Branch";
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
			// 
			// labelChanges
			// 
			this.labelChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelChanges.AutoSize = true;
			this.labelChanges.BackColor = System.Drawing.Color.Transparent;
			this.labelChanges.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelChanges.Location = new System.Drawing.Point(194, 3);
			this.labelChanges.Name = "labelChanges";
			this.labelChanges.Size = new System.Drawing.Size(52, 13);
			this.labelChanges.TabIndex = 9;
			this.labelChanges.Text = "Changes";
			// 
			// buttonMore
			// 
			this.buttonMore.BackColor = System.Drawing.Color.Transparent;
			this.buttonMore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonMore.FlatAppearance.BorderSize = 0;
			this.buttonMore.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace;
			this.buttonMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonMore.Image = global::GitBranchView.Properties.Resources.more;
			this.buttonMore.Location = new System.Drawing.Point(0, 0);
			this.buttonMore.Name = "buttonMore";
			this.buttonMore.Size = new System.Drawing.Size(21, 21);
			this.buttonMore.TabIndex = 7;
			this.buttonMore.UseVisualStyleBackColor = false;
			this.buttonMore.Click += new System.EventHandler(this.ButtonMore_Click);
			// 
			// linkLabelBranchError
			// 
			this.linkLabelBranchError.ActiveLinkColor = System.Drawing.Color.Tomato;
			this.linkLabelBranchError.AutoSize = true;
			this.linkLabelBranchError.BackColor = System.Drawing.Color.Transparent;
			this.linkLabelBranchError.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelBranchError.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelBranchError.LinkColor = System.Drawing.Color.Tomato;
			this.linkLabelBranchError.Location = new System.Drawing.Point(252, 3);
			this.linkLabelBranchError.Name = "linkLabelBranchError";
			this.linkLabelBranchError.Size = new System.Drawing.Size(43, 13);
			this.linkLabelBranchError.TabIndex = 10;
			this.linkLabelBranchError.TabStop = true;
			this.linkLabelBranchError.Text = "Branch";
			this.toolTip.SetToolTip(this.linkLabelBranchError, "An error occurred. Click link for more information.");
			this.linkLabelBranchError.VisitedLinkColor = System.Drawing.Color.Tomato;
			this.linkLabelBranchError.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelBranchError_LinkClicked);
			// 
			// FolderEntry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScrollMargin = new System.Drawing.Size(16, 0);
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.linkLabelBranchError);
			this.Controls.Add(this.labelChanges);
			this.Controls.Add(this.buttonMore);
			this.Controls.Add(this.labelBranch);
			this.Controls.Add(this.linkLabelFolder);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "FolderEntry";
			this.Size = new System.Drawing.Size(366, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel linkLabelFolder;
		private System.Windows.Forms.Label labelBranch;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.Label labelChanges;
		private System.Windows.Forms.Button buttonMore;
		private System.Windows.Forms.LinkLabel linkLabelBranchError;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
