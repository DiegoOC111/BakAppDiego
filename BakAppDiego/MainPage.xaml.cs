using Microsoft.Maui.Controls;

namespace BakAppDiego
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            // Mostrar mensaje de confirmación
            ConfirmBackNavigation();
            return true; // Impedimos la acción de retroceso automática
        }

        private async void ConfirmBackNavigation()
        {
            bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de que quieres volver?", "Sí", "No");
            if (answer)
            {
                // Si el usuario acepta, entonces navegamos hacia atrás
                await Navigation.PopAsync();
            }
        }
    }
}