using RelateLibrary;
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
				entryLabel.Text = _selectedEntry?.Name ?? "";
			}
		}

		public MainForm()
		{
			InitializeComponent();

			SetUpEntriesView();
			ReadEntries();
			UpdateAddButton();
			SelectedEntry = null;
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
		}

		private void FilterEntriesView()
		{
			if (filterTextBox.Text.Trim() == "")
			{
				UpdateEntriesView(Database.ReadEntries());
			}
			else
			{
				UpdateEntriesView(Database.SearchEntry(filterTextBox.Text));
			}
		}

		private void UpdateAddButton()
		{
			addButton.Enabled =
				filterTextBox.Text.Trim() != "" &&
				!Database.EntryExists(filterTextBox.Text);
		}

		private void CreateEntry()
		{
			_ = Database.Create(new Entry(filterTextBox.Text));

			FilterEntriesView();
			UpdateAddButton();
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
	}
}
