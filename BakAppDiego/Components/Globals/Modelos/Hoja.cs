using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos
{
    public class Hoja
    {
        // Propiedades de la clase
        public int Id { get; private set; }
        public int IdInventario { get; private set; }
        public string Nro_Hoja { get; private set; }
        public string NombreEquipo { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public string CodResponsable { get; private set; }
        public int IdContador1 { get; private set; }
        public int IdContador2 { get; private set; }
        public DateTime FechaLevantamiento { get; private set; }
        public bool Reconteo { get; private set; }
        public int CantItems { get; private set; }

        // Constructor para inicializar la clase
        public Hoja(int id, int idInventario, string nro_Hoja, string nombreEquipo, DateTime fechaCreacion,
                          string codResponsable, int idContador1, int idContador2, DateTime fechaLevantamiento, bool reconteo, int cantidad)
        {
            Id = id;
            IdInventario = idInventario;
            Nro_Hoja = nro_Hoja;
            NombreEquipo = nombreEquipo;
            FechaCreacion = fechaCreacion;
            CodResponsable = codResponsable;
            IdContador1 = idContador1;
            IdContador2 = idContador2;
            FechaLevantamiento = fechaLevantamiento;
            Reconteo = reconteo;
            CantItems = cantidad;
        }
        public Dictionary<string, object> ToMap()
        {
            var mapAux = new Dictionary<string, object>
        {
            { "Id", Id },
            { "IdInventario", IdInventario },
            { "Nro_Hoja", Nro_Hoja },
            { "NombreEquipo", NombreEquipo },
            { "FechaCreacion", FechaCreacion },
            { "CodResponsable", CodResponsable },
            { "IdContador1", IdContador1 },
            { "IdContador2", IdContador2 },
            { "FechaLevantamiento", FechaLevantamiento },
            { "Reconteo", Reconteo }
        };

            return mapAux;
        }

        // Método para convertir la instancia a JSON
        public string ToJson()
        {
            var mapAux = ToMap();
            string jsonString = JsonConvert.SerializeObject(mapAux, Formatting.Indented);
            Console.WriteLine(jsonString);
            return jsonString;
        }


    }

    public class HojaDetalle
    {
        // Propiedades de la clase
        public int Id { get; private set; }
        public int IdHoja { get; private set; }
        public string Nro_Hoja { get; private set; }
        public int IdInventario { get; private set; }
        public string Empresa { get; private set; }
        public string Sucursal { get; private set; }
        public string Bodega { get; private set; }
        public string Responsable { get; private set; }
        public int IdContador1 { get; private set; }
        public int IdContador2 { get; private set; }
        public string Item_Hoja { get; private set; }
        public int IdSector { get; private set; }
        public string Sector { get; private set; }
        public string Ubicacion { get; private set; }
        public string TipoConteo { get; private set; }
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }
        public int EsSeriado { get; private set; }
        public string NroSerie { get; private set; }
        public DateTime FechaHoraToma { get; private set; }
        public double Rtu { get; private set; }
        public int RtuVariable { get; private set; }
        public int Udtrpr { get; private set; }
        public double Cantidad { get; private set; }
        public string Ud1 { get; private set; }
        public double CantidadUd1 { get; private set; }
        public string Ud2 { get; private set; }
        public double CantidadUd2 { get; private set; }
        public string Observaciones { get; private set; }
        public int Recontado { get; private set; }
        public string Actualizado_por { get; private set; }
        public string Obs_Actualizacion { get; private set; }

        // Constructor para inicializar las propiedades
        public HojaDetalle(int id, int idHoja, string nro_Hoja, int idInventario, string empresa, string sucursal,
                           string bodega, string responsable, int idContador1, int idContador2, string item_Hoja, int idSector,
                           string sector, string ubicacion, string tipoConteo, string codigo, string descripcion, int esSeriado,
                           string nroSerie, DateTime fechaHoraToma, double rtu, int rtuVariable, int udtrpr, double cantidad,
                           string ud1, double cantidadUd1, string ud2, double cantidadUd2, string observaciones, int recontado,
                           string actualizado_por, string obs_Actualizacion)
        {
            Id = id;
            IdHoja = idHoja;
            Nro_Hoja = nro_Hoja;
            IdInventario = idInventario;
            Empresa = empresa;
            Sucursal = sucursal;
            Bodega = bodega;
            Responsable = responsable;
            IdContador1 = idContador1;
            IdContador2 = idContador2;
            Item_Hoja = item_Hoja;
            IdSector = idSector;
            Sector = sector;
            Ubicacion = ubicacion;
            TipoConteo = tipoConteo;
            Codigo = codigo;
            Descripcion = descripcion;
            EsSeriado = esSeriado;
            NroSerie = nroSerie;
            FechaHoraToma = fechaHoraToma;
            Rtu = rtu;
            RtuVariable = rtuVariable;
            Udtrpr = udtrpr;
            Cantidad = cantidad;
            Ud1 = ud1;
            CantidadUd1 = cantidadUd1;
            Ud2 = ud2;
            CantidadUd2 = cantidadUd2;
            Observaciones = observaciones;
            Recontado = recontado;
            Actualizado_por = actualizado_por;
            Obs_Actualizacion = obs_Actualizacion;
        }
        public Dictionary<string, object> ToMap()
        {
            var datos = new Dictionary<string, object>
    {
        { "Id", Id },
        { "IdHoja", IdHoja },
        { "Nro_Hoja", Nro_Hoja },
        { "IdInventario", IdInventario },
        { "Empresa", Empresa },
        { "Sucursal", Sucursal },
        { "Bodega", Bodega },
        { "Responsable", Responsable },
        { "IdContador1", IdContador1 },
        { "IdContador2", IdContador2 },
        { "Item_Hoja", Item_Hoja },
        { "IdSector", IdSector },
        { "Sector", Sector },
        { "Ubicacion", Ubicacion },
        { "TipoConteo", TipoConteo },
        { "Codigo", Codigo },
        { "Descripcion", Descripcion },
        { "EsSeriado", EsSeriado },
        { "NroSerie", NroSerie },
        { "FechaHoraToma", FechaHoraToma },
        { "Rtu", Rtu },
        { "RtuVariable", RtuVariable },
        { "Udtrpr", Udtrpr },
        { "Cantidad", Cantidad },
        { "Ud1", Ud1 },
        { "CantidadUd1", CantidadUd1 },
        { "Ud2", Ud2 },
        { "CantidadUd2", CantidadUd2 },
        { "Observaciones", Observaciones },
        { "Recontado", Recontado },
        { "Actualizado_por", Actualizado_por },
        { "Obs_Actualizacion", Obs_Actualizacion }
    };

            return datos;
        }
    }
}
