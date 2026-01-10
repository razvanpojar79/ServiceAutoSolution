using System.Collections.ObjectModel;
using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

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

            _ = IncarcaMasini();
        }

        private async Task IncarcaMasini()
        {
            var data = await _apiClient.GetAsync<MasinaDto>(ApiRoutes.Masini);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Masini.Clear();
                foreach (var m in data)
                    Masini.Add(m);
            });
        }

        private async Task StergeMasina(MasinaDto masina)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmare", "Sigur vrei să ștergi această mașină?", "Da", "Nu");
            if (!confirm) return;

            var ok = await _apiClient.DeleteAsync($"{ApiRoutes.Masini}/{masina.Id}");
            if (!ok)
            {
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut șterge mașina.", "OK");
                return;
            }

            await IncarcaMasini();
        }

        private async Task EditeazaMasina(MasinaDto masina)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Masina", masina }
            };
            await Shell.Current.GoToAsync(nameof(EditCarPage), navigationParameter);
        }
    }
}
