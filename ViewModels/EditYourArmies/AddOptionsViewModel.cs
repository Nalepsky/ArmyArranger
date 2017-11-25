using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddOptionsViewModel
    {
        #region Propeties



        #endregion

        #region Commands

        public ICommand Back { get; set; }

        #endregion

        #region Constructors

        public AddOptionsViewModel()
        {
            Back = new DelegateCommand(ChangeViewToAddUnits);
        }

        #endregion

        #region Actions

        private void ChangeViewToAddUnits()
        {
            App.Current.MainWindow.DataContext = new AddUnitsViewModel();
        }

        #endregion
    }
}
