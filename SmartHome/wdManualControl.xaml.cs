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

namespace SmartHome
{
    /// <summary>
    /// Interaction logic for wdManualControl.xaml
    /// </summary>
    public partial class wdManualControl : Window
    {

        BLDatabase oBL = new BLDatabase();


        public wdManualControl()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetButtonStatus();
        }

        private void GetButtonStatus()
        {
            
        }

        private void btnLamp11_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLamp12_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLamp21_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLamp22_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLamp31_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLamp32_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
