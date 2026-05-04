using System.Globalization;

namespace Prokopovich_453503.UI.ValueConverters
{
    internal class ListToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is List<string> list)
                return string.Join(", ", list);
            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
