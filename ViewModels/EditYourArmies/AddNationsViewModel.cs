﻿using System;
using System.Windows.Input;
using Prism.Commands;
using ArmyArranger.ViewModels.Menu;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddNationsViewModel
    {
        #region Propeties



        #endregion

        #region Commands

        public ICommand Back { get; set; }

        #endregion

        #region Constructors

        public AddNationsViewModel()
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
