using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class AddMecanicPage : ContentPage
    {
        public AddMecanicPage(AddMecanicViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}