using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Data.SQLite;
using ArmyArranger.ViewModels;
using ArmyArranger.Global;
using ArmyArranger.ViewModels.EditYourArmies;

namespace ArmyArranger
{
    /// <summary>
    /// Interaction logic for AddEntryWindow.xaml
    /// </summary>
    public partial class AddEntryWindow : Window
    {       
        public AddEntryWindow(WindowsService service)
        {
            InitializeComponent();
            DataContext = new AddEntryViewModel(service);             
        }

        public AddEntryWindow(WindowsService service, Entry E)
        {
            InitializeComponent();
            DataContext = new AddEntryViewModel(service, E);
        }
    }
}
