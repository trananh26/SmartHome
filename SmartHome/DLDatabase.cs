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

    }
}
