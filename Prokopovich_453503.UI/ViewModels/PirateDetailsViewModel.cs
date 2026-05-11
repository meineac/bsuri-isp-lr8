using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prokopovich_453503.Application.PirateCrewUseCases.Queries;
using Prokopovich_453503.Application.PirateUseCases.Commands;
using Prokopovich_453503.UI.Pages;
using System.Collections.ObjectModel;

namespace Prokopovich_453503.UI.ViewModels
{
    [QueryProperty(nameof(Pirate), "Pirate")]
    public partial class PirateDetailsViewModel : ObservableObject
    {
        private readonly IMediator _mediator;

        public PirateDetailsViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ObservableProperty]
        private Pirate? pirate;

        // Путь к изображению — обновляется после сохранения фото
        [ObservableProperty]
        private ImageSource? pirateImage;

        partial void OnPirateChanged(Pirate? value)
        {
            if (value is not null)
                RefreshImage();
        }

        private void RefreshImage()
        {
            if (Pirate is null) return;

            string dir = Path.Combine(FileSystem.Current.AppDataDirectory, "Images");
            if (Directory.Exists(dir))
            {
                var file = Directory.GetFiles(dir, $"{Pirate.Id}.*").FirstOrDefault();
                if (file != null)
                {
                    PirateImage = ImageSource.FromFile(file);
                    return;
                }
            }
            PirateImage = "profile.png";
        }

        [RelayCommand]
        private async Task Edit()
        {
            if (Pirate is null) return;
            await Shell.Current.GoToAsync(nameof(AddEditPiratePage),
                new Dictionary<string, object> { ["Pirate"] = Pirate });
        }

        [RelayCommand]
        private async Task Delete()
        {
            if (Pirate is null) return;
            bool confirm = await Shell.Current.DisplayAlertAsync(
                "Удаление", $"Удалить пирата «{Pirate.Name}»?", "Да", "Нет");
            if (!confirm) return;

            await _mediator.Send(new DeletePirateCommand(Pirate.Id));
            await Shell.Current.GoToAsync("..");
        }

        public ObservableCollection<PirateCrew> AllCrews { get; } = new();
        [RelayCommand]
        private async Task PickImage()
        {
            if (Pirate is null) return;

            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Выберите фото",
                    FileTypes = FilePickerFileType.Images
                });
                if (result is null) return;

                string dir = Path.Combine(
                    FileSystem.Current.AppDataDirectory, "Images");
                Directory.CreateDirectory(dir);

                string ext = Path.GetExtension(result.FileName);
                foreach (var old in Directory.GetFiles(dir, $"{Pirate.Id}.*"))
                    File.Delete(old);

                string dest = Path.Combine(dir, $"{Pirate.Id}{ext}");
                using var source = await result.OpenReadAsync();
                using var destStream = File.OpenWrite(dest);
                await source.CopyToAsync(destStream);

                RefreshImage();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Ошибка", ex.Message, "OK");
            }
        }
    }
}