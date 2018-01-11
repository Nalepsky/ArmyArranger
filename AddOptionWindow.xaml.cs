using ArmyArranger.ViewModels.EditYourArmies;
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
using System.Windows.Shapes;

namespace ArmyArranger.Views
{
    /// <summary>
    /// Interaction logic for AddOptionWindow.xaml
    /// </summary>
    public partial class AddOptionWindow : Window
    {
        public AddOptionWindow(int id)
        {
            InitializeComponent();
            DataContext = new AddOptionViewModel(id);
        }
    }
}
