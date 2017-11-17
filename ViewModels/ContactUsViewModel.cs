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


        private string _topic;
        public string Topic
        {
            get
            {
                return _topic;
            }
            set
            {
                _topic = value;
                RaisePropertyChanged(nameof(Topic));
            }
        }

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

        public ICommand Send { get; set; }
        public ICommand Back { get; set; }

        #endregion

        #region Constructors

        public ContactUsViewModel()
        {
            Send = new DelegateCommand(SendEmail);
            Back = new DelegateCommand(ShowMenuView);
        }

        #endregion

        #region Actions

        private void ShowMenuView()
        {
            App.Current.MainWindow.DataContext = new MenuViewModel();
        }

        private void SendEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

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
            catch (Exception ex)
            {
                MessageBox.Show("Email error.\nPlease check your internet connection.\n\n\n" + ex.ToString());
            }
        }

        #endregion
    }
}
