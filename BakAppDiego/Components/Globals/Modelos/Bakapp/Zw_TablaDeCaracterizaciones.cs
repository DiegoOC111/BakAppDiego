using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos.Bakapp
{
    public class Zw_TablaDeCaracterizaciones
    {
        public int Id { get; set; }
        public string Tabla { get; set; }
        public string DescripcionTabla { get; set; }
        public string CodigoTabla { get; set; }
        public string NombreTabla { get; set; }
        public int Orden { get; set; }
        public bool ApColor { get; set; }
        public bool ApModelo { get; set; }
        public bool ApMedida { get; set; }
        public double Porcentaje { get; set; }
        public double Valor { get; set; }
        public string Padre_Tabla { get; set; }
        public string Padre_CodigoTabla { get; set; }
        public DateTime? Fecha { get; set; }
        public string Equiv_Kotabla { get; set; }
        public string Equiv_Kocarac { get; set; }
        public string Emp { get; set; }
        public string Suc { get; set; }
        public string Bod { get; set; }
    }
}
