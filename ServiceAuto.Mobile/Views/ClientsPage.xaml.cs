using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class ClientsPage : ContentPage
    {
        public ClientsPage(ClientsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ClientsViewModel vm)
            {
                vm.LoadClientiCommand.Execute(null);
            }
        }
    }
}
