using BakAppDiego.Components.Globals.Modelos;
using BakAppDiego.Components.Globals.Modelos.Bakapp;
using BakAppDiego.Components.Globals.Modelos.Clases;
using BakAppDiego.Components.Globals.Modelos.Random;
using BakAppDiego.Components.Globals.Modelos.Responses;
using BakAppDiego.Components.Globals.Statics;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BakAppDiego.Components.Modulos_de_funciones
{
    public class ConectarConf
    {
        [Inject] private HttpClient HttpClient { get; set; }
        private FuncionesWebService ComunicacionWB;
        public ConectarConf(){
             HttpClient = new HttpClient();  
             ComunicacionWB = new FuncionesWebService();
        }
        public async Task<MensajeAsync> Sb_Cargar_Datos_De_Configuracion() {
            MensajeAsync MsjRet = new MensajeAsync();
            string sqlQuery = @"Select Top 1 *,NOKOCARAC+'.dbo.' As Global_BaseBk From TABCARAC Where KOTABLA = 'BAKAPP' And KOCARAC = 'BASE'";
            MensajeAsync mensaje = await ComunicacionWB.Sb_GetDataSet_Json(sqlQuery);
            if (mensaje.EsCorrecto)
            {

                var obj = mensaje.Tag;
                try
                {

                    var ax = ComunicacionWB.Fx_DataRow((string)mensaje.Detalle);
                    if (ax != null)
                    {
                       
                        GlobalData.Global_BaseBk = (string)ax["Global_BaseBk"];
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron filas.");
                        MsjRet.EsCorrecto = false;
                        MsjRet.Msg = "No se encontraron filas";
                        return MsjRet;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }

            }
            else
            {
            
                return mensaje;

            }

            mensaje = await Fx_Cargar_Configuracion_Estacion();

            if (mensaje.EsCorrecto == false)
            {
                return mensaje;


            }

            MensajeAsync MsjCargar = await Fx_Cargar_Listas_Precios_Por_Usuario();
            if (MsjCargar.EsCorrecto ==  false) {
                return MsjCargar;


            }
            MensajeAsync MsjConf = await Fx_Cargar_Configuracion_Estacion_Y_General();
            if (MsjConf.EsCorrecto == false)
            {
                return MsjConf;


            }
            MensajeAsync MsjMonedas = await Sb_Cargar_Modedas();
            if (MsjMonedas.EsCorrecto == false)
            {
                return MsjMonedas;


            }
            MensajeAsync MsjTMP = await Sb_Revisar_Carptea_Tmp_Servidor();
            if (MsjTMP.EsCorrecto == false)
            {
                return MsjTMP;


            }
            MensajeAsync MsjDocDestino = await Fx_Cargar_Sis_DespachoSimple_DocDestino();
            if (MsjDocDestino.EsCorrecto == false)
            {
                return MsjDocDestino;


            }
            MensajeAsync MsjDespachoSimple = await Fx_Cargar_Sis_DespachoSimple_Tipo();
            if (MsjDespachoSimple.EsCorrecto == false)
            {
                return MsjDespachoSimple;


            }
            MensajeAsync MsjDespachoSimpleTP = await Fx_Cargar_Sis_DespachoSimple_TipoPago();
            if (MsjDespachoSimpleTP.EsCorrecto == false)
            {
                return MsjDespachoSimpleTP;


            }
            MsjRet.EsCorrecto = true;
            MsjRet.Msg = "Se cargaron todos los datos de configuracion";
            return MsjRet;




        }
        public async Task<MensajeAsync> Fx_Cargar_Sis_DespachoSimple_DocDestino()
        {
            MensajeAsync Retorno = new MensajeAsync();
            string Consulta_sql = $@"Select * From {GlobalData.Global_BaseBk}Zw_TablaDeCaracterizaciones Where Tabla = 'SIS_DESPACHOSIMPLE_DOCDESTINO'";
            MensajeAsync respuesta = await ComunicacionWB.Sb_GetDataSet_Json(Consulta_sql);
            if (respuesta.EsCorrecto)
            {
                Zw_TablaDeCaracterizacionesResponse? Res = JsonConvert.DeserializeObject<Zw_TablaDeCaracterizacionesResponse>(respuesta.Detalle);
                if (Res != null) {
                    GlobalData.DocDestino = Res.Table[0];
                    Retorno.EsCorrecto = true;
                    Retorno.Msg = "Tabla guardada y creada con exito";
                }
                
            }
            else { 
                Retorno.EsCorrecto = false;
                Retorno.Msg = respuesta.Msg;

            }
            return Retorno;


        }
        private async Task<MensajeAsync> Fx_Cargar_Sis_DespachoSimple_TipoPago()
        {
            MensajeAsync Retorno = new MensajeAsync();

            
            string Consulta_sql = $@"Select * From  {GlobalData.Global_BaseBk}Zw_TablaDeCaracterizaciones Where Tabla = 'SIS_DESPACHOSIMPLE_TIPOPAGO'";
            MensajeAsync respuesta = await ComunicacionWB.Sb_GetDataSet_Json(Consulta_sql);
            if (respuesta.EsCorrecto)
            {
                Zw_TablaDeCaracterizacionesResponse? Res = JsonConvert.DeserializeObject<Zw_TablaDeCaracterizacionesResponse>(respuesta.Detalle);
                if (Res != null) {
                    GlobalData.TablaDeCaracterizacionesTipoPago = Res.Table[0];
                    Retorno.EsCorrecto = true;
                    Retorno.Msg = "Tabla guardada y creada con exito";
                }
                
            }
            else
            {
                Retorno.EsCorrecto = false;
                Retorno.Msg = respuesta.Msg;

            }
            return Retorno;



        }
        private async Task<MensajeAsync> Fx_Cargar_Sis_DespachoSimple_Tipo()
        {
            MensajeAsync Retorno = new MensajeAsync();


            string Consulta_sql = $@"Select * From  {GlobalData.Global_BaseBk}Zw_TablaDeCaracterizaciones Where Tabla = 'SIS_DESPACHOSIMPLE_TIPO'";
            MensajeAsync respuesta = await ComunicacionWB.Sb_GetDataSet_Json(Consulta_sql);
            if (respuesta.EsCorrecto)
            {
                Zw_TablaDeCaracterizacionesResponse? Res = JsonConvert.DeserializeObject<Zw_TablaDeCaracterizacionesResponse>(respuesta.Detalle);
                if (Res != null)
                {
                    GlobalData.TablaDeCaracterizacionesTipo = Res.Table[0];
                    Retorno.EsCorrecto = true;
                    Retorno.Msg = "Tabla guardada y creada con exito";
                }
            }
            else
            {
                Retorno.EsCorrecto = false;
                Retorno.Msg = respuesta.Msg;

            }
            return Retorno;



        }
        private async Task<MensajeAsync> Sb_Revisar_Carptea_Tmp_Servidor() {
            MensajeAsync mensajeAsync = await ComunicacionWB.Fx_HttJob_Ws_Sb_RevCarpetaTmp();
            if (mensajeAsync.EsCorrecto)
            {

                string response = mensajeAsync.Detalle;
                List<Dictionary<string, object>> listdic = ComunicacionWB.Fx_DataTable(response);
                string existeRuta = "false";
                
                Dictionary<string, object> dic = listdic[0];

                existeRuta = (string)dic["ExisteRuta"];


                
             
                        
                MensajeAsync Respuesta = new MensajeAsync();
                if (existeRuta == "True")
                {
                    Respuesta.EsCorrecto = true;
                    Respuesta.Msg = "Existe";
                    return Respuesta;

                }
                else {
                    Respuesta.EsCorrecto = false;
                    Respuesta.Msg = "NoExiste";
                    return Respuesta;


                }
                


            }
            else { 
                MensajeAsync MensajeError = new MensajeAsync();
                MensajeError.EsCorrecto = false;
                MensajeError.Msg = "Error al revisar la carpeta"; 




            }
            return mensajeAsync;



        }

        private async Task<MensajeAsync> Sb_Cargar_Modedas() {

            string Consulta_Sql = "Select TOP 1 * From TABMO Where KOMO = '$'";
            MensajeAsync respuesta = await ComunicacionWB.Sb_GetDataSet_Json(Consulta_Sql);
            if (respuesta.EsCorrecto)
            {
                TABMOResponse? Response = JsonConvert.DeserializeObject<TABMOResponse>(respuesta.Detalle);
                
                if (Response != null) { GlobalData.Moneda = Response.Table[0]; }




            }
            else{ 
                MensajeAsync MsjError = new MensajeAsync(); 
                MsjError.EsCorrecto = false;
                MsjError.Msg = "Ocurrio un error definiendo la moneda";
                return MsjError;
            
            }

            //trae el ultimo registro del valor del dolar
            Consulta_Sql = "SELECT  top 1 *  FROM MAEMO WHERE KOMO = 'US$' ORDER BY IDMAEMO DESC;";
            respuesta = await ComunicacionWB.Sb_GetDataSet_Json(Consulta_Sql);
            if (respuesta.EsCorrecto)
            {
                MAEOMResponse? Response = JsonConvert.DeserializeObject<MAEOMResponse>(respuesta.Detalle);
                if (Response != null) { 
                GlobalData.MonedaDolar = Response.Table[0];
                }




            }
            else
            {
                MensajeAsync MsjError = new MensajeAsync();
                MsjError.EsCorrecto = false;
                MsjError.Msg = "Ocurrio un error definiendo la moneda";
                return MsjError;

            }
            //trae el ultimo registro del valor del uf

            Consulta_Sql = "SELECT TOP 1 *  FROM MAEMO WHERE KOMO = 'UF' ORDER BY IDMAEMO DESC;";
            respuesta = await ComunicacionWB.Sb_GetDataSet_Json(Consulta_Sql);
            if (respuesta.EsCorrecto)
            {
                MAEOMResponse? Response = JsonConvert.DeserializeObject<MAEOMResponse>(respuesta.Detalle);
                if (Response != null)
                {
                    GlobalData.UF = Response.Table[0];
                }



            }
            else
            {
                MensajeAsync MsjError = new MensajeAsync();
                MsjError.EsCorrecto = false;
                MsjError.Msg = "Ocurrio un error definiendo las monedas";
                return MsjError;

            }
            return respuesta;



         }
        private async Task<MensajeAsync> Fx_Cargar_Listas_Precios_Por_Usuario() {

            string Usuario_X_Defecto = GlobalData.Usuario_Activo!.Kofu!;
            string Consulta_Sql = $@"
            Select KOLT As Kolt, KOLT + '-' + NOKOLT As Nokolt
            From TABPP
            Where KOLT In (
                Select SUBSTRING(KOOP, 4, 3)
                From MAEUS
                Where KOUS = '{Usuario_X_Defecto}' And KOOP LIKE 'LI-%'
            ) And TILT = 'P'
            Order By Nokolt";

            MensajeAsync respuesta = await ComunicacionWB.Sb_GetDataSet_Json(Consulta_Sql);
            if (respuesta.EsCorrecto) {
                PreciosUsuario? Response = JsonConvert.DeserializeObject<PreciosUsuario>(respuesta.Detalle);
                MensajeAsync salida = new MensajeAsync();
                if (Response != null)
                {
                    GlobalData.Listas_precios_usuarios = Response.Table;
                    
                    salida.Msg = "PRecios cargados correctamente";
                    salida.EsCorrecto = true;
                }

                return salida;

            }
            else { 
                MensajeAsync salida =  new MensajeAsync();
                salida.Msg = "El usuario no posee permisos para ninguna lista de precios en Random.";
                salida.EsCorrecto = false;
                return salida;


            }



        }
        private async Task<MensajeAsync> Fx_Cargar_Configuracion_Estacion_Y_General() {
            string Empresa = GlobalData.EstacionBk!.Empresa_X_Defecto!;       
            string Modalidad = GlobalData.EstacionBk.Modalidad_X_Defecto;    
            string? Global_BaseBk = GlobalData.Global_BaseBk;
            string JSONResponse;
            string Consulta_Sql = @$"
            Select Top 1 *, Getdate() As Fecha_Servidor 
            From CONFIEST
            Inner Join {Global_BaseBk}Zw_Configuracion 
            On Empresa = EMPRESA And Modalidad = '{Modalidad}'
            Where EMPRESA = '{Empresa}' And MODALIDAD = '{Modalidad}'";
            MensajeAsync respuesta = await ComunicacionWB.Sb_GetDataSet_Json(Consulta_Sql);
            if (respuesta.EsCorrecto) {
                JSONResponse = respuesta.Detalle;
                ResponseConifgEstacion? Respuesta = JsonConvert.DeserializeObject<ResponseConifgEstacion>(JSONResponse);
                if (Respuesta != null) { 
                GlobalData.ConfiguracionEstacion = Respuesta!.Table![0];

                }

            } else {


                MensajeAsync MsjError = new MensajeAsync();
                MsjError.Msg = "Error al cargar la configuracion";
                MsjError.EsCorrecto = false;

                return MsjError;

            }

            Consulta_Sql = @$"
    Select
        Empresa, 
        Pr_AutoPr_Crear_Codigo_Principal_Automatico, 
        Pr_AutoPr_Correlativo_Por_Iniciales, 
        Pr_AutoPr_Correlativo_General,
        Pr_AutoPr_Tablas_Para_Iniciales_Cod_Automatico, 
        Pr_AutoPr_Max_Cant_Caracteres_Del_Codigo, 
        Pr_AutoPr_Ultimo_Codigo_Creado_Correlativo_General,
        Pr_Desc_Producto_Solo_Mayusculas, 
        Pr_Creacion_Exigir_Precio, 
        Pr_Creacion_Exigir_Clasificacion_busqueda, 
        Pr_Creacion_Exigir_Codigo_Alternativo,
        Tbl_Ranking, 
        Revisa_Taza_Cambio, 
        Revisar_Taza_Solo_Mon_Extranjeras, 
        Vnta_Dias_Venci_Coti, 
        Vnta_TipoValor_Bruto_Neto, 
        Vnta_EntidadXdefecto,
        Vnta_SucEntXdefecto, 
        Vnta_Producto_NoCreado, 
        Vnta_Preguntar_Documento, 
        SOC_CodTurno, 
        SOC_Buscar_Producto, 
        SOC_Aprueba_Solo_G1,
        SOC_Aprueba_G1_y_G2, 
        SOC_Prod_Crea_Solo_Marcas_Proveedor, 
        SOC_Prod_Crea_Max_Carac_Nom, 
        SOC_Valor_1ra_Aprobacion, 
        SOC_Dias_Apela,
        SOC_Tipo_Creacion_Producto_Normal_Matriz, 
        Precio_Costos_Desde, 
        Precios_Venta_Desde_Random, 
        Precios_Venta_Desde_BakApp,
        Vnta_Redondear_Dscto_Cero, 
        Nodo_Raiz_Asociados, 
        Vnta_Ofrecer_Otras_Bod_Stock_Insuficiente, 
        Conservar_Responzable_Doc_Relacionado,
        Preguntar_Si_Cambia_Responsable_Doc_Relacionado, 
        ServTecnico_Empresa, 
        ServTecnico_Sucursal, 
        ServTecnico_Bodega
    From {Global_BaseBk}Zw_Configuracion
    Where Modalidad_General = 1";
            respuesta = await ComunicacionWB.Sb_GetDataSet_Json(Consulta_Sql);
            if (respuesta.EsCorrecto)
            {
                JSONResponse = respuesta.Detalle;
                Zw_EstacionesBkpResponse? Respuesta = JsonConvert.DeserializeObject<Zw_EstacionesBkpResponse>(JSONResponse);
                if (Respuesta != null)
                {
                    GlobalData.Configuracion_General = Respuesta.Table[0];
                }

            }
            else { 
            
                MensajeAsync MsjError = new MensajeAsync();
                MsjError.Msg = "Error al cargar la configuracion general";
                MsjError.EsCorrecto = false;
                
                return MsjError;


            }
            MensajeAsync Exitoso = new MensajeAsync();
            Exitoso.Msg = "Se cargaron exitosamente las configuraciones";
            Exitoso.EsCorrecto = true;

            return Exitoso;
            
        }

        private async Task<MensajeAsync> Fx_Cargar_Configuracion_Estacion() {
            //Decomentar lo de abajo para obtener la respueta real, esto es solo para caso de prueba.
            //string Consulta_Sql = $@"Select * From  {GlobalData.Global_BaseBk}Zw_EstacionesBkp Where NombreEquipo = ' {GlobalData.Id_dispositivo} ' And TipoEstacion = 'B4A'";
            string Consulta_Sql = $@"Select * From  {GlobalData.Global_BaseBk}Zw_EstacionesBkp Where NombreEquipo = 'baca536c8d75bf5f' And TipoEstacion = 'B4A'";
            MensajeAsync respuesta = await ComunicacionWB.Sb_GetDataSet_Json(Consulta_Sql);
            if (respuesta.EsCorrecto)
            {
                HttpResponseMessage auxms = (HttpResponseMessage)respuesta.Tag;
                string JSONResponse = respuesta.Detalle;
                //Zw_EstacionesBkpResponse JSONResponse = await auxms.Content.ReadFromJsonAsync<Zw_EstacionesBkpResponse>();
                try
                {
                    Zw_EstacionesBkpResponse? Respuesta = JsonConvert.DeserializeObject<Zw_EstacionesBkpResponse>(JSONResponse);
                    MensajeAsync Res = new MensajeAsync();

                    if (Respuesta != null)
                    {
                        Zw_EstacionesBkp aux = Respuesta.Table[0];
                        GlobalData.EstacionBk = aux;
                        Res.Msg = "Cargada la tabla Zw_EstacionesBkp con exito";
                        Res.EsCorrecto = true;
                        return Res;
                    }
                    else {
                        return Res;
                    }
                    

                }
                catch (Exception ex) {

                    Console.WriteLine(ex.ToString());   
                    return respuesta;


                }
                
               
            }
            else
            {
                if (respuesta.ErrorDeConexionSQL)
                {
                    MensajeAsync Res = new MensajeAsync();
                    Res.Msg = "falla de conexion";
                    Res.EsCorrecto = false;
                    return Res;
                }
                else {
                    MensajeAsync Res = new MensajeAsync();
                    Res.Msg = "Usuario no encontrado";
                    Res.EsCorrecto = false;
                    return Res;


                }
            }


            }


    }
}
