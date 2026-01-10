using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class MecaniciPage : ContentPage
    {
        public MecaniciPage(MecaniciViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is MecaniciViewModel vm)
            {
                vm.RefreshList();
            }
        }
    }
}