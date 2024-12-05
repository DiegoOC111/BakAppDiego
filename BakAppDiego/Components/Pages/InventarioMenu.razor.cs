using BakAppDiego.Components.Globals.Modelos.Clases;
using BakAppDiego.Components.Globals.Statics;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Pages
{
        public partial class InventarioMenu{
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        async Task inventario()
        {
            GlobalData.InventarioActivo = null;
            NavigationManager.NavigateTo("/Inventario");

            
        }
        protected override void OnInitialized()
        {
            NavigationHistory.AddToHistory(NavigationManager.Uri);
        }

    }
}
