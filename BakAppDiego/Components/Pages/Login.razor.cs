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
using BakAppDiego.Components.Interface;
#if ANDROID
using AndroidApp = Android.App.Application;
using Setting = Android.Provider.Settings;
using static Android.Provider.Settings;

#endif
#if WINDOWS
using System.Management;
#endif


namespace BakAppDiego.Components.Pages
{
    public partial class Login
    {


        public string deviceId;
        private LoadingPopUp loadingPopup;
        private DialogoService Dialogo;
        private string responseMessage = string.Empty;
        private string buttonColor = "gray"; // Color inicial del botón
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }
        private PopUpConfirmar PopUp;
        private FuncionesWebService ComunicacionWB;

        async Task IrALog()
        {
            // here're other async action calls
            NavigationManager.NavigateTo("/LoginDatos", true);
        }

        protected override void OnInitialized()
        {
            ComunicacionWB = new FuncionesWebService();
#if ANDROID
            var context = AndroidApp.Context;

            string id = Setting.Secure.GetString(context.ContentResolver, Secure.AndroidId);

            deviceId = id;
#endif

#if WINDOWS
            deviceId = GetDeviceID();
#endif
            GlobalData.Id_dispositivo = deviceId;
        }
#if WINDOWS


        private string GetDeviceID()
        {
            ManagementObjectSearcher mos = new("SELECT * FROM Win32_BaseBoard");
            ManagementObjectCollection moc = mos.Get();
            foreach (ManagementObject mo in moc)
            {
                var motherBoardSerial = (string)mo["SerialNumber"];
                return motherBoardSerial;
            }
            return string.Empty;
        }

#endif
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (GlobalData.Ip_Wb == null)
                {

                    bool aux = await MostrarPopUp("Advertencia", "Falta la configuracion del Webservice", "Continuar", "  ", false);


                }
            }

        }
        private async Task SB_cargar() {

            string sqlQuery = @"Select Top 1 *,NOKOCARAC+'.dbo.' As Global_BaseBk From TABCARAC Where KOTABLA = 'BAKAPP' And KOCARAC = 'BASE'";
            MensajeAsync mensaje = await ComunicacionWB.Sb_GetDataSet_Json(sqlQuery);
            if (mensaje.EsCorrecto)
            {

                var obj = mensaje.Tag;
                try
                {

                    var ax = ComunicacionWB.Fx_DataRow((string)mensaje.Tag);
                    if (ax != null)
                    {
                        GlobalData.Global_BaseBk = (string)ax["Global_BaseBk"];
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron filas.");
                    }
                }
                catch (Exception ex)
                {


                }

            }
            else
            {


            }


        }



        //private string GetDeviceId()
        //{
        //    //System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //    //sb.AppendLine($"Model: {DeviceInfo.Current.Model}");
        //    //sb.AppendLine($"Manufacturer: {DeviceInfo.Current.Manufacturer}");
        //    //sb.AppendLine($"Name: {DeviceInfo.Current.Name}");
        //    //sb.AppendLine($"OS Version: {DeviceInfo.Current.VersionString}");
        //    //sb.AppendLine($"Idiom: {DeviceInfo.Current.Idiom}");
        //    //sb.AppendLine($"Platform: {DeviceInfo.Current.Platform}");

        //    //bool isVirtual = DeviceInfo.Current.DeviceType switch
        //    //{
        //    //    DeviceType.Physical => false,
        //    //    DeviceType.Virtual => true,
        //    //    _ => false
        //    //};

        //    //sb.AppendLine($"Virtual device? {isVirtual}");

        //    //return sb.ToString();
        //    //return   new  GetDeviceInfo().GetDeviceID();
        //    return "";
        //}


        private async Task ContraseñaAsync() {
            Dialogo = new DialogoService();
            string Respuesta = await Dialogo.DisplayText("Ingrese la Clave de acceso", "Clave", "Aceptar", "Cerrar");

            if (Respuesta == "971364")
            {

                MensajeAsync Msg = await PruebaIP();
                if (Msg.EsCorrecto)
                {
                    bool res = await MostrarPopUp("Proceso exitoso", "Ip Valida, conectado a WebsService", "Aceptar", " Usar otro", false);

                    Console.WriteLine(Msg.Msg);
                }
                else if (Msg.Cancelado) {

                    return;
                }
                else
                {

                    bool res = await MostrarPopUp("Error de conexion ", "No se pudo conectar al WebsService", "Aceptar", " Usar otro", false);

                    Console.WriteLine(Msg.Msg);

                }

            } else if (Respuesta == null) {



            }
            else {


                bool res = await MostrarPopUp("Error ", "Contraseña incorrecta", "Aceptar", "Cerrar", false);

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
            if (Respuesta == null | Respuesta == "") {
                MensajeAsync MsgAux = new MensajeAsync();
                MsgAux.EsCorrecto = false;
                MsgAux.Cancelado = true;
                return MsgAux;
            }
            MensajeAsync Msg = await CallSoapService(Respuesta);
            if (Msg.EsCorrecto)
            {



                return Msg;

            }
            else {

                return Msg;
            }





        }


        private async Task<MensajeAsync> CallSoapService(string newIp)
        {
            loadingPopup.Show();
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
                    GlobalData.GuardarIP();
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
        private async Task nombre() {
            string sqlQuery = @"Select Top 1 *,NOKOCARAC+'.dbo.' As Global_BaseBk From TABCARAC Where KOTABLA = 'BAKAPP' And KOCARAC = 'BASE'";
            MensajeAsync mensaje = await ComunicacionWB.Sb_GetDataSet_Json(sqlQuery);
            if (mensaje.EsCorrecto)
            {

                var obj = mensaje.Tag;
                try
                {

                    var ax = ComunicacionWB.Fx_DataRow((string)mensaje.Tag);
                    if (ax != null)
                    {
                        GlobalData.Global_BaseBk = (string)ax["Global_BaseBk"];
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron filas.");
                    }
                }
                catch (Exception ex)
                {


                }

            }
            else
            {


            }



        }
        

    }
}