using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class ServicesPage : ContentPage
    {
        public ServicesPage(ServicesViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ServicesViewModel vm)
            {
                vm.RefreshList();
            }
        }
    }
}