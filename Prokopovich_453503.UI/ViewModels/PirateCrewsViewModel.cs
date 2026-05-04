using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prokopovich_453503.Application.PirateCrewUseCases.Queries;
using Prokopovich_453503.Application.PirateUseCases.Queries;
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
        PirateCrew selectedCrew;
        [RelayCommand]
        async Task UpdateGroupList() => await GetPirateCrews();
        [RelayCommand]
        async Task UpdateMembersList() => await GetPirates();
        public async Task GetPirateCrews()
        {
            var crews = await _mediator.Send(new GetAllPirateCrewsRequest());
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                PirateCrews.Clear();
                foreach(var crew in crews)
                {
                    PirateCrews.Add(crew);
                }
            });
        }
        public async Task GetPirates()
        {
            var pirates = await _mediator.Send(new GetPiratesByCrewRequest(SelectedCrew.Id));
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Pirates.Clear();
                foreach (var pirate in pirates)
                {
                    Pirates.Add(pirate);
                }
            });
        }
    }
}
