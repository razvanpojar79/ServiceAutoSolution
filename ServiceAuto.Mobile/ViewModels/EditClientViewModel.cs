using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class EditClientViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;

        public int Id { get; set; }
        public string Nume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditClientViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            SaveCommand = new Command(async () => await Salveaza());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public void SetClient(ClientDto c)
        {
            Id = c.Id;
            Nume = c.Nume;
            Telefon = c.Telefon;
            Email = c.Email;

            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Nume));
            OnPropertyChanged(nameof(Telefon));
            OnPropertyChanged(nameof(Email));
        }

        private async Task Salveaza()
        {
            if (Id <= 0) return;

            if (string.IsNullOrWhiteSpace(Nume) || string.IsNullOrWhiteSpace(Telefon))
            {
                await Shell.Current.DisplayAlert("Eroare", "Numele și Telefonul sunt obligatorii.", "OK");
                return;
            }

            var dto = new ClientDto
            {
                Id = Id,
                Nume = Nume,
                Telefon = Telefon,
                Email = Email
            };

            var ok = await _apiClient.PutAsync($"{ApiRoutes.Clienti}/{Id}", dto);
            if (!ok)
            {
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut salva modificarea.", "OK");
                return;
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
