using BakAppDiego.Components.Globals.Modelos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Pages
{
    public partial class MenuPrincipal
    {
        private NavigationManager NavigationManager { get; set; }
        private FuncionesWebService ComunicacionWB { get; set; }

        protected override void OnInitialized() {

            ComunicacionWB = new FuncionesWebService(); 


        }
        
            async Task TraerEntidad()
        {
            // here're other async action calls
            string sqlQuery = @"Select Top 1 *,NOKOCARAC+'.dbo.' As Global_BaseBk From TABCARAC Where KOTABLA = 'BAKAPP' And KOCARAC = 'BASE'";
            MensajeAsync mensaje =  await ComunicacionWB.Sb_GetDataSet_Json(sqlQuery);
        }


    }
}
