using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos.Bakapp
{
    public class Contador
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
    }

    public class ContadorResponse
    {
        public List<Contador> Table { get; set; }
    }

}
