using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos.Bakapp
{
    public class Inv_Sector
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

    public class Inv_SectorResponse
    {
        public List<Inv_Sector> Table { get; set; }
    }

    public class Zw_EstacionesBkp
    {
        public int Id { get; set; }
        public string IpEstacion { get; set; }
        public string NombreEquipo { get; set; }
        public string TipoEstacion { get; set; }
        public string IpRandom { get; set; }
        public string KeyReg { get; set; }
        public bool Conectado { get; set; }
        public DateTime? Fecha_Hora_Conec { get; set; }
        public string CodUsuario { get; set; }
        public string NomUsuario { get; set; }
        public string Version { get; set; }
        public string Directorio_GenDTE { get; set; }
        public string Usuario_X_Defecto { get; set; }
        public string Modalidad_X_Defecto { get; set; }
        public bool Mos_Notif_X_CdPermiso { get; set; }
        public string Alias { get; set; }
        public string Empresa_X_Defecto { get; set; }
        public bool Usar_Datos_X_Defecto { get; set; }
        public string Usuario_Actual { get; set; }
        public string Modalidad_Actual { get; set; }
        public bool Buscar_Actualizacion_En_FTP { get; set; }
        public bool Silenciar_Notificaciones { get; set; }
        public bool Es_Diablito { get; set; }
        public bool Tiene_Lector_Huella { get; set; }
        public string Lector_Huella { get; set; }
        public string Modalidad_Caja { get; set; }
        public bool Caja_Habilitada { get; set; }
        public bool ImprDespGrabarCaja { get; set; }
        public string Empresa_Actual { get; set; }
        public bool EsDTEMonitor { get; set; }
        public bool DTEMonitorAmbienteCertificacion { get; set; }
    }
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
