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
        public ObservableCollection<Entry> Entries { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Entries =
                new ObservableCollection<Entry>(Database.GetAllEntries());
        }
    }
}
