using Microsoft.Maui;

namespace CuidadoAutomotrizApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Configurar servicios adicionales aquí si son necesarios
            
            var app = builder.Build();
            app.Run(args);
        }
    }
}