using Microsoft.Maui.Controls;

namespace CuidadoAutomotrizApp.Pages
{
    public partial class ServicePage : ContentPage
    {
        public ServicePage()
        {
            InitializeComponent();
        }

        private async void OnContactoClicked(object sender, EventArgs e)
        {
            try
            {
                // Deshabilitar el botón y mostrar indicador de carga
                ((Button)sender).IsEnabled = false;
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                // Simular un pequeño delay para mostrar el proceso
                await Task.Delay(1000);

                // Navegar a la página de contacto
                await Navigation.PushAsync(new ContactPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert(
                    "Error",
                    "Hubo un problema al cargar la página de contacto. Por favor, intenta nuevamente.",
                    "OK"
                );
            }
            finally
            {
                // Asegurarse de que el botón se habilite y el indicador se oculte
                ((Button)sender).IsEnabled = true;
                LoadingIndicator.IsVisible = false;
                LoadingIndicator.IsRunning = false;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            // Mostrar un mensaje de confirmación si el usuario intenta regresar
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool leave = await DisplayAlert(
                    "Confirmación",
                    "¿Deseas volver al inicio de la aplicación?",
                    "Sí",
                    "No"
                );

                if (leave)
                {
                    // Si el usuario confirma, navegar al inicio
                    await Navigation.PopToRootAsync();
                }
            });

            // Prevenir la navegación hacia atrás por defecto
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Verificar si es la primera vez que se muestra la página
            if (!Application.Current.Properties.ContainsKey("ServicePageShown"))
            {
                // Mostrar mensaje de bienvenida
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert(
                        "¡Bienvenido!",
                        "Aquí encontrarás todas las características y servicios que ofrecemos para mantener tu vehículo en óptimas condiciones.",
                        "Entendido"
                    );
                });

                // Marcar que la página ya se ha mostrado
                Application.Current.Properties["ServicePageShown"] = true;
            }
        }
    }
}