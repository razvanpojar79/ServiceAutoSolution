using ServiceAuto.Mobile.ViewModels;

namespace ServiceAuto.Mobile.Views
{
    public partial class AppointmentsPage : ContentPage
    {
        private readonly AppointmentsViewModel _viewModel;

        public AppointmentsPage(AppointmentsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadProgramariCommand.Execute(null);
        }
    }
}