using ServiceAuto.Mobile.ViewModels;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.Views
{
    public partial class EditClientPage : ContentPage, IQueryAttributable
    {
        private readonly EditClientViewModel _vm;

        public EditClientPage(EditClientViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = _vm;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Client", out var obj) && obj is ClientDto c)
                _vm.SetClient(c);
        }
    }
}
