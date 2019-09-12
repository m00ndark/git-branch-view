namespace GitBranchView.Controls
{
	partial class FilterEntry
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
			this.labelFilter = new System.Windows.Forms.Label();
			this.linkLabelEdit = new System.Windows.Forms.LinkLabel();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonMoveUp = new System.Windows.Forms.Button();
			this.buttonMoveDown = new System.Windows.Forms.Button();
			this.labelTypeAndTarget = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelFilter
			// 
			this.labelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelFilter.AutoEllipsis = true;
			this.labelFilter.BackColor = System.Drawing.Color.Transparent;
			this.labelFilter.Location = new System.Drawing.Point(139, 4);
			this.labelFilter.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.labelFilter.Name = "labelFilter";
			this.labelFilter.Size = new System.Drawing.Size(330, 13);
			this.labelFilter.TabIndex = 9;
			this.labelFilter.Text = "Filter";
			// 
			// linkLabelEdit
			// 
			this.linkLabelEdit.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelEdit.AutoSize = true;
			this.linkLabelEdit.BackColor = System.Drawing.Color.Transparent;
			this.linkLabelEdit.DisabledLinkColor = System.Drawing.Color.Gray;
			this.linkLabelEdit.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabelEdit.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelEdit.Location = new System.Drawing.Point(83, 4);
			this.linkLabelEdit.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
			this.linkLabelEdit.Name = "linkLabelEdit";
			this.linkLabelEdit.Size = new System.Drawing.Size(36, 13);
			this.linkLabelEdit.TabIndex = 10;
			this.linkLabelEdit.TabStop = true;
			this.linkLabelEdit.Text = "Edit...";
			this.linkLabelEdit.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.linkLabelEdit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelEdit_LinkClicked);
			// 
			// buttonDelete
			// 
			this.buttonDelete.BackColor = System.Drawing.SystemColors.Window;
			this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonDelete.FlatAppearance.BorderSize = 0;
			this.buttonDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace;
			this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonDelete.Image = global::GitBranchView.Properties.Resources.remove;
			this.buttonDelete.Location = new System.Drawing.Point(0, 0);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(21, 21);
			this.buttonDelete.TabIndex = 7;
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
			// 
			// buttonMoveUp
			// 
			this.buttonMoveUp.BackColor = System.Drawing.SystemColors.Window;
			this.buttonMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonMoveUp.FlatAppearance.BorderSize = 0;
			this.buttonMoveUp.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace;
			this.buttonMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonMoveUp.Image = global::GitBranchView.Properties.Resources.up;
			this.buttonMoveUp.Location = new System.Drawing.Point(25, 0);
			this.buttonMoveUp.Name = "buttonMoveUp";
			this.buttonMoveUp.Size = new System.Drawing.Size(21, 21);
			this.buttonMoveUp.TabIndex = 11;
			this.buttonMoveUp.UseVisualStyleBackColor = true;
			this.buttonMoveUp.Click += new System.EventHandler(this.ButtonMoveUp_Click);
			// 
			// buttonMoveDown
			// 
			this.buttonMoveDown.BackColor = System.Drawing.SystemColors.Window;
			this.buttonMoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonMoveDown.FlatAppearance.BorderSize = 0;
			this.buttonMoveDown.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace;
			this.buttonMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonMoveDown.Image = global::GitBranchView.Properties.Resources.down;
			this.buttonMoveDown.Location = new System.Drawing.Point(48, 0);
			this.buttonMoveDown.Name = "buttonMoveDown";
			this.buttonMoveDown.Size = new System.Drawing.Size(21, 21);
			this.buttonMoveDown.TabIndex = 12;
			this.buttonMoveDown.UseVisualStyleBackColor = true;
			this.buttonMoveDown.Click += new System.EventHandler(this.ButtonMoveDown_Click);
			// 
			// labelTypeAndTarget
			// 
			this.labelTypeAndTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTypeAndTarget.AutoSize = true;
			this.labelTypeAndTarget.BackColor = System.Drawing.Color.Transparent;
			this.labelTypeAndTarget.Location = new System.Drawing.Point(482, 4);
			this.labelTypeAndTarget.Margin = new System.Windows.Forms.Padding(3, 0, 7, 0);
			this.labelTypeAndTarget.Name = "labelTypeAndTarget";
			this.labelTypeAndTarget.Size = new System.Drawing.Size(56, 13);
			this.labelTypeAndTarget.TabIndex = 13;
			this.labelTypeAndTarget.Text = "Highlight";
			// 
			// FilterEntry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.labelTypeAndTarget);
			this.Controls.Add(this.buttonMoveDown);
			this.Controls.Add(this.buttonMoveUp);
			this.Controls.Add(this.linkLabelEdit);
			this.Controls.Add(this.labelFilter);
			this.Controls.Add(this.buttonDelete);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.Name = "FilterEntry";
			this.Size = new System.Drawing.Size(545, 21);
			this.Load += new System.EventHandler(this.FilterEntry_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Label labelFilter;
		private System.Windows.Forms.LinkLabel linkLabelEdit;
		private System.Windows.Forms.Button buttonMoveUp;
		private System.Windows.Forms.Button buttonMoveDown;
		private System.Windows.Forms.Label labelTypeAndTarget;
	}
}
