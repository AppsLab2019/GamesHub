using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesHub.ViewModels.Games
{
    internal class ReactionViewModel : BaseViewModel
    {
        private readonly Random _rnd = new Random();
        private double _reactTime;
        private DateTime _startTime;
        private readonly Color _startButtonColor = Color.Black;
        private readonly Color _startButtonEventColor = Color.LimeGreen;
        

        public ReactionViewModel()
        {
            StartCommand = new Command(HandleStartButtonClicked);
            PlayerCommand = new Command<string>(HandlePlayerButtonClicked);
            Reset();
        }

        public ICommand StartCommand { get; }
        public ICommand PlayerCommand { get; }
        public Color StartButtonColor { get; private set; }
        public string StartButtonText { get; private set; }
        public bool StartButtonIsEnabled { get; private set; }
        
        private async void HandlePlayerButtonClicked(string player)
        {
            if (StartButtonColor != _startButtonEventColor) return;
            await Application.Current.MainPage.DisplayAlert("Congratulations", $"Player{player} was faster. Your reaction time was {ReactTime()} milliseconds!", "Ok");
            Reset();
        }

        private void HandleStartButtonClicked()
        {
            StartButtonText = "";
            StartButtonIsEnabled = false;
            RaiseAllPropertiesChanged();

            _reactTime = _rnd.Next(3, 8);
            Device.StartTimer(TimeSpan.FromSeconds(_reactTime), () =>
            {
                StartButtonColor = _startButtonEventColor;
                _startTime = DateTime.Now;
                RaiseAllPropertiesChanged();
                return false;
            });
        }
        
        private void Reset()
        {
            StartButtonColor = _startButtonColor;
            StartButtonText = "Start";
            StartButtonIsEnabled = true;
            RaiseAllPropertiesChanged();
        }
        
        private int ReactTime()
        {
            var totalTime = DateTime.Now.Subtract(_startTime).TotalMilliseconds;
            return (int) totalTime;
        }
    }
}