using RelateLibrary;
using RelateLibrary.Database;

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Relate
{
	public partial class MainForm : Form
	{
		#region Fields

		private BindingList<Entry> _entries;
		private Entry _currentEntry;

		#endregion

		public MainForm()
		{
			InitializeComponent();
			ActiveControl = _filterTextBox;
		}

		#region Properties

		private string Filter => _filterTextBox.Text.Trim();

		private Entry SelectedEntry =>
			_entriesDataGridView.CurrentRow.DataBoundItem as Entry;

		private BindingList<Entry> Entries
		{
			get => _entries;

			set
			{
				Debug.Assert(!(value is null));
				_entries = value;

				_entriesDataGridView.DataSource = Entries;
			}
		}

		private Entry CurrentEntry
		{
			get => _currentEntry;

			set
			{
				_currentEntry = value;

				if (CurrentEntry == null)
				{
					_currentEntryGroupBox.Enabled = false;
					_currentEntryNameLabel.Text = "";
				}
				else
				{
					_currentEntryGroupBox.Enabled = true;
					_currentEntryNameLabel.Text = CurrentEntry.Name;
				}

				FilterEntries();
			}
		}

		#endregion

		#region Methods

		private void SetUp()
		{
			FilterEntries();
			SetUpEntriesDataGridView();
			CurrentEntry = null;
			_allEntriesRadioButton.Checked = true;
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
			FormatEntries();
			UpdateCreateEntryButton();
		}

		private void UpdateEntries()
		{
			List<Entry> entries;

			if
			(
				CurrentEntry is null ||
				_allEntriesRadioButton.Checked ||
				_relatedEntriesRadioButton.Checked
			)
			{
				var relatedTo =
					_relatedEntriesRadioButton.Checked ?
					CurrentEntry :
					null;

				entries = Database.ReadEntries(relatedTo, Filter);
			}
			else
			{
				entries = Database.ReadRelatableEntries(CurrentEntry.Id);
			}

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

			var firstEntry = CurrentEntry;
			var secondEntry = SelectedEntry;

			if (firstEntry.Id == secondEntry.Id)
			{
				_relateEntriesButton.Enabled = false;
				return;
			}

			_relateEntriesButton.Enabled = true;

			_relateEntriesButton.Text =
				Database.AreRelated(firstEntry, secondEntry) ?
				"Unrelate" :
				"Relate";
		}

		private void FormatEntries()
		{
			if (CurrentEntry is null)
			{
				return;
			}

			var style = _entriesDataGridView.DefaultCellStyle.Font;

			foreach (DataGridViewRow row in _entriesDataGridView.Rows)
			{
				var entry = row.DataBoundItem as Entry;

				if (entry.Id == CurrentEntry.Id)
				{
					var font = new Font(style, FontStyle.Bold);
					row.DefaultCellStyle.Font = font;
				}
				else if (Database.AreRelated(CurrentEntry, entry))
				{
					var font = new Font(style, FontStyle.Italic);
					row.DefaultCellStyle.Font = font;
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

				if (!(CurrentEntry is null))
				{
					RelateEntries(entry, shouldAskBeforeRelating: true);
				}

				ClearFilterTextBox();
			}
		}

		private void RenameEntry()
		{
			var renameForm = new RenameForm(CurrentEntry);
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
				$"Are you sure you want to delete \"{CurrentEntry.Name}\"?",
				"Delete entry",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2
			);

			if (answer == DialogResult.Yes)
			{
				var wasDeleteSuccessful = Database.Delete(CurrentEntry);

				if (wasDeleteSuccessful)
				{
					CurrentEntry = null;
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

		private void SetCurrentEntry()
		{
			CurrentEntry = SelectedEntry;
			ClearFilterTextBox();
		}

		private void UnsetCurrentEntry()
		{
			if (!(CurrentEntry is null))
			{
				CurrentEntry = null;
			}
		}

		private void UpdateEntry()
		{
			CurrentEntry = Database.ReadEntry(CurrentEntry.Id);
		}

		private void RelateEntries(Entry entry, bool shouldAskBeforeRelating)
		{
			Debug.Assert(entry != null);

			var areRelated = Database.AreRelated(CurrentEntry, entry);

			string action;
			string actionTitle;

			if (areRelated || shouldAskBeforeRelating)
			{
				action = areRelated ? "unrelate" : "relate";
				actionTitle = action.ToUpper()[0] + action.Substring(1);

				var answer = MessageBox.Show
				(
					this,

					$"Do you want to {action} \"{entry.Name}\" " +
					$"to \"{CurrentEntry.Name}\"?",

					$"{actionTitle} entries",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button1
				);

				if (answer == DialogResult.No)
				{
					return;
				}
			}

			bool wasSuccessful;

			if (areRelated)
			{
				var relation = new Relation(CurrentEntry.Id, entry.Id);
				wasSuccessful = Database.Delete(relation);
			}
			else
			{
				var relation = new Relation(CurrentEntry.Id, entry.Id);
				wasSuccessful = Database.Create(relation);
			}

			action = areRelated ? "delete" : "create";
			actionTitle = action.ToUpper()[0] + action.Substring(1);

			if (!wasSuccessful)
			{
				_ = MessageBox.Show
				(
					this,
					$"The relation could not be {action}d.",
					$"{actionTitle} relation",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				return;
			}

			FilterEntries();
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
		private void _currentEntryGroupBox_EnabledChanged
		(
			object sender, System.EventArgs e
		)
		#pragma warning restore IDE1006 // Naming Styles
		{
			_createEntryButton.Text =
				_currentEntryGroupBox.Enabled ?
				"Create entry..." :
				"Create entry";
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _renameCurrentEntryButton_Click
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
				SelectedEntry,
				shouldAskBeforeRelating: false
			);
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _deleteCurrentEntryButton_Click
		#pragma warning restore IDE1006 // Naming Styles
		(
			object sender, System.EventArgs e
		)
		{
			DeleteEntry();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _closeCurrentEntryButton_Click
		(
			object sender, System.EventArgs e
		)
		#pragma warning restore IDE1006 // Naming Styles
		{
			UnsetCurrentEntry();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _allEntriesRadioButton_CheckedChanged(object sender, System.EventArgs e)
		#pragma warning restore IDE1006 // Naming Styles
		{
			FilterEntries();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _relatedEntriesRadioButton_CheckedChanged(object sender, System.EventArgs e)
		#pragma warning restore IDE1006 // Naming Styles
		{
			FilterEntries();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _unrelatedEntriesRadioButton_CheckedChanged(object sender, System.EventArgs e)
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
			SetCurrentEntry();
		}

		#pragma warning disable IDE1006 // Naming Styles
		private void _entriesDataGridView_SelectionChanged
		(
			object sender, System.EventArgs e
		)
		#pragma warning restore IDE1006 // Naming Styles
		{
			if (!(CurrentEntry is null))
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
