using Main.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Main.Views
{
    public partial class LoadFromDB : Window
    {
        private ObservableCollection<File> _File_self = new ObservableCollection<File>();
        public File _picked_file;
        public LoadFromDB()
        {
            InitializeComponent();
        }
        public LoadFromDB(ObservableCollection<File> Files):this()
        {
            _File_self = Files;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DG.ItemsSource = _File_self;
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                _picked_file = (File)DG.SelectedItem;
                DialogResult = true;
            }
        }
    }
}
