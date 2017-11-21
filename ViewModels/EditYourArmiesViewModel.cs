using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using System.Net.Mail;
using System;

namespace ArmyArranger.ViewModels
{
    public class EditYourArmiesViewModel
    {
        #region Propeties



        #endregion

        #region Commands

        public ICommand Nations { get; set; }
        public ICommand Selectors { get; set; }
        public ICommand Units { get; set; }
        public ICommand Weapons { get; set; }
        public ICommand Rules { get; set; }
        public ICommand Back { get; set; }

        #endregion

        #region Constructors

        public EditYourArmiesViewModel()
        {
            Nations = new DelegateCommand(ChangeViewToAddNations);
            Selectors = new DelegateCommand(ChangeViewToAddSelectors);
            Units = new DelegateCommand(ChangeViewToAddUnits);
            Weapons = new DelegateCommand(ChangeViewToAddWeapons);
            Rules = new DelegateCommand(ChangeViewToAddRules);
            Back = new DelegateCommand(ChangeViewToMenu);       
        }

        #endregion

        #region Actions

        private void ChangeViewToAddNations()
        {
            App.Current.MainWindow.DataContext = new EditYourArmies.AddNationsViewModel();
        }

        private void ChangeViewToAddSelectors()
        {
            App.Current.MainWindow.DataContext = new EditYourArmies.AddSelectorsViewModel();
        }

        private void ChangeViewToAddUnits()
        {
            App.Current.MainWindow.DataContext = new EditYourArmies.AddUnitsViewModel();
        }

        private void ChangeViewToAddWeapons()
        {
            App.Current.MainWindow.DataContext = new EditYourArmies.AddWeaponsViewModel();
        }

        private void ChangeViewToAddRules()
        {
            App.Current.MainWindow.DataContext = new EditYourArmies.AddRulesViewModel();
        }

        private void ChangeViewToMenu()
        {
            App.Current.MainWindow.DataContext = new MenuViewModel();
        }

        #endregion
    }
}
