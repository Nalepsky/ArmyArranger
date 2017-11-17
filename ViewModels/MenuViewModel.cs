using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ArmyArranger.ViewModels
{
    public class MenuViewModel
    {
        #region Commands

        public ICommand AddUnits { get; set; }
        public ICommand ContactUs { get; set; }
        public ICommand CreateArmyList { get; set; }

        #endregion

        #region

        public MenuViewModel()
        {
            AddUnits = new DelegateCommand(ChangeViewToAddUnits);
            ContactUs = new DelegateCommand(ChangeViewToContactUs);
            CreateArmyList = new DelegateCommand(ChangeViewToCreateArmyList);
        }

        #endregion

        #region Actions

        private void ChangeViewToAddUnits()
        {
            App.Current.MainWindow.DataContext = new AddUnitsViewModel();
        }

        private void ChangeViewToContactUs()
        {
            App.Current.MainWindow.DataContext = new ContactUsViewModel();
        }

        private void ChangeViewToCreateArmyList()
        {
            App.Current.MainWindow.DataContext = new CreateArmyListViewModel();
        }

        #endregion

    }
}
