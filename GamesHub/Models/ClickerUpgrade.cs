using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace GamesHub.Models
{
    public class ClickerUpgrade : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public int BasePrice { get; set; }
        public int BaseGain { get; set; }
        public string ImageUrl { get; set; }
        public int MaxTier { get; set; }

        public int Price => CalculatePrice();
        public int Tier { get; private set; }
        public int Gain => BaseGain * Tier;

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadTier()
        {
            var savedProperties = Application.Current.Properties;

            if (savedProperties.TryGetValue($"{Title}_tier", out var savedTier))
                Tier = (int) savedTier;
        }

        public void Upgrade()
        {
            ++Tier;
            Application.Current.Properties[$"{Title}_tier"] = Tier;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(default));
        }

        public int CalculatePrice()
        {
            if (Tier == 0)
                return BasePrice;

            return BasePrice * (int) Math.Pow(4, Tier);
        }
    }
}
