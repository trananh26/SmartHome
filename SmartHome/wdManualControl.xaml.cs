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
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq != null)
            {
                btnDoor.Content = _eq.Door == 0 ? "Đóng cửa phòng khách" : "Mở cửa phòng khách";
                btnLamp11.Content = _eq.Lamp1 == 0 ? "Tắt đèn phòng khách" : "Bật đèn phòng khách";
                btnLamp12.Content = _eq.Fan1 == 0 ? "Tắt quạt phòng khách" : "Bật quạt phòng khách";

                btnLamp21.Content = _eq.Lamp2 == 0 ? "Tắt đèn phòng ngủ" : "Bật đèn phòng ngủ";
                btnLamp22.Content = _eq.Fan2 == 0 ? "Tắt quạt phòng ngủ" : "Bật quạt phòng ngủ";

                btnLamp31.Content = _eq.Lamp3 == 0 ? "Tắt đèn phòng bếp" : "Bật đèn phòng bếp";
                btnLamp32.Content = _eq.Fan3 == 0 ? "Tắt quạt phòng bếp" : "Bật quạt phòng bếp";

            }
        }

        private void btnDoor_Click(object sender, RoutedEventArgs e)
        {
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq.Door == 1)
            {
                //Mở ra 
                _eq.Door = 0;
                oBL.SetEqiupmentState(_eq);
            }
            else
            {
                //Đóng
                _eq.Door = 1;
                oBL.SetEqiupmentState(_eq);
            }

            MessageBox.Show(_eq.Door == 1 ? "Đóng cửa phòng khách thành công" : "Mở cửa phòng khách thành công");
            GetButtonStatus();
        }

        private void btnLamp11_Click(object sender, RoutedEventArgs e)
        {
            //mở đèn phòng khách
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq.Lamp1 == 1)
            {
                //Mở ra 
                _eq.Lamp1 = 0;
                oBL.SetEqiupmentState(_eq);
            }
            else
            {
                //Đóng
                _eq.Lamp1 = 1;
                oBL.SetEqiupmentState(_eq);
            }

            MessageBox.Show(_eq.Lamp1 == 1 ? "Tắt đèn phòng khách thành công" : "Bật đèn phòng khách thành công");
            GetButtonStatus();
        }

        private void btnLamp12_Click(object sender, RoutedEventArgs e)
        {
            //mở quạt phòng khách
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq.Fan1 == 1)
            {
                //Mở ra 
                _eq.Fan1 = 0;
                oBL.SetEqiupmentState(_eq);
            }
            else
            {
                //Đóng
                _eq.Fan1 = 1;
                oBL.SetEqiupmentState(_eq);
            }

            MessageBox.Show(_eq.Fan1 == 1 ? "Tắt quạt phòng khách thành công" : "Bật quạt phòng khách thành công");
            GetButtonStatus();
        }

        private void btnLamp21_Click(object sender, RoutedEventArgs e)
        {
            //mở đèn phòng ngủ
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq.Lamp2 == 1)
            {
                //Mở ra 
                _eq.Lamp2 = 0;
                oBL.SetEqiupmentState(_eq);
            }
            else
            {
                //Đóng
                _eq.Lamp2 = 1;
                oBL.SetEqiupmentState(_eq);
            }

            MessageBox.Show(_eq.Lamp2 == 1 ? "Tắt đèn phòng ngủ thành công" : "Bật đèn phòng ngủ thành công");
            GetButtonStatus();
        }

        private void btnLamp22_Click(object sender, RoutedEventArgs e)
        {
            //mở quạt phòng ngủ
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq.Fan2 == 1)
            {
                //Mở ra 
                _eq.Fan2 = 0;
                oBL.SetEqiupmentState(_eq);
            }
            else
            {
                //Đóng
                _eq.Fan2 = 1;
                oBL.SetEqiupmentState(_eq);
            }

            MessageBox.Show(_eq.Fan2 == 1 ? "Tắt quạt phòng ngủ thành công" : "Bật quạt phòng ngủ thành công");
            GetButtonStatus();
        }

        private void btnLamp31_Click(object sender, RoutedEventArgs e)
        {
            //mở đèn phòng bếp
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq.Lamp3 == 1)
            {
                //Mở ra 
                _eq.Lamp3 = 0;
                oBL.SetEqiupmentState(_eq);
            }
            else
            {
                //Đóng
                _eq.Lamp3 = 1;
                oBL.SetEqiupmentState(_eq);
            }

            MessageBox.Show(_eq.Lamp3 == 1 ? "Tắt đèn phòng bếp thành công" : "Bật đèn phòng bếp thành công");
            GetButtonStatus();
        }

        private void btnLamp32_Click(object sender, RoutedEventArgs e)
        {
            //mở quạt phòng bếp
            Eqiupment _eq = new Eqiupment();
            _eq = oBL.GetEqiupmentstate();

            if (_eq.Fan3 == 1)
            {
                //Mở ra 
                _eq.Fan3 = 0;
                oBL.SetEqiupmentState(_eq);
            }
            else
            {
                //Đóng
                _eq.Fan3 = 1;
                oBL.SetEqiupmentState(_eq);
            }

            MessageBox.Show(_eq.Fan3 == 1 ? "Tắt quạt phòng bếp thành công" : "Bật quạt phòng bếp thành công");
            GetButtonStatus();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
