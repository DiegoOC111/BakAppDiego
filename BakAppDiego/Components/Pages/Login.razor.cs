using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace BakAppDiego.Components.Pages
{
    public partial class Login
    {
        private string responseMessage = string.Empty;
        private string buttonColor = "gray"; // Color inicial del botón

        [Inject] private HttpClient HttpClient { get; set; }

        private async Task CallSoapService()
        {
            var soapEnvelope =
                @"<?xml version=""1.0"" encoding=""utf-8""?>
          <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                         xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                         xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
            <soap:Body>
              <Fx_Conectado_Web_Service xmlns=""http://BakApp"" />
            </soap:Body>
          </soap:Envelope>";

            var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "\"http://BakApp/Fx_Conectado_Web_Service\"");

            try
            {
                var httpResponse = await HttpClient.PostAsync("http://192.168.1.87:89/Ws_BakApp.asmx", content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    responseMessage = "Conectado exitosamente!";
                    buttonColor = "green"; // Cambiar el color del botón a verde
                }
                else
                {
                    responseMessage = $"Error: {httpResponse.ReasonPhrase}";
                    buttonColor = "red"; // Cambiar el color del botón a rojo si hay error
                }
            }
            catch (Exception ex)
            {
                responseMessage = $"Exception: {ex.Message}";
                buttonColor = "red"; // Cambiar el color del botón a rojo si ocurre una excepción
            }
        }
    }
}