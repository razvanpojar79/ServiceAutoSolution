using System.Collections.ObjectModel;
using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Shared.AppDtos;
using ServiceAuto.Shared;

namespace ServiceAuto.Mobile.ViewModels
{
    public class CarsViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;
        public ObservableCollection<MasinaDto> Masini { get; set; } = new ObservableCollection<MasinaDto>();

        public ICommand LoadMasiniCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public CarsViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            LoadMasiniCommand = new Command(async () => await IncarcaMasini());
            AddCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(AddCarPage)));
            DeleteCommand = new Command<MasinaDto>(async (m) => await StergeMasina(m));
            EditCommand = new Command<MasinaDto>(async (m) => await EditeazaMasina(m));

            AdaugaDateDeTest();
        }

        private async Task IncarcaMasini()
        {
            await Task.Delay(100);
        }

        private void AdaugaDateDeTest()
        {
            Masini.Clear();

            Masini.Add(new MasinaDto
            {
                Id = 1,
                Marca = "Volkswagen",
                Model = "Golf 7",
                AnFabricatie = 2018,
                NrInmatriculare = "CJ 10 ABC",
                SerieSasiu = "WVWZZZ1234567890",
                ClientId = 1
            });

            Masini.Add(new MasinaDto
            {
                Id = 2,
                Marca = "Ford",
                Model = "Focus",
                AnFabricatie = 2016,
                NrInmatriculare = "B 99 XYZ",
                SerieSasiu = "WF0KXX1234567890",
                ClientId = 2
            });

            Masini.Add(new MasinaDto
            {
                Id = 3,
                Marca = "Audi",
                Model = "A4",
                AnFabricatie = 2020,
                NrInmatriculare = "CJ 20 QWE",
                SerieSasiu = "WAUZZZ1234567890",
                ClientId = 3
            });
        }

        private async Task StergeMasina(MasinaDto masina)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmare", "Sigur vrei să ștergi această mașină?", "Da", "Nu");
            if (!confirm) return;

            Masini.Remove(masina);
        }

        private async Task EditeazaMasina(MasinaDto masina)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Masina", masina }
            };
            await Shell.Current.GoToAsync(nameof(EditCarPage), navigationParameter);
        }

        public void RefreshList()
        {
            for (int i = 0; i < Masini.Count; i++)
            {
                Masini[i] = Masini[i];
            }
        }
    }
}