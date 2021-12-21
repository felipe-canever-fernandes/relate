using RelateLibrary;
using RelateLibrary.Database;

using System.Windows.Forms;

namespace Relate
{
	public partial class RenameForm : Form
	{
		private Entry Entry { get; }

		public RenameForm(Entry entry)
		{
			Entry = entry;
			InitializeComponent();
			Initialize();
		}

		private void Initialize()
		{
			renameLabel.Text = $"Rename \"{Entry.Name}\"";
			nameTextBox.Text = Entry.Name;
		}

		private void nameTextBox_TextChanged(object sender, System.EventArgs e)
		{
			var entryId = Database.GetEntryId(nameTextBox.Text.Trim());

			var isEmpty = string.IsNullOrEmpty(nameTextBox.Text.Trim());
			var isUnique = entryId == 0;

			renameButton.Enabled = !isEmpty && isUnique;
		}

		private void renameButton_Click(object sender, System.EventArgs e)
		{
			Entry.Name = nameTextBox.Text.Trim();
			_ = Database.Update(Entry);
		}
	}
}
