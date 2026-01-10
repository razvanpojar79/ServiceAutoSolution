using System.Collections.ObjectModel;
using System.Windows.Input;
using ServiceAuto.Mobile.Services;
using ServiceAuto.Mobile.Views;
using ServiceAuto.Shared.AppDtos;
using ServiceAuto.Shared;

namespace ServiceAuto.Mobile.ViewModels
{
    public class MecaniciViewModel : BindableObject
    {
        private readonly ApiClient _apiClient;
        public ObservableCollection<MecanicDto> Mecanici { get; set; } = new ObservableCollection<MecanicDto>();

        public ICommand LoadMecaniciCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public MecaniciViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            LoadMecaniciCommand = new Command(async () => await IncarcaMecanici());
            AddCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(AddMecanicPage)));
            DeleteCommand = new Command<MecanicDto>(async (m) => await StergeMecanic(m));
            EditCommand = new Command<MecanicDto>(async (m) => await EditeazaMecanic(m));

            AdaugaDateDeTest();
        }

        private async Task IncarcaMecanici()
        {
            await Task.Delay(100);
        }

        private void AdaugaDateDeTest()
        {
            Mecanici.Clear();

            Mecanici.Add(new MecanicDto
            {
                Id = 1,
                Nume = "Marius Stan",
                Specializare = SpecializareMecanic.Mecanica,
                EsteDisponibil = true
            });

            Mecanici.Add(new MecanicDto
            {
                Id = 2,
                Nume = "Dan Ionescu",
                Specializare = SpecializareMecanic.Electrica,
                EsteDisponibil = false
            });

            Mecanici.Add(new MecanicDto
            {
                Id = 3,
                Nume = "Alex Popa",
                Specializare = SpecializareMecanic.Vopsitorie,
                EsteDisponibil = true
            });
        }

        private async Task StergeMecanic(MecanicDto mecanic)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmare", "Sigur vrei să ștergi acest mecanic?", "Da", "Nu");
            if (!confirm) return;

            Mecanici.Remove(mecanic);
        }

        private async Task EditeazaMecanic(MecanicDto mecanic)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Mecanic", mecanic }
            };
            await Shell.Current.GoToAsync(nameof(EditMecanicPage), navigationParameter);
        }

        public void RefreshList()
        {
            for (int i = 0; i < Mecanici.Count; i++)
            {
                Mecanici[i] = Mecanici[i];
            }
        }
    }
}