using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos.Clases
{
    public class ResponseConifgEstacion
    {
        public List<ConfigEstacion> Table { get; set; }
    }
    public class ConfigEstacion
    {
        public string EMPRESA { get; set; }
        public string MODALIDAD { get; set; }
        public string ESUCURSAL { get; set; }
        public string EBODEGA { get; set; }
        public string ECAJA { get; set; }
        public string ELISTAVEN { get; set; }
        public string NLISTAVEN { get; set; }
        public string ELISTACOM { get; set; }
        public string NLISTACOM { get; set; }
        public string ELISTAINT { get; set; }
        public string NLISTAINT { get; set; }
        public string EPARALELA { get; set; }
        public string NPARALELA { get; set; }
        public string ESERIAL { get; set; }
        public string NSERIAL { get; set; }
        public string EDESFACV { get; set; }
        public string EPAGOSV { get; set; }
        public double CRTO { get; set; }
        public double CRSD { get; set; }
        public double CRCH { get; set; }
        public double CRLT { get; set; }
        public double CRPA { get; set; }
        public double POPICR { get; set; }
        public double NUVECR { get; set; }
        public DateTime? FEVECR { get; set; }
        public DateTime? FMINS { get; set; }
        public DateTime? FMAXS { get; set; }
        public DateTime? FMINV { get; set; }
        public DateTime? FMAXV { get; set; }
        public DateTime? FMINA { get; set; }
        public DateTime? FMAXA { get; set; }
        public DateTime? FMINC { get; set; }
        public DateTime? FMAXC { get; set; }
        public string DRIVE { get; set; }
        public string STOCK { get; set; }
        public string NVI { get; set; }
        public string NVILSR { get; set; }
        public string NVV { get; set; }
        public string NVVLSR { get; set; }
        public bool CAMBIARESP { get; set; }
        public bool NOEVALIST { get; set; }
        public bool VERCONSOL { get; set; }
        public bool SOLOVERSUC { get; set; }
        public bool Pr_AutoPr_Crear_Codigo_Principal_Automatico { get; set; }
        public bool Pr_AutoPr_Correlativo_Por_Iniciales { get; set; }
        public bool Pr_AutoPr_Correlativo_General { get; set; }
        public string DetalleDoc_Acepta_Stock_Negativo { get; set; }
        public string DetalleDoc_Acepta_Stock_Negativo_SubStock { get; set; }
        public string DetalleDoc_Acepta_Stock_Negativo_Otra_Bod { get; set; }
        public bool DetalleDoc_Alerta_Stock_Negativo { get; set; }
        public bool DetalleDoc_Alerta_Stock_Negativo_SubStock { get; set; }
        public bool DetalleDoc_Acepta_Stock_Negativo_OtroDoc { get; set; }
        public bool DetalleDoc_Controla_Stock_Consolidado { get; set; }
        public bool DetalleDoc_Controla_Stock_Consolidado_SubStock { get; set; }
        public bool DetalleDoc_Controla_Stock_Solo_Verifica_Bod_Empresa { get; set; }
        public bool Pr_AutoPr_Incluir_En_Lista_Vta { get; set; }
        public bool Pr_AutoPr_Aceptar_Stock_Negativo { get; set; }
        public bool Pr_AutoPr_Controla_Stock { get; set; }
        public bool Pr_AutoPr_Aceptar_Stock_Negativo_SubStock { get; set; }
        public bool Pr_AutoPr_Controla_Stock_SubStock { get; set; }
        public bool Pr_AutoPr_Controla_Stock_Otra_Bod { get; set; }
        public bool Pr_AutoPr_Controla_Stock_OtroDoc { get; set; }
        public bool Pr_AutoPr_Controla_Stock_Solo_Verifica_Bod_Empresa { get; set; }
    }
}
