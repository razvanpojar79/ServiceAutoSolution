using System.Windows.Input;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class EditClientViewModel : BindableObject, IQueryAttributable
    {
        private ClientDto _client;

        public string Nume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditClientViewModel()
        {
            SaveCommand = new Command(async () => await SalveazaModificarile());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Client"))
            {
                _client = query["Client"] as ClientDto;
                IncarcaDatele();
            }
        }

        private void IncarcaDatele()
        {
            if (_client == null) return;

            Nume = _client.Nume;
            Telefon = _client.Telefon;
            Email = _client.Email;

            OnPropertyChanged(nameof(Nume));
            OnPropertyChanged(nameof(Telefon));
            OnPropertyChanged(nameof(Email));
        }

        private async Task SalveazaModificarile()
        {
            if (string.IsNullOrWhiteSpace(Nume) || string.IsNullOrWhiteSpace(Telefon))
            {
                await Shell.Current.DisplayAlert("Eroare", "Numele și Telefonul sunt obligatorii.", "OK");
                return;
            }

            _client.Nume = Nume;
            _client.Telefon = Telefon;
            _client.Email = Email;

            await Shell.Current.GoToAsync("..");
        }
    }
}