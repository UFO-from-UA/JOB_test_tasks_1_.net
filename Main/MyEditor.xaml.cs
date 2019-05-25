using iTextSharp.text;
using iTextSharp.text.pdf;
using Main.Views;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Main
{
    public partial class MyEditor : Window
    {
        public MyEditor()
        {
            InitializeComponent();
        }
        public MyEditor(string param):this()
        {
            if (param == "Load_File")
            {
                B_OpenFile(null, null);
            }
            else if (param == "Load")
            {
                B_OpenDB(null, null);
            }
        }


        private void B_SaveAs(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "txt|*.txt|All|*.*";
                sfd.Title = "Saving";

                if (sfd.ShowDialog() == true)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        File.WriteAllText(sfd.FileName, Text.Text);
                    }
                    else
                    {
                        FileStream fs = File.Create(sfd.FileName);
                        fs.Close();
                        File.WriteAllText(sfd.FileName, Text.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod().Name + $" {this.GetType().Name}");
            }
        }

        private void B_OpenFile(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text Files (*.txt) | *txt|All|*.*";
                openFileDialog.InitialDirectory = @"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent() + @"\Desktop\";
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "Select a File";
                if (openFileDialog.ShowDialog() == true)
                {
                    //Text.Text = File.ReadAllText(openFileDialog.FileName);
                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        Text.Text = reader.ReadToEnd();
                    }
                    //var sr = new StreamReader(openFileDialog.FileName);
                    //Text.Text =sr.ReadToEnd();                sr.Close();                sr.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod().Name + $" {this.GetType().Name}");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.GetProcessesByName("Main")[0].Kill();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void SaveAsDB()
        {
            SQLiteConnection SQLConnect = null;
            try
            {
                SQLConnect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectStr"].ConnectionString.ToString());
                SQLConnect.Open();

                bool? DR = false;
                string FileName = "",FileData = "";
                Dispatcher.Invoke(new Action(() =>
                {
                    SaveAs_toDB s = new SaveAs_toDB();
                        DR = s.ShowDialog();
                    if (DR==true)
                    {
                        FileName = s._FileName;
                        FileData= Text.Text;
                    }
                }));

                //SaveAs_toDB s = new SaveAs_toDB();
                if (DR==true)
                {
                    SQLiteCommand command = new SQLiteCommand("", SQLConnect);

                    command.CommandText = "insert into Files (FileName, Data) values ('" + FileName + "', @byteArr)";
                    command.Parameters.Add("@byteArr", DbType.Binary, 64000).Value = FileDataToByte(FileData);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Row was added to DB", System.Reflection.MethodInfo.GetCurrentMethod().Name + $" {this.GetType().Name}");
                }
                else
                {
                    MessageBox.Show("Undo saving", System.Reflection.MethodInfo.GetCurrentMethod().Name + $" {this.GetType().Name}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod().Name + $" {this.GetType().Name}");
            }
            finally
            {
                SQLConnect.Close();
            }
        }

        private void B_SaveAsDB(object sender, RoutedEventArgs e)
        {
            var S_Task = Task.Factory.StartNew(() =>
            {
                SaveAsDB();
            });
        }
        #region Data convertors
        private byte[] FileDataToByte(string text)
        {
            return Encoding.UTF32.GetBytes(text);
        }

        private string FileByteToData(object obj)
        {
            return Encoding.UTF32.GetString((byte[])obj);
        } 
        #endregion

        private void B_OpenDB(object sender, RoutedEventArgs e)
        {
            var O_Task = Task.Factory.StartNew(() =>
            {
                OpenDB();
            });
        }

        private void OpenDB()
        {
            SQLiteConnection SQLConnect = null;
            try
            {
                SQLConnect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectStr"].ConnectionString.ToString());
                SQLConnect.Open();
                SQLiteCommand command = new SQLiteCommand("", SQLConnect);
                ObservableCollection<Models.File> Files = new ObservableCollection<Models.File>();
                command.CommandText = "SELECT * FROM Files";
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Files.Add(new Models.File() { FileName = reader["FileName"].ToString(), FileData = FileByteToData(reader["Data"]) });//сабстринг убирает инфо о фйле которое откудато считывется
                    }
                    LoadFromDB s = null;
                    Dispatcher.Invoke(new Action(() =>
                    {
                        s = new LoadFromDB(Files);
                        if (s.ShowDialog() == true)
                        {
                            Text.Text = Files.Where(x => x == s._picked_file).FirstOrDefault().FileData;
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod().Name + $" {this.GetType().Name}");
            }
            finally { SQLConnect.Close(); }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            Text.Text = "";
        }

        private void B_RowsGen(object sender, RoutedEventArgs e)
        {
            Generator f = new Generator();
            f.ShowDialog();
            var zTask = Task.Factory.StartNew(() =>
            {
                Thread T1thread = new Thread(() => { ((App)Application.Current).AddRows(f.row_count); });
                T1thread.Start();
                T1thread.Join();
                MessageBox.Show("successfully", "Generator");
            });
        }

        private void CreatePDF(string path,string text)
        {
            Document pdf = new Document(PageSize.A4.Rotate());  
            try
            {
                var pathFont = Path.GetFullPath(@"Fonts/ArialRegular/ArialRegular.ttf");
                //Подключаем собсвенный  шрифт  чтобы видить русский
                BaseFont baseFont = BaseFont.CreateFont(pathFont, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font f14 = new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.NORMAL);
                PdfWriter.GetInstance(pdf, new FileStream(path + $"MyPDF.pdf", FileMode.Create));
                pdf.Open();
              
                pdf.Add(new Paragraph($"{text}", f14));
                
            }
            catch (DocumentException de)
            {
                MessageBox.Show(de.Message, System.Reflection.MethodInfo.GetCurrentMethod().Name + $" {this.GetType().Name}");
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, System.Reflection.MethodInfo.GetCurrentMethod().Name + $" {this.GetType().Name}");
            }
            pdf.Close();
        }

        private void B_SaveAsPDF(object sender, RoutedEventArgs e)
        {
            var Data = Text.Text;
            var PDF_Task = Task.Factory.StartNew(() =>
            {
                CreateIfMissing(Path.GetFullPath(@"PDF/"));
                CreatePDF(Path.GetFullPath(@"PDF/"), Data);
            });
        }

        private void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
    }
}
