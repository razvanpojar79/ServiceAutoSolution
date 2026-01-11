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

            _ = IncarcaServicii();
        }

        private async Task IncarcaServicii()
        {
            var data = await _apiClient.GetAsync<ServiciuDto>(ApiRoutes.Servicii);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Servicii.Clear();
                foreach (var s in data)
                {
                    Servicii.Add(s);
                }
            });
        }

        private async Task StergeServiciu(ServiciuDto serviciu)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmare", "Sigur vrei să ștergi acest serviciu?", "Da", "Nu");
            if (!confirm) return;

            var ok = await _apiClient.DeleteAsync($"{ApiRoutes.Servicii}/{serviciu.Id}");
            if (!ok)
            {
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut șterge serviciul.", "OK");
                return;
            }

            await IncarcaServicii();
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
            _ = IncarcaServicii();
        }
    }
}