namespace GitBranchView
{
	partial class CommandForm
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
			this.textBoxPath = new System.Windows.Forms.TextBox();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelPath = new System.Windows.Forms.Label();
			this.labelArgs = new System.Windows.Forms.Label();
			this.textBoxArgs = new System.Windows.Forms.TextBox();
			this.labelInfo = new System.Windows.Forms.Label();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxPath
			// 
			this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPath.Location = new System.Drawing.Point(51, 12);
			this.textBoxPath.Name = "textBoxPath";
			this.textBoxPath.Size = new System.Drawing.Size(312, 22);
			this.textBoxPath.TabIndex = 0;
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowse.Location = new System.Drawing.Point(369, 12);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(30, 20);
			this.buttonBrowse.TabIndex = 1;
			this.buttonBrowse.Text = "...";
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(115, 3);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Controls.Add(this.buttonCancel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.buttonOK, 0, 0);
			this.tableLayoutPanel.Location = new System.Drawing.Point(12, 91);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(387, 29);
			this.tableLayoutPanel.TabIndex = 3;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(196, 3);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// labelPath
			// 
			this.labelPath.AutoSize = true;
			this.labelPath.Location = new System.Drawing.Point(12, 15);
			this.labelPath.Name = "labelPath";
			this.labelPath.Size = new System.Drawing.Size(33, 13);
			this.labelPath.TabIndex = 4;
			this.labelPath.Text = "Path:";
			// 
			// labelArgs
			// 
			this.labelArgs.AutoSize = true;
			this.labelArgs.Location = new System.Drawing.Point(12, 43);
			this.labelArgs.Name = "labelArgs";
			this.labelArgs.Size = new System.Drawing.Size(33, 13);
			this.labelArgs.TabIndex = 6;
			this.labelArgs.Text = "Args:";
			// 
			// textBoxArgs
			// 
			this.textBoxArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxArgs.Location = new System.Drawing.Point(51, 40);
			this.textBoxArgs.Name = "textBoxArgs";
			this.textBoxArgs.Size = new System.Drawing.Size(312, 22);
			this.textBoxArgs.TabIndex = 5;
			// 
			// labelInfo
			// 
			this.labelInfo.AutoSize = true;
			this.labelInfo.Location = new System.Drawing.Point(48, 65);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(299, 13);
			this.labelInfo.TabIndex = 7;
			this.labelInfo.Text = "(use <path> as a placeholder for the Git repository path)";
			// 
			// CommandForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(411, 125);
			this.Controls.Add(this.labelInfo);
			this.Controls.Add(this.labelArgs);
			this.Controls.Add(this.textBoxArgs);
			this.Controls.Add(this.labelPath);
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.buttonBrowse);
			this.Controls.Add(this.textBoxPath);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "CommandForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Command...";
			this.Load += new System.EventHandler(this.ShellExeSelectForm_Load);
			this.tableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxPath;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelPath;
		private System.Windows.Forms.Label labelArgs;
		private System.Windows.Forms.TextBox textBoxArgs;
		private System.Windows.Forms.Label labelInfo;
	}
}