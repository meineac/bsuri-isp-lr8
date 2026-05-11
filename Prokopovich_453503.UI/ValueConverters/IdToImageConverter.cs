using System.Globalization;

namespace Prokopovich_453503.UI.ValueConverters
{
    internal class IdToImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType,
            object? parameter, CultureInfo culture)
        {
            if (value is not int id || id <= 0)
                return "dotnet_bot.png";

            string imagesDir = Path.Combine(
                FileSystem.Current.AppDataDirectory, "Images");

            if (Directory.Exists(imagesDir))
            {
                var file = Directory.GetFiles(imagesDir, $"{id}.*")
                                    .FirstOrDefault();
                if (file != null)
                    return ImageSource.FromFile(file);
            }
            return "dotnet_bot.png";
        }

        public object? ConvertBack(object? value, Type targetType,
            object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}