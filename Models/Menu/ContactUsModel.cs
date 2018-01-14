using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArmyArranger.Models
{
    public class ContactUsModel
    {
        private MailMessage mail = new MailMessage();
        private SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        public void SendEmail(string Topic, string EmailAddress, string Message)
        {
            try
            {

                mail.From = new MailAddress("armyarranger@gmail.com");
                mail.To.Add("armyarranger@gmail.com");
                mail.Subject = Topic;
                mail.Body = EmailAddress + "\n\n" + Message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ArmyArranger", "ArmyArrangerPass");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("Email sent.\nThank you for your feedback.");
            }
            catch (Exception)
            {
                MessageBox.Show("Email error.\nPlease check your internet connection.");
            }
        }
    }
}
