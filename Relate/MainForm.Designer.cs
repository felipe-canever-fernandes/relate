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
			this._renameCurrentEntryButton = new System.Windows.Forms.Button();
			this._deleteCurrentEntryButton = new System.Windows.Forms.Button();
			this._closeCurrentEntryButton = new System.Windows.Forms.Button();
			this._currentEntryGroupBox = new System.Windows.Forms.GroupBox();
			this._relateEntriesButton = new System.Windows.Forms.Button();
			this.filterGroupBox = new System.Windows.Forms.GroupBox();
			this._aboutButton = new System.Windows.Forms.Button();
			this._currentEntryNameLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this._entriesDataGridView)).BeginInit();
			this._currentEntryGroupBox.SuspendLayout();
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
			this._entriesDataGridView.Location = new System.Drawing.Point(12, 170);
			this._entriesDataGridView.MultiSelect = false;
			this._entriesDataGridView.Name = "_entriesDataGridView";
			this._entriesDataGridView.ReadOnly = true;
			this._entriesDataGridView.RowHeadersVisible = false;
			this._entriesDataGridView.RowHeadersWidth = 51;
			this._entriesDataGridView.RowTemplate.Height = 24;
			this._entriesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._entriesDataGridView.Size = new System.Drawing.Size(358, 342);
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
			this._relatedEntriesCheckBox.Size = new System.Drawing.Size(166, 20);
			this._relatedEntriesCheckBox.TabIndex = 5;
			this._relatedEntriesCheckBox.Text = "Related to current entry";
			this._relatedEntriesCheckBox.UseVisualStyleBackColor = true;
			this._relatedEntriesCheckBox.CheckedChanged += new System.EventHandler(this._relatedEntriesCheckBox_CheckedChanged);
			// 
			// _renameCurrentEntryButton
			// 
			this._renameCurrentEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._renameCurrentEntryButton.Location = new System.Drawing.Point(87, 40);
			this._renameCurrentEntryButton.Name = "_renameCurrentEntryButton";
			this._renameCurrentEntryButton.Size = new System.Drawing.Size(80, 23);
			this._renameCurrentEntryButton.TabIndex = 4;
			this._renameCurrentEntryButton.Text = "Rename...";
			this._renameCurrentEntryButton.UseVisualStyleBackColor = true;
			this._renameCurrentEntryButton.Click += new System.EventHandler(this._renameCurrentEntryButton_Click);
			// 
			// _deleteCurrentEntryButton
			// 
			this._deleteCurrentEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._deleteCurrentEntryButton.Location = new System.Drawing.Point(254, 40);
			this._deleteCurrentEntryButton.Name = "_deleteCurrentEntryButton";
			this._deleteCurrentEntryButton.Size = new System.Drawing.Size(75, 23);
			this._deleteCurrentEntryButton.TabIndex = 4;
			this._deleteCurrentEntryButton.Text = "Delete";
			this._deleteCurrentEntryButton.UseVisualStyleBackColor = true;
			this._deleteCurrentEntryButton.Click += new System.EventHandler(this._deleteCurrentEntryButton_Click);
			// 
			// _closeCurrentEntryButton
			// 
			this._closeCurrentEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._closeCurrentEntryButton.Location = new System.Drawing.Point(6, 40);
			this._closeCurrentEntryButton.Name = "_closeCurrentEntryButton";
			this._closeCurrentEntryButton.Size = new System.Drawing.Size(75, 23);
			this._closeCurrentEntryButton.TabIndex = 4;
			this._closeCurrentEntryButton.Text = "Close";
			this._closeCurrentEntryButton.UseVisualStyleBackColor = true;
			this._closeCurrentEntryButton.Click += new System.EventHandler(this._closeCurrentEntryButton_Click);
			// 
			// _currentEntryGroupBox
			// 
			this._currentEntryGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._currentEntryGroupBox.Controls.Add(this._currentEntryNameLabel);
			this._currentEntryGroupBox.Controls.Add(this._relateEntriesButton);
			this._currentEntryGroupBox.Controls.Add(this._deleteCurrentEntryButton);
			this._currentEntryGroupBox.Controls.Add(this._renameCurrentEntryButton);
			this._currentEntryGroupBox.Controls.Add(this._closeCurrentEntryButton);
			this._currentEntryGroupBox.Location = new System.Drawing.Point(12, 95);
			this._currentEntryGroupBox.Name = "_currentEntryGroupBox";
			this._currentEntryGroupBox.Size = new System.Drawing.Size(358, 69);
			this._currentEntryGroupBox.TabIndex = 6;
			this._currentEntryGroupBox.TabStop = false;
			this._currentEntryGroupBox.Text = "Current entry";
			this._currentEntryGroupBox.EnabledChanged += new System.EventHandler(this._currentEntryGroupBox_EnabledChanged);
			// 
			// _relateEntriesButton
			// 
			this._relateEntriesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._relateEntriesButton.Location = new System.Drawing.Point(173, 40);
			this._relateEntriesButton.Name = "_relateEntriesButton";
			this._relateEntriesButton.Size = new System.Drawing.Size(75, 23);
			this._relateEntriesButton.TabIndex = 4;
			this._relateEntriesButton.Text = "Relate";
			this._relateEntriesButton.UseVisualStyleBackColor = true;
			this._relateEntriesButton.Click += new System.EventHandler(this._relateEntriesButton_Click);
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
			// _currentEntryNameLabel
			// 
			this._currentEntryNameLabel.AutoSize = true;
			this._currentEntryNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._currentEntryNameLabel.Location = new System.Drawing.Point(6, 18);
			this._currentEntryNameLabel.Name = "_currentEntryNameLabel";
			this._currentEntryNameLabel.Size = new System.Drawing.Size(42, 16);
			this._currentEntryNameLabel.TabIndex = 5;
			this._currentEntryNameLabel.Text = "Entry";
			this._currentEntryNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(382, 553);
			this.Controls.Add(this._aboutButton);
			this.Controls.Add(this.filterGroupBox);
			this.Controls.Add(this._currentEntryGroupBox);
			this.Controls.Add(this._entriesDataGridView);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RELATE";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			((System.ComponentModel.ISupportInitialize)(this._entriesDataGridView)).EndInit();
			this._currentEntryGroupBox.ResumeLayout(false);
			this._currentEntryGroupBox.PerformLayout();
			this.filterGroupBox.ResumeLayout(false);
			this.filterGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView _entriesDataGridView;
		private System.Windows.Forms.TextBox _filterTextBox;
		private System.Windows.Forms.Button _createEntryButton;
		private System.Windows.Forms.Button _deleteCurrentEntryButton;
		private System.Windows.Forms.Button _renameCurrentEntryButton;
		private System.Windows.Forms.CheckBox _relatedEntriesCheckBox;
		private System.Windows.Forms.Button _closeCurrentEntryButton;
		private System.Windows.Forms.GroupBox _currentEntryGroupBox;
		private System.Windows.Forms.GroupBox filterGroupBox;
		private System.Windows.Forms.Button _aboutButton;
		private System.Windows.Forms.Button _relateEntriesButton;
		private System.Windows.Forms.Label _currentEntryNameLabel;
	}
}

