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

            builder.Services.AddTransient<EditAppointmentPage>();
            builder.Services.AddTransient<EditAppointmentViewModel>();

            builder.Services.AddTransient<ClientsPage>();
            builder.Services.AddTransient<ClientsViewModel>();

            builder.Services.AddTransient<AddClientPage>();
            builder.Services.AddTransient<AddClientViewModel>();

            builder.Services.AddTransient<EditClientPage>();
            builder.Services.AddTransient<EditClientViewModel>();

            builder.Services.AddTransient<CarsPage>();
            builder.Services.AddTransient<CarsViewModel>();

            builder.Services.AddTransient<AddCarPage>();
            builder.Services.AddTransient<AddCarViewModel>();

            builder.Services.AddTransient<EditCarPage>();
            builder.Services.AddTransient<EditCarViewModel>();

            builder.Services.AddTransient<AddMecanicPage>();
            builder.Services.AddTransient<AddMecanicViewModel>();

            builder.Services.AddTransient<EditMecanicPage>();
            builder.Services.AddTransient<EditMecanicViewModel>();

            builder.Services.AddTransient<MecaniciPage>();
            builder.Services.AddTransient<MecaniciViewModel>();

            builder.Services.AddTransient<ServicesPage>();
            builder.Services.AddTransient<ServicesViewModel>();

            builder.Services.AddTransient<AddServicePage>();
            builder.Services.AddTransient<AddServiceViewModel>();

            builder.Services.AddTransient<EditServicePage>();
            builder.Services.AddTransient<EditServiceViewModel>();

            return builder.Build();
        }
    }
}