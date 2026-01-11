using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public partial class EditServiceViewModel : BindableObject, IQueryAttributable
    {
        private readonly ApiClient _apiClient;
        private ServiciuDto _serviciu;

        public string Denumire { get; set; }
        public decimal Pret { get; set; }
        public int DurataEstimata { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditServiceViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            SaveCommand = new Command(async () => await SalveazaModificarile());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Serviciu"))
            {
                _serviciu = query["Serviciu"] as ServiciuDto;
                IncarcaDatele();
            }
        }

        private void IncarcaDatele()
        {
            if (_serviciu == null) return;

            Denumire = _serviciu.Denumire;
            Pret = _serviciu.Pret;
            DurataEstimata = _serviciu.DurataEstimata;

            OnPropertyChanged(nameof(Denumire));
            OnPropertyChanged(nameof(Pret));
            OnPropertyChanged(nameof(DurataEstimata));
        }

        private async Task SalveazaModificarile()
        {
            if (string.IsNullOrWhiteSpace(Denumire))
            {
                await Shell.Current.DisplayAlert("Eroare", "Denumirea este obligatorie.", "OK");
                return;
            }

            _serviciu.Denumire = Denumire;
            _serviciu.Pret = Pret;
            _serviciu.DurataEstimata = DurataEstimata;

            await _apiClient.PutAsync($"{ApiRoutes.Servicii}/{_serviciu.Id}", _serviciu);

            await Shell.Current.GoToAsync("..");
        }
    }
}