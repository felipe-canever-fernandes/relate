using RelateLibrary;
using RelateLibrary.Database;

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

				_relatedEntriesCheckBox.Checked = false;

				if (SelectedEntry == null)
				{
					_selectedEntryPanel.Enabled = false;
					_selectedEntryNameLabel.Text = "";
				}
				else
				{
					_selectedEntryPanel.Enabled = true;
					_selectedEntryNameLabel.Text = SelectedEntry.Name;
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
		}

		private void SetUpEntriesDataGridView()
		{
			_entriesDataGridView.Columns["Id"].Visible = false;

			_entriesDataGridView.Columns["Name"].AutoSizeMode =
				DataGridViewAutoSizeColumnMode.Fill;
		}

		private void FilterEntries()
		{
			Entries = new BindingList<Entry>
			(
				Database.ReadEntries(null, Filter)
			);

			UpdateAddButton();
		}

		private void UpdateAddButton()
		{
			var isFilterEmpty = string.IsNullOrEmpty(Filter);
			var entryAlreadyExists = Database.GetEntryId(Filter) > 0;

			_addButton.Enabled = !isFilterEmpty && !entryAlreadyExists;
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
					break;
				}
			}
		}

		private void AddEntry()
		{
			var entry = new Entry(Filter);
			var entryId = Database.Create(entry);


			if (entryId <= 0)
			{
				_ = MessageBox.Show
				(
					this,
					"The entry could not be added.",
					"Add entry",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}
			else
			{
				FilterEntries();
			}
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
		private void _addButton_Click(object sender, System.EventArgs e)
		#pragma warning restore IDE1006 // Naming Styles
		{
			AddEntry();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _entriesDataGridView_CellClick
		#pragma warning restore IDE1006 // Naming Styles
		(
			object sender, DataGridViewCellEventArgs e
		)
		{
			SelectedEntry =
				_entriesDataGridView.CurrentRow.DataBoundItem as Entry;
		} 

		#endregion
	}
}
