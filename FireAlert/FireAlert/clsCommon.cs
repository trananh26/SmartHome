﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireAlert
{
    public class clsCommon
    {
    }

    /// <summary>
    /// dữ liệu điều khiển thiết bị
    /// </summary>
    public class Eqiupment
    {
        [JsonProperty("Door2")]    //Cửa ra vào
        public int Door2 { get; set; }

        [JsonProperty("Fan2")]    //Quạt phòng khách
        public int Fan2 { get; set; }

        [JsonProperty("Door3")]    //Cửa ra vào
        public int Door3 { get; set; }

        [JsonProperty("Fan3")]    //Quạt phòng ngủ
        public int Fan3 { get; set; }

    }

    /// <summary>
    /// dữ liệu quản lý vân tay
    /// </summary>
    public class FingerData
    {
        [JsonProperty("UpdateTime")]    //thời  gian cập nhật
        public DateTime UpdateTime { get; set; }

        [JsonProperty("FingerID")]   //Nhiệt độ phòng 1
        public int FingerID { get; set; }

        [JsonProperty("UserName")]   //Nhiệt độ phòng 1
        public string UserName { get; set; }

        [JsonProperty("FullName")]   //Nhiệt độ phòng 1
        public string FullName { get; set; }

        [JsonProperty("Job")]   //Nhiệt độ phòng 1
        public string Job { get; set; }
    }

    /// <summary>
    /// dữ liệu thông số môi trường các phòng
    /// </summary>
    public class SensorData
    {
        [JsonProperty("epoch_time")]    //thời  gian cập nhật
        public long ID { get; set; }

        public string UpdateTime { get; set; }

        [JsonProperty("gas1")]    //Nồng độ khí gas
        public string Gas1 { get; set; }

        [JsonProperty("gas2")]    //Nồng độ khí gas
        public string Gas2 { get; set; }

        [JsonProperty("gas3")]    //Nồng độ khí gas
        public string Gas3 { get; set; }

    }

    /// <summary>
    /// dữ liệu báo cháy
    /// </summary>
    public class FireSensorData
    {
             
        [JsonProperty("fire1")]    //Nồng độ khí gas
        public int Fire1 { get; set; }

        [JsonProperty("fire2")]    //Nồng độ khí gas
        public int Fire2 { get; set; }
        
        [JsonProperty("fire3")]    //Nồng độ khí gas
        public int Fire3 { get; set; }

        
    }

    /// <summary>
    /// lịch sử cảnh báo
    /// </summary>
    public class AlertHistory
    {
        [JsonProperty("UpdateTime")]    //thời  gian cập nhậ
        public string UpdateTime { get; set; }

        [JsonProperty("Messager")]    //Nồng độ khí gas
        public string Messager { get; set; }

        [JsonProperty("UpdateBy")]    //Nồng độ khí gas
        public string UpdateBy { get; set; }

    }


    public class FingerStatus
    {
      
        [JsonProperty("Status")]
        public string Status { get; set; }
    }
}
