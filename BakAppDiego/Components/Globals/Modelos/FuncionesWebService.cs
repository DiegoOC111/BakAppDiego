using BakAppDiego.Components.Globals.Statics;

using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    if (responseContent == a) {
                        

                        auxAsync.EsCorrecto = false;

                        auxAsync.Msg = "tabla nula, error de query";
                        //buttonColor = "green"; // Cambiar el color del botón a verde
                        return auxAsync;
                        
                    }
                    var result = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, object>>>>(responseContent);

                    auxAsync.EsCorrecto = true;
                    auxAsync.Tag = result;
                    auxAsync.Msg = "Conexion exitosa";
                    //buttonColor = "green"; // Cambiar el color del botón a verde
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
