using BakAppDiego.Components.Globals.Modelos;
using BakAppDiego.Components.Globals.Statics;
using BakAppDiego.Components.Modulos_de_funciones;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Pages
{
    public partial class MenuPrincipal
    {
        private NavigationManager NavigationManager { get; set; }
        private FuncionesWebService ComunicacionWB { get; set; }
        public string UsuarioActivo;
        public string Modalidad;
        protected override void OnInitialized() {

            ComunicacionWB = new FuncionesWebService(); 


        }
        
            async Task TraerEntidad()
        {
            ConectarConf conect = new ConectarConf();

           MensajeAsync ms  = await conect.Sb_Cargar_Datos_De_Configuracion();
        }


    }
}
