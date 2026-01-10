using System.Windows.Input;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class EditCarViewModel : BindableObject, IQueryAttributable
    {
        private MasinaDto _masina;

        public string Marca { get; set; }
        public string Model { get; set; }
        public string NrInmatriculare { get; set; }
        public string SerieSasiu { get; set; }
        public int AnFabricatie { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditCarViewModel()
        {
            SaveCommand = new Command(async () => await SalveazaModificarile());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Masina"))
            {
                _masina = query["Masina"] as MasinaDto;
                IncarcaDatele();
            }
        }

        private void IncarcaDatele()
        {
            if (_masina == null) return;

            Marca = _masina.Marca;
            Model = _masina.Model;
            NrInmatriculare = _masina.NrInmatriculare;
            SerieSasiu = _masina.SerieSasiu;
            AnFabricatie = _masina.AnFabricatie;

            OnPropertyChanged(nameof(Marca));
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(NrInmatriculare));
            OnPropertyChanged(nameof(SerieSasiu));
            OnPropertyChanged(nameof(AnFabricatie));
        }

        private async Task SalveazaModificarile()
        {
            if (string.IsNullOrWhiteSpace(Marca) || string.IsNullOrWhiteSpace(NrInmatriculare))
            {
                await Shell.Current.DisplayAlert("Eroare", "Marca și Numărul de înmatriculare sunt obligatorii.", "OK");
                return;
            }

            _masina.Marca = Marca;
            _masina.Model = Model;
            _masina.NrInmatriculare = NrInmatriculare;
            _masina.SerieSasiu = SerieSasiu;
            _masina.AnFabricatie = AnFabricatie;

            await Shell.Current.GoToAsync("..");
        }
    }
}