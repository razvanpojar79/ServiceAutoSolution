using System.Windows.Input;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class EditAppointmentViewModel : BindableObject, IQueryAttributable
    {
        private ProgramareDto _programare;

        public string NumeMasina { get; set; }
        public DateTime DataSelectata { get; set; }
        public TimeSpan OraSelectata { get; set; }
        public string DescriereProblema { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditAppointmentViewModel()
        {
            SaveCommand = new Command(async () => await SalveazaModificarile());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Programare"))
            {
                _programare = query["Programare"] as ProgramareDto;
                IncarcaDatele();
            }
        }

        private void IncarcaDatele()
        {
            if (_programare == null) return;

            NumeMasina = _programare.MasinaInfo;
            DataSelectata = _programare.DataOra.Date;
            OraSelectata = _programare.DataOra.TimeOfDay;
            DescriereProblema = _programare.DescriereProblema ?? "";

            OnPropertyChanged(nameof(NumeMasina));
            OnPropertyChanged(nameof(DataSelectata));
            OnPropertyChanged(nameof(OraSelectata));
            OnPropertyChanged(nameof(DescriereProblema));
        }

        private async Task SalveazaModificarile()
        {
            _programare.MasinaInfo = NumeMasina;
            _programare.DataOra = DataSelectata + OraSelectata;
            _programare.DescriereProblema = DescriereProblema;

            await Shell.Current.GoToAsync("..");
        }
    }
}