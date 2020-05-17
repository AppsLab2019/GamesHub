using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GamesHub.Models;
using Xamarin.Forms;

namespace GamesHub.ViewModels.Games
{
    public class ClickerViewModel : BaseViewModel   
    {
        public ClickerUpgrade SelectedUpgrade { get; set; }
        public ICommand SelectedUpgradeCommand { get; }
        public ICommand ClickCommand { get; }

        public int Points { get; set; }

        public ObservableCollection<ClickerUpgrade> Upgrades { get; private set; }
        private List<ClickerUpgrade> _pointUpgrades;
        private ClickerUpgrade _multiClickUpgrade;

        public ClickerViewModel()
        {
            var savedProperties = Application.Current.Properties;

            if (!savedProperties.ContainsKey("clicker_points"))
                savedProperties["clicker_points"] = 0;

            Points = (int) savedProperties["clicker_points"];

            CreateUpgrades();

            SelectedUpgradeCommand = new Command(HandleSelectedUpgrade);
            ClickCommand = new Command(HandleClick);

            foreach (var upgrade in Upgrades) 
                upgrade.LoadTier();

            if (savedProperties.ContainsKey("clicker_last_update"))
            {
                var lastSavedTime = (DateTime) savedProperties["clicker_last_update"];
                var totalSeconds = DateTime.Now.Subtract(lastSavedTime).TotalSeconds;

                var seconds = (int) Math.Floor(totalSeconds);
                var profitPerSecond = GetCurrentGainPerSecond();

                Points += (seconds * profitPerSecond);
            }

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Points += GetCurrentGainPerSecond();

                savedProperties["clicker_points"] = Points;
                savedProperties["clicker_last_update"] = DateTime.Now;

                Application.Current.SavePropertiesAsync();
                OnPropertyChanged(nameof(Points));
                return true;
            });
        }

        private void HandleSelectedUpgrade()
        {
            if (SelectedUpgrade is null)
                return;

            if (SelectedUpgrade.Tier >= SelectedUpgrade.MaxTier)
            {
                return;
            }

            if (Points >= SelectedUpgrade.Price)
            {
                Points -= SelectedUpgrade.Price;
                SelectedUpgrade.Upgrade();
            }

            SelectedUpgrade = null;
            RaiseAllPropertiesChanged();
        }

        private void HandleClick()
        {
            Points += _multiClickUpgrade.Gain + 1;
            OnPropertyChanged(nameof(Points));
        }

        private void CreateUpgrades()
        {
            Upgrades = new ObservableCollection<ClickerUpgrade>();
            _pointUpgrades = new List<ClickerUpgrade>();

            _multiClickUpgrade = new ClickerUpgrade
            {
                Title = "Multi Click",
                ImageUrl = "x.png",
                BasePrice = 50,
                BaseGain = 1,
                MaxTier = 5
            };
            _pointUpgrades.Add(new ClickerUpgrade
            {
                Title = "Silver Farm",
                ImageUrl = "circle.png",
                BasePrice = 100,
                BaseGain = 1,
                MaxTier = 10
            });
            _pointUpgrades.Add(new ClickerUpgrade
            {
                Title = "Gold Farm",
                ImageUrl = "circle.png",
                BasePrice = 1000,
                BaseGain = 2,
                MaxTier = 10
            });
            _pointUpgrades.Add(new ClickerUpgrade
            {
                Title = "Platinum Farm",
                ImageUrl = "circle.png",
                BasePrice = 5000,
                BaseGain = 3,
                MaxTier = 10
            });
            _pointUpgrades.Add(new ClickerUpgrade
            {
                Title = "Diamond Farm",
                ImageUrl = "circle.png",
                BasePrice = 10000,
                BaseGain = 5,
                MaxTier = 2
            });

            Upgrades.Add(_multiClickUpgrade);

            foreach (var upgrade in _pointUpgrades) 
                Upgrades.Add(upgrade);
        }

        private int GetCurrentGainPerSecond() =>
            _pointUpgrades.Select(upgrade => upgrade.Gain).Sum();
    }
}