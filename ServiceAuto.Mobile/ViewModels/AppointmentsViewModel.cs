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
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public AppointmentsViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            LoadProgramariCommand = new Command(async () => await IncarcaProgramari());
            AddCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(AddAppointmentPage)));
            DeleteCommand = new Command<ProgramareDto>(async (p) => await StergeProgramare(p));
            EditCommand = new Command<ProgramareDto>(async (p) => await EditeazaProgramare(p));

            AdaugaDateDeTest();
        }

        private async Task IncarcaProgramari()
        {
            await Task.Delay(100);
        }

        private void AdaugaDateDeTest()
        {
            Programari.Clear();

            Programari.Add(new ProgramareDto
            {
                Id = 1,
                MasinaInfo = "TEST: Dacia Logan",
                DenumireServiciu = "Schimb Ulei",
                DataOra = DateTime.Now,
                Status = StatusProgramare.InAsteptare
            });

            Programari.Add(new ProgramareDto
            {
                Id = 2,
                MasinaInfo = "TEST: BMW Seria 3",
                DenumireServiciu = "Verificare Placute",
                DataOra = DateTime.Now.AddDays(1),
                Status = StatusProgramare.Preluata
            });

            Programari.Add(new ProgramareDto
            {
                Id = 3,
                MasinaInfo = "TEST: Audi A4",
                DenumireServiciu = "Diagnoza",
                DataOra = DateTime.Now.AddDays(2),
                Status = StatusProgramare.InLucru
            });
        }

        private async Task StergeProgramare(ProgramareDto programare)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmare", "Sigur vrei să ștergi această programare?", "Da", "Nu");
            if (!confirm) return;

            Programari.Remove(programare);
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
            for (int i = 0; i < Programari.Count; i++)
            {
                Programari[i] = Programari[i];
            }
        }
    }
}