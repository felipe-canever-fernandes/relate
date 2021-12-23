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
			this._createEntryButton = new System.Windows.Forms.Button();
			this._relatedEntriesCheckBox = new System.Windows.Forms.CheckBox();
			this._renameSelectedEntryButton = new System.Windows.Forms.Button();
			this._deleteSelectedEntryButton = new System.Windows.Forms.Button();
			this._deselectButton = new System.Windows.Forms.Button();
			this._selectedEntryGroupBox = new System.Windows.Forms.GroupBox();
			this._relateEntriesButton = new System.Windows.Forms.Button();
			this.filterGroupBox = new System.Windows.Forms.GroupBox();
			this._aboutButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._entriesDataGridView)).BeginInit();
			this._selectedEntryGroupBox.SuspendLayout();
			this.filterGroupBox.SuspendLayout();
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
			this._entriesDataGridView.Location = new System.Drawing.Point(12, 150);
			this._entriesDataGridView.MultiSelect = false;
			this._entriesDataGridView.Name = "_entriesDataGridView";
			this._entriesDataGridView.ReadOnly = true;
			this._entriesDataGridView.RowHeadersVisible = false;
			this._entriesDataGridView.RowHeadersWidth = 51;
			this._entriesDataGridView.RowTemplate.Height = 24;
			this._entriesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._entriesDataGridView.Size = new System.Drawing.Size(358, 362);
			this._entriesDataGridView.TabIndex = 0;
			this._entriesDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._entriesDataGridView_CellDoubleClick);
			this._entriesDataGridView.SelectionChanged += new System.EventHandler(this._entriesDataGridView_SelectionChanged);
			// 
			// _filterTextBox
			// 
			this._filterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._filterTextBox.Location = new System.Drawing.Point(7, 21);
			this._filterTextBox.Name = "_filterTextBox";
			this._filterTextBox.Size = new System.Drawing.Size(236, 22);
			this._filterTextBox.TabIndex = 1;
			this._filterTextBox.TextChanged += new System.EventHandler(this._filterTextBox_TextChanged);
			// 
			// _createEntryButton
			// 
			this._createEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._createEntryButton.Location = new System.Drawing.Point(246, 21);
			this._createEntryButton.Name = "_createEntryButton";
			this._createEntryButton.Size = new System.Drawing.Size(106, 23);
			this._createEntryButton.TabIndex = 2;
			this._createEntryButton.Text = "Create entry...";
			this._createEntryButton.UseVisualStyleBackColor = true;
			this._createEntryButton.Click += new System.EventHandler(this._createEntryButton_Click);
			// 
			// _relatedEntriesCheckBox
			// 
			this._relatedEntriesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._relatedEntriesCheckBox.AutoSize = true;
			this._relatedEntriesCheckBox.Location = new System.Drawing.Point(7, 51);
			this._relatedEntriesCheckBox.Name = "_relatedEntriesCheckBox";
			this._relatedEntriesCheckBox.Size = new System.Drawing.Size(178, 20);
			this._relatedEntriesCheckBox.TabIndex = 5;
			this._relatedEntriesCheckBox.Text = "Related to selected entry";
			this._relatedEntriesCheckBox.UseVisualStyleBackColor = true;
			this._relatedEntriesCheckBox.CheckedChanged += new System.EventHandler(this._relatedEntriesCheckBox_CheckedChanged);
			// 
			// _renameSelectedEntryButton
			// 
			this._renameSelectedEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._renameSelectedEntryButton.Location = new System.Drawing.Point(87, 20);
			this._renameSelectedEntryButton.Name = "_renameSelectedEntryButton";
			this._renameSelectedEntryButton.Size = new System.Drawing.Size(80, 23);
			this._renameSelectedEntryButton.TabIndex = 4;
			this._renameSelectedEntryButton.Text = "Rename...";
			this._renameSelectedEntryButton.UseVisualStyleBackColor = true;
			this._renameSelectedEntryButton.Click += new System.EventHandler(this._renameSelectedEntryButton_Click);
			// 
			// _deleteSelectedEntryButton
			// 
			this._deleteSelectedEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._deleteSelectedEntryButton.Location = new System.Drawing.Point(254, 20);
			this._deleteSelectedEntryButton.Name = "_deleteSelectedEntryButton";
			this._deleteSelectedEntryButton.Size = new System.Drawing.Size(75, 23);
			this._deleteSelectedEntryButton.TabIndex = 4;
			this._deleteSelectedEntryButton.Text = "Delete";
			this._deleteSelectedEntryButton.UseVisualStyleBackColor = true;
			this._deleteSelectedEntryButton.Click += new System.EventHandler(this._deleteSelectedEntryButton_Click);
			// 
			// _deselectButton
			// 
			this._deselectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._deselectButton.Location = new System.Drawing.Point(6, 20);
			this._deselectButton.Name = "_deselectButton";
			this._deselectButton.Size = new System.Drawing.Size(75, 23);
			this._deselectButton.TabIndex = 4;
			this._deselectButton.Text = "Deselect";
			this._deselectButton.UseVisualStyleBackColor = true;
			this._deselectButton.Click += new System.EventHandler(this._deselectButton_Click);
			// 
			// _selectedEntryGroupBox
			// 
			this._selectedEntryGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._selectedEntryGroupBox.Controls.Add(this._relateEntriesButton);
			this._selectedEntryGroupBox.Controls.Add(this._deleteSelectedEntryButton);
			this._selectedEntryGroupBox.Controls.Add(this._renameSelectedEntryButton);
			this._selectedEntryGroupBox.Controls.Add(this._deselectButton);
			this._selectedEntryGroupBox.Location = new System.Drawing.Point(12, 95);
			this._selectedEntryGroupBox.Name = "_selectedEntryGroupBox";
			this._selectedEntryGroupBox.Size = new System.Drawing.Size(358, 49);
			this._selectedEntryGroupBox.TabIndex = 6;
			this._selectedEntryGroupBox.TabStop = false;
			this._selectedEntryGroupBox.Text = "Entry";
			this._selectedEntryGroupBox.EnabledChanged += new System.EventHandler(this._selectedEntryGroupBox_EnabledChanged);
			// 
			// _relateEntriesButton
			// 
			this._relateEntriesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._relateEntriesButton.Location = new System.Drawing.Point(173, 20);
			this._relateEntriesButton.Name = "_relateEntriesButton";
			this._relateEntriesButton.Size = new System.Drawing.Size(75, 23);
			this._relateEntriesButton.TabIndex = 4;
			this._relateEntriesButton.Text = "Relate";
			this._relateEntriesButton.UseVisualStyleBackColor = true;
			// 
			// filterGroupBox
			// 
			this.filterGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filterGroupBox.Controls.Add(this._filterTextBox);
			this.filterGroupBox.Controls.Add(this._createEntryButton);
			this.filterGroupBox.Controls.Add(this._relatedEntriesCheckBox);
			this.filterGroupBox.Location = new System.Drawing.Point(12, 12);
			this.filterGroupBox.Name = "filterGroupBox";
			this.filterGroupBox.Size = new System.Drawing.Size(358, 77);
			this.filterGroupBox.TabIndex = 7;
			this.filterGroupBox.TabStop = false;
			this.filterGroupBox.Text = "Filter";
			// 
			// _aboutButton
			// 
			this._aboutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._aboutButton.Location = new System.Drawing.Point(254, 518);
			this._aboutButton.Name = "_aboutButton";
			this._aboutButton.Size = new System.Drawing.Size(116, 23);
			this._aboutButton.TabIndex = 8;
			this._aboutButton.Text = "About RELATE...";
			this._aboutButton.UseVisualStyleBackColor = true;
			this._aboutButton.Click += new System.EventHandler(this._aboutButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(382, 553);
			this.Controls.Add(this._aboutButton);
			this.Controls.Add(this.filterGroupBox);
			this.Controls.Add(this._selectedEntryGroupBox);
			this.Controls.Add(this._entriesDataGridView);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RELATE";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			((System.ComponentModel.ISupportInitialize)(this._entriesDataGridView)).EndInit();
			this._selectedEntryGroupBox.ResumeLayout(false);
			this.filterGroupBox.ResumeLayout(false);
			this.filterGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView _entriesDataGridView;
		private System.Windows.Forms.TextBox _filterTextBox;
		private System.Windows.Forms.Button _createEntryButton;
		private System.Windows.Forms.Button _deleteSelectedEntryButton;
		private System.Windows.Forms.Button _renameSelectedEntryButton;
		private System.Windows.Forms.CheckBox _relatedEntriesCheckBox;
		private System.Windows.Forms.Button _deselectButton;
		private System.Windows.Forms.GroupBox _selectedEntryGroupBox;
		private System.Windows.Forms.GroupBox filterGroupBox;
		private System.Windows.Forms.Button _aboutButton;
		private System.Windows.Forms.Button _relateEntriesButton;
	}
}

