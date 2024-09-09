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

namespace SmartHome
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
        private bool _isAlarm = false;
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
            double[] Temp1 = new double[7];
            double[] Hum1 = new double[7];
            double[] Temp2 = new double[7];
            double[] Hum2 = new double[7];
            double[] Gas2 = new double[7];

            foreach (SensorData sensor in lst)
            {
                // tính từng thông số theo ngày
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date)
                {
                    Temp1[0] += double.Parse(sensor.Temp1);
                    Hum1[0] += double.Parse(sensor.Hum1);
                    Temp2[0] += double.Parse(sensor.Temp2);
                    Hum2[0] += double.Parse(sensor.Hum2);
                    Gas2[0] += double.Parse(sensor.Gas);
                    k[0]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-1))
                {
                    Temp1[1] += double.Parse(sensor.Temp1);
                    Hum1[1] += double.Parse(sensor.Hum1);
                    Temp2[1] += double.Parse(sensor.Temp2);
                    Hum2[1] += double.Parse(sensor.Hum2);
                    Gas2[1] += double.Parse(sensor.Gas);
                    k[1]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-2))
                {
                    Temp1[2] += double.Parse(sensor.Temp1);
                    Hum1[2] += double.Parse(sensor.Hum1);
                    Temp2[2] += double.Parse(sensor.Temp2);
                    Hum2[2] += double.Parse(sensor.Hum2);
                    Gas2[2] += double.Parse(sensor.Gas);
                    k[2]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-3))
                {
                    Temp1[3] += double.Parse(sensor.Temp1);
                    Hum1[3] += double.Parse(sensor.Hum1);
                    Temp2[3] += double.Parse(sensor.Temp2);
                    Hum2[3] += double.Parse(sensor.Hum2);
                    Gas2[3] += double.Parse(sensor.Gas);
                    k[3]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-4))
                {
                    Temp1[4] += double.Parse(sensor.Temp1);
                    Hum1[4] += double.Parse(sensor.Hum1);
                    Temp2[4] += double.Parse(sensor.Temp2);
                    Hum2[4] += double.Parse(sensor.Hum2);
                    Gas2[4] += double.Parse(sensor.Gas);
                    k[4]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-5))
                {
                    Temp1[5] += double.Parse(sensor.Temp1);
                    Hum1[5] += double.Parse(sensor.Hum1);
                    Temp2[5] += double.Parse(sensor.Temp2);
                    Hum2[5] += double.Parse(sensor.Hum2);
                    Gas2[5] += double.Parse(sensor.Gas);
                    k[5]++;
                }
                if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-6))
                {
                    Temp1[6] += double.Parse(sensor.Temp1);
                    Hum1[6] += double.Parse(sensor.Hum1);
                    Temp2[6] += double.Parse(sensor.Temp2);
                    Hum2[6] += double.Parse(sensor.Hum2);
                    Gas2[6] += double.Parse(sensor.Gas);
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
                Temp1[i] = Math.Round(Temp1[i] / k[i], 3) >= 0 ? Math.Round(Temp1[i] / k[i], 3) : 0;
                Hum1[i] = Math.Round(Hum1[i] / k[i], 3) >= 0 ? Math.Round(Hum1[i] / k[i], 3) : 0;
                Temp2[i] = Math.Round(Temp2[i] / k[i], 3) >= 0 ? Math.Round(Temp2[i] / k[i], 3) : 0;
                Hum2[i] = Math.Round(Hum2[i] / k[i], 3) >= 0 ? Math.Round(Hum2[i] / k[i], 3) : 0;
                Gas2[i] = Math.Round(Gas2[i] / k[i], 3) >= 0 ? Math.Round(Gas2[i] / k[i], 3) : 0;
            }

            //gán giá trị theo từng mốc lên biểu đồ
            uc_Temp1Report.srValue.Values = new ChartValues<double> { Temp1[6], Temp1[5], Temp1[4], Temp1[3], Temp1[2], Temp1[1], Temp1[0] };
            uc_Hum1Report.srValue.Values = new ChartValues<double> { Hum1[6], Hum1[5], Hum1[4], Hum1[3], Hum1[2], Hum1[1], Hum1[0] };
            uc_Temp2Report.srValue.Values = new ChartValues<double> { Temp2[6], Temp2[5], Temp2[4], Temp2[3], Temp2[2], Temp2[1], Temp2[0] };
            uc_Gas2Report.srValue.Values = new ChartValues<double> { Gas2[6], Gas2[5], Gas2[4], Gas2[3], Gas2[2], Gas2[1], Gas2[0] };

            //Gán label cho biểu đồ
            uc_Temp1Report.srValue.Title = "Nhiệt độ trung bình phòng ngủ";
            uc_Hum1Report.srValue.Title = "Độ ẩm trung bình phòng ngủ";
            uc_Gas2Report.srValue.Title = "Nồng độ khí gas trung bình phòng bếp";
            uc_Temp2Report.srValue.Title = "Nhiệt độ trung bình phòng bếp";


            uc_Gas2Report.Label.Labels = new[] {
                           DateTime.Now.AddDays(-6).ToString("dd/MM/yy"), DateTime.Now.AddDays(-5).ToString("dd/MM/yy"),
                           DateTime.Now.AddDays(-4).ToString("dd/MM/yy"), DateTime.Now.AddDays(-3).ToString("dd/MM/yy"),
                           DateTime.Now.AddDays(-2).ToString("dd/MM/yy"), DateTime.Now.AddDays(-1).ToString("dd/MM/yy"),
                           DateTime.Now.ToString("dd/MM/yy")
                   };

            uc_Temp1Report.Label.Labels = new[] {
                           DateTime.Now.AddDays(-6).ToString("dd/MM/yy"), DateTime.Now.AddDays(-5).ToString("dd/MM/yy"),
                           DateTime.Now.AddDays(-4).ToString("dd/MM/yy"), DateTime.Now.AddDays(-3).ToString("dd/MM/yy"),
                           DateTime.Now.AddDays(-2).ToString("dd/MM/yy"), DateTime.Now.AddDays(-1).ToString("dd/MM/yy"),
                           DateTime.Now.ToString("dd/MM/yy")
                   };

            uc_Hum1Report.Label.Labels = new[] {
                           DateTime.Now.AddDays(-6).ToString("dd/MM/yy"), DateTime.Now.AddDays(-5).ToString("dd/MM/yy"),
                           DateTime.Now.AddDays(-4).ToString("dd/MM/yy"), DateTime.Now.AddDays(-3).ToString("dd/MM/yy"),
                           DateTime.Now.AddDays(-2).ToString("dd/MM/yy"), DateTime.Now.AddDays(-1).ToString("dd/MM/yy"),
                           DateTime.Now.ToString("dd/MM/yy")
                   };

            uc_Temp2Report.Label.Labels = new[] {
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
            lblGas.Text = _ss.Gas;

            SetDeviceParam(_ss.Device);

            if (double.Parse(_ss.Gas) >= 60.0)
            {
                if (!_isAlarm)
                {
                    _isAlarm = true;
                    _alert.SendAlert("Có phát hiện nồng độ khí gas cao bất thường tại nhà bếp");
                }
            }
            else
                _isAlarm = false;
            ///check nhiệt độ, độ ẩm tại phòng ngủ nếu có trẻ em
        }

        private void SetDeviceParam(string State)
        {
            if (State.Length >= 6)
            {
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
