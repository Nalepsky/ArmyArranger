using System;
using System.Windows.Input;
using Prism.Commands;
using ArmyArranger.ViewModels.Menu;
using ArmyArranger.Global;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddWeaponsViewModel
    {
        #region Propeties

        Weapon NewWeapon = new Weapon();

        #endregion

        #region Commands

        public ICommand Back { get; set; }
        public ICommand Confirm { get; set; }

        #endregion

        #region Constructors

        public AddWeaponsViewModel()
        {
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            Confirm = new DelegateCommand(ConfirmChanges);
        }

        #endregion

        #region Actions

        private void ChangeViewToEditYourArmies()
        {
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void ConfirmChanges()
        {
            NewWeapon.SaveToDB();
        }

        #endregion
    }
}
