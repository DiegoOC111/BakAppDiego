//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;



//using Android.App;
//using Android.Widget;
//using AndroidX.Activity;
//using BakAppDiego.Components.Interface;
//using System;

//[assembly: Dependency(typeof(BackButtonHandler))] // Asegura que esté accesible en DependencyService

//public class BackButtonHandler : IBackButtonHandler
//{
//    private BackPress? backPressHandler;

//    public void RegisterBackPressHandler(bool canGoBack)
//    {
//        var activity = Platform.CurrentActivity;
//        backPressHandler = new BackPress(activity)
//        {
//            CanGoBack = canGoBack
//        };
//        object value = activity.OnBackPressedDispatcher.AddCallback(activity, backPressHandler);
//    }

//    public void UnregisterBackPressHandler()
//    {
//        backPressHandler?.Remove(); // Elimina el manejador de "volver atrás"
//        backPressHandler = null;
//    }
//}