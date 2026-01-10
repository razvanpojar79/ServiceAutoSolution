using System.Windows.Input;
using ServiceAuto.Shared.AppDtos;

namespace ServiceAuto.Mobile.ViewModels
{
    public partial class AddServiceViewModel : BindableObject
    {
        public string Denumire { get; set; }
        public decimal Pret { get; set; }
        public int DurataEstimata { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddServiceViewModel()
        {
            SaveCommand = new Command(async () => await SalveazaServiciu());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        private async Task SalveazaServiciu()
        {
            if (string.IsNullOrWhiteSpace(Denumire))
            {
                await Shell.Current.DisplayAlert("Eroare", "Denumirea este obligatorie.", "OK");
                return;
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}