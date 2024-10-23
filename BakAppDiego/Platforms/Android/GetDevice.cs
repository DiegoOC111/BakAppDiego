//using AndroidApp = Android.App.Application;
//using Setting = Android.Provider.Settings;
//using static Android.Provider.Settings;

//namespace BakAppDiego.Components.Interface
//{
//    public partial class GetDevice
//    {
//        public  partial string GetDeviceId() // Implementación pública
//        {
//            var context = AndroidApp.Context;

//            string id = Setting.Secure.GetString(context.ContentResolver, Secure.AndroidId);

//            return id;
//        }
//    }
//}