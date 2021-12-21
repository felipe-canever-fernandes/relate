namespace Relate
{
	partial class RenameForm
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
			this.renamePanel = new System.Windows.Forms.Panel();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.renameButton = new System.Windows.Forms.Button();
			this.renameLabel = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.renamePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// renamePanel
			// 
			this.renamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.renamePanel.Controls.Add(this.renameLabel);
			this.renamePanel.Controls.Add(this.cancelButton);
			this.renamePanel.Controls.Add(this.renameButton);
			this.renamePanel.Controls.Add(this.nameTextBox);
			this.renamePanel.Location = new System.Drawing.Point(12, 12);
			this.renamePanel.Name = "renamePanel";
			this.renamePanel.Size = new System.Drawing.Size(258, 72);
			this.renamePanel.TabIndex = 0;
			// 
			// nameTextBox
			// 
			this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameTextBox.Location = new System.Drawing.Point(0, 19);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(252, 22);
			this.nameTextBox.TabIndex = 0;
			this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
			// 
			// renameButton
			// 
			this.renameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.renameButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.renameButton.Location = new System.Drawing.Point(96, 46);
			this.renameButton.Name = "renameButton";
			this.renameButton.Size = new System.Drawing.Size(75, 23);
			this.renameButton.TabIndex = 1;
			this.renameButton.Text = "Rename";
			this.renameButton.UseVisualStyleBackColor = true;
			this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
			// 
			// renameLabel
			// 
			this.renameLabel.AutoSize = true;
			this.renameLabel.Location = new System.Drawing.Point(3, 0);
			this.renameLabel.Name = "renameLabel";
			this.renameLabel.Size = new System.Drawing.Size(102, 16);
			this.renameLabel.TabIndex = 2;
			this.renameLabel.Text = "Rename \"Entry\"";
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(177, 47);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// RenameForm
			// 
			this.AcceptButton = this.renameButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(282, 97);
			this.Controls.Add(this.renamePanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "RenameForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Rename";
			this.renamePanel.ResumeLayout(false);
			this.renamePanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel renamePanel;
		private System.Windows.Forms.Button renameButton;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label renameLabel;
		private System.Windows.Forms.Button cancelButton;
	}
}