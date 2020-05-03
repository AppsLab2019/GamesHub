using GamesHub.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace GamesHub.Converters
{
    public class IsUpgradeMaxTierConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ClickerUpgrade upgrade))
                return null;

            return upgrade.Tier >= upgrade.MaxTier;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
