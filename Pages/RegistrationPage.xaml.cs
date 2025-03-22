using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;

namespace CuidadoAutomotrizApp.Pages
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void OnRegistrarClicked(object sender, EventArgs e)
        {
            try
            {
                // Deshabilitar el botón y mostrar indicador de carga
                ((Button)sender).IsEnabled = false;
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                // Validar campos vacíos
                if (string.IsNullOrWhiteSpace(NombreEntry.Text))
                {
                    await DisplayAlert("Error", "Por favor, ingresa tu nombre.", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(EmailEntry.Text))
                {
                    await DisplayAlert("Error", "Por favor, ingresa tu correo electrónico.", "OK");
                    return;
                }

                // Validar formato de correo electrónico
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(EmailEntry.Text, emailPattern))
                {
                    await DisplayAlert("Error", "Por favor, ingresa un correo electrónico válido.", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(ModeloEntry.Text))
                {
                    await DisplayAlert("Error", "Por favor, ingresa el modelo de tu automóvil.", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(MarcaEntry.Text))
                {
                    await DisplayAlert("Error", "Por favor, ingresa la marca de tu automóvil.", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(AnioEntry.Text))
                {
                    await DisplayAlert("Error", "Por favor, ingresa el año de tu automóvil.", "OK");
                    return;
                }

                // Validar que el año sea un número válido
                if (!int.TryParse(AnioEntry.Text, out int anio))
                {
                    await DisplayAlert("Error", "Por favor, ingresa un año válido.", "OK");
                    return;
                }

                // Validar que el año esté en un rango razonable (por ejemplo, entre 1950 y el año actual + 1)
                int currentYear = DateTime.Now.Year;
                if (anio < 1950 || anio > currentYear + 1)
                {
                    await DisplayAlert("Error", 
                        $"Por favor, ingresa un año entre 1950 y {currentYear + 1}.", 
                        "OK");
                    return;
                }

                // Si todas las validaciones pasan, guardar los datos (aquí podrías implementar el guardado real)
                // Por ahora, simplemente navegamos a la siguiente página

                // Simular un pequeño delay para mostrar el proceso
                await Task.Delay(1000);

                // Navegar a la página de política de privacidad
                await Navigation.PushAsync(new PrivacyPolicyPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", 
                    "Hubo un problema al procesar el registro. Por favor, intenta nuevamente.", 
                    "OK");
            }
            finally
            {
                // Asegurarse de que el botón se habilite y el indicador se oculte
                ((Button)sender).IsEnabled = true;
                LoadingIndicator.IsVisible = false;
                LoadingIndicator.IsRunning = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Limpiar los campos cuando la página aparece
            NombreEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            ModeloEntry.Text = string.Empty;
            MarcaEntry.Text = string.Empty;
            AnioEntry.Text = string.Empty;
        }
    }
}