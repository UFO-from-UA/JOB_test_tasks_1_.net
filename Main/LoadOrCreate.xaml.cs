using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Main
{
    public partial class LoadOrCreate : Window
    {
        public string _Action;
        public LoadOrCreate()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {   //здесь я отошол от многих стандартов и вторая форма будет открываться всегда,
            // что в свою  очередь упрощает логику отмены Load from DB и Load_File просто переведя пользователя на Create
            _Action = "Create";
        }

        private void B_Load(object sender, RoutedEventArgs e)//from DB
        {
            _Action = "Load";
            this.Close();
        }

        private void B_Create(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void B_Load_file(object sender, RoutedEventArgs e)
        {
            _Action = "Load_File";
            this.Close();
        }
    }
}
