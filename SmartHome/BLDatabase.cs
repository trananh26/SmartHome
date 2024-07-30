using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
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
    }
}
