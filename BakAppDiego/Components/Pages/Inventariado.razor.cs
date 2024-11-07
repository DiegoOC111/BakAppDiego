using BakAppDiego.Components.Dialogs;
using BakAppDiego.Components.Globals.Modelos;
using BakAppDiego.Components.Globals.Modelos.Bakapp;
using EO.WebBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Pages
{
    public partial class Inventariado 
    {
        private InputContador InCon;
        private DialogoService Dialogo;
        private FuncionesWebService ComunicacionWB;
        private string rut1 = null;
        private string rut2 = null;
        
        protected override void OnInitialized()
        {

            ComunicacionWB = new FuncionesWebService();
            Dialogo = new DialogoService();

        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                MensajeAsync res = await ComunicacionWB.Sb_Inv_BuscarContador(rut1, rut2);
                if (res.EsCorrecto) {

                ContadorResponse contadorResponse = JsonSerializer.Deserialize<ContadorResponse>(res.Detalle);
                    List<string> botones = new List<string>();

                    // Agrega valores dinámicamente a la lista

                    Contador sel = await MostrarContadores("Elija el contador", "", "", "",false, contadorResponse);

                    //foreach (Contador contador in contadorResponse.Table)
                    //{
                    //    botones.Add($"{contador.Nombre} - {contador.Rut}");

                    //}
                    //string[] opciones = botones.ToArray();
                    //string respuesta = await Dialogo.DisplayActionSheet("Elija al contador asociado", null, null, opciones);

                }



                
            }

        }
        private async Task<Contador> MostrarContadores(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible,ContadorResponse contadoress)
        {
            InCon.Crear(titulo, mensaje, btnStr, CancelarStr, Visible, contadoress);

            // Configura el popup
            //InCon.crear(titulo, mensaje, btnStr, CancelarStr, Visible,);

            // Espera hasta que el usuario presione un botón
            Contador resultado = await InCon.ShowAsync();

            // Aquí puedes manejar el resultado
            
            return resultado;
        }

    }
}
