using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ArmyArranger.Global;
using ArmyArranger.ViewModels.Menu;
using ArmyArranger.ViewModels.ArmyList;

namespace ArmyArranger.ViewModels
{
    public class MenuViewModel
    {
        #region Commands
       
        public ICommand CreateArmyList { get; set; }
        public ICommand EditYourArmies { get; set; }
        public ICommand ContactUs { get; set; }
        public ICommand About { get; set; }
        public ICommand Exit { get; set; }

        #endregion

        #region Constructors

        public MenuViewModel()
        {          
            CreateArmyList = new DelegateCommand(ChangeViewToCreateArmyList);
            EditYourArmies = new DelegateCommand(ChangeViewToEditYourArmies);
            ContactUs = new DelegateCommand(ChangeViewToContactUs);
            About = new DelegateCommand(ChangeViewToAbout);
            Exit = new DelegateCommand(QuitApp);
        }

        #endregion

        #region Actions
        
        private void ChangeViewToCreateArmyList()
        {
            App.Current.MainWindow.DataContext = new ChooseSelectorViewModel();
        }

        private void ChangeViewToEditYourArmies()
        {
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void ChangeViewToContactUs()
        {
            App.Current.MainWindow.DataContext = new ContactUsViewModel();
        }

        private void ChangeViewToAbout()
        {
            App.Current.MainWindow.DataContext = new AboutViewModel();
        }

        private void QuitApp()
        {
            Database.Close();
            App.Current.MainWindow.Close();
        }

        #endregion

    }
}
