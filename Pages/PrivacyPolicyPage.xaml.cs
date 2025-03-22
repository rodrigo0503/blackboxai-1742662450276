using Microsoft.Maui.Controls;

namespace CuidadoAutomotrizApp.Pages
{
    public partial class PrivacyPolicyPage : ContentPage
    {
        public PrivacyPolicyPage()
        {
            InitializeComponent();
        }

        private async void OnAceptarClicked(object sender, EventArgs e)
        {
            try
            {
                // Deshabilitar el botón y mostrar indicador de carga
                ((Button)sender).IsEnabled = false;
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                // Simular un pequeño delay para mostrar el proceso de aceptación
                await Task.Delay(1000);

                // Mostrar mensaje de confirmación
                bool userAccepted = await DisplayAlert(
                    "Confirmación",
                    "Al aceptar, confirmas que has leído y comprendido nuestra política de privacidad. ¿Deseas continuar?",
                    "Sí, Acepto",
                    "Cancelar"
                );

                if (userAccepted)
                {
                    // Si el usuario acepta, navegar a la página de servicios
                    await Navigation.PushAsync(new ServicePage());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(
                    "Error",
                    "Hubo un problema al procesar tu aceptación. Por favor, intenta nuevamente.",
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
            // Mostrar un mensaje de advertencia si el usuario intenta regresar
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool leave = await DisplayAlert(
                    "Advertencia",
                    "Debes aceptar la política de privacidad para continuar usando la aplicación. ¿Deseas salir?",
                    "Sí, Salir",
                    "No, Quedarme"
                );

                if (leave)
                {
                    // Si el usuario decide salir, navegar al inicio o cerrar la aplicación
                    await Navigation.PopToRootAsync();
                }
            });

            // Prevenir la navegación hacia atrás por defecto
            return true;
        }
    }
}