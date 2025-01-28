using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;
using BakAppDiego.Components.Globals.Statics;

namespace BakAppDiego
{
    public partial class MainPage : ContentPage
    {
        private bool lohago = true;
        public MainPage()
        {
            InitializeComponent();

        }

        //protected override bool OnBackButtonPressed()
        //{
        //    if (GlobalData.Volver)
        //    {
        //        MainThread.BeginInvokeOnMainThread(async () =>
        //        {
        //            bool answer = await Shell.Current.DisplayAlert("Confirmación", "¿Deseas salir?", "Sí", "No");
        //            if (answer)
        //            {
        //                await Shell.Current.GoToAsync(".."); // Navegar hacia atrás
        //            }
        //        });
        //        return true; // Bloquear el comportamiento predeterminado
        //    }
        //    return base.OnBackButtonPressed();
        //}
        //protected override bool OnBackButtonPressed()
        //{


        //    // Return true to prevent back button 
        //    return true;
        //}
        //protected override bool OnBackButtonPressed()
        //{
        //    // Llamar a la confirmación de navegación en un hilo separado
        //    ConfirmBackNavigation();
        //    return lohago;
        //}

        //private async void ConfirmBackNavigation()
        //{
        //    bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de que quieres volver?", "Sí", "No");
        //    lohago = answer;

        //}
        //protected override async Task<bool> OnBackButtonPressed()
        //{
        //    // Mostrar mensaje de confirmación
        //    bool shouldGoBack = await ConfirmBackNavigation();

        //    if (shouldGoBack)
        //    {
        //        // Si el usuario acepta, permitimos la navegación hacia atrás
        //        return true;


        //    }
        //    return false;
        //}

        //private async Task<bool> ConfirmBackNavigation()
        //{
        //    bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de que quieres volver?", "Sí", "No");
        //    return answer; // Devuelve true si el usuario acepta, false si no.
        //}
        //protected override bool OnBackButtonPressed()
        //{
        //    // Mostrar mensaje de confirmación
        //    Task<bool> a =  ConfirmBackNavigation();

        //    return a.Result; // Impedimos la acción de retroceso automática
        //}

        //private async Task<bool> ConfirmBackNavigation()
        //{
        //    bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de que quieres volver?", "Sí", "No");
        //    if (answer)
        //    {
        //        // Si el usuario acepta, entonces navegamos hacia atrás
        //        return true;
        //    }
        //    return true;

        //}
    }
}