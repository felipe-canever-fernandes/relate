using RelateLibrary;
using RelateLibrary.Database;

using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Relate
{
	public partial class MainForm : Form
	{
		private BindingList<Entry> _entries;
		private Entry _selectedEntry;

		public MainForm() => InitializeComponent();

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

		private void MainForm_Shown(object sender, System.EventArgs e)
		{
			FilterEntries();
			SetUpEntriesDataGridView();
			SelectedEntry = null;
		}

		private void _filterTextBox_TextChanged
		(
			object sender, System.EventArgs e
		) =>
			FilterEntries();

		private void _entriesDataGridView_CellClick
		(
			object sender, DataGridViewCellEventArgs e
		) =>
			SelectedEntry =
				_entriesDataGridView.CurrentRow.DataBoundItem as Entry;
	}
}
