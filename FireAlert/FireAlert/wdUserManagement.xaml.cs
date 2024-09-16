using iTextSharp.text;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FireAlert
{
    /// <summary>
    /// Interaction logic for wdUserManagement.xaml
    /// </summary>
    public partial class wdUserManagement : Window
    {
        BLDatabase oBL = new BLDatabase();
        FingerStatus _fingerStatus = new FingerStatus();
        List<FingerData> lstUser = new List<FingerData>();
        private int _newID;
        DispatcherTimer timer;
        public wdUserManagement()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Kiểm tra trạng thái thêm mới vân tay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            //FingerStatus _fingerStatus = new FingerStatus();
            //_fingerStatus = oBL.GetCurentFireSensorData();
            //if (!string.IsNullOrEmpty(_fingerStatus.Status) && _fingerStatus.Status.StartsWith("A"))
            //{
            //    timer.Stop();
            //    //gọi form nhập thông tin
            //    wdAddUser frm = new wdAddUser();
            //    frm.NewFingerID = _newID;
            //    frm.ShowDialog();

            //    LoadUserList();
            //}
        }

        /// <summary>
        /// Sự kiện thêm mới vân tay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {             
                //Lấy Id vân tay mới
                List<FingerData> lstUser = new List<FingerData>();
                lstUser = oBL.GetFingerList();
                
                if (lstUser.Count > 0)
                {
                    _newID = lstUser[0].FingerID + 1;
                    lstUser = lstUser.OrderByDescending(x => x.FingerID).ToList();
                }                        
                else
                    _newID = 1;
                //Set ID mới cho cảm biến vân tay
                _fingerStatus.Status = "t" + _newID.ToString();
                oBL.SetFingerStatus(_fingerStatus);
                timer.Start();
                MessageBox.Show("Vui lòng đặt tay vào cảm biến để quét vân tay");

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUserList();
        }

        private void LoadUserList()
        {
            lstUser.Clear();
            lstUser = oBL.GetFingerList();
            dtgUser.ItemsSource = ToDataTable(lstUser).DefaultView;

        }

        /// <summary>
        /// convert list to table
        /// </summary>
        /// <typeparam name="FingerData"></typeparam>
        /// <param name="lst"></param>
        /// <returns></returns>
        private DataTable ToDataTable<FingerData>(List<FingerData> lst)
        {
            DataTable dataTable = new DataTable(typeof(FingerData).Name);

            // Lấy ra danh sách các thuộc tính của kiểu dữ liệu T
            var props = typeof(FingerData).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

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
    }
}
