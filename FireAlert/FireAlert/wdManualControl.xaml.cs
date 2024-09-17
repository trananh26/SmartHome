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

namespace FireAlert
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
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq != null)
            {              
                btnLamp21.Content = _eq.Fan2 == 1 ? "Tắt quạt tầng 2" : "Bật quạt tầng 2";
                btnLamp22.Content = _eq.Door2 == 1 ? "Đóng cửa tầng 2" : "Mở cửa tầng 2";

                btnLamp31.Content = _eq.Fan3 == 1 ? "Tắt quạt tầng 2" : "Bật quạt tầng 2";
                btnLamp32.Content = _eq.Door3 == 1 ? "Đóng cửa tầng 2" : "Mở cửa tầng 2";

            }
        }

        private void btnLamp21_Click(object sender, RoutedEventArgs e)
        {
            
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq.Fan2 == 1)
            {
                _eq.Fan2 = 0;
                oBL.SetEqiupmentState(_eq);
            }
            else
            {
                _eq.Fan2 = 1;
                oBL.SetEqiupmentState(_eq);
            }

            MessageBox.Show(_eq.Fan2 == 0 ? "Tắt quạt tầng 2 thành công" : "Bật quạt tầng 2 thành công");
            GetButtonStatus();
        }

        private void btnLamp22_Click(object sender, RoutedEventArgs e)
        {
            
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq.Door2 == 1)
            {
                _eq.Door2 = 0;
                oBL.SetEqiupmentState(_eq);
            }
            else
            {
                _eq.Door2 = 1;
                oBL.SetEqiupmentState(_eq);
            }

            MessageBox.Show(_eq.Door2 == 0 ? "Đóng cửa tầng 2 thành công" : "Mở cửa tầng 2 thành công");
            GetButtonStatus();
        }

        private void btnLamp31_Click(object sender, RoutedEventArgs e)
        {
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq.Fan3 == 1)
            {
                _eq.Fan3 = 0;
                oBL.SetEqiupmentState(_eq);
            }
            else
            {
                _eq.Fan3 = 1;
                oBL.SetEqiupmentState(_eq);
            }

            MessageBox.Show(_eq.Fan3 == 0 ? "Tắt quạt tầng 3 thành công" : "Bật quạt tầng 3 thành công");
            GetButtonStatus();
        }

        private void btnLamp32_Click(object sender, RoutedEventArgs e)
        {
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq.Door3 == 1)
            {
                _eq.Door3 = 0;
                oBL.SetEqiupmentState(_eq);
            }
            else
            {
                _eq.Door3 = 1;
                oBL.SetEqiupmentState(_eq);
            }

            MessageBox.Show(_eq.Door3 == 0 ? "Đóng cửa tầng 3 thành công" : "Mở cửa tầng 3 thành công");
            GetButtonStatus();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
