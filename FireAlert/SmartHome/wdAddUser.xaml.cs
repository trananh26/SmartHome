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
using System.Windows.Threading;

namespace SmartHome
{
    /// <summary>
    /// Interaction logic for wdAddUser.xaml
    /// </summary>
    public partial class wdAddUser : Window
    {
        BLDatabase oBL = new BLDatabase();
        FingerStatus _fingerStatus = new FingerStatus();
        private int _mNewFingerID;

        public int NewFingerID { get => _mNewFingerID; set => _mNewFingerID = value; }

        public wdAddUser()
        {
            InitializeComponent();
           
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            FingerData User = new FingerData();
            User.UpdateTime = DateTime.Now;
            User.FingerID = NewFingerID;
            User.UserName = "NVA";
            User.FullName = "Nguyễn Văn A";
            User.Job = "CN";
            oBL.AddNewUser(User);

            MessageBox.Show("Thêm người dùng thành công");
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //đẩy status về 0
            _fingerStatus.Status = "0";
            oBL.SetFingerStatus(_fingerStatus);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
