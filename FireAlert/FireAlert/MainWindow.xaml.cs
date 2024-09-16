using LiveCharts;
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
using System.Windows.Threading;

namespace FireAlert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BLDatabase oBL = new BLDatabase();
        clsAlert _alert = new clsAlert();
        List<FingerData> lstUser = new List<FingerData>();
        DispatcherTimer timerCloseDoor;
        private bool _isAlarm1 = false;
        private bool _isAlarm2 = false;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += Timer_Tick;
            timer.Start();

            DispatcherTimer timerCheckDoor = new DispatcherTimer();
            timerCheckDoor.Interval = TimeSpan.FromSeconds(1);
            timerCheckDoor.Tick += timerCheckDoor_Tick;
            timerCheckDoor.Start();

            timerCloseDoor = new DispatcherTimer();
            timerCloseDoor.Interval = TimeSpan.FromSeconds(5);
            timerCloseDoor.Tick += timerCloseDoor_Tick;
        }


        /// <summary>
        /// Lấy số liệu 1 tuần lên biểu đồ
        /// </summary>
        private void GetParamForChart()
        {
            List<SensorData> lst = new List<SensorData>();
            lst = oBL.GetHistoryByWeek();
            double[] k = new double[7];
            double[] Gas1 = new double[7];
            double[] Gas2 = new double[7];

            foreach (SensorData sensor in lst)
            {
                // tính từng thông số theo ngày
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date)
                {
                    Gas1[0] += double.Parse(sensor.Gas1);
                    Gas2[0] += double.Parse(sensor.Gas2);
                    k[0]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-1))
                {
                    Gas1[1] += double.Parse(sensor.Gas1);
                    Gas2[1] += double.Parse(sensor.Gas2);
                    k[1]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-2))
                {
                    Gas1[2] += double.Parse(sensor.Gas1);
                    Gas2[2] += double.Parse(sensor.Gas2);
                    k[2]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-3))
                {
                    Gas1[3] += double.Parse(sensor.Gas1);
                    Gas2[3] += double.Parse(sensor.Gas2);
                    k[3]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-4))
                {
                    Gas1[4] += double.Parse(sensor.Gas1);
                    Gas2[4] += double.Parse(sensor.Gas2);
                    k[4]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-5))
                {
                    Gas1[5] += double.Parse(sensor.Gas1);
                    Gas2[5] += double.Parse(sensor.Gas2);
                    k[5]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-6))
                {
                    Gas1[6] += double.Parse(sensor.Gas1);
                    Gas2[6] += double.Parse(sensor.Gas2);
                    k[6]++;
                }
            }

            k[0] = k[0] > 0 ? k[0] : 1;
            k[1] = k[1] > 0 ? k[1] : 1;
            k[2] = k[2] > 0 ? k[2] : 1;
            k[3] = k[3] > 0 ? k[3] : 1;
            k[4] = k[4] > 0 ? k[4] : 1;
            k[5] = k[5] > 0 ? k[5] : 1;
            k[6] = k[6] > 0 ? k[6] : 1;

            for (int i = 0; i < 7; i++)
            {
                Gas1[i] = Math.Round(Gas1[i] / k[i], 3) >= 0 ? Math.Round(Gas1[i] / k[i], 3) : 0;
                Gas2[i] = Math.Round(Gas2[i] / k[i], 3) >= 0 ? Math.Round(Gas2[i] / k[i], 3) : 0;
            }

            //gán giá trị theo từng mốc lên biểu đồ
            uc_Gas1Report.srValue.Values = new ChartValues<double> { Gas1[6], Gas1[5], Gas1[4], Gas1[3], Gas1[2], Gas1[1], Gas1[0] };
            uc_Gas2Report.srValue.Values = new ChartValues<double> { Gas2[6], Gas2[5], Gas2[4], Gas2[3], Gas2[2], Gas2[1], Gas2[0] };

            //Gán label cho biểu đồ
            uc_Gas1Report.srValue.Title = "Nồng độ khí gas trung bình phòng bếp";
            uc_Gas2Report.srValue.Title = "Nồng độ khí gas trung bình phòng bếp";


            uc_Gas1Report.Label.Labels = new[] {
                           DateTime.Now.AddDays(-6).ToString("dd/MM/yy"), DateTime.Now.AddDays(-5).ToString("dd/MM/yy"),
                           DateTime.Now.AddDays(-4).ToString("dd/MM/yy"), DateTime.Now.AddDays(-3).ToString("dd/MM/yy"),
                           DateTime.Now.AddDays(-2).ToString("dd/MM/yy"), DateTime.Now.AddDays(-1).ToString("dd/MM/yy"),
                           DateTime.Now.ToString("dd/MM/yy")
                   };

            uc_Gas2Report.Label.Labels = new[] {
                           DateTime.Now.AddDays(-6).ToString("dd/MM/yy"), DateTime.Now.AddDays(-5).ToString("dd/MM/yy"),
                           DateTime.Now.AddDays(-4).ToString("dd/MM/yy"), DateTime.Now.AddDays(-3).ToString("dd/MM/yy"),
                           DateTime.Now.AddDays(-2).ToString("dd/MM/yy"), DateTime.Now.AddDays(-1).ToString("dd/MM/yy"),
                           DateTime.Now.ToString("dd/MM/yy")
                   };


        }

        private void GetCurrentParam()
        {
            SensorData _ss = new SensorData();
            _ss = oBL.GetCurentParameter();

            lblTemp1.Text = _ss.Temp1;
            lblHum1.Text = _ss.Hum1;
            lblTemp2.Text = _ss.Temp2;
            lblHum2.Text = _ss.Hum2;
            lblGas.Text = _ss.Gas1;

            SetDeviceParam(_ss.Device);

            //Cảnh báo gas tầng 2
            if (double.Parse(_ss.Gas1) >= 60.0)
            {
                if (!_isAlarm1)
                {
                    _isAlarm1 = true;
                    _alert.SendAlert("Có phát hiện nồng độ khí gas cao bất thường tại tầng 2");
                }
            }
            else
                _isAlarm1 = false;

            //Cảnh báo gas tầng 3
            if (double.Parse(_ss.Gas2) >= 60.0)
            {
                if (!_isAlarm2)
                {
                    _isAlarm2 = true;
                    _alert.SendAlert("Có phát hiện nồng độ khí gas cao bất thường tại tầng 3");
                }
            }
            else
                _isAlarm2 = false;

            //Cảnh báo cháy tầng 2

            //Cảnh báo cháy tầng 3
        }

        private void SetDeviceParam(string State)
        {
            if (State.Length >= 6)
            {
                State = State.Substring(1);
                Eqiupment _eq = new Eqiupment();
                _eq.Lamp1 = int.Parse(State.Substring(0, 1));
                _eq.Fan1 = int.Parse(State.Substring(1, 1));
                _eq.Lamp3 = int.Parse(State.Substring(2, 1));
                _eq.Fan3 = int.Parse(State.Substring(3, 1));
                _eq.Lamp2 = int.Parse(State.Substring(4, 1));
                _eq.Fan2 = int.Parse(State.Substring(5, 1));

                oBL.SetEqiupmentState(_eq);

            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                GetCurrentParam();
                GetParamForChart();
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// Kiểm tra báo cháy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCheckDoor_Tick(object sender, EventArgs e)
        {
            try
            {
                ///Kiểm tra mở cửa
                FingerStatus _fingerStatus = new FingerStatus();
                _fingerStatus = oBL.GetCurentFingerStatus();
                if (!string.IsNullOrEmpty(_fingerStatus.Status) && _fingerStatus.Status.StartsWith("i"))
                {
                    foreach (FingerData finger in lstUser)
                    {
                        if (finger.FingerID.ToString() == _fingerStatus.Status.Substring(1))
                        {
                            //mở cửa
                            Eqiupment _eq = new Eqiupment();
                            _eq.Door = 1;
                            oBL.SetEqiupmentState(_eq);
                            timerCloseDoor.Start();

                            //đẩy status về 0
                            _fingerStatus.Status = "0";
                            oBL.SetFingerStatus(_fingerStatus);

                            //lưu lịch sử
                            oBL.SaveInOutHistory(finger);
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Error");
            }

        }

        /// <summary>
        /// đóng cửa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCloseDoor_Tick(object sender, EventArgs e)
        {
            Eqiupment _eq = new Eqiupment();
            _eq.Door = 0;
            oBL.SetEqiupmentState(_eq);
            timerCloseDoor.Stop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetCurrentParam();
                GetParamForChart();
                LoadFingerList();
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// Load ra danh sách vân tay
        /// </summary>
        private void LoadFingerList()
        {
            lstUser.Clear();
            lstUser = oBL.GetFingerList();
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            wdHistory frm = new wdHistory();
            frm.ShowDialog();
        }

        private void btnControl_Click(object sender, RoutedEventArgs e)
        {
            wdManualControl wd = new wdManualControl();
            wd.ShowDialog();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            wdUserManagement frm = new wdUserManagement();
            frm.ShowDialog();
            LoadFingerList();
        }
    }
}
