using System.Collections.ObjectModel;
using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Shared.AppDtos;
using ServiceAuto.Shared;

namespace ServiceAuto.Mobile.ViewModels
{
    public class ServicesViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;
        public ObservableCollection<ServiciuDto> Servicii { get; set; } = new ObservableCollection<ServiciuDto>();

        public ICommand LoadServiciiCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public ServicesViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            LoadServiciiCommand = new Command(async () => await IncarcaServicii());
            AddCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(AddServicePage)));
            DeleteCommand = new Command<ServiciuDto>(async (s) => await StergeServiciu(s));
            EditCommand = new Command<ServiciuDto>(async (s) => await EditeazaServiciu(s));

            AdaugaDateDeTest();
        }

        private async Task IncarcaServicii()
        {
            await Task.Delay(100);
        }

        private void AdaugaDateDeTest()
        {
            Servicii.Clear();

            Servicii.Add(new ServiciuDto
            {
                Id = 1,
                Denumire = "Schimb Ulei + Filtre",
                Pret = 150,
                DurataEstimata = 45
            });

            Servicii.Add(new ServiciuDto
            {
                Id = 2,
                Denumire = "Diagnoză Computerizată",
                Pret = 100,
                DurataEstimata = 30
            });

            Servicii.Add(new ServiciuDto
            {
                Id = 3,
                Denumire = "Înlocuire Plăcuțe Frână",
                Pret = 200,
                DurataEstimata = 60
            });
        }

        private async Task StergeServiciu(ServiciuDto serviciu)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmare", "Sigur vrei să ștergi acest serviciu?", "Da", "Nu");
            if (!confirm) return;

            Servicii.Remove(serviciu);
        }

        private async Task EditeazaServiciu(ServiciuDto serviciu)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Serviciu", serviciu }
            };
            await Shell.Current.GoToAsync(nameof(EditServicePage), navigationParameter);
        }

        public void RefreshList()
        {
            for (int i = 0; i < Servicii.Count; i++)
            {
                Servicii[i] = Servicii[i];
            }
        }
    }
}