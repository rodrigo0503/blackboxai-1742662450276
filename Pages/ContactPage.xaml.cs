using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace CuidadoAutomotrizApp.Pages
{
    public partial class ContactPage : ContentPage
    {
        // Base de conocimientos del chatbot
        private readonly Dictionary<string, string> knowledgeBase = new Dictionary<string, string>
        {
            // Sensores
            {"sensor de oxigeno", "El sensor de oxígeno (O2) mide la cantidad de oxígeno en los gases de escape. Una falla puede causar mayor consumo de combustible y emisiones elevadas. Se recomienda revisión cada 80,000 km."},
            {"sensor map", "El sensor MAP (Presión Absoluta del Múltiple) mide la presión del aire en el múltiple de admisión. Si falla, puede causar pérdida de potencia y arranque difícil."},
            {"sensor tps", "El sensor TPS (Posición del Acelerador) monitorea la posición del acelerador. Su falla puede causar aceleración irregular y consumo excesivo de combustible."},
            {"sensor ckp", "El sensor CKP (Posición del Cigüeñal) determina la posición y velocidad del cigüeñal. Si falla, el auto puede no arrancar o detenerse repentinamente."},
            
            // Aceites
            {"aceite motor", "Los aceites más recomendados son:\n• 5W-30: Para uso general y clima moderado\n• 10W-40: Para motores con más de 100,000 km\n• 5W-40: Para alto rendimiento\nSe recomienda cambio cada 5,000-7,500 km."},
            {"aceite transmision", "Para transmisiones automáticas se recomienda ATF Dexron III o VI. Para manuales, aceite 75W-90. El cambio se recomienda cada 40,000-60,000 km."},
            {"aceite sintetico", "El aceite sintético ofrece mejor protección, mayor durabilidad y mejor rendimiento en temperaturas extremas. Aunque es más costoso, puede durar hasta 10,000 km entre cambios."},
            
            // Anticongelantes
            {"anticongelante", "Existen tres tipos principales:\n• IAT (verde): Para autos antiguos\n• OAT (naranja/rojo): Para vehículos modernos\n• HOAT (amarillo): Híbrido para varios modelos\nSe recomienda cambio cada 2 años o 60,000 km."},
            {"refrigerante", "El refrigerante debe mantenerse entre los niveles MIN y MAX. Usar siempre el tipo recomendado por el fabricante. Mezclar 50% refrigerante y 50% agua destilada."},
            
            // Fallas comunes
            {"motor no arranca", "Causas comunes:\n1. Batería descargada\n2. Sensor CKP fallando\n3. Bomba de combustible\n4. Fusibles quemados\nSe recomienda diagnóstico profesional."},
            {"consumo combustible", "Alto consumo puede deberse a:\n1. Sensor O2 fallando\n2. Filtro de aire sucio\n3. Inyectores sucios\n4. Presión incorrecta en llantas"},
            {"ruido motor", "Ruidos anormales pueden indicar:\n1. Falta de aceite\n2. Bandas desgastadas\n3. Rodamientos dañados\n4. Válvulas desajustadas"},
            
            // Respuesta por defecto
            {"default", "No tengo información específica sobre esa consulta. Te sugiero preguntar sobre:\n• Sensores automotrices\n• Tipos de aceite\n• Anticongelantes\n• Fallas comunes\n• Mantenimiento preventivo"}
        };

        public ContactPage()
        {
            InitializeComponent();
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MessageEntry.Text))
                return;

            try
            {
                // Deshabilitar entrada mientras se procesa
                MessageEntry.IsEnabled = false;
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                // Obtener la pregunta del usuario
                string userMessage = MessageEntry.Text.Trim().ToLower();

                // Agregar mensaje del usuario al chat
                AddMessageToChat(userMessage, true);

                // Limpiar entrada
                MessageEntry.Text = string.Empty;

                // Simular procesamiento
                await Task.Delay(1000);

                // Obtener y mostrar respuesta
                string response = GetBotResponse(userMessage);
                AddMessageToChat(response, false);

                // Scroll al final del chat
                await ChatScrollView.ScrollToAsync(ChatMessages, ScrollToPosition.End, true);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", 
                    "Hubo un problema al procesar tu mensaje. Por favor, intenta nuevamente.", 
                    "OK");
            }
            finally
            {
                // Rehabilitar entrada
                MessageEntry.IsEnabled = true;
                LoadingIndicator.IsVisible = false;
                LoadingIndicator.IsRunning = false;
            }
        }

        private string GetBotResponse(string userMessage)
        {
            // Buscar coincidencias en la base de conocimientos
            foreach (var entry in knowledgeBase)
            {
                if (userMessage.Contains(entry.Key))
                {
                    return entry.Value;
                }
            }

            // Si no hay coincidencias específicas, buscar palabras clave
            if (userMessage.Contains("sensor") || userMessage.Contains("falla"))
                return knowledgeBase["sensor de oxigeno"];
            else if (userMessage.Contains("aceite"))
                return knowledgeBase["aceite motor"];
            else if (userMessage.Contains("anticongelante") || userMessage.Contains("refrigerante"))
                return knowledgeBase["anticongelante"];
            else if (userMessage.Contains("problema") || userMessage.Contains("ruido"))
                return knowledgeBase["ruido motor"];

            // Respuesta por defecto
            return knowledgeBase["default"];
        }

        private void AddMessageToChat(string message, bool isUser)
        {
            var frame = new Frame
            {
                Style = (Style)Application.Current.Resources["CardFrame"],
                BackgroundColor = isUser ? 
                    (Color)Application.Current.Resources["Primary"] : 
                    (Color)Application.Current.Resources["LightGray"],
                Padding = new Thickness(10),
                Margin = new Thickness(isUser ? 40 : 5, 5, isUser ? 5 : 40, 5)
            };

            var label = new Label
            {
                Text = message,
                Style = (Style)Application.Current.Resources["BodyLabel"],
                TextColor = isUser ? 
                    Colors.White : 
                    (Color)Application.Current.Resources["Dark"]
            };

            frame.Content = label;
            ChatMessages.Children.Add(frame);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessageEntry.Focus();
        }

        protected override bool OnBackButtonPressed()
        {
            // Mostrar mensaje de confirmación si el usuario intenta regresar
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
                    await Navigation.PopToRootAsync();
                }
            });

            return true;
        }
    }
}