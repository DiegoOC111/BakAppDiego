using BakAppDiego.Components.Dialogs;
using BakAppDiego.Components.Globals.Modelos;
using BakAppDiego.Components.Globals.Modelos.Bakapp;
using BakAppDiego.Components.Globals.Statics;
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
        private bool Escaneado;
        private InputContador InCon;
        private DialogoService Dialogo;
        private FuncionesWebService ComunicacionWB;
        Contador c1 = new Contador();
        Contador c2 = new Contador();
        private bool iniciado = false;
        
        protected override void OnInitialized()
        {

            ComunicacionWB = new FuncionesWebService();
            Dialogo = new DialogoService();
            GlobalData.menu = true;
            Escaneado = false;
        }
        private async Task CambiarContador(int numero) {


            if (numero == 1)
            {
                MensajeAsync res = await elijeContador();
                if (res.EsCorrecto)
                {
                    if (res.Tag != null)
                    {
                        c1 = (Contador)res.Tag;

                    }

                }

            }
            else { 
                MensajeAsync res = await elijeContador();
                if (res.EsCorrecto) {
                    if (res.Tag != null) { 
                        c2 = (Contador)res.Tag;


                    }

                }


            }

        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {

                MensajeAsync res = await elijeContador();
                if (res.EsCorrecto) {
                    c1 =(Contador) res.Tag;


                    //foreach (Contador contador in contadorResponse.Table)
                    //{
                    //    botones.Add($"{contador.Nombre} - {contador.Rut}");

                    //}
                    //string[] opciones = botones.ToArray();
                    //string respuesta = await Dialogo.DisplayActionSheet("Elija al contador asociado", null, null, opciones);

                }
                bool r = await Dialogo.DisplayConfirm("Confirmación", "¿Desea agregar un segundo contador ?","Si","No");
                if (r)
                {
                    res = await elijeContador();
                    if (res.EsCorrecto)
                    {
                        c2 = (Contador)res.Tag;


                        //foreach (Contador contador in contadorResponse.Table)
                        //{
                        //    botones.Add($"{contador.Nombre} - {contador.Rut}");

                        //}
                        //string[] opciones = botones.ToArray();
                        //string respuesta = await Dialogo.DisplayActionSheet("Elija al contador asociado", null, null, opciones);

                    }
                }
                iniciado = true;
                StateHasChanged();

            }

        }
        private async Task<MensajeAsync> elijeContador() {

            MensajeAsync res = await ComunicacionWB.Sb_Inv_BuscarContador(c1.Rut, c2.Rut);
            MensajeAsync Retorno = new MensajeAsync();
            if (res.EsCorrecto)
            {

                ContadorResponse contadorResponse = JsonSerializer.Deserialize<ContadorResponse>(res.Detalle);
                List<string> botones = new List<string>();

                // Agrega valores dinámicamente a la lista
              
                Contador sel = await MostrarContadores("Elija el contador", "", "Cancelar", "", iniciado, contadorResponse);
                Retorno.EsCorrecto = true;
                Retorno.Tag = sel;
                return Retorno;

                //foreach (Contador contador in contadorResponse.Table)
                //{
                //    botones.Add($"{contador.Nombre} - {contador.Rut}");

                //}
                //string[] opciones = botones.ToArray();
                //string respuesta = await Dialogo.DisplayActionSheet("Elija al contador asociado", null, null, opciones);

            }
            Retorno.EsCorrecto = false;
            Retorno.Msg = res.Msg;
            return Retorno;


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
