using RelateLibrary;
using System.Windows.Forms;

namespace Relate
{
	public partial class MainForm : Form
	{
		private Entry _selectedEntry;

		public MainForm()
		{
			InitializeComponent();
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

		private void MainForm_Shown(object sender, System.EventArgs e)
		{
			SelectedEntry = null;
		}
	}
}
