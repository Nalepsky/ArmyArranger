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

namespace ArmyArranger
{
    /// <summary>
    /// Logika interakcji dla klasy InputUnitsWindow.xaml
    /// </summary>
    public partial class InputUnitsWindow : Window
    {
        public InputUnitsWindow()
        {
            InitializeComponent();
        }

        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.WindowState = this.WindowState;
            newWindow.Top = this.Top;
            newWindow.Left = this.Left;
            newWindow.Height = this.Height;
            newWindow.Width = this.Width;
            newWindow.Show();
            this.Close();
        }
    }
}
