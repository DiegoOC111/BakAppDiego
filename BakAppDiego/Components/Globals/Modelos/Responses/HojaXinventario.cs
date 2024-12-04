using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos.Responses
{
    public class Zw_Inv_Hoja
    {
        public int Id { get; set; }
        public int IdInventario { get; set; }
        public string Nro_Hoja { get; set; }
        public string NombreEquipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CodResponsable { get; set; }
        public int IdContador1 { get; set; }
        public int IdContador2 { get; set; }
        public DateTime? FechaLevantamiento { get; set; }
        public bool Reconteo { get; set; }
    }

    public class Ls_Zw_Inv_Hoja
    {
        
        public List<Zw_Inv_Hoja> Table { get; set; }
    }
}
