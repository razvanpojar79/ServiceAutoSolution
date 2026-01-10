using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class AddClientViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;

        public string Nume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddClientViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            SaveCommand = new Command(async () => await SalveazaClient());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        private async Task SalveazaClient()
        {
            if (string.IsNullOrWhiteSpace(Nume) || string.IsNullOrWhiteSpace(Telefon))
            {
                await Shell.Current.DisplayAlert("Eroare", "Numele și Telefonul sunt obligatorii.", "OK");
                return;
            }

            var dto = new ClientDto
            {
                Nume = Nume,
                Telefon = Telefon,
                Email = Email
            };

            var created = await _apiClient.PostAsync(ApiRoutes.Clienti, dto);

            if (created == null)
            {
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut salva clientul.", "OK");
                return;
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
