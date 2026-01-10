using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class AddCarPage : ContentPage
    {
        public AddCarPage(AddCarViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}