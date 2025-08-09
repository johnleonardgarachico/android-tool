using ChicoKoodo.AndroidApp.Platforms.Android;

namespace ChicoKoodo.AndroidApp
{
    public static partial class MauiProgram
    {
        static partial void RegisterPlatformServices(IServiceCollection services)
        {
            services.AddSingleton<Interfaces.Platforms.Android.IFileHelper, FileHelper>(); // ✅ Android-only registration
        }
    }
}
