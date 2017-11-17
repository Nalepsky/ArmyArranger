using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using System.Net.Mail;
using System;

namespace ArmyArranger.ViewModels
{
    public class CreateArmyListViewModel
    {
        #region Propeties



        #endregion

        #region Commands

        public ICommand Back { get; set; }

        #endregion

        #region Constructors

        public CreateArmyListViewModel()
        {
            Back = new DelegateCommand(ChangeViewToMenu);
        }

        #endregion

        #region Actions

        private void ChangeViewToMenu()
        {
            App.Current.MainWindow.DataContext = new MenuViewModel();
        }

        #endregion
    }
}
