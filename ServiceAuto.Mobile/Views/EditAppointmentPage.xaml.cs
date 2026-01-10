using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class EditAppointmentPage : ContentPage
    {
        public EditAppointmentPage(EditAppointmentViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}