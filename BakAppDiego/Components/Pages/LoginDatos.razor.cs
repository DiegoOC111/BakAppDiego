
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
using BakAppDiego.Components.Dialogs;

using BakAppDiego.Components.Globals.Modelos;


namespace BakAppDiego.Components.Pages
{
    public partial class LoginDatos
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }
        private string password;
        private ElementReference passwordInput;
        private DialogoService Dialogo;
        private PopUpConfirmar PopUp;
        
        public MensajeAsync Msj;
        
        protected override void OnInitialized()
        {

            Dialogo = new DialogoService();
            
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await passwordInput.FocusAsync(); // Establecer el foco en el input
                if (GlobalData.usuario != null)
                {
                    string mensaje = "Hay un usuario activo : " + GlobalData.usuario.NoKofu;
                    bool res = await MostrarPopUp("Usuario Activo", mensaje, "Iniciar", " Usar otro", true);
                    if (res)
                    {


                    }
                    else
                    {

                    }


                }
            }
            
        }

        private async void Validate()
        {
            // Lógica para validar la contraseña o lo que necesites.
            Console.WriteLine($"Contraseña ingresada: {password}");

            // Llamar al método para realizar el login mediante SOAP
            MensajeAsync mensajeAsync =  await Login();
            if (mensajeAsync.EsCorrecto)
            {
                await MostrarPopUp("Operacion exitosa", "Usuario ingresado bienvenido " + GlobalData.usuario.NoKofu, "Seguir", " Cancelar", false);

                Console.WriteLine(mensajeAsync.Msg);

            }
            else {
                await MostrarPopUp("Operacion fallida", "Contraseña incorrecta", "Seguir", " Cancelar", true);

                Console.WriteLine(mensajeAsync.Msg);

            }

            // mensaje = Login
            // si mensaje.esCorrecto.
            //....

        }

        private async Task<MensajeAsync> Login()
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

            MensajeAsync AuxAsync  = new MensajeAsync();
            using (var client = new HttpClient())
            {
                // Establecer la URL del servicio web
                var url = GlobalData.Ip_Wb + "/Ws_BakApp.asmx"; // Asegúrate de usar el puerto correcto

                // Configurar la solicitud
                var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
                content.Headers.Add("SOAPAction", "\"http://BakApp/Sb_Login_Usuario_Json\"");

                try
                {

                    // Enviar la solicitud POST
                    var response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode(); // Asegurarse de que la respuesta fue exitosa

                    var responseContent = await response.Content.ReadAsStringAsync();
                    string a = "{\"Table\":[]}";
                    if (responseContent == a)
                    {
                        AuxAsync.EsCorrecto = false;
                        AuxAsync.Msg = "LogIn fallido, contrasela incorrecta";

                    }
                    else
                    {
                                               

                        MensajeAsync mensajeAsync = ParseSoapResponse(responseContent);


                        AuxAsync = mensajeAsync;
                        // Aquí puedes usar el objeto usuario
                        Console.WriteLine($"Resultado del login: {mensajeAsync}");


                    }
                }

                    // Procesar la respuesta
                   
                
                catch (HttpRequestException e)
                {
                    AuxAsync.EsCorrecto = false;
                    AuxAsync.Msg = $"LogIn fallido, fallo de conexion: {e.Message}";
                    AuxAsync.ErrorDeConexionSQL = true;
                    await MostrarPopUp("Operacion fallida", "Fallo de conexion", "Seguir", " Cancelar", true);

                    Console.WriteLine($"Error al enviar la solicitud: {e.Message}");
                }
                catch (Exception ex)
                {
                    AuxAsync.EsCorrecto = false;
                    AuxAsync.Msg = $"LogIn fallido, fallo de codigo: {ex.Message}";
                    AuxAsync.ErrorDeConexionSQL = false;
                    await MostrarPopUp("Operacion fallida", "Fallo de codigo", "Seguir", " Cancelar", true);
                    Console.WriteLine($"Ocurrió un error: {ex.Message}");
                }
            }
            return AuxAsync;
        }
        private async Task<bool> MostrarPopUp(string titulo,string mensaje, string btnStr, string CancelarStr, bool Visible)
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
        private MensajeAsync ParseSoapResponse(string soapResponse)
        {
            Console.WriteLine("Contenido de la respuesta SOAP:");
            Console.WriteLine(soapResponse); // Para depurar
            MensajeAsync AuxAsync = new MensajeAsync(); 
            try
            {
                // Deserializar el JSON a un objeto TabfuResponse
                TabfuResponse response = JsonConvert.DeserializeObject<TabfuResponse>(soapResponse);
                TABFU Respuesta = response.Table[0];
                GlobalData.usuario = Respuesta;
                GlobalData.GuardarTABFU();
                AuxAsync.EsCorrecto = true;
                AuxAsync.Msg = "Tabla Creada";
                
                return AuxAsync; // Retorna el objeto que contiene la lista de Tabfu
            }
            catch (JsonException ex)
            {
                AuxAsync.EsCorrecto = false;
                AuxAsync.Msg = "Tabla fallida" + ex.Message;
                Console.WriteLine("Error al deserializar el JSON: " + ex.Message);
                return AuxAsync; // Vuelve a lanzar la excepción si es necesario
            }
        }
    }
}



