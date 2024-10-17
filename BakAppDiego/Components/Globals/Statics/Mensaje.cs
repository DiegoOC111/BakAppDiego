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
        public static void SetMensaje(
    bool? esCorrecto = null,
    string id = null,
    string detalle = null,
    string msg = null,
    string resultado = null,
    object tag = null,
    string nombreImagen = null,
    object icono = null,
    bool? cancelado = null,
    bool? mostrarMensaje = null,
    bool? cerrar = null,
    bool? errorDeConexionSQL = null,
    bool? errorDeCompilacion = null)
        {
            if (esCorrecto.HasValue) EsCorrecto = esCorrecto.Value;
            if (id != null) Id = id;
            if (detalle != null) Detalle = detalle;
            if (msg != null) Msg = msg;
            if (resultado != null) Resultado = resultado;
            if (tag != null) Tag = tag;
            if (nombreImagen != null) NombreImagen = nombreImagen;
            if (icono != null) Icono = icono;
            if (cancelado.HasValue) Cancelado = cancelado.Value;
            if (mostrarMensaje.HasValue) MostrarMensaje = mostrarMensaje.Value;
            if (cerrar.HasValue) Cerrar = cerrar.Value;
            if (errorDeConexionSQL.HasValue) ErrorDeConexionSQL = errorDeConexionSQL.Value;
            if (errorDeCompilacion.HasValue) ErrorDeCompilacion = errorDeCompilacion.Value;
        }

    }


}
