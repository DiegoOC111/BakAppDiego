using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos
{
    public class InventarioData
    {
        public List<InventarioItem> Table { get; set; }
    }

    public class InventarioItem
    {
        public int Id { get; set; }
        public string Ano { get; set; }
        public string Mes { get; set; }
        public string Dia { get; set; }
        public DateTime Fecha_Inventario { get; set; }
        public string Empresa { get; set; }
        public string Sucursal { get; set; }
        public string Bodega { get; set; }
        public string Nombre_Empresa { get; set; }
        public string Nombre_Sucursal { get; set; }
        public string Nombre_Bodega { get; set; }
        public string NombreInventario { get; set; }
        public string FuncionarioCargo { get; set; }
        public string NombreFuncionario { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaCierre { get; set; }
    }
}
