using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos.Responses
{
    internal class RespuestaHoja
    {
        public class Respuesta_Hoja
        {
            public bool EsCorrecto { get; set; }
            public string Error { get; set; }
            public string Version { get; set; }
            public int Id { get; set; }
            public string Numero { get; set; }
        }

        public class ls_Respuesta
        {
            public List<Respuesta_Hoja> Table { get; set; }
        }
    }
}
