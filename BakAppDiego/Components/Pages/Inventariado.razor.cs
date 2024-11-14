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
        private string Sector = "";
        private bool Escaneado;
        private InputContador InCon;
        private InputDialog InDIalog;
        private DialogoService Dialogo;
        private Zw_Inv_Sector SectorActivo;
        private FuncionesWebService ComunicacionWB;
        Zw_Inv_Contador c1 = new Zw_Inv_Contador();
        Zw_Inv_Contador c2 = new Zw_Inv_Contador();
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
                        c1 = (Zw_Inv_Contador)res.Tag;

                    }

                }

            }
            else {
                MensajeAsync res = await elijeContador();
                if (res.EsCorrecto) {
                    if (res.Tag != null) {
                        c2 = (Zw_Inv_Contador)res.Tag;


                    }

                }


            }

        }
        private async Task EscanearSector()
        {
            string sector = await MostrarInput("Ingrese el sector", "", "Aceptar", "Cancelar", true);
            if (sector != null) {

                MensajeAsync res = await ComunicacionWB.Sb_Inv_BuscarSector(sector, GlobalData.InventarioActivo.Id.ToString());
                if (res.EsCorrecto) {

                    ls_Zw_Inv_Sector resCont =  JsonSerializer.Deserialize<ls_Zw_Inv_Sector>(res.Detalle);
                    SectorActivo = resCont.Table[0];
                    Sector = SectorActivo.Sector;
                    Escaneado = true;
                    StateHasChanged();
                }
            
            }


        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {

                MensajeAsync res = await elijeContador();
                if (res.EsCorrecto) {
                    c1 = (Zw_Inv_Contador)res.Tag;


                    //foreach (Contador contador in contadorResponse.Table)
                    //{
                    //    botones.Add($"{contador.Nombre} - {contador.Rut}");

                    //}
                    //string[] opciones = botones.ToArray();
                    //string respuesta = await Dialogo.DisplayActionSheet("Elija al contador asociado", null, null, opciones);

                }
                bool r = await Dialogo.DisplayConfirm("Confirmación", "¿Desea agregar un segundo contador ?", "Si", "No");
                if (r)
                {
                    res = await elijeContador();
                    if (res.EsCorrecto)
                    {
                        c2 = (Zw_Inv_Contador)res.Tag;


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

                ls_Zw_Inv_Contador contadorResponse = JsonSerializer.Deserialize<ls_Zw_Inv_Contador>(res.Detalle);
                List<string> botones = new List<string>();

                // Agrega valores dinámicamente a la lista

                Zw_Inv_Contador sel = await MostrarContadores("Elija el contador", "", "Cancelar", "", iniciado, contadorResponse);
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
        private async Task<Zw_Inv_Contador> MostrarContadores(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible, ls_Zw_Inv_Contador contadoress)
        {
            InCon.Crear(titulo, mensaje, btnStr, CancelarStr, Visible, contadoress);

            // Configura el popup
            //InCon.crear(titulo, mensaje, btnStr, CancelarStr, Visible,);

            // Espera hasta que el usuario presione un botón
            Zw_Inv_Contador resultado = await InCon.ShowAsync();

            // Aquí puedes manejar el resultado

            return resultado;
        }
        private async Task<string> MostrarInput(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible)
        {
            InDIalog.Crear(titulo, mensaje, btnStr, CancelarStr, Visible);

            // Configura el popup
            //InCon.crear(titulo, mensaje, btnStr, CancelarStr, Visible,);

            // Espera hasta que el usuario presione un botón
            string resultado = await InDIalog.ShowAsync();

            // Aquí puedes manejar el resultado

            return resultado;
        }
    }
}
