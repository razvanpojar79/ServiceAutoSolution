using System.Windows.Input;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public class AddCarViewModel : BindableObject
    {
        public string Marca { get; set; }
        public string Model { get; set; }
        public string NrInmatriculare { get; set; }
        public string SerieSasiu { get; set; }
        public int AnFabricatie { get; set; } = DateTime.Now.Year;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddCarViewModel()
        {
            SaveCommand = new Command(async () => await SalveazaMasina());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        private async Task SalveazaMasina()
        {
            if (string.IsNullOrWhiteSpace(Marca) || string.IsNullOrWhiteSpace(NrInmatriculare))
            {
                await Shell.Current.DisplayAlert("Eroare", "Marca și Numărul de înmatriculare sunt obligatorii.", "OK");
                return;
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}