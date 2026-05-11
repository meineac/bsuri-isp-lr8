using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prokopovich_453503.Application.PirateCrewUseCases.Commands;
using Prokopovich_453503.Application.PirateCrewUseCases.Queries;
using Prokopovich_453503.Application.PirateUseCases.Queries;
using Prokopovich_453503.UI.Pages;
using System.Collections.ObjectModel;

namespace Prokopovich_453503.UI.ViewModels
{
    public partial class PirateCrewsViewModel : ObservableObject
    {
        private readonly IMediator _mediator;

        public PirateCrewsViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ObservableCollection<PirateCrew> PirateCrews { get; set; } = new();
        public ObservableCollection<Pirate> Pirates { get; set; } = new();

        [ObservableProperty]
        private PirateCrew selectedCrew;

        [RelayCommand]
        public async Task UpdateGroupList() => await GetPirateCrews();

        [RelayCommand]
        public async Task UpdateMembersList() => await GetPirates();

        [RelayCommand]
        private async Task ShowDetails(Pirate pirate)
        {
            if (pirate is null)
                return;
            await Shell.Current.GoToAsync(nameof(PirateDetails), new Dictionary<string, object>
            {
                ["Pirate"] = pirate
            });
        }
        [RelayCommand]
        private async Task AddCrew()
        {
            await Shell.Current.GoToAsync(nameof(AddEditCrewPage));
        }

        [RelayCommand]
        private async Task EditCrew()
        {
            if (SelectedCrew is null)
            {
                await Shell.Current.DisplayAlert("Внимание", "Сначала выберите команду", "OK");
                return;
            }
            await Shell.Current.GoToAsync(nameof(AddEditCrewPage),
                new Dictionary<string, object> { ["Crew"] = SelectedCrew });
        }

        [RelayCommand]
        private async Task DeleteCrew()
        {
            if (SelectedCrew is null) return;
            bool confirm = await Shell.Current.DisplayAlert(
                "Удаление", $"Удалить команду «{SelectedCrew.Name}»?", "Да", "Нет");
            if (!confirm) return;

            await _mediator.Send(new DeletePirateCrewCommand(SelectedCrew.Id));
            await GetPirateCrews();
            SelectedCrew = null;
        }

        [RelayCommand]
        private async Task AddPirate()
        {
            if (SelectedCrew is null)
            {
                await Shell.Current.DisplayAlert("Внимание", "Сначала выберите команду", "OK");
                return;
            }
            await Shell.Current.GoToAsync(nameof(AddEditPiratePage),
                new Dictionary<string, object> { ["CrewId"] = SelectedCrew.Id });
        }

        public async Task GetPirateCrews()
        {
            var crews = await _mediator.Send(new GetAllPirateCrewsRequest());

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                PirateCrews.Clear();
                foreach (var crew in crews)
                    PirateCrews.Add(crew);
            });
        }

        public async Task GetPirates()
        {
            var crew = SelectedCrew;
            if (crew is null)
            {
                await MainThread.InvokeOnMainThreadAsync(() => Pirates.Clear());
                return;
            }

            var pirates = await _mediator.Send(new GetPiratesByCrewRequest(crew.Id))
                          ?? Enumerable.Empty<Pirate>();

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Pirates.Clear();
                foreach (var pirate in pirates)
                    Pirates.Add(pirate);
            });
        }
    }
}
