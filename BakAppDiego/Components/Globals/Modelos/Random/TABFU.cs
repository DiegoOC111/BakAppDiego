using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.TablasBackApp
{
    public class TABFUResponse
    {
        public List<TABFU> Table { get; set; }
    }
    public class TABFU


    {
        public int IdTabfu { get; set; } // [IDTABFU] [int] NOT NULL
        public  string Kofu { get; set; } //   NOT NULL
        public string NoKofu { get; set; } //   NOT NULL
        public string Tifu { get; set; } //   NOT NULL
        public string RtFu { get; set; } //   NOT NULL
        public string CiFu { get; set; } //   NOT NULL
        public string CmFu { get; set; } //   NOT NULL
        public string DiFu { get; set; } //   NOT NULL
        public string FoFu { get; set; } //   NOT NULL
        public string PwFu { get; set; } //   NOT NULL
        public string Plano { get; set; } //   NOT NULL
        public string KoFuAuto { get; set; } //   NOT NULL
        public string KoOp { get; set; } //   NOT NULL
        public string Tema { get; set; } //   NOT NULL
        public string Otorga { get; set; } //   NOT NULL
        public string Nudo { get; set; } //   NOT NULL
        public string Modalidad { get; set; } //   NOT NULL
        public bool Inactivo { get; set; } // [INACTIVO] [bit] NOT NULL
        public string KoTabla { get; set; } //   NOT NULL
        public string KoCarac { get; set; } //   NOT NULL
        public string Email { get; set; } //   NOT NULL
        public string? ParaFirma { get; set; } //   NULL
        public string? CodExtern { get; set; } //   NULL
        public string? PerContact { get; set; } //   NULL
        public string? EmailSup { get; set; } //   NULL
        public int? NroComu { get; set; } // [NROCOMU] [int] NULL
        public bool RegActiv { get; set; } // [REGACTIV] [bit] NOT NULL
        public bool ClaveExpi { get; set; } // [CLAVEEXPI] [bit] NOT NULL
        public int? DiasExpi { get; set; } // [DIASEXPI] [int] NULL
        public DateTime? FechaExpi { get; set; }

    }
}
