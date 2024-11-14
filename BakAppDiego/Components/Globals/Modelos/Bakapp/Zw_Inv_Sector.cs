using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos.Bakapp
{
    public class Zw_Inv_Sector
    {
        public int Id { get; set; }
        public int IdInventario { get; set; }
        public string Empresa { get; set; }
        public string Sucursal { get; set; }
        public string Bodega { get; set; }
        public string Sector { get; set; }
        public string NombreSector { get; set; }
        public string CodFuncionario { get; set; }
        public bool Abierto { get; set; }
    }

    public class ls_Zw_Inv_Sector
    {
        public List<Zw_Inv_Sector> Table { get; set; }
    }

   
}
