using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class AddServicePage : ContentPage
    {
        public AddServicePage(AddServiceViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}