﻿using RelateLibrary;
using RelateLibrary.Database;

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Relate
{
	public partial class MainForm : Form
	{
		private BindingList<Entry> _entries = new BindingList<Entry>();
		private Entry _selectedEntry;
		
		private Entry SelectedEntry
		{
			get => _selectedEntry;

			set
			{
				_selectedEntry = value;

				if (_selectedEntry == null)
				{
					entryLabel.Text = "";
					entryPanel.Enabled = false;
				}
				else
				{
					entryLabel.Text = _selectedEntry.Name;
					entryPanel.Enabled = true;
				}
			}
		}

		public MainForm()
		{
			InitializeComponent();
			SetUpEntriesView();
		}

		private void SetUpEntriesView()
		{
			entriesView.DataSource = _entries;

			entriesView.Columns["Id"].Visible = false;
			entriesView.Columns["Name"].AutoSizeMode =
				DataGridViewAutoSizeColumnMode.Fill;
		}

		private void ReadEntries()
		{
			UpdateEntriesView(Database.ReadEntries());
		}

		private void UpdateEntriesView(List<Entry> entries)
		{
			_entries = new BindingList<Entry>(entries);
			entriesView.DataSource = _entries;
			RefreshSelection();
		}

		private void FilterEntriesView()
		{
			var shouldFilterRelated =
				SelectedEntry != null && relatedCheckBox.Checked;

			var related = shouldFilterRelated ? SelectedEntry : null;

			var entries = Database.ReadEntries(related, filterTextBox.Text);
			
			if (shouldFilterRelated)
			{
				entries.Insert(0, SelectedEntry);
			}

			UpdateEntriesView(entries);
		}

		private void RefreshSelection()
		{
			if (SelectedEntry == null)
			{
				entriesView.ClearSelection();
			}
			else
			{
				for (var i = 0; i < _entries.Count; i++)
				{
					if (_entries[i].Id == SelectedEntry.Id)
					{
						entriesView.Rows[i].Selected = true;
						break;
					}
				}
			}
		}

		private void UpdateAddButton()
		{
			addButton.Enabled =
				filterTextBox.Text.Trim() != "" &&
				Database.GetEntryId(filterTextBox.Text) == 0;
		}

		private void CreateEntry()
		{
			_ = Database.Create(new Entry(filterTextBox.Text));

			FilterEntriesView();
			UpdateAddButton();
		}

		private void DeleteEntry()
		{
			var result = MessageBox.Show
			(
				this,
				$"Are you sure you want to delete \"{SelectedEntry.Name}\"?",
				"Delete Entry",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question
			);

			if (result == DialogResult.No)
			{
				return;
			}

			_ = Database.Delete(SelectedEntry);

			SelectedEntry = null;
			FilterEntriesView();
			UpdateAddButton();
		}

		private void MainForm_Shown(object sender, System.EventArgs e)
		{
			ReadEntries();
		}

		private void filterTextBox_TextChanged
		(
			object sender,
			System.EventArgs e
		)
		{
			FilterEntriesView();
			UpdateAddButton();
		}

		private void addButton_Click(object sender, System.EventArgs e)
		{
			CreateEntry();
		}

		private void entriesView_CellMouseClick
		(
			object sender,
			DataGridViewCellMouseEventArgs e
		)
		{
			SelectedEntry = entriesView.CurrentRow.DataBoundItem as Entry;
		}

		private void deleteButton_Click(object sender, System.EventArgs e)
		{
			DeleteEntry();
		}

		private void renameButton_Click(object sender, System.EventArgs e)
		{
			var renameForm = new RenameForm(SelectedEntry);
			
			if (renameForm.ShowDialog(this) == DialogResult.OK)
			{
				FilterEntriesView();
				UpdateAddButton();
				SelectedEntry = SelectedEntry;
			}
		}

		private void relatedCheckBox_CheckedChanged
		(
			object sender,
			System.EventArgs e
		)
		{
			FilterEntriesView();
		}
	}
}
