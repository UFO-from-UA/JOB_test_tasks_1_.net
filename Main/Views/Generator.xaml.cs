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
    public partial class Generator : Window
    {
        public int row_count;

        public Generator()
        {
            InitializeComponent();
        }

        private void OK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Length == 2)
            {
                if (Char.IsNumber(e.Key.ToString()[1]))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
            if (e.Key == Key.Enter)
            {
                OK();
                return;
            }
        }

        private void OK()
        {
            if (!String.IsNullOrEmpty(tb_count.Text))
            {
                row_count =  Convert.ToInt32( tb_count.Text);
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
