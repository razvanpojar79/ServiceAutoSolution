using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class AddAppointmentPage : ContentPage
    {
        public AddAppointmentPage(AddAppointmentViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}