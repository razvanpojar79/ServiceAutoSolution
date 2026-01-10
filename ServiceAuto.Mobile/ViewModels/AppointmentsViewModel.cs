using System.Collections.ObjectModel;
using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class AppointmentsViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;
        public ObservableCollection<ProgramareDto> Programari { get; set; } = new ObservableCollection<ProgramareDto>();
        public ICommand LoadProgramariCommand { get; }

        public AppointmentsViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            LoadProgramariCommand = new Command(async () => await IncarcaProgramari());
        }

        private async Task IncarcaProgramari()
        {
            var lista = await _apiClient.GetAsync<ProgramareDto>(ApiRoutes.Programari);

            Programari.Clear();
            foreach (var p in lista)
            {
                Programari.Add(p);
            }
        }
    }
}