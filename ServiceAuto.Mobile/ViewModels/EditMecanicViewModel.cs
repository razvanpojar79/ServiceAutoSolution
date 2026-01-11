using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class EditMecanicViewModel : BindableObject, IQueryAttributable
    {
        private readonly ApiClient _apiClient;
        private MecanicDto _mecanic;

        public string Nume { get; set; }
        public string SpecializareSelectata { get; set; }
        public bool EsteDisponibil { get; set; }
        public List<string> ListaSpecializari { get; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditMecanicViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            ListaSpecializari = Enum.GetNames(typeof(SpecializareMecanic)).ToList();

            SaveCommand = new Command(async () => await SalveazaModificarile());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Mecanic"))
            {
                _mecanic = query["Mecanic"] as MecanicDto;
                IncarcaDatele();
            }
        }

        private void IncarcaDatele()
        {
            if (_mecanic == null) return;

            Nume = _mecanic.Nume;
            SpecializareSelectata = _mecanic.Specializare.ToString();
            EsteDisponibil = _mecanic.EsteDisponibil;

            OnPropertyChanged(nameof(Nume));
            OnPropertyChanged(nameof(SpecializareSelectata));
            OnPropertyChanged(nameof(EsteDisponibil));
        }

        private async Task SalveazaModificarile()
        {
            if (string.IsNullOrWhiteSpace(Nume))
            {
                await Shell.Current.DisplayAlert("Eroare", "Numele este obligatoriu.", "OK");
                return;
            }

            _mecanic.Nume = Nume;
            if (Enum.TryParse(SpecializareSelectata, out SpecializareMecanic spec))
            {
                _mecanic.Specializare = spec;
            }
            _mecanic.EsteDisponibil = EsteDisponibil;

            await _apiClient.PutAsync($"{ApiRoutes.Mecanici}/{_mecanic.Id}", _mecanic);

            await Shell.Current.GoToAsync("..");
        }
    }
}