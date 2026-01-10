using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class CarsPage : ContentPage
    {
        public CarsPage(CarsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is CarsViewModel vm)
                vm.LoadMasiniCommand.Execute(null);
        }
    }
}
