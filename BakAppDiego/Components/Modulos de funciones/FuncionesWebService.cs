using BakAppDiego.Components.Globals.Statics;

using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos
{
    public class FuncionesWebService
    {
        private string? ip_wb;
        [Inject] private HttpClient HttpClient { get; set; }
        public FuncionesWebService()
        {
            ip_wb = GlobalData.Ip_Wb;
            HttpClient = new HttpClient();
        }

        
        public  List<Dictionary<string, object>> Fx_DataTable(string jsonString)
        {
            var dataSet = Fx_DataSet(jsonString);
            if (dataSet.ContainsKey("Table") && dataSet["Table"] is List<Dictionary<string, object>> table)
            {
                return table;
            }
            return new List<Dictionary<string, object>>();
        }

        public async Task<MensajeAsync> Sb_ExisteTabla(string Tabla) {

            MensajeAsync retorno = new MensajeAsync();

            string Consulta_sql = $@"Select Top 1 * From INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{Tabla}'";
            MensajeAsync respuesta = await Sb_GetDataSet_Json(Consulta_sql);


            if (respuesta.EsCorrecto)
            {

                retorno.EsCorrecto = true;
                retorno.Msg = "Existe la tabla";
                GlobalData.ExisteTabla_MS_GATEWAY_STOCK = true;


            }
            else {

                retorno.EsCorrecto = false;
                retorno.Msg = "No existe la tabla";
                GlobalData.ExisteTabla_MS_GATEWAY_STOCK = false;

            }
            return retorno;


        }
        public  Dictionary<string, object> Fx_DataSet(string jsonString)
        {
            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                var result = new Dictionary<string, object>();

                foreach (JsonProperty element in doc.RootElement.EnumerateObject())
                {
                    
                    if (element.Value.ValueKind == JsonValueKind.Array)
                    {
                        var list = new List<Dictionary<string, object>>();
                        foreach (var item in element.Value.EnumerateArray())
                        {
                            var dict = new Dictionary<string, object>();
                            foreach (var prop in item.EnumerateObject())
                            {
                                dict[prop.Name] = prop.Value.ToString(); 
                            }
                            list.Add(dict);
                        }
                        result[element.Name] = list;
                    }
                    else
                    {
                        result[element.Name] = element.Value.ToString();
                    }
                }

                return result;
            }
        }
        public async Task<MensajeAsync> Sb_Inv_TraerProductoInventario(int _IdInventario, string _Empresa, string _Sucursal, string _Bodega, string _Tipo, string _Codigo)
        {
            MensajeAsync respuesta = new MensajeAsync();

            string soapMessage = @$"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <Sb_Inv_TraerProductoInventario xmlns=""http://BakApp"">
      <_IdInventario>{_IdInventario}</_IdInventario>
      <_Empresa>{_Empresa}</_Empresa>
      <_Sucursal>{_Sucursal}</_Sucursal>
      <_Bodega>{_Bodega}</_Bodega>
      <_Tipo>{_Tipo}</_Tipo>
      <_Codigo>{_Codigo}</_Codigo>
    </Sb_Inv_TraerProductoInventario>
  </soap:Body>
</soap:Envelope>";
            var content = new StringContent(soapMessage, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "\"http://BakApp/Sb_Inv_TraerProductoInventario\"");
            HttpResponseMessage httpResponse = await HttpClient.PostAsync(ip_wb + "/Ws_BakApp.asmx", content);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            string a = "{\"Table\":[{\"Codigo\":\"Error_No hay ninguna fila en la posición 0.\",\"Version\":\"1.0.0.35\"}]}";

            if (httpResponse.IsSuccessStatusCode)
            {
                if (responseContent == a | responseContent == null)
                {


                    respuesta.EsCorrecto = false;
                    respuesta.ErrorDeConexionSQL = false;
                    respuesta.Msg = "Tabla vacia";
                    return respuesta;

                }

                respuesta.Tag = httpResponse;
                respuesta.EsCorrecto = true;
                respuesta.Detalle = responseContent;
                respuesta.Msg = "Conexion exitosa";
                return respuesta;
            }
            else

            {
                respuesta.EsCorrecto = false;
                respuesta.ErrorDeConexionSQL = true;

                respuesta.Msg = "Conexion fallida";

                return respuesta;

            }




        }
        public async Task<MensajeAsync> Sb_Inv_BuscarInventario(string inventario)
        { 
         MensajeAsync respuesta = new MensajeAsync();

            string soapMessage = @$"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <Sb_Inv_BuscarInventario xmlns=""http://BakApp"">
      <Inventario>{inventario}</Inventario>
    </Sb_Inv_BuscarInventario>
  </soap:Body>
</soap:Envelope>";
            var content = new StringContent(soapMessage, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "\"http://BakApp/Sb_Inv_BuscarInventario\"");
            HttpResponseMessage httpResponse = await HttpClient.PostAsync(ip_wb + "/Ws_BakApp.asmx", content);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            string a = "{\"Table\":[]}";

            if (httpResponse.IsSuccessStatusCode)
            {
                if (responseContent == a | responseContent == null)
                {


                    respuesta.EsCorrecto = false;
                    respuesta.ErrorDeConexionSQL = false;
                    respuesta.Msg = "Tabla vacia";
                    return respuesta;

                }

                respuesta.Tag = httpResponse;
                respuesta.EsCorrecto = true;
                respuesta.Detalle = responseContent;
                respuesta.Msg = "Conexion exitosa";
                return respuesta;
            }
            else

            {
                respuesta.EsCorrecto = false;
                respuesta.ErrorDeConexionSQL = true;

                respuesta.Msg = "Conexion fallida";

                return respuesta;

            }
            return respuesta;




        }
        public async Task<MensajeAsync> Sb_Inv_IngresarHoja(string _Inv_Hoja, string _Ls_Inv_Hoja_Detalle)
        {
            MensajeAsync respuesta = new MensajeAsync();

            string soapMessage = @$"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <Sb_Inv_IngresarHoja xmlns=""http://BakApp"">
      <_Inv_Hoja>{_Inv_Hoja}</_Inv_Hoja>
      <_Ls_Inv_Hoja_Detalle>{_Ls_Inv_Hoja_Detalle}</_Ls_Inv_Hoja_Detalle>
    </Sb_Inv_IngresarHoja>
  </soap:Body>
</soap:Envelope>";
            var content = new StringContent(soapMessage, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "\"http://BakApp/Sb_Inv_IngresarHoja\"");
            HttpResponseMessage httpResponse = await HttpClient.PostAsync(ip_wb + "/Ws_BakApp.asmx", content);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            string a = "{\"Table\":[]}";

            if (httpResponse.IsSuccessStatusCode)
            {
                if (responseContent == a | responseContent == null)
                {


                    respuesta.EsCorrecto = false;
                    respuesta.ErrorDeConexionSQL = false;
                    respuesta.Msg = "Tabla vacia";
                    return respuesta;

                }

                respuesta.Tag = httpResponse;
                respuesta.EsCorrecto = true;
                respuesta.Detalle = responseContent;
                respuesta.Msg = "Conexion exitosa";
                return respuesta;
            }
            else

            {
                respuesta.EsCorrecto = false;
                respuesta.ErrorDeConexionSQL = true;

                respuesta.Msg = "Conexion fallida";
                respuesta.Msg = $"Conexión fallida: {httpResponse.StatusCode} - {httpResponse.ReasonPhrase}";
                return respuesta;

            }
            return respuesta;




        }
        public async Task<MensajeAsync> Sb_Inv_BuscarContador(string rut1, string rut2)
        {
            MensajeAsync respuesta = new MensajeAsync();

            string soapMessage = @$"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <Sb_Inv_BuscarContador xmlns=""http://BakApp"">
      <Rut>{rut1}</Rut>
      <Rut2>{rut2}</Rut2>
    </Sb_Inv_BuscarContador>
  </soap:Body>
</soap:Envelope>";
            var content = new StringContent(soapMessage, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "\"http://BakApp/Sb_Inv_BuscarContador\"");
            HttpResponseMessage httpResponse = await HttpClient.PostAsync(ip_wb + "/Ws_BakApp.asmx", content);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            string a = "{\"Table\":[]}";

            if (httpResponse.IsSuccessStatusCode)
            {
                if (responseContent == a | responseContent == null)
                {


                    respuesta.EsCorrecto = false;
                    respuesta.ErrorDeConexionSQL = false;
                    respuesta.Msg = "Tabla vacia";
                    return respuesta;

                }

                respuesta.Tag = httpResponse;
                respuesta.EsCorrecto = true;
                respuesta.Detalle = responseContent;
                respuesta.Msg = "Conexion exitosa";
                return respuesta;
            }
            else

            {
                respuesta.EsCorrecto = false;
                respuesta.ErrorDeConexionSQL = true;

                respuesta.Msg = "Conexion fallida";

                return respuesta;

            }
            return respuesta;




        }
        public async Task<MensajeAsync> Sb_Inv_BuscarSector(string Sector, string IdInv)
        {
            MensajeAsync respuesta = new MensajeAsync();

            string soapMessage = @$"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <Sb_Inv_BuscarSector xmlns=""http://BakApp"">
      <Sector>{Sector}</Sector>
      <IdInv>{IdInv}</IdInv>
    </Sb_Inv_BuscarSector>
  </soap:Body>
</soap:Envelope>";
            var content = new StringContent(soapMessage, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "\"http://BakApp/Sb_Inv_BuscarSector\"");
            HttpResponseMessage httpResponse = await HttpClient.PostAsync(ip_wb + "/Ws_BakApp.asmx", content);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            string a = "{\"Table\":[]}";

            if (httpResponse.IsSuccessStatusCode)
            {
                if (responseContent == a | responseContent == null)
                {


                    respuesta.EsCorrecto = false;
                    respuesta.ErrorDeConexionSQL = false;
                    respuesta.Msg = "Tabla vacia";
                    return respuesta;

                }

                respuesta.Tag = httpResponse;
                respuesta.EsCorrecto = true;
                respuesta.Detalle = responseContent;
                respuesta.Msg = "Conexion exitosa";
                return respuesta;
            }
            else

            {
                respuesta.EsCorrecto = false;
                respuesta.ErrorDeConexionSQL = true;

                respuesta.Msg = "Conexion fallida";

                return respuesta;

            }
            return respuesta;




        }

        public async Task<MensajeAsync> Fx_HttJob_Ws_Sb_RevCarpetaTmp() {

            string soapMessage = @$"<?xml version=""1.0"" encoding=""utf-8""?>
<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
  <soap12:Body>
    <Sb_RevCarpetaTmp xmlns=""http://BakApp"" />
  </soap12:Body>
</soap12:Envelope>";
            MensajeAsync auxAsync = new MensajeAsync();
            var content = new StringContent(soapMessage, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "\"http://BakApp/Sb_RevCarpetaTmp\"");
            HttpResponseMessage httpResponse = await HttpClient.PostAsync(ip_wb + "/Ws_BakApp.asmx", content);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            string a = "{\"Table\":[]}";
           
            if (httpResponse.IsSuccessStatusCode)
            {
                if (responseContent == a | responseContent == null)
                {


                    auxAsync.EsCorrecto = false;
                    auxAsync.ErrorDeConexionSQL = false;
                    auxAsync.Msg = "Tabla vacia";
                    return auxAsync;

                }

                auxAsync.Tag = httpResponse;
                auxAsync.EsCorrecto = true;
                auxAsync.Detalle = responseContent;
                auxAsync.Msg = "Conexion exitosa";
                return auxAsync;
            }
            else

            {
                auxAsync.EsCorrecto = false;
                auxAsync.ErrorDeConexionSQL = true;

                auxAsync.Msg = "Conexion fallida";

                return auxAsync;

            }
        }
        public  Dictionary<string, object> Fx_DataRow(string jsonString)
        {
            var table = Fx_DataTable(jsonString);
            return table.Count > 0 ? table[0] : null;
        }
        public async Task<MensajeAsync> Sb_GetDataSet_Json(string SqlQuery)
        {
            MensajeAsync auxAsync = new MensajeAsync();
            var soapEnvelope =
                $@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                      <soap:Body>
                        <Sb_GetDataSet_Json xmlns=""http://BakApp"">
                          <Consulta_Sql>{SqlQuery}</Consulta_Sql>
                        </Sb_GetDataSet_Json>
                      </soap:Body>
                    </soap:Envelope>";

            var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "\"http://BakApp/Sb_GetDataSet_Json\"");


            try
            {
                HttpResponseMessage httpResponse = await HttpClient.PostAsync(ip_wb + "/Ws_BakApp.asmx", content);
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                string a = "{\"Table\":[]}";
                if (httpResponse.IsSuccessStatusCode)
                {
                    if (responseContent == a | responseContent == null)
                    {


                        auxAsync.EsCorrecto = false;
                        auxAsync.ErrorDeConexionSQL = false;
                        auxAsync.Msg = "Tabla vacia";
                        return auxAsync;

                    }

                    auxAsync.Tag = httpResponse;
                    auxAsync.EsCorrecto = true;
                    auxAsync.Detalle = responseContent;
                    auxAsync.Msg = "Conexion exitosa";
                    return auxAsync;
                }
                else
                   
                {
                    auxAsync.EsCorrecto = false;
                    auxAsync.ErrorDeConexionSQL = true;

                    auxAsync.Msg = "Conexion fallida";

                    return auxAsync;

                }
            }
            catch (Exception ex)
            {
                

                auxAsync.EsCorrecto = false;
                auxAsync.Msg = "Conexion fallida" + ex;
                return auxAsync;


            }
        }

    }
}
