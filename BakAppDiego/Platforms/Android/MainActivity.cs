using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using AndroidX.Activity;
using BakAppDiego.Components.Globals.Statics;

namespace BakAppDiego
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {

        public override void OnBackPressed()
        {
            // Comprobamos si la variable global indica que se debe mostrar la confirmación
            if (GlobalData.volver)
            {
                // Mostrar un mensaje de confirmación si la variable es true
                var result = ShowConfirmationDialog();

                if (result) // Si el usuario acepta, se permite el retroceso
                {
                    base.OnBackPressed();
                }
                else
                {
                    // Si el usuario cancela, no hacer nada (mantenerse en la misma página)
                }
            }
            else
            {
                // Si la variable es false, no mostramos el diálogo y procedemos normalmente
                base.OnBackPressed();
            }
        }

        // Método para mostrar el diálogo de confirmación
        private bool ShowConfirmationDialog()
        {
            var confirmation = new AlertDialog.Builder(this);
            confirmation.SetMessage("¿Seguro que deseas salir?");
            confirmation.SetPositiveButton("Sí", (sender, e) => { });
            confirmation.SetNegativeButton("No", (sender, e) => { });

            var dialog = confirmation.Create();
            dialog.Show();

            // Este valor debe basarse en la respuesta del usuario.
            // Lo simulo con un valor temporal para el ejemplo.
            return true; // Cambiar según lo que el usuario seleccione en el diálogo
        }
        //public override void OnBackPressed()
        //{
        //    // Aquí manejas la acción personalizada para el botón "Volver" en toda la app
        //    // Ejemplo: Mostrar un mensaje de confirmación o bloquear el botón
        //    bool cancelar = true; // Cambia a 'false' si quieres permitir volver

        //    if (cancelar)
        //    {
        //        Android.Widget.Toast.MakeText(this, "No puedes volver en este momento", Android.Widget.ToastLength.Short).Show();
        //        // Evita el volver presionando el botón de retroceso
        //        return;
        //    }

        //    base.OnBackPressed(); // Permite la acción de volver
        //}
        //private BackPress backPressHandler;
        //private bool backPressEnabled = false;

        //protected override void OnCreate(Bundle? savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);
        //    OnBackPressedDispatcher.AddCallback(this, new BackPress(this));
        //}

    }
    
    

}
//class BackPress : OnBackPressedCallback
//{
//    private readonly Activity activity;
//    private long backPressed;

//    public BackPress(Activity activity) : base(true)
//    {
//        this.activity = activity;
//    }

//    public override void HandleOnBackPressed()
//    {
//        var navigation = Microsoft.Maui.Controls.Application.Current?.MainPage?.Navigation;
//        if (navigation is not null && navigation.NavigationStack.Count <= 1 && navigation.ModalStack.Count <= 0)
//        {
//            const int delay = 2000;
//            if (backPressed + delay > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
//            {
//                activity.FinishAndRemoveTask();
//                Process.KillProcess(Process.MyPid());
//            }
//            else
//            {
//                Toast.MakeText(activity, "Close", ToastLength.Long)?.Show();
//                backPressed = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
//            }
//        }
//    }
//}