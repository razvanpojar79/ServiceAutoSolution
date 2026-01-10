using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class AddCarViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;

        public string Marca { get; set; }
        public string Model { get; set; }
        public string NrInmatriculare { get; set; }
        public string SerieSasiu { get; set; }
        public int AnFabricatie { get; set; } = DateTime.Now.Year;
        public int ClientId { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddCarViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            SaveCommand = new Command(async () => await SalveazaMasina());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        private async Task SalveazaMasina()
        {
            if (string.IsNullOrWhiteSpace(Marca) || string.IsNullOrWhiteSpace(NrInmatriculare) || ClientId <= 0)
            {
                await Shell.Current.DisplayAlert("Eroare", "Marca, Numărul de înmatriculare și ClientId sunt obligatorii.", "OK");
                return;
            }

            var dto = new MasinaDto
            {
                Marca = Marca,
                Model = Model,
                NrInmatriculare = NrInmatriculare,
                SerieSasiu = SerieSasiu,
                AnFabricatie = AnFabricatie,
                ClientId = ClientId
            };

            var created = await _apiClient.PostAsync(ApiRoutes.Masini, dto);
            if (created == null)
            {
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut salva mașina.", "OK");
                return;
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
