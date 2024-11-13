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

            //ConectarConf conect = new ConectarConf();

            //MensajeAsync ms  = await conect.Sb_Cargar_Datos_De_Configuracion();
            //if (ms.EsCorrecto)
            //{

            //    Console.WriteLine(ms.Msg);

            //}
            //else { 

            //    Console.WriteLine(ms.Msg);


            //}
        }

    }
}
