using System.Collections.ObjectModel;
using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Shared.AppDtos;
using ServiceAuto.Shared;

namespace ServiceAuto.Mobile.ViewModels
{
    public class AppointmentsViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;
        public ObservableCollection<ProgramareDto> Programari { get; set; } = new ObservableCollection<ProgramareDto>();

        public ICommand LoadProgramariCommand { get; }
        public ICommand AddCommand { get; }

        public AppointmentsViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            LoadProgramariCommand = new Command(async () => await IncarcaProgramari());
            AddCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(AddAppointmentPage)));
        }

        private async Task IncarcaProgramari()
        {
            try
            {
                var lista = await _apiClient.GetAsync<ProgramareDto>(ApiRoutes.Programari);
                if (lista != null)
                {
                    Programari.Clear();
                    foreach (var p in lista)
                    {
                        Programari.Add(p);
                    }
                }
            }
            catch
            {
            }
        }
    }
}