using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireAlert
{
    public class BLDatabase
    {

        DLDatabase oDL = new DLDatabase();

        /// <summary>
        /// kéo trạng thái thiết bị
        /// </summary>
        /// <returns></returns>
        public Eqiupment GetEqiupmentstate()
        {
            return oDL.GetEqiupmentstate();
        }

        /// <summary>
        /// set lại  trạng thái thiết bị
        /// </summary>
        /// <param name="eq"></param>
        public void SetEqiupmentState(Eqiupment eqiupment)
        {
            oDL.SetEqiupmentState(eqiupment);
        }

        public SensorData GetCurentParameter()
        {
            List<SensorData> lst = new List<SensorData>();
            lst = oDL.GetHistory();
            return lst.First();
        }

        public List<SensorData> GetHistoryByWeek()
        {
            List<SensorData> lst = new List<SensorData>();
            lst = oDL.GetHistoryByWeek();
            return lst;
        }

        public List<FingerData> GetFingerList()
        {
            List<FingerData> lst = new List<FingerData>();
            lst = oDL.GetFingerList();
            return lst;
        }

        /// <summary>
        /// Thêm người dùng mới
        /// </summary>
        /// <param name="user"></param>
        public void AddNewUser(FingerData user)
        {
            oDL.AddNewUser(user);
        }

        public FingerStatus GetCurentFingerStatus()
        {
            return oDL.GetCurentFingerStatus();
        }

        public void SetFingerStatus(FingerStatus Status)
        {
            oDL.SetFingerStatus(Status);
        }

        public void SaveInOutHistory(FingerData finger)
        {
            oDL.SaveInOutHistory(finger);
        }
    }
}
