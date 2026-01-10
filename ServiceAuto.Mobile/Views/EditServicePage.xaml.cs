using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class EditServicePage : ContentPage
    {
        public EditServicePage(EditServiceViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}