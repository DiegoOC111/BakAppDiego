using BakAppDiego.Components.Globals.Statics;

namespace BakAppDiego
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            GlobalData.Cargar();
        }

    }
}
