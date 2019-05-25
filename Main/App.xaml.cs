using System.Configuration;
using System.Data;
using System.Windows;
using System.IO;
using System.Data.Common;
using System.Data.SQLite;
using Faker;
using System.Text;

//Дополнительные задания:
//- Обеспечить сжатие информации при хранении в БД средством любой ThirdParty библиотеки
//- Для форматов json и xml обеспечить подсветку синтаксиса и форматирование

namespace Main
{
    public partial class App : Application
    {
        private void AppStartup(object sender, StartupEventArgs e)
        {
            if (!File.Exists(ConfigurationSettings.AppSettings.Get("DB_Name")))
            {
                CreateDB();
                AddSomeDataRows(10);
                SpecialRow();
            }
            MyEditor mainView = new MyEditor();
            LoadOrCreate start = new LoadOrCreate();
            start.ShowDialog();
            if (start._Action == "Load")
            {
                mainView = new MyEditor("Load");
                mainView.Show();
            }
            else if (start._Action == "Create")
            {
                mainView = new MyEditor();
                mainView.Show();
            }
            else if (start._Action == "Load_File")
            {
                mainView = new MyEditor("Load_File");
                mainView.Show();
            }
        }

        public void AddRows(int count)
        {
            AddSomeDataRows(count);
        }

        private void AddSomeDataRows(int count)
        {
            SQLiteConnection SQLConnect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectStr"].ConnectionString.ToString());
            SQLConnect.Open();

            for (int i = 0; i < count; i++)
            {
                SQLiteCommand command = new SQLiteCommand("", SQLConnect);

                command.CommandText = "insert into Files (FileName, Data) values ('" + Faker.Company.CatchPhrase() + "', @byteArr)";
                command.Parameters.Add("@byteArr", DbType.Binary, 2000).Value = FileDataToByte(Faker.Lorem.Sentence());

                command.ExecuteNonQuery();
            }
            SQLConnect.Close();
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

        private void SpecialRow()
        {
            SQLiteConnection SQLConnect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectStr"].ConnectionString.ToString());
            SQLConnect.Open();
            SQLiteCommand command2 = new SQLiteCommand("", SQLConnect);
            #region  custom string

            command2.CommandText = "insert into Files (FileName, Data) values ('" + Faker.Company.CatchPhrase() + "', @byteArr)";
            command2.Parameters.Add("@byteArr", DbType.Binary, 2000).Value = FileDataToByte(@"djssdf  				←тут табуляции
↑ Enter was pressed  

some символы   ♪ ♫ ♀ ♂
		☻♥ ♣♦♠•◘█ „Ґ`╚╝ ");
            #endregion

            command2.ExecuteNonQuery();
            SQLConnect.Close();
        }

        private void CreateDB()
        {
            string Name = ConfigurationSettings.AppSettings.Get("DB_Name");

            SQLiteConnection.CreateFile(Name);

            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectStr"].ConnectionString.ToString();
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"CREATE TABLE [Files] (
                    [id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                    [FileName] char(100) NOT NULL,
                    [Data] Blob NOT NULL
                    );"; // varchar(MAX) → error
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
