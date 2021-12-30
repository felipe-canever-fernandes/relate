using Core.Database;
using Core.Models;

using System.Collections.ObjectModel;
using System.Windows;

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

		private void InitializeEntries()
		{
			Entries = new ObservableCollection<Entry>();
			Filter.Text = " ";
		}

		private void Filter_TextChanged(
			object sender, System.Windows.Controls.TextChangedEventArgs e
		)
		{
			var filterText = Filter.Text.Trim();

			UpdateCreateEntryButton();
			UpdateEntriesList();

			void UpdateCreateEntryButton()
			{
				var isFilterEmpty = string.IsNullOrEmpty(filterText);

				if (isFilterEmpty)
				{
					CreateEntry.IsEnabled = false;
				}
				else
				{
					var entry = new Entry(filterText);
					var entryExists = Database.Exists(entry);

					CreateEntry.IsEnabled = !entryExists;
				}
			}

			void UpdateEntriesList()
			{
				Entries.Clear();

				var entries = Database.GetEntries(filterText);

				foreach (var entry in entries)
				{
					Entries.Add(entry);
				}
			}
		}
	}
}
