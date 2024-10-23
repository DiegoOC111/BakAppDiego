using BakAppDiego.Components.Globals.Modelos;
using BakAppDiego.Components.Globals.Statics;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Pages
{
    public partial class MenuPrincipal
    {
        private NavigationManager NavigationManager { get; set; }
        private FuncionesWebService ComunicacionWB { get; set; }
        public string UsuarioActivo;
        public string Modalidad;
        protected override void OnInitialized() {

            ComunicacionWB = new FuncionesWebService(); 


        }
        
            async Task TraerEntidad()
        {
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
                        string ass = (string)ax["Global_BaseBk"];
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron filas.");
                    }
                }
                catch (Exception ex) { 
                
                
                }
                
            }
            else {

                 
            }
        }


    }
}
