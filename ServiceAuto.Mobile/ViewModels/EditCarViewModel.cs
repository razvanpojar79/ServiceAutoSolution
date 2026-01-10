using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class EditCarViewModel : BindableObject, IQueryAttributable
    {
        private readonly ApiClient _apiClient;
        private MasinaDto _masina;

        public int Id { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }
        public string NrInmatriculare { get; set; }
        public string SerieSasiu { get; set; }
        public int AnFabricatie { get; set; }
        public int ClientId { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditCarViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            SaveCommand = new Command(async () => await SalveazaModificarile());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Masina", out var obj) && obj is MasinaDto m)
            {
                _masina = m;
                IncarcaDatele();
            }
        }

        private void IncarcaDatele()
        {
            if (_masina == null) return;

            Id = _masina.Id;
            Marca = _masina.Marca;
            Model = _masina.Model;
            NrInmatriculare = _masina.NrInmatriculare;
            SerieSasiu = _masina.SerieSasiu;
            AnFabricatie = _masina.AnFabricatie;
            ClientId = _masina.ClientId;

            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Marca));
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(NrInmatriculare));
            OnPropertyChanged(nameof(SerieSasiu));
            OnPropertyChanged(nameof(AnFabricatie));
            OnPropertyChanged(nameof(ClientId));
        }

        private async Task SalveazaModificarile()
        {
            if (Id <= 0 || string.IsNullOrWhiteSpace(Marca) || string.IsNullOrWhiteSpace(NrInmatriculare) || ClientId <= 0)
            {
                await Shell.Current.DisplayAlert("Eroare", "Marca, Numărul de înmatriculare și ClientId sunt obligatorii.", "OK");
                return;
            }

            var dto = new MasinaDto
            {
                Id = Id,
                Marca = Marca,
                Model = Model,
                NrInmatriculare = NrInmatriculare,
                SerieSasiu = SerieSasiu,
                AnFabricatie = AnFabricatie,
                ClientId = ClientId
            };

            var ok = await _apiClient.PutAsync($"{ApiRoutes.Masini}/{Id}", dto);
            if (!ok)
            {
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut salva modificările.", "OK");
                return;
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
