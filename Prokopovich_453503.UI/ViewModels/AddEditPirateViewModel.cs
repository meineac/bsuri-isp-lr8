using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prokopovich_453503.Application.PirateCrewUseCases.Queries;
using Prokopovich_453503.Application.PirateUseCases.Commands;
using System.Collections.ObjectModel;

namespace Prokopovich_453503.UI.ViewModels
{
    [QueryProperty(nameof(Pirate), "Pirate")]
    [QueryProperty(nameof(DefaultCrewId), "CrewId")]
    public partial class AddEditPirateViewModel : ObservableObject
    {
        private readonly IMediator _mediator;

        public AddEditPirateViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string PageTitle => Pirate is null ? "Новый пират" : "Редактировать пирата";

        [ObservableProperty]
        private Pirate? pirate;

        [ObservableProperty]
        private int defaultCrewId;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string bountyText = string.Empty;

        [ObservableProperty]
        private string role = string.Empty;

        [ObservableProperty]
        private string abilitiesText = string.Empty;

        [ObservableProperty]
        private PirateCrew? selectedCrew;

        public ObservableCollection<PirateCrew> AllCrews { get; } = new();

        partial void OnPirateChanged(Pirate? value)
        {
            OnPropertyChanged(nameof(PageTitle));

            if (value is null) return;

            Name = value.Name;
            BountyText = value.Bounty.ToString();
            Role = value.Role;
            AbilitiesText = string.Join(", ", value.Abilities);
        }

        [RelayCommand]
        private async Task LoadCrews()
        {
            var crews = await _mediator.Send(new GetAllPirateCrewsRequest());
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                AllCrews.Clear();
                foreach (var c in crews)
                    AllCrews.Add(c);

                var targetId = Pirate?.CrewId ?? DefaultCrewId;
                SelectedCrew = AllCrews.FirstOrDefault(c => c.Id == targetId);
            });
        }

        [RelayCommand]
        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Role))
            {
                await Shell.Current.DisplayAlert("Ошибка", "Заполните имя и роль", "OK");
                return;
            }

            if (!long.TryParse(BountyText, out long bounty) || bounty < 0)
            {
                await Shell.Current.DisplayAlert("Ошибка", "Некорректный Bounty", "OK");
                return;
            }

            var abilities = AbilitiesText
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim())
                .ToList();

            if (Pirate is null)
            {
                await _mediator.Send(new AddPirateCommand(
                    Name, bounty, Role, abilities, SelectedCrew?.Id));
            }
            else
            {
                await _mediator.Send(new UpdatePirateCommand(
                    Pirate.Id, Name, bounty, Role, abilities));

                if (SelectedCrew is not null && SelectedCrew.Id != Pirate.CrewId)
                    await _mediator.Send(new MovePirateToCrewCommand(
                        Pirate.Id, SelectedCrew.Id));
            }

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Cancel() => await Shell.Current.GoToAsync("..");
    }
}