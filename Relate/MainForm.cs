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

		public MainForm()
		{
			InitializeComponent();

			SetUpEntriesView();
			ReadEntries();
		}

		private void SetUpEntriesView()
		{
			entriesView.DataSource = _entries;

			entriesView.Columns["Id"].Visible = false;
			entriesView.Columns["Name"].AutoSizeMode =
				DataGridViewAutoSizeColumnMode.Fill;
		}

		private void ReadEntries() =>
			UpdateEntriesView(Database.ReadEntries());

		private void UpdateEntriesView(List<Entry> entries)
		{
			_entries = new BindingList<Entry>(entries);
			entriesView.DataSource = _entries;
		}

		private void filterTextBox_TextChanged
		(
			object sender,
			System.EventArgs e
		)
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
	}
}
