using Core;
using Core.Models;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Interface
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		#region Fields

		private Entry currentEntry;

		#endregion

		#region Constructors

		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;
			InitializeEntries();
		}

		#endregion

		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Properties

		private string Filter => FilterTextBox.Text.Trim();

		public Entry CurrentEntry
		{
			get => currentEntry;

			private set
			{
				currentEntry = value;
				NotifyPropertyChanged(nameof(CurrentEntry));
			}
		}

		public ObservableCollection<Entry> Entries { get; private set; }

		#endregion

		#region Methods

		private void InitializeEntries()
		{
			Entries = new ObservableCollection<Entry>();
			FilterTextBox.Text = " ";
		}

		private void UpdateEntriesList()
		{
			Entries.Clear();

			var entries = Database.GetEntries(Filter);

			foreach (var entry in entries)
			{
				Entries.Add(entry);
			}
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			Debug.Assert(!string.IsNullOrEmpty(propertyName));

			var e = new PropertyChangedEventArgs(propertyName);
			PropertyChanged?.Invoke(this, e);
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
			Database.Insert(entry);

			FilterTextBox.Text = "";
		}

		private void CloseEntryButton_Click(object sender, RoutedEventArgs e)
		{
			CurrentEntry = null;
		}

		private void DeleteEntryButton_Click(object sender, RoutedEventArgs e)
		{
			var answer = MessageBox.Show(
				this,
				$"Are you sure you want to delete \"{CurrentEntry}\"?",
				"Delete entry",
				MessageBoxButton.YesNo,
				MessageBoxImage.Question,
				MessageBoxResult.No
			);

			if (answer == MessageBoxResult.No)
			{
				return;
			}

			Database.Delete(CurrentEntry);
			CurrentEntry = null;
			UpdateEntriesList();
		}

		private void EntriesListViewTextBlock_MouseDown(
			object sender,
			System.Windows.Input.MouseButtonEventArgs e
		)
		{
			if (e.ClickCount == 2)
			{
				CurrentEntry = EntriesListView.SelectedItem as Entry;
			}
		}

		#endregion
	}
}
