using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class AddClientPage : ContentPage
    {
        public AddClientPage(AddClientViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}