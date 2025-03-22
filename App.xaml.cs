using Microsoft.Maui.Controls;

namespace CuidadoAutomotrizApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Configurar la página inicial dentro de una NavigationPage para permitir la navegación
            MainPage = new NavigationPage(new SplashPage())
            {
                BarBackgroundColor = Colors.White,
                BarTextColor = Colors.Black
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}