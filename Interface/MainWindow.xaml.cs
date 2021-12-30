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
        public MainWindow() => InitializeComponent();

        public ObservableCollection<Entry> Entries { get; set; }

        private void Initialize()
        {
            DataContext = this;

            var entries = Database.GetAllEntries();
            Entries = new ObservableCollection<Entry>(entries);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) =>
            Initialize();
    }
}
