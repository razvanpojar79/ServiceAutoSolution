using Microsoft.Extensions.Logging;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Mobile.ViewModels;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Shared;

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

            builder.Services.AddSingleton(sp => new ApiClient("http://10.0.2.2:5000"));

            builder.Services.AddTransient<AppointmentsPage>();
            builder.Services.AddTransient<AppointmentsViewModel>();

            builder.Services.AddTransient<AddAppointmentPage>();
            builder.Services.AddTransient<AddAppointmentViewModel>();

            return builder.Build();
        }
    }
}