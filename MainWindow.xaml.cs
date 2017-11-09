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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Data.SQLite;

namespace ArmyArranger
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLiteConnection sqlconnect = new SQLiteConnection(string.Format("Data Source={0}", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testbase.db")));
        SQLiteCommand command;
        string query = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            String subject = "Wololo test";
            String message = "Mail sample content";
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("armyarranger@gmail.com");
                mail.To.Add("armyarranger@gmail.com");
                mail.Subject = subject;
                mail.Body = message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ArmyArranger", "ArmyArrangerPass");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("Mail został pomyslnie wysłany. Dziękujemy za kontakt.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wysyłanie nie powiodło się. Sprawdź swoje połączenie z internetem.");
                //MessageBox.Show(ex.ToString());
            }

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            sqlconnect.Open();
            if (sqlconnect.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    query = string.Format("CREATE TABLE test(id integer primary key autoincrement, name varchar(100))");
                    command = new SQLiteCommand(query, sqlconnect);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                MessageBox.Show("Created");
            }
            else
            {
                MessageBox.Show("Database connection error.");
            }
            sqlconnect.Close();
        }

        private void CreateArmyListButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(((App)Application.Current).sharedTestValue);

            CreateArmyListWindow newWindow = new CreateArmyListWindow();
            newWindow.WindowState = this.WindowState;
            newWindow.Top = this.Top;
            newWindow.Left = this.Left;
            newWindow.Height = this.Height;
            newWindow.Width = this.Width;
            newWindow.Show();
            this.Close();
        }

        private void InputUnitsButton_Click(object sender, RoutedEventArgs e)
        {
            InputUnitsWindow newWindow = new InputUnitsWindow();
            newWindow.WindowState = this.WindowState;
            newWindow.Top = this.Top;
            newWindow.Left = this.Left;
            newWindow.Height = this.Height;
            newWindow.Width = this.Width;
            newWindow.Show();
            this.Close();
        }
    }
}
