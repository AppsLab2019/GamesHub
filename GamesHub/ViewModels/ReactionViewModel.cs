using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesHub.ViewModels
{
    internal class ReactionViewModel : INotifyPropertyChanged
    {
        private readonly Random _rnd = new Random();
        private double _reactTime;

        public ReactionViewModel()
        {
            StartCommand = new Command(HandleStartButtonClicked);
            PlayerCommand = new Command<string>(HandlePlayerButtonClicked);

            Reset();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(default));
        }

        public ICommand StartCommand { get; }
        public ICommand PlayerCommand { get; }
        public Color StartButtonColor { get; private set; }
        public string StartButtonText { get; private set; }
        public bool StartButtonIsEnabled { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private async void HandlePlayerButtonClicked(string player)
        {
            if (StartButtonColor != Color.LimeGreen) return;
            await Application.Current.MainPage.DisplayAlert("Congratulations", $"Player{player} was faster!", "Ok");

            Reset();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(default));
        }

        private void HandleStartButtonClicked()
        {
            StartButtonText = "";
            StartButtonIsEnabled = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(default));

            _reactTime = _rnd.Next(3, 8);
            Device.StartTimer(TimeSpan.FromSeconds(_reactTime), () =>
            {
                StartButtonColor = Color.LimeGreen;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(default));
                return false;
            });
        }

        private void Reset()
        {
            StartButtonColor = Color.White;
            StartButtonText = "Start";
            StartButtonIsEnabled = true;
        }
    }
}