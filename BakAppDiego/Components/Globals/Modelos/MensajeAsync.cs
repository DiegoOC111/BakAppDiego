using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos
{
    public class MensajeAsync
    {
        
            public bool EsCorrecto { get; set; }
            public string Id { get; set; }
            public string Detalle { get; set; }
            public string Msg { get; set; }
            public string Resultado { get; set; }
            public object Tag { get; set; }
            public string NombreImagen { get; set; }
            public object Icono { get; set; }
            public bool Cancelado { get; set; }
            public bool MostrarMensaje { get; set; } = true;
            public bool Cerrar { get; set; }
            public bool ErrorDeConexionSQL { get; set; }
            public bool ErrorDeCompilacion { get; set; }

            public void SetMensaje(
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



