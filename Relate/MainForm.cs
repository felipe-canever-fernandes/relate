using RelateLibrary;
using RelateLibrary.Database;

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

		private void ReadEntries()
		{
			_entries = new BindingList<Entry>(Database.ReadEntries());
			entriesView.DataSource = _entries;
		}
	}
}
