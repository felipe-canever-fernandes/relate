namespace Relate
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
			this._entriesDataGridView = new System.Windows.Forms.DataGridView();
			this._filterTextBox = new System.Windows.Forms.TextBox();
			this._addButton = new System.Windows.Forms.Button();
			this._selectedEntryNameLabel = new System.Windows.Forms.Label();
			this._selectedEntryPanel = new System.Windows.Forms.Panel();
			this._relatedEntriesCheckBox = new System.Windows.Forms.CheckBox();
			this._renameSelectedEntryButton = new System.Windows.Forms.Button();
			this._deleteSelectedEntryButton = new System.Windows.Forms.Button();
			this._filterPanel = new System.Windows.Forms.Panel();
			this._deselectButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._entriesDataGridView)).BeginInit();
			this._selectedEntryPanel.SuspendLayout();
			this._filterPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _entriesDataGridView
			// 
			this._entriesDataGridView.AllowUserToAddRows = false;
			this._entriesDataGridView.AllowUserToResizeColumns = false;
			this._entriesDataGridView.AllowUserToResizeRows = false;
			this._entriesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._entriesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._entriesDataGridView.ColumnHeadersVisible = false;
			this._entriesDataGridView.Location = new System.Drawing.Point(12, 103);
			this._entriesDataGridView.MultiSelect = false;
			this._entriesDataGridView.Name = "_entriesDataGridView";
			this._entriesDataGridView.ReadOnly = true;
			this._entriesDataGridView.RowHeadersVisible = false;
			this._entriesDataGridView.RowHeadersWidth = 51;
			this._entriesDataGridView.RowTemplate.Height = 24;
			this._entriesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._entriesDataGridView.Size = new System.Drawing.Size(458, 338);
			this._entriesDataGridView.TabIndex = 0;
			this._entriesDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._entriesDataGridView_CellClick);
			// 
			// _filterTextBox
			// 
			this._filterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._filterTextBox.Location = new System.Drawing.Point(3, 3);
			this._filterTextBox.Name = "_filterTextBox";
			this._filterTextBox.Size = new System.Drawing.Size(371, 22);
			this._filterTextBox.TabIndex = 1;
			this._filterTextBox.TextChanged += new System.EventHandler(this._filterTextBox_TextChanged);
			// 
			// _addButton
			// 
			this._addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._addButton.Location = new System.Drawing.Point(380, 3);
			this._addButton.Name = "_addButton";
			this._addButton.Size = new System.Drawing.Size(75, 23);
			this._addButton.TabIndex = 2;
			this._addButton.Text = "Add";
			this._addButton.UseVisualStyleBackColor = true;
			this._addButton.Click += new System.EventHandler(this._addButton_Click);
			// 
			// _selectedEntryNameLabel
			// 
			this._selectedEntryNameLabel.AutoSize = true;
			this._selectedEntryNameLabel.Location = new System.Drawing.Point(3, 6);
			this._selectedEntryNameLabel.Name = "_selectedEntryNameLabel";
			this._selectedEntryNameLabel.Size = new System.Drawing.Size(37, 16);
			this._selectedEntryNameLabel.TabIndex = 3;
			this._selectedEntryNameLabel.Text = "Entry";
			// 
			// _selectedEntryPanel
			// 
			this._selectedEntryPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._selectedEntryPanel.Controls.Add(this._relatedEntriesCheckBox);
			this._selectedEntryPanel.Controls.Add(this._deselectButton);
			this._selectedEntryPanel.Controls.Add(this._renameSelectedEntryButton);
			this._selectedEntryPanel.Controls.Add(this._deleteSelectedEntryButton);
			this._selectedEntryPanel.Controls.Add(this._selectedEntryNameLabel);
			this._selectedEntryPanel.Location = new System.Drawing.Point(12, 46);
			this._selectedEntryPanel.Name = "_selectedEntryPanel";
			this._selectedEntryPanel.Size = new System.Drawing.Size(458, 51);
			this._selectedEntryPanel.TabIndex = 4;
			// 
			// _relatedEntriesCheckBox
			// 
			this._relatedEntriesCheckBox.AutoSize = true;
			this._relatedEntriesCheckBox.Location = new System.Drawing.Point(84, 28);
			this._relatedEntriesCheckBox.Name = "_relatedEntriesCheckBox";
			this._relatedEntriesCheckBox.Size = new System.Drawing.Size(77, 20);
			this._relatedEntriesCheckBox.TabIndex = 5;
			this._relatedEntriesCheckBox.Text = "Related";
			this._relatedEntriesCheckBox.UseVisualStyleBackColor = true;
			this._relatedEntriesCheckBox.CheckedChanged += new System.EventHandler(this._relatedEntriesCheckBox_CheckedChanged);
			// 
			// _renameSelectedEntryButton
			// 
			this._renameSelectedEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._renameSelectedEntryButton.Location = new System.Drawing.Point(299, 3);
			this._renameSelectedEntryButton.Name = "_renameSelectedEntryButton";
			this._renameSelectedEntryButton.Size = new System.Drawing.Size(75, 23);
			this._renameSelectedEntryButton.TabIndex = 4;
			this._renameSelectedEntryButton.Text = "Rename";
			this._renameSelectedEntryButton.UseVisualStyleBackColor = true;
			this._renameSelectedEntryButton.Click += new System.EventHandler(this._renameSelectedEntryButton_Click);
			// 
			// _deleteSelectedEntryButton
			// 
			this._deleteSelectedEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._deleteSelectedEntryButton.Location = new System.Drawing.Point(380, 3);
			this._deleteSelectedEntryButton.Name = "_deleteSelectedEntryButton";
			this._deleteSelectedEntryButton.Size = new System.Drawing.Size(75, 23);
			this._deleteSelectedEntryButton.TabIndex = 4;
			this._deleteSelectedEntryButton.Text = "Delete";
			this._deleteSelectedEntryButton.UseVisualStyleBackColor = true;
			this._deleteSelectedEntryButton.Click += new System.EventHandler(this._deleteSelectedEntryButton_Click);
			// 
			// _filterPanel
			// 
			this._filterPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._filterPanel.Controls.Add(this._filterTextBox);
			this._filterPanel.Controls.Add(this._addButton);
			this._filterPanel.Location = new System.Drawing.Point(12, 12);
			this._filterPanel.Name = "_filterPanel";
			this._filterPanel.Size = new System.Drawing.Size(458, 28);
			this._filterPanel.TabIndex = 5;
			// 
			// _deselectButton
			// 
			this._deselectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._deselectButton.Location = new System.Drawing.Point(3, 25);
			this._deselectButton.Name = "_deselectButton";
			this._deselectButton.Size = new System.Drawing.Size(75, 23);
			this._deselectButton.TabIndex = 4;
			this._deselectButton.Text = "Deselect";
			this._deselectButton.UseVisualStyleBackColor = true;
			this._deselectButton.Click += new System.EventHandler(this._renameSelectedEntryButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(482, 453);
			this.Controls.Add(this._filterPanel);
			this.Controls.Add(this._selectedEntryPanel);
			this.Controls.Add(this._entriesDataGridView);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RELATE";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			((System.ComponentModel.ISupportInitialize)(this._entriesDataGridView)).EndInit();
			this._selectedEntryPanel.ResumeLayout(false);
			this._selectedEntryPanel.PerformLayout();
			this._filterPanel.ResumeLayout(false);
			this._filterPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView _entriesDataGridView;
		private System.Windows.Forms.TextBox _filterTextBox;
		private System.Windows.Forms.Button _addButton;
		private System.Windows.Forms.Label _selectedEntryNameLabel;
		private System.Windows.Forms.Panel _selectedEntryPanel;
		private System.Windows.Forms.Button _deleteSelectedEntryButton;
		private System.Windows.Forms.Panel _filterPanel;
		private System.Windows.Forms.Button _renameSelectedEntryButton;
		private System.Windows.Forms.CheckBox _relatedEntriesCheckBox;
		private System.Windows.Forms.Button _deselectButton;
	}
}

