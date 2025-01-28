
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
using BakAppDiego.Components.Globals.Modelos.Responses;
using BakAppDiego.Components.Modulos_de_funciones;
using BakAppDiego.Components.Globals.Modelos.Clases;


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
        private LoadingPopUp loadingPopup;

        public MensajeAsync Msj;
       
        async Task IrMenuPrincipal()
        {
            // here're other async action calls
            NavigationManager.NavigateTo("/MenuPrincipal", true);
        }
        protected override void OnInitialized()
        {
            NavigationHistory.AddToHistory(NavigationManager.Uri);
            Dialogo = new DialogoService();
            GlobalData.prev = "/Login";

        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await passwordInput.FocusAsync(); // Establecer el foco en el input
                
            }
            
        }

        /// <summary>
        /// Valida las credenciales ingresadas por el usuario.
        /// </summary>
        private async void Validate()
        {
            Console.WriteLine($"Contraseña ingresada: {password}");

            // Muestra un popup de carga mientras se valida la contraseña
            loadingPopup.Show();

            // Llama al método de inicio de sesión y espera la respuesta
            MensajeAsync mensajeAsync = await Login();
            if (mensajeAsync.EsCorrecto)
            {
                ConectarConf conect = new ConectarConf();

                // Carga la configuración necesaria si el inicio de sesión fue exitoso
                MensajeAsync ms = await conect.Sb_Cargar_Datos_De_Configuracion();
                loadingPopup.Hide();

                if (ms.EsCorrecto)
                {
                    // Muestra un popup de bienvenida al usuario
                    bool r = await MostrarPopUp("Operación exitosa", "Usuario ingresado, bienvenido " + GlobalData.Usuario_Activo.NoKofu, "Ingresar", "Cancelar", false);
                    if (r)
                    {
                        // Redirige al menú principal
                        await IrMenuPrincipal();
                    }
                    else
                    {
                        // Si el usuario cancela, se limpia la información del usuario activo
                        GlobalData.Usuario_Activo = null;
                    }
                }
                else
                {
                    GlobalData.Usuario_Activo = null;
                    // Muestra un mensaje de error si hubo un problema al cargar la configuración
                    bool r = await MostrarPopUp("Operación fallida", "Ocurrió un error cargando la configuración", "Continuar", "Cancelar", false);
                    if (r)
                    {
                        await IrMenuPrincipal();
                    }
                }
                Console.WriteLine(mensajeAsync.Msg);
            }
            else
            {
                // Muestra un mensaje de error si la contraseña es incorrecta
                await MostrarPopUp("Operación fallida", "Contraseña incorrecta", "Continuar", "Cancelar", false);
                Console.WriteLine(mensajeAsync.Msg);
            }
        }

        /// <summary>
        /// Realiza el proceso de inicio de sesión enviando las credenciales al  web service.
        /// </summary>
        /// <returns>Un objeto MensajeAsync con el resultado del inicio de sesión.</returns>
        private async Task<MensajeAsync> Login()
        {
            // Crea la solicitud SOAP para autenticar al usuario
            var soapRequest = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <Sb_Login_Usuario_Json xmlns=""http://BakApp"">
      <_Clave>{password}</_Clave>
    </Sb_Login_Usuario_Json>
  </soap:Body>
</soap:Envelope>";

            MensajeAsync AuxAsync = new MensajeAsync();
            using (var client = new HttpClient())
            {
                var url = GlobalData.Ip_Wb + "/Ws_BakApp.asmx"; // URL del servicio web

                var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
                content.Headers.Add("SOAPAction", "\"http://BakApp/Sb_Login_Usuario_Json\"");

                try
                {
                    var response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    string a = "{\"Table\":[]}";
                    if (responseContent == a)
                    {
                        loadingPopup.Hide();
                        AuxAsync.EsCorrecto = false;
                        AuxAsync.Msg = "Inicio de sesión fallido, contraseña incorrecta";
                    }
                    else
                    {
                        MensajeAsync mensajeAsync = ParseSoapResponse(responseContent);
                        AuxAsync = mensajeAsync;
                        Console.WriteLine($"Resultado del login: {mensajeAsync}");
                    }
                }
                catch (HttpRequestException e)
                {
                    loadingPopup.Hide();
                    AuxAsync.EsCorrecto = false;
                    AuxAsync.Msg = $"Inicio de sesión fallido, fallo de conexión: {e.Message}";
                    AuxAsync.ErrorDeConexionSQL = true;
                    await MostrarPopUp("Operación fallida", "Fallo de conexión", "Seguir", "Cancelar", true);
                    Console.WriteLine($"Error al enviar la solicitud: {e.Message}");
                }
                catch (Exception ex)
                {
                    loadingPopup.Hide();
                    AuxAsync.EsCorrecto = false;
                    AuxAsync.Msg = $"Inicio de sesión fallido, error en el código: {ex.Message}";
                    AuxAsync.ErrorDeConexionSQL = false;
                    await MostrarPopUp("Operación fallida", "Fallo en el código", "Seguir", "Cancelar", true);
                    Console.WriteLine($"Ocurrió un error: {ex.Message}");
                }
            }

            return AuxAsync;
        }

        /// <summary>
        /// Muestra un popup con opciones para el usuario.
        /// </summary>
        /// <param name="titulo">Título del popup.</param>
        /// <param name="mensaje">Mensaje del popup.</param>
        /// <param name="btnStr">Texto del botón de acción.</param>
        /// <param name="CancelarStr">Texto del botón de cancelar.</param>
        /// <param name="Visible">Indica si el popup es visible.</param>
        /// <returns>Un valor booleano que indica si el usuario aceptó.</returns>
        private async Task<bool> MostrarPopUp(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible)
        {
            PopUp.crear(titulo, mensaje, btnStr, CancelarStr, Visible);

            bool resultado = await PopUp.ShowAsync();

            if (resultado)
            {
                Console.WriteLine("El usuario aceptó.");
            }
            else
            {
                Console.WriteLine("El usuario canceló.");
            }
            return resultado;
        }

        /// <summary>
        /// Analiza la respuesta SOAP del servicio web y convierte los datos a un objeto MensajeAsync.
        /// </summary>
        /// <param name="soapResponse">La respuesta SOAP en formato de texto.</param>
        /// <returns>Un objeto MensajeAsync con el resultado del análisis.</returns>
        private MensajeAsync ParseSoapResponse(string soapResponse)
        {
            Console.WriteLine("Contenido de la respuesta SOAP:");
            Console.WriteLine(soapResponse);
            MensajeAsync AuxAsync = new MensajeAsync();
            try
            {
                TABFUResponse response = JsonConvert.DeserializeObject<TABFUResponse>(soapResponse);
                TABFU Respuesta = response.Table[0];
                GlobalData.Usuario_Activo = Respuesta;
                AuxAsync.EsCorrecto = true;
                AuxAsync.Msg = "Tabla creada correctamente";
                return AuxAsync;
            }
            catch (JsonException ex)
            {
                AuxAsync.EsCorrecto = false;
                AuxAsync.Msg = "Error al crear la tabla: " + ex.Message;
                Console.WriteLine("Error al deserializar el JSON: " + ex.Message);
                return AuxAsync;
            }
        }
    }
}



