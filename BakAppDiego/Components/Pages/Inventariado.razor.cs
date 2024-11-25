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
        private ObjInventar inventar;
        private InputObjetos InObj;
        private DialogoService Dialogo;
        private Zw_Inv_Sector SectorActivo;
        private FuncionesWebService ComunicacionWB;
        Zw_Inv_Contador c1 = new Zw_Inv_Contador();
        Zw_Inv_Contador c2 = new Zw_Inv_Contador();
        private bool iniciado = false;
        private List<Zw_Producto_inventariado> ListaProductos;
        private System.Timers.Timer longClickTimer;
        private Zw_Producto_inventariado selectedProducto;
        protected override void OnInitialized()
        {
            ListaProductos = new List<Zw_Producto_inventariado>();
            ComunicacionWB = new FuncionesWebService();
            Dialogo = new DialogoService();
            GlobalData.menu = true;
            Escaneado = false;
        }
        private async Task EscanearObjeto() {


            List<string> res = await MostrarInputObjeto("Ingrese el objeto", "", "Aceptar", "Cancelar", true);
            string Tipo = res[0];
            string Codigo = res[1];
            MensajeAsync msg = await ComunicacionWB.Sb_Inv_TraerProductoInventario(GlobalData.InventarioActivo.Id, GlobalData.InventarioActivo.Empresa,GlobalData.InventarioActivo.Sucursal, GlobalData.InventarioActivo.Bodega, Tipo,Codigo);
            if (msg.EsCorrecto) {

                ls_Zw_Producto response = JsonSerializer.Deserialize<ls_Zw_Producto>(msg.Detalle);
                Zw_Producto prod = response.Table[0];

                Zw_Producto_inventariado obj = new Zw_Producto_inventariado(prod);
                obj.tipo_esc = Tipo;
                Zw_Producto_inventariado res2 = await MostrarInventarObj("Ingrese el objeto", "", "Aceptar", "Cancelar", true, obj);
                if (res2 != null) {
                    ListaProductos.Add(res2);
                    StateHasChanged();


                }
            }
            
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
        private async Task<Zw_Inv_Contador> MostrarContadores(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible,  ls_Zw_Inv_Contador contadoress)
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
        private async Task<double?> MostrarInput(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible, bool numeric,double defaul)
        {
            InDIalog.Crear(titulo, mensaje, btnStr, CancelarStr, Visible,numeric, defaul);

            // Configura el popup
            //InCon.crear(titulo, mensaje, btnStr, CancelarStr, Visible,);

            // Espera hasta que el usuario presione un botón
            double? resultado = await InDIalog.ShowAsynNumeric();

            // Aquí puedes manejar el resultado

            return resultado;
        }
        private async Task<List<string>> MostrarInputObjeto(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible)
        {
            InObj.Crear(titulo, mensaje, btnStr, CancelarStr, Visible);

            // Configura el popup
            //InCon.crear(titulo, mensaje, btnStr, CancelarStr, Visible,);

            // Espera hasta que el usuario presione un botón
            List<string> resultado = await InObj.ShowAsync();

            // Aquí puedes manejar el resultado

            return resultado;
        }
        private async Task<Zw_Producto_inventariado> MostrarInventarObj(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible, Zw_Producto_inventariado obj)
        {
            inventar.Crear(titulo, mensaje, btnStr, CancelarStr, Visible, obj);

            // Configura el popup
            //InCon.crear(titulo, mensaje, btnStr, CancelarStr, Visible,);

            // Espera hasta que el usuario presione un botón
            Zw_Producto_inventariado resultado = await inventar.ShowAsync();

            // Aquí puedes manejar el resultado

            return resultado;
        }
       

        private void StartLongClick(Zw_Producto_inventariado producto, int index)
        {
            selectedProducto = producto;
            longClickTimer = new System.Timers.Timer(500); // Duración del "long click" en ms
            longClickTimer.Elapsed += (sender, args) =>
            {
                longClickTimer.Stop();
                InvokeAsync(() => HandleLongClick(producto, index));
            };
            longClickTimer.Start();
        }

        private void CancelLongClick()
        {
            longClickTimer?.Stop();
            longClickTimer = null;
        }

        private async Task HandleLongClick(Zw_Producto_inventariado producto, int index)
          
        {
            string[] str = { "Editar Cantidad", "Editar Comentario", "Borrar Inventariado", "Volver" };
            string accion = await Dialogo.DisplayActionSheet("Que desea Hacer",null,null, str);
            if (accion == null)
            {

                return;
            }
            //Editar 
            else if (accion == str[0]) {
                await EditNumero(producto);

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
        private async Task EditNumero(Zw_Producto_inventariado producto) {
            double cantidad = producto.Cantidad;
            string text = $" producto : {producto.Descripcion}";
            double? respuesta_double = await MostrarInput(text, "Ingrese la nueva cantidad de inventariado", "Aceptar", "Cancelar", true, true, cantidad);
            if (respuesta_double == null)
            {
                return;

            }
            else
            {

                producto.Cantidad = (double)respuesta_double;
                StateHasChanged();


            }


        }
    }
}
