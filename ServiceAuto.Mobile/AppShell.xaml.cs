using ServiceAuto.Mobile.Views;

namespace ServiceAuto.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddAppointmentPage), typeof(AddAppointmentPage));
        }
    }
}   