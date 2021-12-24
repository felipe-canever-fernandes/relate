using RelateLibrary;
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

		private Entry SelectedEntryInEntriesDataGridView =>
			_entriesDataGridView.CurrentRow.DataBoundItem as Entry;

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

		private void UpdateRelateEntriesButton()
		{
			if (_entriesDataGridView.SelectedRows.Count <= 0)
			{
				_relateEntriesButton.Enabled = false;
				return;
			}

			var firstEntry = SelectedEntry;
			var secondEntry = SelectedEntryInEntriesDataGridView;

			var areEntriesTheSame = firstEntry.Id == secondEntry.Id;

			_relateEntriesButton.Enabled =
				!areEntriesTheSame &&
				!Database.AreRelated(firstEntry, secondEntry);
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

		private void DisplayAboutDialog()
		{
			_ = MessageBox.Show
			(
				this,

				"RELATE\nVersion 2.0.0-beta\n\n" +
				"2021 Felipe Canever Fernandes\n" +
				"<felipe.canever.fernandes@outlook.com>",

				"About RELATE",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1
			);
		}

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
				entry.Id = entryId;

				if (!(SelectedEntry is null))
				{
					RelateEntries(entry, shouldAskFirst: true);
				}

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
			SelectedEntry = SelectedEntryInEntriesDataGridView;
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

		private void RelateEntries(Entry entry, bool shouldAskFirst)
		{
			Debug.Assert(entry != null);

			if (shouldAskFirst)
			{
				var answer = MessageBox.Show
				(
					this,

					$"Do you want to relate \"{entry.Name}\" " +
					$"to \"{SelectedEntry.Name}\"?",

					"Relate entries",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button1
				);

				if (answer == DialogResult.No)
				{
					return;
				}
			}

			var relation = new Relation(SelectedEntry.Id, entry.Id);

			var wasSuccessful = Database.Create(relation);

			if (!wasSuccessful)
			{
				_ = MessageBox.Show
				(
					this,
					"The relation could not be created.",
					"Create relation",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}
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

			_createEntryButton.Text =
				_relatedEntriesCheckBox.Enabled ?
				"Create entry..." :
				"Create entry";
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
		private void _relateEntriesButton_Click
		#pragma warning restore IDE1006 // Naming Styles
		(
			object sender, System.EventArgs e
		)
		{
			RelateEntries
			(
				SelectedEntryInEntriesDataGridView,
				shouldAskFirst: false
			);
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
		private void _entriesDataGridView_CellDoubleClick
		#pragma warning restore IDE1006 // Naming Styles
		(
			object sender, DataGridViewCellEventArgs e
		)
		{
			SelectEntry();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _entriesDataGridView_SelectionChanged
		(
			object sender, System.EventArgs e
		)
		#pragma warning restore IDE1006 // Naming Styles
		{
			if (!(SelectedEntry is null))
			{
				UpdateRelateEntriesButton();
			}
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _aboutButton_Click(object sender, System.EventArgs e)
		#pragma warning restore IDE1006 // Naming Styles
		{
			DisplayAboutDialog();
		}

		#endregion
	}
}
