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

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        /// <summary>
        /// Lấy số liệu 1 tuần lên biểu đồ
        /// </summary>
        private void GetParamForChart()
        {
            //    List<SensorData> lst = new List<SensorData>();
            //    lst = oBL.GetHistoryByWeek();
            //    double[] k = new double[7];
            //    double[] Volt = new double[7];
            //    double[] Ampe = new double[7];
            //    double[] Woat = new double[7];
            //    double[] KwH = new double[8];

            //    foreach (SensorData sensor in lst)
            //    {
            //        // tính từng thông số theo ngày
            //        if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date)
            //        {
            //            Volt[0] += double.Parse(sensor.Voltage);
            //            Ampe[0] += double.Parse(sensor.Current);
            //            Woat[0] += double.Parse(sensor.Power);
            //            KwH[0] = double.Parse(sensor.Energy);
            //            k[0]++;
            //        }
            //        if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-1))
            //        {
            //            Volt[1] += double.Parse(sensor.Voltage);
            //            Ampe[1] += double.Parse(sensor.Current);
            //            Woat[1] += double.Parse(sensor.Power);
            //            KwH[1] = double.Parse(sensor.Energy);
            //            k[1]++;
            //        }
            //        if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-2))
            //        {
            //            Volt[2] += double.Parse(sensor.Voltage);
            //            Ampe[2] += double.Parse(sensor.Current);
            //            Woat[2] += double.Parse(sensor.Power);
            //            KwH[2] = double.Parse(sensor.Energy);
            //            k[2]++;
            //        }
            //        if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-3))
            //        {
            //            Volt[3] += double.Parse(sensor.Voltage);
            //            Ampe[3] += double.Parse(sensor.Current);
            //            Woat[3] += double.Parse(sensor.Power);
            //            KwH[3] = double.Parse(sensor.Energy);
            //            k[3]++;
            //        }
            //        if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-4))
            //        {
            //            Volt[4] += double.Parse(sensor.Voltage);
            //            Ampe[4] += double.Parse(sensor.Current);
            //            Woat[4] += double.Parse(sensor.Power);
            //            KwH[4] = double.Parse(sensor.Energy);
            //            k[4]++;
            //        }
            //        if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-5))
            //        {
            //            Volt[5] += double.Parse(sensor.Voltage);
            //            Ampe[5] += double.Parse(sensor.Current);
            //            Woat[5] += double.Parse(sensor.Power);
            //            KwH[5] = double.Parse(sensor.Energy);
            //            k[5]++;
            //        }
            //        if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-6))
            //        {
            //            Volt[6] += double.Parse(sensor.Voltage);
            //            Ampe[6] += double.Parse(sensor.Current);
            //            Woat[6] += double.Parse(sensor.Power);
            //            KwH[6] = double.Parse(sensor.Energy);
            //            k[6]++;
            //        }

            //        if (DateTime.Parse(sensor.UpdateTime).Date == DateTime.Now.Date.AddDays(-7))
            //        {
            //            KwH[7] = double.Parse(sensor.Energy);
            //        }

            //    }

            //    k[0] = k[0] > 0 ? k[0] : 1;
            //    k[1] = k[1] > 0 ? k[1] : 1;
            //    k[2] = k[2] > 0 ? k[2] : 1;
            //    k[3] = k[3] > 0 ? k[3] : 1;
            //    k[4] = k[4] > 0 ? k[4] : 1;
            //    k[5] = k[5] > 0 ? k[5] : 1;
            //    k[6] = k[6] > 0 ? k[6] : 1;

            //    for (int i = 0; i < 7; i++)
            //    {
            //        Volt[i] = Math.Round(Volt[i] / k[i], 3) >= 0 ? Math.Round(Volt[i] / k[i], 3) : 0;
            //        Ampe[i] = Math.Round(Ampe[i] / k[i], 3) >= 0 ? Math.Round(Ampe[i] / k[i], 3) : 0;
            //        Woat[i] = Math.Round(Woat[i] / k[i], 3) >= 0 ? Math.Round(Woat[i] / k[i], 3) : 0;
            //        KwH[i] = Math.Round(KwH[i] - KwH[i + 1], 3) >= 0 ? Math.Round(KwH[i] - KwH[i + 1], 3) : 0;
            //    }

            //    //gán giá trị theo từng mốc lên biểu đồ
            //    uc_AmpeReport.srValue.Values = new ChartValues<double> { Ampe[6], Ampe[5], Ampe[4], Ampe[3], Ampe[2], Ampe[1], Ampe[0] };
            //    uc_VoltReport.srValue.Values = new ChartValues<double> { Volt[6], Volt[5], Volt[4], Volt[3], Volt[2], Volt[1], Volt[0] };
            //    uc_WoatReport.srValue.Values = new ChartValues<double> { Woat[6], Woat[5], Woat[4], Woat[3], Woat[2], Woat[1], Woat[0] };
            //    uc_KWhReport.srValue.Values = new ChartValues<double> { KwH[6], KwH[5], KwH[4], KwH[3], KwH[2], KwH[1], KwH[0] };

            //    //Gán label cho biểu đồ
            //    uc_AmpeReport.srValue.Title = "Cường độ dòng điện trung bình";
            //    uc_VoltReport.srValue.Title = "Điện áp trung bình";
            //    uc_KWhReport.srValue.Title = "Điện năng tiêu thụ";
            //    uc_WoatReport.srValue.Title = "Công suất tải trung bình";


            //    uc_KWhReport.Label.Labels = new[] {
            //                DateTime.Now.AddDays(-6).ToString("dd/MM/yy"), DateTime.Now.AddDays(-5).ToString("dd/MM/yy"),
            //                DateTime.Now.AddDays(-4).ToString("dd/MM/yy"), DateTime.Now.AddDays(-3).ToString("dd/MM/yy"),
            //                DateTime.Now.AddDays(-2).ToString("dd/MM/yy"), DateTime.Now.AddDays(-1).ToString("dd/MM/yy"),
            //                DateTime.Now.ToString("dd/MM/yy")
            //        };

            //    uc_AmpeReport.Label.Labels = new[] {
            //                DateTime.Now.AddDays(-6).ToString("dd/MM/yy"), DateTime.Now.AddDays(-5).ToString("dd/MM/yy"),
            //                DateTime.Now.AddDays(-4).ToString("dd/MM/yy"), DateTime.Now.AddDays(-3).ToString("dd/MM/yy"),
            //                DateTime.Now.AddDays(-2).ToString("dd/MM/yy"), DateTime.Now.AddDays(-1).ToString("dd/MM/yy"),
            //                DateTime.Now.ToString("dd/MM/yy")
            //        };

            //    uc_VoltReport.Label.Labels = new[] {
            //                DateTime.Now.AddDays(-6).ToString("dd/MM/yy"), DateTime.Now.AddDays(-5).ToString("dd/MM/yy"),
            //                DateTime.Now.AddDays(-4).ToString("dd/MM/yy"), DateTime.Now.AddDays(-3).ToString("dd/MM/yy"),
            //                DateTime.Now.AddDays(-2).ToString("dd/MM/yy"), DateTime.Now.AddDays(-1).ToString("dd/MM/yy"),
            //                DateTime.Now.ToString("dd/MM/yy")
            //        };

            //    uc_WoatReport.Label.Labels = new[] {
            //                DateTime.Now.AddDays(-6).ToString("dd/MM/yy"), DateTime.Now.AddDays(-5).ToString("dd/MM/yy"),
            //                DateTime.Now.AddDays(-4).ToString("dd/MM/yy"), DateTime.Now.AddDays(-3).ToString("dd/MM/yy"),
            //                DateTime.Now.AddDays(-2).ToString("dd/MM/yy"), DateTime.Now.AddDays(-1).ToString("dd/MM/yy"),
            //                DateTime.Now.ToString("dd/MM/yy")
            //        };
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
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

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {

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

        }
    }
}
