using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace BakAppDiego.Services
{
    public class SoapService
    {

        private readonly HttpClient _httpClient;

        public SoapService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendSoapRequest(string soapMessage)
        {
            var requestContent = new StringContent(soapMessage, Encoding.UTF8, "application/soap+xml");

            // Reemplaza la URL con la del WebService SOAP real
            var response = await _httpClient.PostAsync("http://localhost:89/Ws_BakApp.asmx", requestContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Error en la solicitud SOAP: {response.StatusCode}");
            }
        }
    } }

