using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;

namespace CuidadoAutomotrizApp.Pages
{
    public partial class BenefitsPage : ContentPage
    {
        public BenefitsPage()
        {
            InitializeComponent();
        }

        private async void OnContinuarClicked(object sender, EventArgs e)
        {
            try
            {
                // Deshabilitar el botón mientras se procesa la navegación
                ((Button)sender).IsEnabled = false;

                // Navegar a la página de registro
                await Navigation.PushAsync(new RegistrationPage());

                // Habilitar el botón nuevamente
                ((Button)sender).IsEnabled = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", 
                    "Hubo un problema al cargar la página de registro. Por favor, intenta nuevamente.", 
                    "OK");
                ((Button)sender).IsEnabled = true;
            }
        }

        private async void OnBuscarMecanicoClicked(object sender, EventArgs e)
        {
            try
            {
                // Deshabilitar el botón mientras se procesa la solicitud
                ((Button)sender).IsEnabled = false;

                // Solicitar permiso de ubicación
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permiso Requerido", 
                        "Necesitamos acceso a tu ubicación para buscar mecánicos cercanos.", 
                        "OK");
                    ((Button)sender).IsEnabled = true;
                    return;
                }

                // Obtener la ubicación actual
                var location = await Geolocation.GetLastKnownLocationAsync();
                
                if (location == null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    location = await Geolocation.GetLocationAsync(request);
                }

                if (location != null)
                {
                    // Construir la URL de Google Maps para buscar mecánicos cercanos
                    string searchQuery = "mecanicos+automotrices";
                    string url = $"https://www.google.com/maps/search/{searchQuery}/@{location.Latitude},{location.Longitude},15z";
                    
                    // Abrir Google Maps en el navegador
                    await Launcher.OpenAsync(new Uri(url));
                }
                else
                {
                    await DisplayAlert("Error", 
                        "No se pudo obtener tu ubicación actual. Por favor, verifica que el GPS esté activado.", 
                        "OK");
                }
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Error", 
                    "Tu dispositivo no soporta la función de geolocalización.", 
                    "OK");
            }
            catch (PermissionException)
            {
                await DisplayAlert("Error", 
                    "No tenemos permiso para acceder a tu ubicación.", 
                    "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", 
                    "Hubo un problema al buscar mecánicos cercanos. Por favor, intenta nuevamente.", 
                    "OK");
            }
            finally
            {
                // Asegurarse de que el botón se habilite nuevamente
                ((Button)sender).IsEnabled = true;
            }
        }
    }
}