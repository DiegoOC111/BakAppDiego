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
using BakAppDiego.Components.Globals.Modelos;


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
        private PopUpConfirmar PopUp;


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

                MensajeAsync Msg =  await PruebaIP();
                if (Msg.EsCorrecto)
                {
                    bool res = await MostrarPopUp("Proceso exitoso", "Ip Valida, conectado a WebsService", "Aceptar", " Usar otro", false);

                    Console.WriteLine(Msg.Msg);
                }
                else
                {

                    bool res = await MostrarPopUp("Error de conexion ", "No se pudo conectar al WebsService", "Aceptar", " Usar otro", false);

                    Console.WriteLine(Msg.Msg);

                }

            } else if (Respuesta == null) { 
            
            
            
            }
            else {


                bool res = await MostrarPopUp("Error ", "Contraseña incorrecta", "Aceptar", "Cerrar",false);
                
            }
            
            


        }
        private async Task<bool> MostrarPopUp(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible)
        {
            
            // Configura el popup
            PopUp.crear(titulo, mensaje, btnStr, CancelarStr, Visible);

            // Espera hasta que el usuario presione un botón
            bool resultado = await PopUp.ShowAsync();

            // Aquí puedes manejar el resultado
            if (resultado)
            {
                // El usuario presionó "Aceptar"
                Console.WriteLine("El usuario aceptó.");
            }
            else
            {
                // El usuario presionó "Cancelar"
                Console.WriteLine("El usuario canceló.");
            }
            return resultado;
        }
        private async Task<MensajeAsync> PruebaIP()
        {
            Dialogo = new DialogoService();
            
            string Respuesta = await Dialogo.DisplayText("Ingrese la IP ", "IP", "Aceptar", "Cerrar");
            if (Respuesta == null | Respuesta == "" ) {

            }
            MensajeAsync Msg =  await CallSoapService(Respuesta);
            if (Msg.EsCorrecto)
            {


                GlobalData.GuardarIP();
                
                return Msg;

            }
            else {
               
                return Msg;
            }





        }


        private async Task<MensajeAsync> CallSoapService(string newIp )
        {
            loadingPopup.Show();
            await Task.Delay(3000);
            MensajeAsync auxAsync = new MensajeAsync();
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
                    auxAsync.EsCorrecto = true;
                    auxAsync.Msg = "Conexion exitosa";
                    //buttonColor = "green"; // Cambiar el color del botón a verde
                    return auxAsync;
                }
                else
                loadingPopup.Hide();
                {
                    auxAsync.EsCorrecto = false;
                    auxAsync.Msg = "Conexion fallida";

                    return auxAsync;

                }
            }
            catch (Exception ex)
            {
                loadingPopup.Hide();

                auxAsync.EsCorrecto = false;
                auxAsync.Msg = "Conexion fallida" + ex;
                return auxAsync;


            }
        }
    }
}