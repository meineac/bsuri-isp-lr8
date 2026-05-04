using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Prokopovich_453503.UI.ValueConverters
{
    internal class BountyToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is long bounty && bounty < 100_000_000)
                return Colors.LightPink;

            return Colors.LightGreen;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
