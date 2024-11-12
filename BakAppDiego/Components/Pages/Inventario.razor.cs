using BakAppDiego.Components.Globals.Modelos;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using BakAppDiego.Components.Dialogs;
using System.Collections.Generic;
using BakAppDiego.Components.Globals.Statics;

namespace BakAppDiego.Components.Pages
{
    public partial class Inventario
    {
        PopUpConfirmar PopUp;
        private bool isTableVisible = false;
        private bool iniciado = false;
        InventarioItem inventarioItem;
        private DialogoService Dialogo;
        // Inyección de NavigationManager
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private FuncionesWebService ComunicacionWB;
        

        private void ToggleTableVisibility()
        {
            isTableVisible = !isTableVisible;
            
        }

        protected override void OnInitialized()
        {

            ComunicacionWB = new FuncionesWebService();
          

        }
        public async Task<List<InventarioItem>> ObtenerDatosInventario(string json)
        {
            var inventarioData = JsonSerializer.Deserialize<InventarioData>(json);
            return inventarioData?.Table ?? new List<InventarioItem>();
        }
        public async Task boton() {
            Dialogo = new DialogoService();
            string Respuesta = await Dialogo.DisplayText("Ingrese la id del inventario", "Id ", "Aceptar", "Cerrar");
            MensajeAsync ms = await ComunicacionWB.Sb_Inv_BuscarInventario(Respuesta);
            List < InventarioItem > inventarioItems = new List < InventarioItem >();
            if (ms.EsCorrecto)
            {
                inventarioItems = await ObtenerDatosInventario(ms.Detalle);
                inventarioItem = inventarioItems.FirstOrDefault();
                iniciado = true;
                GlobalData.InventarioActivo = inventarioItem;
                ToggleTableVisibility();
            }
            else {

                string msj = ms.Msg;
                try
                {
                    bool R = await MostrarPopUp("Error", "No se encontro la tabla", "Continuar", " ", false);


                    if (R)
                    {


                    }


                }
                catch (Exception ex) { 
                
                    Console.WriteLine(ex.ToString());
                
                }
               
            }
            


        }
        public async Task boton_inventariado() {
            NavigationManager.NavigateTo("/Inventariado");


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
    }
}