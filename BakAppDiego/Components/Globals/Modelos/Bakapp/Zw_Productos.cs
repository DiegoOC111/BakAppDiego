using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos.Bakapp
{
    public class ls_Zw_Producto

    {
        public List<Zw_Producto> Table { get; set; }
    }

    public class Zw_Producto
    {
        public string Principal { get; set; }

        public string Rapido { get; set; }

        public string Tecnico { get; set; }
        public double Rtu { get; set; }

        public string Ud1 { get; set; }

        public string Ud2 { get; set; }

        public string Descripcion { get; set; }

        public double StFisicoUd1 { get; set; }

        public double StFisicoUd2 { get; set; }

        public string SuperFamilia { get; set; }

        public string NombreSuper { get; set; }

        public string Familia { get; set; }

        public string NombreFamilia { get; set; }

      
        public string SubFamilia { get; set; }

     
        public string NombreSub { get; set; }

        public string MRPR { get; set; }

        public string Marca { get; set; }

        public double PrecioListaUd1 { get; set; }

        public double PrecioListaUd2 { get; set; }
    }
}
