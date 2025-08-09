using ChicoKoodo.AndroidApp.Pages;
using ChicoKoodo.AndroidApp.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ChicoKoodo.AndroidApp
{
    public static partial class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            RegisterPlatformServices(builder.Services);

            builder.Services.AddTransient<NihongoDataManagementService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<NihongoBenkyou>();
            builder.Services.AddSingleton<NihongoPractice>();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        static partial void RegisterPlatformServices(IServiceCollection services);
    }
}
