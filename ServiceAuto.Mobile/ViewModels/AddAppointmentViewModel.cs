using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Shared.AppDtos; 
using ServiceAuto.Shared;

namespace ServiceAuto.Mobile.ViewModels
{
    public class AddAppointmentViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;

        public DateTime DataSelectata { get; set; } = DateTime.Now;
        public TimeSpan OraSelectata { get; set; } = DateTime.Now.TimeOfDay;

        public string NumeMasina { get; set; }
        public string DescriereProblema { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddAppointmentViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            SaveCommand = new Command(async () => await SalveazaProgramare());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        private async Task SalveazaProgramare()
        {
            var dataCompleta = new DateTime(DataSelectata.Year, DataSelectata.Month, DataSelectata.Day,
                                          OraSelectata.Hours, OraSelectata.Minutes, 0);

            var programareNoua = new ProgramareDto
            {
                DataOra = dataCompleta,
                MasinaInfo = NumeMasina,
                DenumireServiciu = DescriereProblema,
                Status = StatusProgramare.Preluata
            };

            
            await Shell.Current.GoToAsync("..");
        }
    }
}