using LiveCharts;
using System;
using System.Collections.Generic;
using System.Data;
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
        private bool _isAlarm1 = false;
        private bool _isAlarm2 = false;
        private bool _isFire1 = false;
        private bool _isFire2 = false;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();

            DispatcherTimer timerCheckFire = new DispatcherTimer();
            timerCheckFire.Interval = TimeSpan.FromSeconds(1);
            timerCheckFire.Tick += TimerCheckFire_Tick;
            timerCheckFire.Start();

        }


        /// <summary>
        /// Lấy số liệu 1 tuần lên biểu đồ
        /// </summary>
        private void GetParamForChart()
        {
            try
            {
                List<SensorData> lst = new List<SensorData>();
                lst = oBL.GetHistoryByWeek();

                DataTable dt = new DataTable();
                dt = ToDataTable(lst);
                dtgGasHistory.ItemsSource = dt.DefaultView;

                double[] k = new double[7];
                double[] Gas1 = new double[7];
                double[] Gas2 = new double[7];

                foreach (SensorData sensor in lst)
                {
                    if (string.IsNullOrEmpty(sensor.Gas2))
                        sensor.Gas2 = "0";

                    if (string.IsNullOrEmpty(sensor.Gas1))
                        sensor.Gas1 = "0";

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
            catch (Exception)
            {

            }
        }

        private void GetCurrentParam()
        {
            SensorData _ss = new SensorData();
            _ss = oBL.GetCurentParameter();
            lblGas1.Text = _ss.Gas1;
            lblGas2.Text = _ss.Gas2;

            //Cảnh báo gas tầng 2
            if (double.Parse(_ss.Gas1) >= 100)
            {
                if (!_isAlarm1)
                {
                    _isAlarm1 = true;
                    _alert.SendAlert("Có phát hiện nồng độ khí gas cao bất thường tại tầng 2. Vui lòng tới kiểm tra!");

                    SaveHistory("Có phát hiện nồng độ khí gas cao bất thường tại tầng 2. Vui lòng tới kiểm tra!");
                }
            }
            else
                _isAlarm1 = false;

            //Cảnh báo gas tầng 3
            if (double.Parse(_ss.Gas2) >= 100)
            {
                if (!_isAlarm2)
                {
                    _isAlarm2 = true;
                    _alert.SendAlert("Có phát hiện nồng độ khí gas cao bất thường tại tầng 3. Vui lòng tới kiểm tra!");

                    SaveHistory("Có phát hiện nồng độ khí gas cao bất thường tại tầng 3. Vui lòng tới kiểm tra!");
                }
            }
            else
                _isAlarm2 = false;

        }

        /// <summary>
        /// Lưu lịch sử cảnh báo
        /// </summary>
        /// <param name="mess"></param>
        private void SaveHistory(string mess)
        {
            try
            {
                AlertHistory _alert = new AlertHistory();
                _alert.Messager = mess;
                _alert.UpdateTime = DateTime.Now.ToString("HH:mm:dd dd/MM/yyyy");
                _alert.UpdateBy = "System";
                oBL.SaveHistory(_alert);

                List<AlertHistory> lst = new List<AlertHistory>();
                lst = oBL.GetAlertHistoryByWeek();

                DataTable dt = new DataTable();
                dt = ToAlertDataTable(lst);
                dtgAlert.ItemsSource = dt.DefaultView;
            }
            catch (Exception)
            {


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
        /// Kiểm tra xem có cháy không
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerCheckFire_Tick(object sender, EventArgs e)
        {
            try
            {
                ///Kiểm tra cháy
                FireSensorData _FireSensor = new FireSensorData();
                _FireSensor = oBL.GetCurentFireSensorData();

                ///Báo cháy tầng 2
                if (_FireSensor != null)
                {
                    if (_FireSensor.Fire1 == 1)
                    {
                        if (!_isFire1)
                        {
                            _isFire1 = true;
                            _alert.SendAlert("Có phát hiện cháy tại tầng 2. Vui lòng tới kiểm tra!");

                            SaveHistory("Có phát hiện cháy tại tầng 2. Vui lòng tới kiểm tra!");
                        }
                    }
                    else
                        _isFire1 = false;

                    ///Báo cháy tầng 2
                    if (_FireSensor.Fire2 == 1)
                    {
                        if (!_isFire2)
                        {
                            _isFire2 = true;
                            _alert.SendAlert("Có phát hiện cháy tại tầng 3. Vui lòng tới kiểm tra!");

                            SaveHistory("Có phát hiện cháy tại tầng 3. Vui lòng tới kiểm tra!");
                        }
                    }
                    else
                        _isFire2 = false;
                }
            }
            catch (Exception ex)
            {

            }
        }


        private DataTable ToDataTable<SensorData>(List<SensorData> lst)
        {

            DataTable dataTable = new DataTable(typeof(SensorData).Name);

            // Lấy ra danh sách các thuộc tính của kiểu dữ liệu T
            var props = typeof(SensorData).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            // Thêm các cột vào DataTable dựa trên các thuộc tính của đối tượng
            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }

            // Thêm các hàng vào DataTable dựa trên giá trị của từng đối tượng
            foreach (var item in lst)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    // Lấy giá trị của từng thuộc tính của đối tượng
                    values[i] = props[i].GetValue(item);
                }
                // Thêm hàng mới vào DataTable
                dataTable.Rows.Add(values);
            }

            return dataTable;

        }

        private DataTable ToAlertDataTable<AlertHistory>(List<AlertHistory> lst)
        {

            DataTable dataTable = new DataTable(typeof(AlertHistory).Name);

            // Lấy ra danh sách các thuộc tính của kiểu dữ liệu T
            var props = typeof(AlertHistory).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            // Thêm các cột vào DataTable dựa trên các thuộc tính của đối tượng
            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }

            // Thêm các hàng vào DataTable dựa trên giá trị của từng đối tượng
            foreach (var item in lst)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    // Lấy giá trị của từng thuộc tính của đối tượng
                    values[i] = props[i].GetValue(item);
                }
                // Thêm hàng mới vào DataTable
                dataTable.Rows.Add(values);
            }

            return dataTable;

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetCurrentParam();
                GetParamForChart();

                List<AlertHistory> lst = new List<AlertHistory>();
                lst = oBL.GetAlertHistoryByWeek();

                DataTable dt = new DataTable();
                dt = ToAlertDataTable(lst);
                dtgAlert.ItemsSource = dt.DefaultView;
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
        }
    }
}
