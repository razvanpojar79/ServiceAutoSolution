using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class EditCarPage : ContentPage
    {
        public EditCarPage(EditCarViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
