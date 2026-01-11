using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public partial class AddServiceViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;

        public string Denumire { get; set; }
        public decimal Pret { get; set; }
        public int DurataEstimata { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddServiceViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            SaveCommand = new Command(async () => await SalveazaServiciu());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        private async Task SalveazaServiciu()
        {
            if (string.IsNullOrWhiteSpace(Denumire))
            {
                await Shell.Current.DisplayAlert("Eroare", "Denumirea este obligatorie.", "OK");
                return;
            }

            var serviciuNou = new ServiciuDto
            {
                Denumire = Denumire,
                Pret = Pret,
                DurataEstimata = DurataEstimata
            };

            await _apiClient.PostAsync(ApiRoutes.Servicii, serviciuNou);

            await Shell.Current.GoToAsync("..");
        }
    }
}