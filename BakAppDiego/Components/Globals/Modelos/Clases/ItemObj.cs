using BakAppDiego.Components.Globals.Modelos.Bakapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos.Clases
{
    public class ItemObj
    {
        public Zw_Producto producto  { get; set; }
        public string comentario = null;
        public int cantidad = 0;
        public string tipocodigo = null;
        public string codigo = null;
    }
}
