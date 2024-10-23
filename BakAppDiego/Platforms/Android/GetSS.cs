//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;
//using AndroidApp = Android.App.Application;
//using Setting = Android.Provider.Settings;
//using static Android.Provider.Settings;

//namespace BakAppDiego.Components.Interface
//{
//    public class GetDevice
//    {
//        public  string GET()
//        {
//            var context = AndroidApp.Context;

//            string id = Setting.Secure.GetString(context.ContentResolver, Secure.AndroidId);

//            return id;
//        }
//    }
//}
