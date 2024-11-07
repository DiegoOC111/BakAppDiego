using BakAppDiego.Components.Globals.TablasBackApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using System.Text.Json;
using BakAppDiego.Components.Globals.Modelos.Bakapp;
using BakAppDiego.Components.Globals.Modelos.Responses;
using BakAppDiego.Components.Globals.Modelos.Clases;
using BakAppDiego.Components.Globals.Modelos.Bakapp;
using BakAppDiego.Components.Globals.Modelos.Clases;
using BakAppDiego.Components.Globals.Modelos.Random;
using BakAppDiego.Components.Pages;
using BakAppDiego.Components.Globals.Modelos;

namespace BakAppDiego.Components.Globals.Statics
{
    public static class GlobalData
    {
        public static bool volver { get; set; }
        public static string Ip_Wb { get; set; }
        public static string Id_dispositivo { get; set; }
        public static string Global_BaseBk { get; set; }
        public static TABFU? Usuario_Activo { get; set;}

        public static Zw_EstacionesBkp? EstacionBk { get; set; }
        public static List<Precios> Listas_precios_usuarios { get; set; }
        public static ConfigEstacion ConfiguracionEstacion { get; set; }
        public static Zw_EstacionesBkp Configuracion_General { get; set; }
        public static TABMO Moneda { get; set; }
        public static MAEMO MonedaDolar { get; set; }
        public static MAEMO UF { get; set; }
        public static bool ExisteTabla_MS_GATEWAY_STOCK  { get; set; }
        public static Zw_TablaDeCaracterizaciones TablaDeCaracterizacionesTipo { get; set; }
        public static Zw_TablaDeCaracterizaciones TablaDeCaracterizacionesTipoPago { get; set; }
        public static Zw_TablaDeCaracterizaciones DocDestino { get; set; }

        public static InventarioItem InventarioActivo { get; set; }
        public static void  GuardarIP() {
            
            // Guardar el JSON en Preferences
            Preferences.Set("Ip", Ip_Wb);
            


        } 
        public static void Cargar()
        {
            // Obtener el JSON de Preferences
            Ip_Wb = Preferences.Get("Ip", null);
            volver = false;


        }

    }

}
