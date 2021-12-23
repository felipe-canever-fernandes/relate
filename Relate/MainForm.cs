﻿using RelateLibrary;
using RelateLibrary.Database;

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Relate
{
	public partial class MainForm : Form
	{
		#region Fields

		private BindingList<Entry> _entries;
		private Entry _selectedEntry;

		#endregion

		public MainForm()
		{
			InitializeComponent();
			ActiveControl = _filterTextBox;
		}

		#region Properties

		private string Filter => _filterTextBox.Text.Trim();

		private BindingList<Entry> Entries
		{
			get => _entries;

			set
			{
				Debug.Assert(!(value is null));
				_entries = value;

				_entriesDataGridView.DataSource = Entries;

				SelectEntriesDataGridViewRow();
			}
		}

		private Entry SelectedEntry
		{
			get => _selectedEntry;

			set
			{
				_selectedEntry = value;

				if (SelectedEntry == null)
				{
					_selectedEntryGroupBox.Enabled = false;
					_selectedEntryGroupBox.Text = "";
				}
				else
				{
					_selectedEntryGroupBox.Enabled = true;
					_selectedEntryGroupBox.Text = SelectedEntry.Name;
				}

				if (_relatedEntriesCheckBox.Checked)
				{
					FilterEntries();
				}
			}
		}

		#endregion

		#region Methods

		private void SetUp()
		{
			FilterEntries();
			SetUpEntriesDataGridView();
			SelectedEntry = null;
			_relatedEntriesCheckBox.Checked = true;
		}

		private void SetUpEntriesDataGridView()
		{
			_entriesDataGridView.Columns["Id"].Visible = false;

			_entriesDataGridView.Columns["Name"].AutoSizeMode =
				DataGridViewAutoSizeColumnMode.Fill;
		}

		private void FilterEntries()
		{
			UpdateEntries();
			UpdateCreateEntryButton();
		}

		private void UpdateEntries()
		{
			var relatedTo =
				_relatedEntriesCheckBox.Checked ?
				SelectedEntry :
				null;

			var entries = new List<Entry>();

			if (!(relatedTo is null))
				entries.Add(relatedTo);

			entries.AddRange(Database.ReadEntries(relatedTo, Filter));

			Entries = new BindingList<Entry>(entries);
		}

		private void UpdateCreateEntryButton()
		{
			var isFilterEmpty = string.IsNullOrEmpty(Filter);
			var entryAlreadyExists = Database.GetEntryId(Filter) > 0;

			_createEntryButton.Enabled = !isFilterEmpty && !entryAlreadyExists;
		}

		private void SelectEntriesDataGridViewRow()
		{
			_entriesDataGridView.ClearSelection();

			if (!(SelectedEntry is null))
			{
				HighlightEntriesDataGridViewRow();
			}
		}

		private void HighlightEntriesDataGridViewRow()
		{
			for (int i = 0; i < Entries.Count; i++)
			{
				if (Entries[i].Id == SelectedEntry.Id)
				{
					_entriesDataGridView.Rows[i].Selected = true;
					_entriesDataGridView.FirstDisplayedScrollingRowIndex = i;

					break;
				}
			}
		}

		private void ClearFilterTextBox() => _filterTextBox.Text = "";

		private void CreateEntry()
		{
			var entry = new Entry(Filter);
			var entryId = Database.Create(entry);


			if (entryId <= 0)
			{
				_ = MessageBox.Show
				(
					this,
					"The entry could not be created.",
					"Create entry",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}
			else
			{
				ClearFilterTextBox();
			}
		}

		private void RenameEntry()
		{
			var renameForm = new RenameForm(SelectedEntry);
			var answer = renameForm.ShowDialog();

			if (answer == DialogResult.OK)
			{
				UpdateEntry();
				FilterEntries();
			}
		}

		private void DeleteEntry()
		{
			var answer = MessageBox.Show
			(
				this,
				$"Are you sure you want to delete \"{SelectedEntry.Name}\"?",
				"Delete entry",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2
			);

			if (answer == DialogResult.Yes)
			{
				var wasDeleteSuccessful = Database.Delete(SelectedEntry);

				if (wasDeleteSuccessful)
				{
					SelectedEntry = null;
					FilterEntries();
				}
				else
				{
					_ = MessageBox.Show
					(
						this,
						"The entry could not be deleted.",
						"Delete entry",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
				}
			}
		}

		private void SelectEntry()
		{
			SelectedEntry =
				_entriesDataGridView.CurrentRow.DataBoundItem as Entry;

			ClearFilterTextBox();
		}

		private void DeselectEntry()
		{
			if (!(SelectedEntry is null))
			{
				SelectedEntry = null;
			}
		}

		private void UpdateEntry()
		{
			SelectedEntry = Database.ReadEntry(SelectedEntry.Id);
		}

		#endregion

		#region Event Handlers

		private void MainForm_Shown(object sender, System.EventArgs e)
		{
			SetUp();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _filterTextBox_TextChanged
		#pragma warning restore IDE1006 // Naming Styles
		(
			object sender, System.EventArgs e
		)
		{
			FilterEntries();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _createEntryButton_Click
		(
			object sender, System.EventArgs e
		)
		#pragma warning restore IDE1006 // Naming Styles
		{
			CreateEntry();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _selectedEntryGroupBox_EnabledChanged
		(
			object sender, System.EventArgs e
		)
		#pragma warning restore IDE1006 // Naming Styles
		{
			_relatedEntriesCheckBox.Enabled = _selectedEntryGroupBox.Enabled;
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _renameSelectedEntryButton_Click
		#pragma warning restore IDE1006 // Naming Styles
		(
			object sender, System.EventArgs e
		)
		{
			RenameEntry();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _deleteSelectedEntryButton_Click
		#pragma warning restore IDE1006 // Naming Styles
		(
			object sender, System.EventArgs e
		)
		{
			DeleteEntry();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _deselectButton_Click(object sender, System.EventArgs e)
		#pragma warning restore IDE1006 // Naming Styles
		{
			DeselectEntry();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _relatedEntriesCheckBox_CheckedChanged
		(
			object sender, System.EventArgs e
		)
		#pragma warning restore IDE1006 // Naming Styles
		{
			FilterEntries();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _entriesDataGridView_CellClick
		#pragma warning restore IDE1006 // Naming Styles
		(
			object sender, DataGridViewCellEventArgs e
		)
		{
			SelectEntry();
		}

		#endregion
	}
}
