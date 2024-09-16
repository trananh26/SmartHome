using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json;
using RestSharp.Extensions;
using static iTextSharp.text.pdf.events.IndexEvents;
using System.Reflection;

namespace FireAlert
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

                        _ss.Gas1 = entry.Value.Gas1;
                        _ss.Gas2 = entry.Value.Gas2;

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
                            _ss.Gas1 = entry.Value.Gas1;
                            _ss.Gas2 = entry.Value.Gas2;

                            lst.Add(_ss);

                        }
                    }
                }
            }
            return lst;
        }

        /// <summary>
        /// Lấy trạng thái hiện tại cảu vân tay
        /// </summary>
        /// <returns></returns>
        public FireSensorData GetCurentFireSensorData()
        {
            FireSensorData _status = new FireSensorData();
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                var Result = client.Get("/FireStatus");
                Dictionary<string, FireSensorData> sensorDataDict = JsonConvert.DeserializeObject<Dictionary<string, FireSensorData>>(Result.Body.ToString());

                if (sensorDataDict != null)
                {
                    foreach (var key in sensorDataDict)
                    {
                        DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(key.Value.ID).UtcDateTime;

                        _status.ID = key.Value.ID;
                        _status.UpdateTime = dateTime.AddHours(7).ToString("yyyy-MM-dd HH:mm:ss");
                        _status.Fire1 = key.Value.Fire1;
                        _status.Fire2 = key.Value.Fire2;
                    }
                }
            }
            return _status;
        }

        /// <summary>
        /// Lưu lịch sử alert
        /// </summary>
        /// <param name="alert"></param>
        public async void SaveHistory(AlertHistory alert)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                FirebaseResponse response = await client.UpdateTaskAsync("/AlertHistory/" + DateTime.Now.ToString("ddMMyyyy"), alert);
            }
        }

        /// <summary>
        /// Lấy ra lịch sử 1  tuần gần nhất
        /// </summary>
        /// <returns></returns>
        public List<AlertHistory> GetHistoryAlertByWeek()
        {
            List<AlertHistory> lst = new List<AlertHistory>();
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                var Result = client.Get("/AlertHistory");
                Dictionary<string, AlertHistory> sensorDataDict = JsonConvert.DeserializeObject<Dictionary<string, AlertHistory>>(Result.Body.ToString());

                if (sensorDataDict != null)
                {
                    foreach (var entry in sensorDataDict)
                    {

                        DateTime dateTime = DateTime.Parse(entry.Value.UpdateTime);
                        ///Lấy lịch sử 7 giờ gần nhất trong ngày
                        if (dateTime.Date <= DateTime.Now.Date && dateTime.Date >= DateTime.Now.AddDays(-7))
                        {
                            AlertHistory _ss = new AlertHistory();
                            _ss.UpdateTime = dateTime.ToString();
                            _ss.Messager = entry.Value.Messager;
                            _ss.UpdateBy = entry.Value.UpdateBy;

                            lst.Add(_ss);

                        }
                    }
                }
            }
            return lst;
        }
        ///-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Lấy danh sách vân tay
        /// </summary>
        /// <returns></returns>
        public List<FingerData> GetFingerList()
        {
            List<FingerData> lst = new List<FingerData>();
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                var Result = client.Get("/Finger");
                Dictionary<string, FingerData> sensorDataDict = JsonConvert.DeserializeObject<Dictionary<string, FingerData>>(Result.Body.ToString());

                if (sensorDataDict != null)
                {
                    foreach (var entry in sensorDataDict)
                    {
                        FingerData _ss = new FingerData();
                        _ss.UpdateTime = entry.Value.UpdateTime;
                        _ss.FingerID = entry.Value.FingerID;
                        _ss.UserName = entry.Value.UserName;
                        _ss.FullName = entry.Value.FullName;
                        _ss.Job = entry.Value.Job;

                        lst.Add(_ss);
                    }
                }
            }
            return lst;
        }

        /// <summary>
        /// Thêm người dùng
        /// </summary>
        /// <param name="user"></param>
        public async void AddNewUser(FingerData user)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                FirebaseResponse response = client.Set("Finger/" + user.UserName + user.FingerID.ToString(), user);
            }
        }

        /// <summary>
        /// Set trạng thái vân tay
        /// </summary>
        /// <param name="Status"></param>
        public async void SetFingerStatus(FingerStatus Status)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                FirebaseResponse response = await client.UpdateTaskAsync("/FingerStatus", Status);
            }
        }

        public async void SaveInOutHistory(FingerData finger)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                FirebaseResponse response = await client.UpdateTaskAsync("/InOutHistory", finger);
            }
        }
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
        public async void SetEqiupmentState(Eqiupment eqiupment)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                FirebaseResponse response = await client.UpdateTaskAsync("/Control", eqiupment);
            }
        }

    }
}
