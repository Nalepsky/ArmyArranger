using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using System.Net.Mail;
using System;

namespace ArmyArranger.ViewModels
{
    public class ContactUsViewModel : BindableBase
    {

        #region Propeties


        private string _emailAddress;
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;
                RaisePropertyChanged(nameof(EmailAddress));
            }
        }


        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }

        #endregion

        #region Commands

        public ICommand ConfirmMessage { get; set; }

        #endregion

        #region Constructors

        public ContactUsViewModel()
        {
            ConfirmMessage = new DelegateCommand(SendEmail);
        }

        #endregion

        #region Actions

        private void SendEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("armyarranger@gmail.com");
                mail.To.Add("armyarranger@gmail.com");
                mail.Subject = EmailAddress;
                mail.Body = Message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ArmyArranger", "ArmyArrangerPass");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("Mail został pomyslnie wysłany. Dziękujemy za kontakt.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wysyłanie nie powiodło się. \nSprawdź swoje połączenie z internetem.\n\n\n" + ex.ToString());
            }
        }

        #endregion
    }
}
