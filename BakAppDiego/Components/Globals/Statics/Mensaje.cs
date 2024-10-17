using BakAppDiego.Components.Globals.TablasBackApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace BakAppDiego.Components.Globals.Statics
{
    public static class Mensaje
    {
        public static bool EsCorrecto { get; set; }
        public static string Id { get; set; }
        // public DateTime Fecha { get; set; } // Descomentar si es necesario
        public static string Detalle { get; set; }
        public static string Msg { get; set; }
        public static string Resultado { get; set; }
        public static object Tag { get; set; }

        public static string NombreImagen { get; set; }
        public static object Icono { get; set; }
        public static bool Cancelado { get; set; }
        public static bool MostrarMensaje { get; set; } = true;
        public static bool Cerrar { get; set; }
        public static bool ErrorDeConexionSQL { get; set; }
        public static bool ErrorDeCompilacion { get; set; }



    }
}