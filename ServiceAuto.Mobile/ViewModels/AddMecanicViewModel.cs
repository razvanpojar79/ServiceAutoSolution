using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class AddMecanicViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;

        public string Nume { get; set; }
        public SpecializareMecanic SpecializareSelectata { get; set; }
        public bool EsteDisponibil { get; set; } = true;
        public List<string> ListaSpecializari { get; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddMecanicViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            ListaSpecializari = Enum.GetNames(typeof(SpecializareMecanic)).ToList();

            SaveCommand = new Command(async () => await SalveazaMecanic());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        private async Task SalveazaMecanic()
        {
            if (string.IsNullOrWhiteSpace(Nume))
            {
                await Shell.Current.DisplayAlert("Eroare", "Numele este obligatoriu.", "OK");
                return;
            }

            var mecanicNou = new MecanicDto
            {
                Nume = Nume,
                Specializare = SpecializareSelectata,
                EsteDisponibil = EsteDisponibil
            };

            await _apiClient.PostAsync(ApiRoutes.Mecanici, mecanicNou);

            await Shell.Current.GoToAsync("..");
        }
    }
}