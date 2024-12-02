using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Dialogs
{
    internal class DialogoService : DialogInterface
    {
        public async Task<bool> DisplayConfirm(string titulo, string mensaje, string aceptar, string cerrar)
        {
            if (Application.Current?.MainPage != null)
            {
                return await Application.Current.MainPage.DisplayAlert(title: titulo, message: mensaje, aceptar, cancel: cerrar);
            }

            return false;
        }
        public async Task<string> DisplayActionSheet(string titulo, string aceptar, string cerrar, string[] botones)
        {
            if (Application.Current?.MainPage != null)
            {
                return await Application.Current.MainPage.DisplayActionSheet(title: titulo, cancel: cerrar, destruction: aceptar, buttons: botones);
            }

            return string.Empty;
        }
        public async Task<string> DisplayText(string titulo, string mensaje, string aceptar, string cerrar)
        {
            if (Application.Current?.MainPage != null)
            {
                return await Application.Current.MainPage.DisplayPromptAsync(title: titulo, message: mensaje, aceptar, cancel: cerrar);
            }

            return string.Empty;

        }
    }
}
