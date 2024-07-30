using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json;

namespace SmartHome
{
    public class DLDatabase
    {
        IFirebaseClient client;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "AIzaSyDCo4NSp_tt1TV0ol74gR1734NpucwJ7v4",
            BasePath = "https://smart-house-haui-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        /// <summary>
        /// Kéo trạng thái thiết bị
        /// </summary>
        /// <returns></returns>
        public Eqiupment GetEqiupmentstate()
        {
            Eqiupment eqiupment = new Eqiupment();
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                var Result = client.Get("/Control");
                Dictionary<string, string> State = JsonConvert.DeserializeObject<Dictionary<string, string>>(Result.Body.ToString());
                if (State != null)
                {
                    foreach (var key in State)
                    {
                        if (key.Key == "Door")
                            eqiupment.Door = int.Parse(key.Value);

                        if (key.Key == "Lamp1")
                            eqiupment.Lamp1 = int.Parse(key.Value);

                        if (key.Key == "Fan1")
                            eqiupment.Fan1 = int.Parse(key.Value);

                        if (key.Key == "Lamp2")
                            eqiupment.Lamp2 = int.Parse(key.Value);

                        if (key.Key == "Fan2")
                            eqiupment.Fan2 = int.Parse(key.Value);

                        if (key.Key == "Lamp3")
                            eqiupment.Lamp3 = int.Parse(key.Value);

                        if (key.Key == "Fan3")
                            eqiupment.Fan3 = int.Parse(key.Value);
                    }
                }
            }
            return eqiupment;
        }

        /// <summary>
        /// Set trạng thái thiết bị
        /// </summary>
        /// <param name="eqiupment"></param>
        /// <exception cref="NotImplementedException"></exception>
        public async void SetEqiupmentState(Eqiupment eqiupment)
        {
            FirebaseResponse response = await client.UpdateTaskAsync("Control", eqiupment);
        }

        /// <summary>
        /// Lấy lên lịch sử  quản lý môi trường
        /// </summary>
        /// <returns></returns>
        public List<SensorData> GetHistory()
        {
            List<SensorData> lst = new List<SensorData>();
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                var Result = client.Get("/Data");
                Dictionary<string, SensorData> sensorDataDict = JsonConvert.DeserializeObject<Dictionary<string, SensorData>>(Result.Body.ToString());

                if (sensorDataDict != null)
                {
                    foreach (var entry in sensorDataDict)
                    {
                        DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(entry.Value.ID).UtcDateTime;

                        SensorData _ss = new SensorData();
                        _ss.ID = entry.Value.ID;
                        _ss.UpdateTime = dateTime.AddHours(7).ToString("yyyy-MM-dd HH:mm:ss");
                        _ss.Temp1 = entry.Value.Temp1;
                        _ss.Hum1 = entry.Value.Hum1;
                        _ss.Temp2 = entry.Value.Temp2;
                        _ss.Hum2 = entry.Value.Hum2;
                        _ss.Gas = entry.Value.Gas;

                        lst.Add(_ss);
                    }
                    lst = lst.OrderByDescending(s => s.ID).ToList();
                }
            }

            return lst;
        }

        /// <summary>
        /// Lấy ra lịch sử 1  tuần gần nhất
        /// </summary>
        /// <returns></returns>
        public List<SensorData> GetHistoryByWeek()
        {
            List<SensorData> lst = new List<SensorData>();
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                var Result = client.Get("/Data");
                Dictionary<string, SensorData> sensorDataDict = JsonConvert.DeserializeObject<Dictionary<string, SensorData>>(Result.Body.ToString());

                if (sensorDataDict != null)
                {
                    foreach (var entry in sensorDataDict)
                    {

                        DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(entry.Value.ID).UtcDateTime;
                        ///Lấy lịch sử 7 giờ gần nhất trong ngày
                        if (dateTime.Date <= DateTime.Now.Date && dateTime.Date >= DateTime.Now.AddDays(-7))
                        {
                            SensorData _ss = new SensorData();
                            _ss.ID = entry.Value.ID;
                            _ss.UpdateTime = dateTime.AddHours(7).ToString("yyyy-MM-dd HH:mm:ss");
                            _ss.Temp1 = entry.Value.Temp1;
                            _ss.Hum1 = entry.Value.Hum1;
                            _ss.Temp2 = entry.Value.Temp2;
                            _ss.Hum2 = entry.Value.Hum2;
                            _ss.Gas = entry.Value.Gas;

                            lst.Add(_ss);

                        }
                    }
                }
            }
            return lst;
        }
    }
}
