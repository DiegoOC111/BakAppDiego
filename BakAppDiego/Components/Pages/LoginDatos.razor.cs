
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using BakAppDiego.Components.Globals.TablasBackApp;
using BakAppDiego.Components.Globals.Statics;

namespace BakAppDiego.Components.Pages
{
    public partial class LoginDatos
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }
        private string password;
        private ElementReference passwordInput;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await passwordInput.FocusAsync(); // Establecer el foco en el input
            }
        }

        private async void Validate()
        {
            // Lógica para validar la contraseña o lo que necesites.
            Console.WriteLine($"Contraseña ingresada: {password}");

            // Llamar al método para realizar el login mediante SOAP
            await log();
        }

        private async Task log()
        {
            // Crear el XML del cuerpo de la solicitud SOAP
            var soapRequest = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <Sb_Login_Usuario_Json xmlns=""http://BakApp"">
      <_Clave>{password}</_Clave>
    </Sb_Login_Usuario_Json>
  </soap:Body>
</soap:Envelope>";

            using (var client = new HttpClient())
            {
                // Establecer la URL del servicio web
                var url = "http://192.168.1.87:89/Ws_BakApp.asmx"; // Asegúrate de usar el puerto correcto

                // Configurar la solicitud
                var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
                content.Headers.Add("SOAPAction", "\"http://BakApp/Sb_Login_Usuario_Json\"");

                try
                {
                    // Enviar la solicitud POST
                    var response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode(); // Asegurarse de que la respuesta fue exitosa

                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Procesar la respuesta
                    var loginResponse = ParseSoapResponse(responseContent);

                    // Aquí puedes usar el objeto usuario
                    Console.WriteLine($"Resultado del login: {loginResponse}");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error al enviar la solicitud: {e.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error: {ex.Message}");
                }
            }
        }

        private TabfuResponse ParseSoapResponse(string soapResponse)
        {
            Console.WriteLine("Contenido de la respuesta SOAP:");
            Console.WriteLine(soapResponse); // Para depurar

            try
            {
                // Deserializar el JSON a un objeto TabfuResponse
                TabfuResponse response = JsonConvert.DeserializeObject<TabfuResponse>(soapResponse);
                TABFU Respuesta = response.Table[0];
                GlobalData.usuario = Respuesta;
                GlobalData.GuardarTABFU();
                return response; // Retorna el objeto que contiene la lista de Tabfu
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Error al deserializar el JSON: " + ex.Message);
                throw; // Vuelve a lanzar la excepción si es necesario
            }
        }
    }
}



