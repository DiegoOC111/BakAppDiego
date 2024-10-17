﻿using BakAppDiego.Components.Globals.TablasBackApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using System.Text.Json;

namespace BakAppDiego.Components.Globals.Statics
{
    public static class GlobalData
    {
        public static string Ip_Wb { get; set; }
        public static TABFU? usuario {get; set;}
        public static void  GuardarIP() {
            string json = JsonSerializer.Serialize(usuario);
            // Guardar el JSON en Preferences
            Preferences.Set("Ip", Ip_Wb);
            


        } public static void  GuardarTABFU() {
            string json = JsonSerializer.Serialize(usuario);
            
            Preferences.Set("TABFU", json);


        }
        public static void Cargar()
        {
            // Obtener el JSON de Preferences
            Ip_Wb = Preferences.Get("Ip", null);
            string jsonGuardado = Preferences.Get("TABFU", null);

            if (jsonGuardado != null)
            {
                
                // Deserializar el JSON a un objeto anónimo
                usuario = JsonSerializer.Deserialize<TABFU>(jsonGuardado);
              
            }
        }

    }

}
