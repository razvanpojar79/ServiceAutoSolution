using System.Collections.ObjectModel;
using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Shared.AppDtos;
using ServiceAuto.Shared;

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

            AdaugaDateDeTest();
        }

        private async Task IncarcaClienti()
        {
            await Task.Delay(100);
        }

        private void AdaugaDateDeTest()
        {
            Clienti.Clear();

            Clienti.Add(new ClientDto
            {
                Id = 1,
                Nume = "Popescu Ion",
                Telefon = "0722123456",
                Email = "ion.popescu@email.com"
            });

            Clienti.Add(new ClientDto
            {
                Id = 2,
                Nume = "Ionescu Maria",
                Telefon = "0744987654",
                Email = "maria.ionescu@email.com"
            });

            Clienti.Add(new ClientDto
            {
                Id = 3,
                Nume = "Georgescu Vlad",
                Telefon = "0766112233",
                Email = "vlad.g@email.com"
            });
        }

        private async Task StergeClient(ClientDto client)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmare", "Sigur vrei să ștergi acest client?", "Da", "Nu");
            if (!confirm) return;

            Clienti.Remove(client);
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
            {
                Clienti[i] = Clienti[i];
            }
        }
    }
}