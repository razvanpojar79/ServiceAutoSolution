using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class EditClientPage : ContentPage
    {
        public EditClientPage(EditClientViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}