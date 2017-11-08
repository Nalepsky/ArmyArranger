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

namespace ArmyArranger
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

        
    }
}
