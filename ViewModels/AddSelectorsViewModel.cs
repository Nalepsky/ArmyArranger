using System;
using System.Windows.Input;
using Prism.Commands;

namespace ArmyArranger.ViewModels
{
    class AddSelectorsViewModel
    {
        #region Propeties



        #endregion

        #region Commands

        public ICommand Back { get; set; }

        #endregion

        #region Constructors

        public AddSelectorsViewModel()
        {
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
        }

        #endregion

        #region Actions

        private void ChangeViewToEditYourArmies()
        {
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        #endregion
    }
}
