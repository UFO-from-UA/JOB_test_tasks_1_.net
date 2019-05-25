using System;
using System.Collections.Generic;
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

namespace Main.Views
{
    public partial class SaveAs_toDB : Window
    {
        public string _FileName;
        public SaveAs_toDB()
        {
            InitializeComponent();
        }

        private void OK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OK();
            }
        }

        private void OK()
        {
            if (!String.IsNullOrEmpty(tb_FileName.Text))
            {
                _FileName = tb_FileName.Text;
                DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("File Name is null or empty\n undo saving", System.Reflection.MethodInfo.GetCurrentMethod().Name + $" {this.GetType().Name}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OK();
        }
    }
}
