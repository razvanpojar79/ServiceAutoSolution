using System.Windows.Input;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class AddClientViewModel : BindableObject
    {
        public string Nume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddClientViewModel()
        {
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

            await Shell.Current.GoToAsync("..");
        }
    }
}