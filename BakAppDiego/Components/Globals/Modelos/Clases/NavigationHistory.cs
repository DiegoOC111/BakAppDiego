using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakAppDiego.Components.Globals.Modelos.Clases
{
    public static class NavigationHistory
    {
        private static Stack<string> _history = new();

        public static void AddToHistory(string url) => _history.Push(url);

        public static string? GetPreviousUrl()
        {
            if (_history.Count > 1)
            {
                _history.Pop(); // Remover la actual
                return _history.Peek(); // Obtener la anterior
            }
            return null;
        }
    }
}
