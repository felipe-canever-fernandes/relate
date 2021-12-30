using Core.Database;
using Core.Models;

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Interface
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;
			InitializeEntries();
		}

		public ObservableCollection<Entry> Entries { get; set; }

		public string Filter => FilterTextBox.Text.Trim();

		private void InitializeEntries()
		{
			Entries = new ObservableCollection<Entry>();
			FilterTextBox.Text = " ";
		}

		private void FilterTextBox_TextChanged(
			object sender,
			System.Windows.Controls.TextChangedEventArgs e
		)
		{
			UpdateCreateEntryButton();
			UpdateEntriesList();

			void UpdateCreateEntryButton()
			{
				var isFilterEmpty = string.IsNullOrEmpty(Filter);

				if (isFilterEmpty)
				{
					CreateEntryButton.IsEnabled = false;
				}
				else
				{
					var entry = new Entry(Filter);
					var entryExists = Database.Exists(entry);

					CreateEntryButton.IsEnabled = !entryExists;
				}
			}

			void UpdateEntriesList()
			{
				Entries.Clear();

				var entries = Database.GetEntries(Filter);

				foreach (var entry in entries)
				{
					Entries.Add(entry);
				}
			}
		}

		private void FilterTextBox_KeyDown(
			object sender,
			System.Windows.Input.KeyEventArgs e
		)
		{
			if (e.Key != System.Windows.Input.Key.Enter)
			{
				return;
			}

			if (!CreateEntryButton.IsEnabled)
			{
				return;
			}

			var args = new RoutedEventArgs(ButtonBase.ClickEvent);
			CreateEntryButton.RaiseEvent(args);
		}

		private void CreateEntryButton_Click(object sender, RoutedEventArgs e)
		{
			var entry = new Entry(Filter);
			_ = Database.Insert(entry);

			FilterTextBox.Text = "";
		}
	}
}
