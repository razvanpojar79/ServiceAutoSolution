using Microsoft.Extensions.Logging;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Mobile.ViewModels;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Mobile.Services;

namespace ServiceAuto.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<AppointmentsPage>();
            builder.Services.AddTransient<AppointmentsViewModel>();

            builder.Services.AddSingleton<ApiClient>();

            return builder.Build();
        }
    }
}
