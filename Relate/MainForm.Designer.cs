﻿namespace Relate
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
			this.entriesView = new System.Windows.Forms.DataGridView();
			this.filterTextBox = new System.Windows.Forms.TextBox();
			this.addButton = new System.Windows.Forms.Button();
			this.entryLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.entriesView)).BeginInit();
			this.SuspendLayout();
			// 
			// entriesView
			// 
			this.entriesView.AllowUserToAddRows = false;
			this.entriesView.AllowUserToResizeColumns = false;
			this.entriesView.AllowUserToResizeRows = false;
			this.entriesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.entriesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.entriesView.ColumnHeadersVisible = false;
			this.entriesView.Location = new System.Drawing.Point(12, 40);
			this.entriesView.MultiSelect = false;
			this.entriesView.Name = "entriesView";
			this.entriesView.ReadOnly = true;
			this.entriesView.RowHeadersVisible = false;
			this.entriesView.RowHeadersWidth = 51;
			this.entriesView.RowTemplate.Height = 24;
			this.entriesView.Size = new System.Drawing.Size(258, 385);
			this.entriesView.TabIndex = 0;
			this.entriesView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.entriesView_CellMouseClick);
			// 
			// filterTextBox
			// 
			this.filterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filterTextBox.Location = new System.Drawing.Point(12, 12);
			this.filterTextBox.Name = "filterTextBox";
			this.filterTextBox.Size = new System.Drawing.Size(177, 22);
			this.filterTextBox.TabIndex = 1;
			this.filterTextBox.TextChanged += new System.EventHandler(this.filterTextBox_TextChanged);
			// 
			// addButton
			// 
			this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.addButton.Location = new System.Drawing.Point(195, 11);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(75, 23);
			this.addButton.TabIndex = 2;
			this.addButton.Text = "Add";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// entryLabel
			// 
			this.entryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.entryLabel.AutoSize = true;
			this.entryLabel.Location = new System.Drawing.Point(12, 428);
			this.entryLabel.Name = "entryLabel";
			this.entryLabel.Size = new System.Drawing.Size(37, 16);
			this.entryLabel.TabIndex = 3;
			this.entryLabel.Text = "Entry";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(282, 453);
			this.Controls.Add(this.entryLabel);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.filterTextBox);
			this.Controls.Add(this.entriesView);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RELATE";
			((System.ComponentModel.ISupportInitialize)(this.entriesView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView entriesView;
		private System.Windows.Forms.TextBox filterTextBox;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Label entryLabel;
	}
}

