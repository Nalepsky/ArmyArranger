using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using System;
using ArmyArranger.ViewModels.Menu;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    public class AddUnitsViewModel
    {
        
        #region Propeties
       
        #endregion

        #region Commands
        
        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        #endregion

        #region Constructors

        public AddUnitsViewModel()
        {
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            Next = new DelegateCommand(ChangeViewToAddOptions);
        }

        #endregion

        #region Actions       

        private void ChangeViewToEditYourArmies()
        {
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void ChangeViewToAddOptions()
        {
            App.Current.MainWindow.DataContext = new AddOptionsViewModel();
        }

        #endregion
    }
}
