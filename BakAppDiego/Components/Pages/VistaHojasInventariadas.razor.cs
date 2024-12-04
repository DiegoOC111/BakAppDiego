using BakAppDiego.Components.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakAppDiego.Components.Dialogs;
using BakAppDiego.Components.Globals.Modelos;
using BakAppDiego.Components.Globals.Statics;
using Microsoft.AspNetCore.Components;
using BakAppDiego.Components.Globals.Modelos.Responses;
using Newtonsoft.Json;
using static BakAppDiego.Components.Globals.Modelos.Responses.RespuestaHoja;

namespace BakAppDiego.Components.Pages
{
    public partial class VistaHojasInventariadas
    {
        public required System.Timers.Timer longClickTimer;

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private FuncionesWebService ComunicacionWB;
        private DialogoService Dialogo;
        private Ls_Zw_Inv_Hoja Lista;
        private List<Zw_Inv_Hoja> ListaHojas;
        private bool inciado = false;
        protected override async void OnInitialized()
        {
            ComunicacionWB = new FuncionesWebService();
            inciado = false;
            Dialogo = new DialogoService();

            MensajeAsync Respuesta = await ComunicacionWB.Sb_Inv_BuscarHojas(GlobalData.InventarioActivo.Id);
            if (Respuesta.EsCorrecto) {

               Lista =  JsonConvert.DeserializeObject<Ls_Zw_Inv_Hoja>(Respuesta.Detalle);
                ListaHojas = Lista.Table;
                inciado = true;
                StateHasChanged();

            }

        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                StateHasChanged();
            }
        }
        private void StartLongClick(Zw_Inv_Hoja producto)
        {
            Zw_Inv_Hoja selectedProducto = producto;
            longClickTimer = new System.Timers.Timer(500); // Duración del "long click" en ms
            longClickTimer.Elapsed += (sender, args) =>
            {
                longClickTimer.Stop();
                InvokeAsync(() => HandleLongClick(producto));
            };
            longClickTimer.Start();
        }


        private void CancelLongClick()
        {
            longClickTimer?.Stop();
            longClickTimer = null;
        }


        private async Task HandleLongClick(Zw_Inv_Hoja producto)

        {
            string[] str = { "Editar Cantidad", "Editar Comentario", "Borrar Inventariado", "Volver" };
            string accion = await Dialogo.DisplayActionSheet("Que desea Hacer", null, null, str);
            if (accion == null)
            {

                return;
            }
            //Editar 
            else if (accion == str[0])
            {

                return;

            }
            //Comentar
            else if (accion == str[1])
            {

                return;


            }
            //Borrar
            else if (accion == str[2])
            {

                return;

            }
            return;
        }


    }
}
