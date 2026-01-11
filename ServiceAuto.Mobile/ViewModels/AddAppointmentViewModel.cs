using System.Collections.ObjectModel;
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

        public ObservableCollection<MasinaDto> ListaMasini { get; set; } = new ObservableCollection<MasinaDto>();
        public MasinaDto MasinaSelectata { get; set; }

        public ObservableCollection<ServiciuDto> ListaServicii { get; set; } = new ObservableCollection<ServiciuDto>();
        public ServiciuDto ServiciuSelectat { get; set; }

        public ObservableCollection<MecanicDto> ListaMecanici { get; set; } = new ObservableCollection<MecanicDto>();
        public MecanicDto MecanicSelectat { get; set; }

        public string DescriereProblema { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddAppointmentViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;

            SaveCommand = new Command(async () => await SalveazaProgramare());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));

            _ = IncarcaDatele();
        }

        private async Task IncarcaDatele()
        {
            try
            {
                var masini = await _apiClient.GetAsync<MasinaDto>(ApiRoutes.Masini);
                var servicii = await _apiClient.GetAsync<ServiciuDto>(ApiRoutes.Servicii);
                var mecanici = await _apiClient.GetAsync<MecanicDto>(ApiRoutes.Mecanici);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    ListaMasini.Clear();
                    foreach (var m in masini) ListaMasini.Add(m);

                    ListaServicii.Clear();
                    foreach (var s in servicii) ListaServicii.Add(s);

                    ListaMecanici.Clear();
                    foreach (var mec in mecanici) ListaMecanici.Add(mec);
                });
            }
            catch
            {
                // Putem trata erorile aici
            }
        }

        private async Task SalveazaProgramare()
        {
            if (MasinaSelectata == null || ServiciuSelectat == null)
            {
                await Shell.Current.DisplayAlert("Eroare", "Selectează o mașină și un serviciu.", "OK");
                return;
            }

            var dataCompleta = new DateTime(DataSelectata.Year, DataSelectata.Month, DataSelectata.Day,
                                            OraSelectata.Hours, OraSelectata.Minutes, 0);

            var programareNoua = new ProgramareDto
            {
                DataOra = dataCompleta,
                MasinaId = MasinaSelectata.Id,
                ServiciuId = ServiciuSelectat.Id,
                MecanicId = MecanicSelectat?.Id,
                DescriereProblema = DescriereProblema,
                Status = StatusProgramare.InAsteptare
            };

            await _apiClient.PostAsync(ApiRoutes.Programari, programareNoua);

            await Shell.Current.GoToAsync("..");
        }
    }
}