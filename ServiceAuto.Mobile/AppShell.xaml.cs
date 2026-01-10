using ServiceAuto.Mobile.Views;

namespace ServiceAuto.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddAppointmentPage), typeof(AddAppointmentPage));
            Routing.RegisterRoute(nameof(EditAppointmentPage), typeof(EditAppointmentPage));
            Routing.RegisterRoute(nameof(AddClientPage), typeof(AddClientPage));
            Routing.RegisterRoute(nameof(EditClientPage), typeof(EditClientPage));
            Routing.RegisterRoute(nameof(AddCarPage), typeof(AddCarPage));
            Routing.RegisterRoute(nameof(EditCarPage), typeof(EditCarPage));
            Routing.RegisterRoute(nameof(AddMecanicPage), typeof(AddMecanicPage));
            Routing.RegisterRoute(nameof(EditMecanicPage), typeof(EditMecanicPage));
            Routing.RegisterRoute(nameof(AddServicePage), typeof(AddServicePage));
            Routing.RegisterRoute(nameof(EditServicePage), typeof(EditServicePage));
        }
    }
}   