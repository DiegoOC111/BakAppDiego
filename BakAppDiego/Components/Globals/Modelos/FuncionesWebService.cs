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
        private string ip_wb;
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
                var httpResponse = await HttpClient.PostAsync(ip_wb + "/Ws_BakApp.asmx", content);
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                string a = "{\"Table\":[]}";
                if (httpResponse.IsSuccessStatusCode)
                {
                    if (responseContent == a)
                    {


                        auxAsync.EsCorrecto = false;

                        auxAsync.Msg = "tabla nula, error de query";
                        return auxAsync;

                    }

                    auxAsync.Tag = responseContent;
                    auxAsync.EsCorrecto = true;

                    auxAsync.Msg = "Conexion exitosa";
                    return auxAsync;
                }
                else
                   
                {
                    auxAsync.EsCorrecto = false;
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
