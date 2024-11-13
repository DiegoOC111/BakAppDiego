using BakAppDiego.Components.Dialogs;
using BakAppDiego.Components.Globals.Modelos;
using BakAppDiego.Components.Globals.Statics;
using BakAppDiego.Components.Modulos_de_funciones;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
#if ANDROID
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using AndroidX.Activity;


#endif


namespace BakAppDiego.Components.Pages
{
    public partial class MenuPrincipal

    {
        private DialogoService Dialogo;
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private EditContext? editContext;
        private bool changed = false;
        private FuncionesWebService ComunicacionWB { get; set; }
        public string UsuarioActivo;
        public string Modalidad;
        private IDisposable registration;
       
        
        public void Dispose()
        {
            registration?.Dispose();
        }
        protected override void OnInitialized() {
            GlobalData.InventarioActivo = null;
            ComunicacionWB = new FuncionesWebService();
            UsuarioActivo = GlobalData.Usuario_Activo.Kofu + "-" + GlobalData.Usuario_Activo.NoKofu;
            Modalidad = GlobalData.Usuario_Activo.Modalidad;
            Dialogo = new DialogoService();
            // Desactivar el botón de "volver atrás"

        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                
            }
        }

        async Task TraerEntidad()
        {
            NavigationManager.NavigateTo("/LoginDatos");

            //ConectarConf conect = new ConectarConf();

            //MensajeAsync ms  = await conect.Sb_Cargar_Datos_De_Configuracion();
            //if (ms.EsCorrecto)
            //{

            //    Console.WriteLine(ms.Msg);

            //}
            //else { 

            //    Console.WriteLine(ms.Msg);


            //}
        }async Task MenuInventario()
        {
            NavigationManager.NavigateTo("/InventarioMenu");

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
