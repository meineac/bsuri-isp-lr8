using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prokopovich_453503.Application.PirateCrewUseCases.Commands;

namespace Prokopovich_453503.UI.ViewModels
{
    [QueryProperty(nameof(Crew), "PirateCrew")]
    public partial class AddEditCrewViewModel : ObservableObject
    {
        private readonly IMediator _mediator;

        public AddEditCrewViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string PageTitle => Crew is null ? "Новая команда" : "Редактировать команду";

        [ObservableProperty]
        private PirateCrew? crew;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string ship = string.Empty;

        [ObservableProperty]
        private CrewStatus selectedStatus;

        public List<CrewStatus> AllStatuses { get; } =
            Enum.GetValues<CrewStatus>().ToList();

        partial void OnCrewChanged(PirateCrew? value)
        {
            OnPropertyChanged(nameof(PageTitle));

            if (value is null) return;

            Name = value.Name;
            Ship = value.Ship;
            SelectedStatus = value.Status;
        }

        [RelayCommand]
        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Ship))
            {
                await Shell.Current.DisplayAlert("Ошибка", "Заполните все поля", "OK");
                return;
            }

            if (Crew is null)
                await _mediator.Send(new AddPirateCrewCommand(Name, Ship, SelectedStatus));
            else
                await _mediator.Send(new UpdatePirateCrewCommand(
                    Crew.Id, Name, Ship, SelectedStatus));

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Cancel() => await Shell.Current.GoToAsync("..");
    }
}