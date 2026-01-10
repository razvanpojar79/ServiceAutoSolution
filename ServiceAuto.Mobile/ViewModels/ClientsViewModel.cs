using System.Collections.ObjectModel;
using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class ClientsViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;

        public ObservableCollection<ClientDto> Clienti { get; set; } = new ObservableCollection<ClientDto>();

        public ICommand LoadClientiCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public ClientsViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;

            LoadClientiCommand = new Command(async () => await IncarcaClienti());
            AddCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(AddClientPage)));
            DeleteCommand = new Command<ClientDto>(async (c) => await StergeClient(c));
            EditCommand = new Command<ClientDto>(async (c) => await EditeazaClient(c));

            _ = IncarcaClienti();
        }

        private async Task IncarcaClienti()
        {
            var data = await _apiClient.GetAsync<ClientDto>(ApiRoutes.Clienti);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Clienti.Clear();
                foreach (var c in data)
                    Clienti.Add(c);
            });
        }

        private async Task StergeClient(ClientDto client)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmare", "Sigur vrei să ștergi acest client?", "Da", "Nu");
            if (!confirm) return;

            var ok = await _apiClient.DeleteAsync($"{ApiRoutes.Clienti}/{client.Id}");
            if (!ok)
            {
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut șterge clientul.", "OK");
                return;
            }

            await IncarcaClienti();
        }


        private async Task EditeazaClient(ClientDto client)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Client", client }
            };
            await Shell.Current.GoToAsync(nameof(EditClientPage), navigationParameter);
        }

        public void RefreshList()
        {
            for (int i = 0; i < Clienti.Count; i++)
                Clienti[i] = Clienti[i];
        }
    }
}