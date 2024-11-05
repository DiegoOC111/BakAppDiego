using BakAppDiego.Components.Globals.Modelos;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using BakAppDiego.Components.Dialogs;
using System.Collections.Generic;

namespace BakAppDiego.Components.Pages
{
    public partial class Inventario
    {
        private bool isTableVisible = false;
        private bool iniciado = false;
        InventarioItem inventarioItem;
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

            MensajeAsync ms = await ComunicacionWB.Sb_Inv_BuscarInventario("4");
            List < InventarioItem > inventarioItems = new List < InventarioItem >();
            inventarioItems = await ObtenerDatosInventario(ms.Detalle);
            inventarioItem = inventarioItems.FirstOrDefault();
            iniciado = true;

        }
    }
}