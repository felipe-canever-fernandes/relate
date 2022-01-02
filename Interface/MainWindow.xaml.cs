﻿using Core;
using Core.Models;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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

		private bool canCreate;
		private Entry currentEntry;
		private bool canRename;

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

		public bool CanCreate
		{
			get => canCreate;

			private set
			{
				canCreate = value;
				NotifyPropertyChanged(nameof(CanCreate));
			}
		}

		private Database.FilterType FilterType;

		public Entry CurrentEntry
		{
			get => currentEntry;

			private set
			{
				currentEntry = value;
				NotifyPropertyChanged(nameof(CurrentEntry));
				UpdateEntriesList();
			}
		}

		public string EntryName => EntryNameTextBox.Text.Trim();

		public bool CanRename
		{
			get => canRename;

			private set
			{
				canRename = value;
				NotifyPropertyChanged(nameof(CanRename));
			}
		}

		public ObservableCollection<Entry> Entries { get; private set; }

		#endregion

		#region Methods

		private void InitializeEntries()
		{
			Entries = new ObservableCollection<Entry>();
			FilterTextBox.Text = " ";
			AllEntriesRadioButton.IsChecked = true;
		}

		private void UpdateEntriesList()
		{
			Entries.Clear();

			var filterType =
				CurrentEntry is null ?
				Database.FilterType.All :
				FilterType;

			var entries = Database.GetEntries(
				search: Filter,
				reference: CurrentEntry,
				filterType: filterType
			);

			foreach (var entry in entries)
			{
				Entries.Add(entry);
			}
		}

		private void UpdateCanChange()
		{
			CanRename = CanChange(EntryName);
		}

		private bool CanChange(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}

			var entry = new Entry(name);
			var entryExists = Database.Exists(entry);

			return !entryExists;
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
			CanCreate = CanChange(Filter);
			UpdateEntriesList();
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

		private void AllEntriesRadioButton_Checked(
			object sender,
			RoutedEventArgs e
		)
		{
			FilterType = Database.FilterType.All;
			UpdateEntriesList();
		}

		private void RelatedCurrentEntryRadioButton_Checked(
			object sender,
			RoutedEventArgs e
		)
		{
			FilterType = Database.FilterType.Related;
			UpdateEntriesList();
		}

		private void UnrelatedCurrentEntryRadioButton_Checked(
			object sender,
			RoutedEventArgs e
		)
		{
			FilterType = Database.FilterType.Unrelated;
			UpdateEntriesList();
		}

		private void EntryNameTextBox_TextChanged(
			object sender,
			System.Windows.Controls.TextChangedEventArgs e
		)
		{
			UpdateCanChange();
		}

		private void EntryNameTextBox_LostFocus(
			object sender,
			RoutedEventArgs e
		)
		{
			EntryNameTextBox.Text = CurrentEntry.Name;
		}

		private void CloseEntryButton_Click(object sender, RoutedEventArgs e)
		{
			CurrentEntry = null;
		}

		private void RenameButton_Click(object sender, RoutedEventArgs e)
		{
			var renamedEntry = new Entry(EntryName, CurrentEntry.Id);
			Database.Rename(renamedEntry);

			CurrentEntry = Database.GetEntry(renamedEntry);
			UpdateCanChange();
			UpdateEntriesList();
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
