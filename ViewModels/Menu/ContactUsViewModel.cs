using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using System.Net.Mail;
using System;
using ArmyArranger.Models;

namespace ArmyArranger.ViewModels.Menu
{
    public class ContactUsViewModel : BindableBase
    {
        ContactUsModel ContactUsModel = new ContactUsModel();

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
            Back = new DelegateCommand(ChangeViewToMenu);
        }

        #endregion

        #region Actions

        private void SendEmail()
        {
            ContactUsModel.SendEmail(Topic, EmailAddress, Message);
        }

        private void ChangeViewToMenu()
        {
            App.Current.MainWindow.DataContext = new MenuViewModel();
        }

        #endregion
    }
}
