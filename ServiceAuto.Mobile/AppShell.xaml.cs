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
        }
    }
}   