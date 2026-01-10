using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class EditMecanicPage : ContentPage
    {
        public EditMecanicPage(EditMecanicViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}