using RelateLibrary;
using RelateLibrary.Database;

using System.Windows.Forms;

namespace Relate
{
	public partial class RenameForm : Form
	{
		public RenameForm(Entry entry)
		{
			Entry = entry;
			ThereWasAProblem = false;

			InitializeComponent();
			SetUp();
		}

		#region Properties

		private Entry Entry { get; }
		private string EntryName => nameTextBox.Text.Trim();
		private bool ThereWasAProblem { get; set; }

		#endregion

		#region Methods

		private void SetUp()
		{
			renameLabel.Text = $"Rename \"{Entry.Name}\"";
			nameTextBox.Text = Entry.Name;
		}

		private void UpdateRenameButton()
		{
			var entryId = Database.GetEntryId(EntryName);

			var isEmpty = string.IsNullOrEmpty(EntryName);
			var isUnique = entryId == 0;

			renameButton.Enabled = !isEmpty && isUnique;
		}

		private void RenameEntry()
		{
			Entry.Name = EntryName;
			ThereWasAProblem = !Database.Update(Entry);
		}

		#endregion

		#region Event Handlers

		private void RenameForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (ThereWasAProblem)
			{
				_ = MessageBox.Show
				(
					this,
					"The entry could not be renamed.",
					"Rename entry",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				e.Cancel = true;
			}
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void nameTextBox_TextChanged(object sender, System.EventArgs e)
		#pragma warning restore IDE1006 // Naming Styles
		{
			UpdateRenameButton();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void renameButton_Click(object sender, System.EventArgs e)
		#pragma warning restore IDE1006 // Naming Styles
		{
			RenameEntry();
		}

		#endregion
	}
}
