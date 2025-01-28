using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos.Random
{
    public class MAEMO
    {
        public int IDMAEMO { get; set; }
        public string KOMO { get; set; }
        public string TIMO { get; set; }
        public string NOKOMO { get; set; }
        public double VAMO { get; set; }
        public DateTime FEMO { get; set; }
        public double VAMOCOM { get; set; }
    }
    public class MAEOMResponse
    {
        public List<MAEMO> Table { get; set; }
    }
}
