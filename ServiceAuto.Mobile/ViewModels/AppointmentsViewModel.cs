using System.Collections.ObjectModel;
using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class AppointmentsViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;

        public ObservableCollection<ProgramareDto> Programari { get; set; } = new ObservableCollection<ProgramareDto>();

        public ICommand LoadProgramariCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public AppointmentsViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;

            LoadProgramariCommand = new Command(async () => await IncarcaProgramari());
            AddCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(AddAppointmentPage)));
            DeleteCommand = new Command<ProgramareDto>(async (p) => await StergeProgramare(p));
            EditCommand = new Command<ProgramareDto>(async (p) => await EditeazaProgramare(p));

            _ = IncarcaProgramari();
        }

        private async Task IncarcaProgramari()
        {
            var data = await _apiClient.GetAsync<ProgramareDto>(ApiRoutes.Programari);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Programari.Clear();
                foreach (var p in data)
                    Programari.Add(p);
            });
        }

        private async Task StergeProgramare(ProgramareDto programare)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmare", "Sigur vrei să ștergi această programare?", "Da", "Nu");
            if (!confirm) return;

            var ok = await _apiClient.DeleteAsync($"{ApiRoutes.Programari}/{programare.Id}");
            if (!ok)
            {
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut șterge programarea.", "OK");
                return;
            }

            await IncarcaProgramari();
        }

        private async Task EditeazaProgramare(ProgramareDto programare)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Programare", programare }
            };
            await Shell.Current.GoToAsync(nameof(EditAppointmentPage), navigationParameter);
        }

        public void RefreshList()
        {
            _ = IncarcaProgramari();
        }
    }
}