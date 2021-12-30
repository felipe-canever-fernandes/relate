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

		public string FilterText => Filter.Text.Trim();

		private void InitializeEntries()
		{
			Entries = new ObservableCollection<Entry>();
			Filter.Text = " ";
		}

		private void Filter_TextChanged(
			object sender, System.Windows.Controls.TextChangedEventArgs e
		)
		{
			UpdateCreateEntryButton();
			UpdateEntriesList();

			void UpdateCreateEntryButton()
			{
				var isFilterEmpty = string.IsNullOrEmpty(FilterText);

				if (isFilterEmpty)
				{
					CreateEntry.IsEnabled = false;
				}
				else
				{
					var entry = new Entry(FilterText);
					var entryExists = Database.Exists(entry);

					CreateEntry.IsEnabled = !entryExists;
				}
			}

			void UpdateEntriesList()
			{
				Entries.Clear();

				var entries = Database.GetEntries(FilterText);

				foreach (var entry in entries)
				{
					Entries.Add(entry);
				}
			}
		}

		private void CreateEntry_Click(object sender, RoutedEventArgs e)
		{
			var entry = new Entry(FilterText);
			_ = Database.Insert(entry);

			Filter.Text = "";
		}
	}
}
