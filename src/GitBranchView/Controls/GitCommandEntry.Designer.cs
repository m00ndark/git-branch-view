namespace GitBranchView.Controls
{
	partial class GitCommandEntry
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
			this.buttonMoveDown = new System.Windows.Forms.Button();
			this.buttonMoveUp = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.textBoxCaption = new System.Windows.Forms.TextBox();
			this.labelCaption = new System.Windows.Forms.Label();
			this.labelCommand = new System.Windows.Forms.Label();
			this.textBoxCommand = new System.Windows.Forms.TextBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// buttonMoveDown
			// 
			this.buttonMoveDown.BackColor = System.Drawing.SystemColors.Window;
			this.buttonMoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonMoveDown.FlatAppearance.BorderSize = 0;
			this.buttonMoveDown.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace;
			this.buttonMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonMoveDown.Image = global::GitBranchView.Properties.Resources.down;
			this.buttonMoveDown.Location = new System.Drawing.Point(51, 3);
			this.buttonMoveDown.Name = "buttonMoveDown";
			this.buttonMoveDown.Size = new System.Drawing.Size(21, 21);
			this.buttonMoveDown.TabIndex = 15;
			this.buttonMoveDown.UseVisualStyleBackColor = true;
			this.buttonMoveDown.Click += new System.EventHandler(this.ButtonMoveDown_Click);
			// 
			// buttonMoveUp
			// 
			this.buttonMoveUp.BackColor = System.Drawing.SystemColors.Window;
			this.buttonMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonMoveUp.FlatAppearance.BorderSize = 0;
			this.buttonMoveUp.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace;
			this.buttonMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonMoveUp.Image = global::GitBranchView.Properties.Resources.up;
			this.buttonMoveUp.Location = new System.Drawing.Point(28, 3);
			this.buttonMoveUp.Name = "buttonMoveUp";
			this.buttonMoveUp.Size = new System.Drawing.Size(21, 21);
			this.buttonMoveUp.TabIndex = 14;
			this.buttonMoveUp.UseVisualStyleBackColor = true;
			this.buttonMoveUp.Click += new System.EventHandler(this.ButtonMoveUp_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.BackColor = System.Drawing.SystemColors.Window;
			this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonDelete.FlatAppearance.BorderSize = 0;
			this.buttonDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace;
			this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonDelete.Image = global::GitBranchView.Properties.Resources.remove;
			this.buttonDelete.Location = new System.Drawing.Point(3, 3);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(21, 21);
			this.buttonDelete.TabIndex = 13;
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
			// 
			// textBoxCaption
			// 
			this.textBoxCaption.Location = new System.Drawing.Point(140, 3);
			this.textBoxCaption.Name = "textBoxCaption";
			this.textBoxCaption.Size = new System.Drawing.Size(164, 22);
			this.textBoxCaption.TabIndex = 16;
			this.textBoxCaption.TextChanged += new System.EventHandler(this.TextBoxCaption_TextChanged);
			// 
			// labelCaption
			// 
			this.labelCaption.AutoSize = true;
			this.labelCaption.Location = new System.Drawing.Point(83, 6);
			this.labelCaption.Name = "labelCaption";
			this.labelCaption.Size = new System.Drawing.Size(51, 13);
			this.labelCaption.TabIndex = 17;
			this.labelCaption.Text = "Caption:";
			// 
			// labelCommand
			// 
			this.labelCommand.AutoSize = true;
			this.labelCommand.Location = new System.Drawing.Point(310, 7);
			this.labelCommand.Name = "labelCommand";
			this.labelCommand.Size = new System.Drawing.Size(62, 13);
			this.labelCommand.TabIndex = 19;
			this.labelCommand.Text = "Command:";
			// 
			// textBoxCommand
			// 
			this.textBoxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCommand.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxCommand.Location = new System.Drawing.Point(378, 3);
			this.textBoxCommand.Name = "textBoxCommand";
			this.textBoxCommand.Size = new System.Drawing.Size(219, 22);
			this.textBoxCommand.TabIndex = 18;
			this.toolTip.SetToolTip(this.textBoxCommand, "Use <branch> to represent current branch.");
			this.textBoxCommand.TextChanged += new System.EventHandler(this.TextBoxCommand_TextChanged);
			// 
			// GitCommandEntry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.labelCommand);
			this.Controls.Add(this.textBoxCommand);
			this.Controls.Add(this.labelCaption);
			this.Controls.Add(this.textBoxCaption);
			this.Controls.Add(this.buttonMoveDown);
			this.Controls.Add(this.buttonMoveUp);
			this.Controls.Add(this.buttonDelete);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.Name = "GitCommandEntry";
			this.Size = new System.Drawing.Size(600, 28);
			this.Load += new System.EventHandler(this.GitCommandEntry_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonMoveDown;
		private System.Windows.Forms.Button buttonMoveUp;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.TextBox textBoxCaption;
		private System.Windows.Forms.Label labelCaption;
		private System.Windows.Forms.Label labelCommand;
		private System.Windows.Forms.TextBox textBoxCommand;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
