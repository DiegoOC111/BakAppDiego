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

using System.Runtime.CompilerServices;
using BakAppDiego.Components.Dialogs;
using BakAppDiego.Components.Globals.Statics;

namespace BakAppDiego.Components.Pages
{
    public partial class Login
    {
        
        private LoadingPopUp loadingPopup;
        private DialogoService Dialogo;
        private string responseMessage = string.Empty;
        private string buttonColor = "gray"; // Color inicial del botón
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }

       
        async Task IrALog()
        {
            // here're other async action calls
            NavigationManager.NavigateTo("/LoginDatos", true);
        }

        private void asd() {
            
        
        }


        private async Task ContraseñaAsync() {
            Dialogo = new DialogoService();
            string Respuesta  = await  Dialogo.DisplayText("Ingrese la Clave de acceso", "Clave", "Aceptar", "Cerrar");

            if (Respuesta == "971364")
            {

                await PruebaIP();
                if (Mensaje.EsCorrecto)
                {
                    bool res = await Dialogo.DisplayConfirm("Conexion Exitosa ", "", "Aceptar", "Cerrar");

                }
                else
                {




                }

            } else if (Respuesta == null) { 
            
            
            
            }
            else {


                bool res = await Dialogo.DisplayConfirm("Error ", "Contraseña incorrecta", "Aceptar", "Cerrar");

            }
            
            


        }

        private async Task PruebaIP()
        {
            Dialogo = new DialogoService();
            string Respuesta = await Dialogo.DisplayText("Ingrese la IP ", "IP", "Aceptar", "Cerrar");
            if (Respuesta == null | Respuesta == "" ) {

            }
            await CallSoapService(Respuesta);
            if (Mensaje.EsCorrecto)
            {

                GlobalData.GuardarIP();
                Mensaje.EsCorrecto = true;
                Mensaje.Msg = "Conexion a ip exitosa";
                Console.WriteLine(Mensaje.Msg);
            }
            else {

                Console.WriteLine(Mensaje.Msg);
                Mensaje.EsCorrecto = false;
                Mensaje.Msg = "Conexion a ip fallida";
                bool res = await Dialogo.DisplayConfirm("Conexion Fallida ", "", "Aceptar", "Cerrar");
                
            }





        }


        private async Task<bool> CallSoapService(string newIp )
        {
            loadingPopup.Show();
            await Task.Delay(3000);
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
                var httpResponse = await HttpClient.PostAsync("http://" + newIp + "/Ws_BakApp.asmx", content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    loadingPopup.Hide();

                    GlobalData.Ip_Wb = "http://" + newIp;
                    Mensaje.EsCorrecto = true;
                    Mensaje.Msg = "Conexion exitosa";
                    //buttonColor = "green"; // Cambiar el color del botón a verde
                    return true;
                }
                else
                loadingPopup.Hide();
                {
                    Mensaje.EsCorrecto = false;
                    Mensaje.Msg = "Conexion fallida";

                    return false;

                }
            }
            catch (Exception ex)
            {
                loadingPopup.Hide();

                Mensaje.EsCorrecto = false;
                Mensaje.Msg = "Conexion fallida" + ex;
                return false;


            }
        }
    }
}