using System.Collections.ObjectModel;
using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Shared.AppDtos;
using ServiceAuto.Shared;

namespace ServiceAuto.Mobile.ViewModels
{
    public class MecaniciViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;
        public ObservableCollection<MecanicDto> Mecanici { get; set; } = new ObservableCollection<MecanicDto>();

        public ICommand LoadMecaniciCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public MecaniciViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;

            LoadMecaniciCommand = new Command(async () => await IncarcaMecanici());
            AddCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(AddMecanicPage)));
            DeleteCommand = new Command<MecanicDto>(async (m) => await StergeMecanic(m));
            EditCommand = new Command<MecanicDto>(async (m) => await EditeazaMecanic(m));

            _ = IncarcaMecanici();
        }

        private async Task IncarcaMecanici()
        {
            var data = await _apiClient.GetAsync<MecanicDto>(ApiRoutes.Mecanici);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Mecanici.Clear();
                foreach (var m in data)
                {
                    Mecanici.Add(m);
                }
            });
        }

        private async Task StergeMecanic(MecanicDto mecanic)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmare", "Sigur vrei să ștergi acest mecanic?", "Da", "Nu");
            if (!confirm) return;

            var ok = await _apiClient.DeleteAsync($"{ApiRoutes.Mecanici}/{mecanic.Id}");
            if (!ok)
            {
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut șterge mecanicul.", "OK");
                return;
            }

            await IncarcaMecanici();
        }

        private async Task EditeazaMecanic(MecanicDto mecanic)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Mecanic", mecanic }
            };
            await Shell.Current.GoToAsync(nameof(EditMecanicPage), navigationParameter);
        }

        public void RefreshList()
        {
            _ = IncarcaMecanici();
        }
    }
}