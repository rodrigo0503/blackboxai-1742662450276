using Microsoft.Maui.Controls;

namespace CuidadoAutomotrizApp.Pages
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        private async void OnAceptarClicked(object sender, EventArgs e)
        {
            try
            {
                // Deshabilitar el botón mientras se procesa la navegación
                ((Button)sender).IsEnabled = false;

                // Navegar a la página de beneficios
                await Navigation.PushAsync(new BenefitsPage());

                // Habilitar el botón nuevamente
                ((Button)sender).IsEnabled = true;
            }
            catch (Exception ex)
            {
                // En caso de error, mostrar un mensaje al usuario
                await DisplayAlert("Error", 
                    "Hubo un problema al cargar la siguiente página. Por favor, intenta nuevamente.", 
                    "OK");

                // Asegurarse de que el botón se habilite nuevamente
                ((Button)sender).IsEnabled = true;
            }
        }
    }
}