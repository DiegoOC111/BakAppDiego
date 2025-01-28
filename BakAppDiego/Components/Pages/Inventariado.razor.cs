
using BakAppDiego.Components.Dialogs;
using BakAppDiego.Components.Globals.Modelos;
using BakAppDiego.Components.Globals.Modelos.Bakapp;
using BakAppDiego.Components.Globals.Modelos.Clases;
using BakAppDiego.Components.Globals.Modelos.Responses;
using BakAppDiego.Components.Globals.Statics;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static BakAppDiego.Components.Globals.Modelos.Responses.RespuestaHoja;

namespace BakAppDiego.Components.Pages
{
    public partial class Inventariado
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        public bool MenuAbierto { get; set; }
        private string Sector = "";
        private bool Escaneado;
        private InputContador? InCon;
        private InputDialog? InDIalog;
        private ObjInventar? inventar;
        private InputObjetos? InObj;
        private DialogoService? Dialogo;
        private Zw_Inv_Sector? SectorActivo;
        private FuncionesWebService? ComunicacionWB;
        Zw_Inv_Contador c1 = new Zw_Inv_Contador();
        Zw_Inv_Contador c2 = new Zw_Inv_Contador();
        private bool iniciado = false;
        private List<Zw_Producto_inventariado>? ListaProductos;
        public required System.Timers.Timer longClickTimer;
        private Zw_Producto_inventariado? selectedProducto;
        private bool menuAbierto;
        private LoadingPopUp loadingPopup;
        private PopUpConfirmar PopUp;


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
       
        protected override void OnInitialized()
        {
            NavigationHistory.AddToHistory(NavigationManager.Uri);
            GlobalData.prev = "/InventarioMenu";

            ListaProductos = new List<Zw_Producto_inventariado>();
            ComunicacionWB = new FuncionesWebService();
            Dialogo = new DialogoService();
            GlobalData.menu = true;
            GlobalData.Volver = true;
            GlobalData.mensajevolver = "Desea salir de la hoja, los datos se perderan?";
            Escaneado = false;
        }
        private async Task EscanearObjeto() {

            List<string> res = await MostrarInputObjeto("Ingrese el objeto", "", "Aceptar", "Cancelar", true);

            if (res == null) { return; }
            else
            {
                string Tipo = res[0];
                string Codigo = res[1];
                MensajeAsync msg = await ComunicacionWB!.Sb_Inv_TraerProductoInventario(GlobalData.InventarioActivo!.Id, GlobalData.InventarioActivo.Empresa, GlobalData.InventarioActivo.Sucursal, GlobalData.InventarioActivo.Bodega, Tipo, Codigo);

                if (msg.EsCorrecto)
                {
                    ls_Zw_Producto response = System.Text.Json.JsonSerializer.Deserialize<ls_Zw_Producto>(msg.Detalle)!;
                    Zw_Producto prod = response.Table[0];
                    Zw_Producto_inventariado obj = new Zw_Producto_inventariado(prod);
                    obj.tipo_esc = Tipo;
                    Zw_Producto_inventariado res2 = await MostrarInventarObj("Ingrese el objeto", "", "Aceptar", "Cancelar", true, obj);

                    if (res2 != null)
                    {

                        ListaProductos!.Add(res2);
                        StateHasChanged();

                    }
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
                if (res.Msg == "Tabla vacia") {

                    await MostrarPopUp("Error", "No hay más contadores dispoinbles", "Continuar", " ", false);
                
                }
            }
            else {

                MensajeAsync res = await elijeContador();

                if (res.EsCorrecto) {
                    if (res.Tag != null) {

                        c2 = (Zw_Inv_Contador)res.Tag;

                    }
                }
                if (res.Msg == "Tabla vacia")
                {

                    bool r = await MostrarPopUp("Advertencia", "No hay más contadores dispoinbles, Desea eliminar al contador 2 ? ", "Eliminar", "Cancelar", true);
                    if (r) {
                        c2 = new Zw_Inv_Contador();
                    
                    }
                }
            }
        }

        private async Task EnviarHoja()
        {

            if (ListaProductos.Count < 1) {

                bool a = await MostrarPopUp("Operacion fallida", "No se puede enviar una hoja vacia", "Continuar", " ", false);
                return;
            }
            else {

                loadingPopup.Show();
                StateHasChanged();

                MensajeAsync Respuesta = await SerializarLista();

                if (Respuesta.EsCorrecto) {

                    loadingPopup.Hide();
                    Respuesta_Hoja aux = JsonConvert.DeserializeObject<Respuesta_Hoja>(Respuesta.Detalle);

                    if (aux.EsCorrecto)
                    {
                        bool r = await MostrarPopUp("Operacion Exitosa", $"Hoja Nro : {aux.Numero} creada con exito", "Continuar", " ", false);
                        var previousUrl = NavigationHistory.GetPreviousUrl();

                        

                        NavigationManager.NavigateTo("/Inventario",true,true);
                       
                       
                    }
                    else {
                        loadingPopup.Hide();
                        StateHasChanged(); 

                        if (aux.MensajeError == "La hoja ya fue levantada y enviada anteriormente") {
                            bool x = await MostrarPopUp("Operacion fallida", $"{aux.MensajeError}, Numero de hoja: {aux.Numero}", "Continuar", " ", false);
                            return;

                        }
                       
                        bool a = await MostrarPopUp("Operacion fallida", $"{aux.MensajeError}", "Continuar", " ", false);
                        return;

                    }
                }

                loadingPopup.Hide();
                StateHasChanged();

                bool c = await MostrarPopUp("Operacion fallida", "Ocurrio un error creando la hoja", "Continuar", " ", false);
                StateHasChanged();

            }
        }
       
        private async Task EscanearSector()
        {
            string sector = await MostrarInput("Ingrese el sector", "", "Aceptar", "Cancelar", true);
            if (sector != null) {

                MensajeAsync res = await ComunicacionWB!.Sb_Inv_BuscarSector(sector, GlobalData.InventarioActivo!.Id.ToString())!;
                if (res.EsCorrecto) {

                    ls_Zw_Inv_Sector resCont = System.Text.Json.JsonSerializer.Deserialize<ls_Zw_Inv_Sector>(res.Detalle)!;
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

                }

                bool r = await Dialogo!.DisplayConfirm("Confirmación", "¿Desea agregar un segundo contador ?", "Si", "No");

                if (r)
                {
                    res = await elijeContador();
                    if (res.EsCorrecto)
                    {
                        c2 = (Zw_Inv_Contador)res.Tag;


                    }
                }

                iniciado = true;
                StateHasChanged();

            }
        }
        
        private async Task<MensajeAsync> elijeContador() {

            MensajeAsync res = await ComunicacionWB!.Sb_Inv_BuscarContador(c1.Rut, c2.Rut);
            MensajeAsync Retorno = new MensajeAsync();

            if (res.EsCorrecto)
            {
                ls_Zw_Inv_Contador contadorResponse = System.Text.Json.JsonSerializer.Deserialize<ls_Zw_Inv_Contador>(res.Detalle)!;
                List<string> botones = new List<string>();
                Zw_Inv_Contador? sel = await MostrarContadores("Elija el contador", "", "Cancelar", "", iniciado, contadorResponse);

                Retorno.EsCorrecto = true;
                Retorno.Tag = sel!;
                return Retorno;

            }

            Retorno.EsCorrecto = false;
            Retorno.Msg = res.Msg;

            return Retorno;
        }
        /// <summary>
        /// Muestra en pantalla la lista de contadores.
        /// </summary>
        /// <param name="titulo">El título del diálogo.</param>
        /// <param name="mensaje">El mensaje descriptivo.</param>
        /// <param name="btnStr">Texto del botón de confirmación.</param>
        /// <param name="CancelarStr">Texto del botón de cancelación.</param>
        /// <param name="Visible">Define si el diálogo es visible inicialmente.</param>
        /// <param name="contadoress">Lista de contadores a mostrar.</param>
        /// <returns>
        /// Un objeto <see cref="Zw_Inv_Contador"/> seleccionado por el usuario.
        /// </returns>
        private async Task<Zw_Inv_Contador?> MostrarContadores(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible, ls_Zw_Inv_Contador contadoress)
        {
            InCon!.Crear(titulo, mensaje, btnStr, CancelarStr, Visible, contadoress);
            Zw_Inv_Contador? resultado = await InCon.ShowAsync();

            return resultado;
        }
        /// <summary>
        /// Muestra en pantalla un input personalizado.
        /// </summary>
        /// <param name="titulo">Título del diálogo.</param>
        /// <param name="mensaje">Mensaje a mostrar.</param>
        /// <param name="btnStr">Texto del botón de confirmación.</param>
        /// <param name="CancelarStr">Texto del botón de cancelación.</param>
        /// <param name="Visible">Define si el diálogo es visible.</param>
        /// <returns>
        /// El texto ingresado por el usuario.
        /// </returns>
        private async Task<string> MostrarInput(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible)
        {
            InDIalog!.Crear(titulo, mensaje, btnStr, CancelarStr, Visible);

            
            string? resultado = await InDIalog.ShowAsync();

          

            return resultado!;

        }
        /// <summary>
        /// Muestra en pantalla un input personalizado.
        /// </summary>
        /// <param name="titulo">Título del diálogo.</param>
        /// <param name="mensaje">Mensaje a mostrar.</param>
        /// <param name="btnStr">Texto del botón de confirmación.</param>
        /// <param name="CancelarStr">Texto del botón de cancelación.</param>
        /// <param name="Visible">Define si el diálogo es visible.</param>
        /// <returns>
        /// El texto ingresado por el usuario.
        /// </returns>
        private async Task<string> MostrarInputLong(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible, string comentario)
        {
            InDIalog!.CrearLong(titulo, mensaje, btnStr, CancelarStr, Visible, comentario);

           
            string? resultado = await InDIalog.ShowAsync();

            

            return resultado!;

        }
        /// <summary>
        /// Muestra en pantalla un input personalizado numérico.
        /// </summary>
        /// <param name="titulo">Título del diálogo.</param>
        /// <param name="mensaje">Mensaje a mostrar.</param>
        /// <param name="btnStr">Texto del botón de confirmación.</param>
        /// <param name="CancelarStr">Texto del botón de cancelación.</param>
        /// <param name="Visible">Define si el diálogo es visible.</param>
        /// <param name="numeric">Si es true, permite solo entradas numéricas.</param>
        /// <param name="defaul">Valor predeterminado.</param>
        /// <returns>
        /// Un valor numérico ingresado por el usuario.
        /// </returns>
        private async Task<double?> MostrarInput(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible, bool numeric, double defaul)
        {
            InDIalog.Crear(titulo, mensaje, btnStr, CancelarStr, Visible, numeric, defaul);

            
            double? resultado = await InDIalog.ShowAsynNumeric();


            return resultado;
        }
        /// <summary>
        /// Muestra en pantalla un input personalizado de objetos.
        /// </summary>
        /// <param name="titulo">Título del diálogo.</param>
        /// <param name="mensaje">Mensaje a mostrar.</param>
        /// <param name="btnStr">Texto del botón de confirmación.</param>
        /// <param name="CancelarStr">Texto del botón de cancelación.</param>
        /// <param name="Visible">Define si el diálogo es visible.</param>
        /// <returns>
        /// Una lista de cadenas ingresadas por el usuario.
        /// </returns>
        private async Task<List<string>> MostrarInputObjeto(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible)
        {
            InObj.Crear(titulo, mensaje, btnStr, CancelarStr, Visible);

            
            List<string> resultado = await InObj.ShowAsync();

           

            return resultado;
        }
        /// <summary>
        /// Muestra en pantalla la información de un objeto.
        /// </summary>
        /// <param name="titulo">Título del diálogo.</param>
        /// <param name="mensaje">Mensaje a mostrar.</param>
        /// <param name="btnStr">Texto del botón de confirmación.</param>
        /// <param name="CancelarStr">Texto del botón de cancelación.</param>
        /// <param name="Visible">Define si el diálogo es visible.</param>
        /// <param name="obj">Objeto a inventariar.</param>
        /// <returns>
        /// Un objeto inventariado con los datos actualizados.
        /// </returns>
        private async Task<Zw_Producto_inventariado> MostrarInventarObj(string titulo, string mensaje, string btnStr, string CancelarStr, bool Visible, Zw_Producto_inventariado obj)
        {
            inventar.Crear(titulo, mensaje, btnStr, CancelarStr, Visible, obj);

            

           
            Zw_Producto_inventariado resultado = await inventar.ShowAsync();

           

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
            string accion = await Dialogo.DisplayActionSheet("Que desea Hacer", null, null, str);
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
                await EditComentario(producto);

                return;


            }
            //Borrar
            else if (accion == str[2])
            {

                return;

            }
            return;
        }

        /// <summary>
        /// Edita la cantidad de inventario de un objeto.
        /// </summary>
        /// <param name="producto">El producto cuya cantidad será editada.</param>
        /// <returns>
        /// Una tarea asincrónica que actualiza la cantidad del producto.
        /// </returns>
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
        /// <summary>
        /// Serializa la lista de objetos en formato JSON para enviarla al web service bakapp.
        /// </summary>
        /// <returns>
        /// Un mensaje indicando si la serialización y la preparación para el envío fueron exitosas.
        /// </returns>
        public async Task<MensajeAsync>  SerializarLista()
        {
            MensajeAsync msg = new MensajeAsync();
            List<HojaDetalle> InventariadoHojaDetalle = new List<HojaDetalle>();
            int id;

            if (c2 == null)
            {

                id = 0;
            }
            else { id = c2.Id; }
            foreach (var obj in ListaProductos.Select((value, index) => new { value, index })) {
                 Zw_Producto_inventariado aux = obj.value;
                string indes = (obj.index + 1).ToString();
                
                 HojaDetalle Detalle = new HojaDetalle(1,1,"111",GlobalData.InventarioActivo.Id,GlobalData.InventarioActivo.Empresa,GlobalData.InventarioActivo.Sucursal,GlobalData.InventarioActivo.Bodega,GlobalData.InventarioActivo.FuncionarioCargo,c1.Id, id, indes, SectorActivo.Id,SectorActivo.Sector,"","",aux.Principal,"",0,"", DateTime.Now,aux.Rtu,0,1,aux.Cantidad,aux.Ud1,aux.Cantidad,aux.Ud2,aux.Cantidad,aux.Descripcion,0,"","");



                InventariadoHojaDetalle.Add(Detalle);
            }
            Hoja hoja = new Hoja(1,GlobalData.InventarioActivo.Id, "1" , GlobalData.EstacionBk.NombreEquipo, DateTime.Now,GlobalData.InventarioActivo.FuncionarioCargo,c1.Id, id ,DateTime.Now,false,ListaProductos.Count());
            string json = JsonConvert.SerializeObject(InventariadoHojaDetalle, Formatting.Indented);
            string json2 = hoja.ToJson();
            MensajeAsync creacion = await ComunicacionWB.Sb_Inv_IngresarHojaPrevia(json2);
            if (creacion.EsCorrecto == false) {


                return creacion;
            }
            Respuesta_Hoja aux2 = JsonConvert.DeserializeObject<Respuesta_Hoja>(creacion.Detalle);
            if(aux2.EsCorrecto == false) {
                MensajeAsync r = new MensajeAsync();
                r.EsCorrecto = false;
                r.Msg = aux2.MensajeError;
                return r;
                    }
            int id3 = aux2.Id!.Value;
            Hoja hoja2 = new Hoja(id3, GlobalData.InventarioActivo.Id, aux2.Numero, GlobalData.EstacionBk.NombreEquipo, DateTime.Now, GlobalData.InventarioActivo.FuncionarioCargo, c1.Id, id, DateTime.Now, false, ListaProductos.Count());
            json2 = hoja2.ToJson();
            MensajeAsync Respuesta = await ComunicacionWB.Sb_Inv_IngresarHojaFinal(json2,json);
           


            return Respuesta;
        }


        /// <summary>
        /// Edita el comentario de un objeto en la lista.
        /// </summary>
        /// <param name="producto">El producto cuyo comentario será editado.</param>
        /// <returns>
        /// Una tarea asincrónica que actualiza el comentario del producto.
        /// </returns>
        /// <seealso cref="MostrarInputLong"/>
        private async Task EditComentario(Zw_Producto_inventariado producto)
        {
            string comentario = producto.Comentario;
            string text = $" producto : {producto.Descripcion}";
            string? Respuesta_string = await MostrarInputLong(text, "Edite su comentario del producto", "Aceptar", "Cancelar", true, comentario);
            if (Respuesta_string == null)
            {
                return;

            }
            else
            {

                producto.Comentario = (string)Respuesta_string;
                StateHasChanged();


            }


        }
    }
}
