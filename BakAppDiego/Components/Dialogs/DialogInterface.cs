using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Dialogs
{
    internal interface DialogInterface
    {
        Task<bool> DisplayConfirm(string titulo, string mensaje, string aceptar, string cerrar);
    }
}
