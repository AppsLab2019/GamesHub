using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GameYa.Models;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;


namespace GameYa.ViewModels.Games
{
    public class SwipeGameViewModel : BaseViewModel
    {
        private readonly Random _random = new Random();
        private bool _waitTime;

        private int BaseSpeed => 1600;
        private int ScoreAverage => CalculateScoreAverage();
        private int Speed => CalculateSpeed();

        public string DirectionText { get; set; }
        public ICommand SwipeCommand { get; }
        public Player[] Players { get; }

        private readonly List<string> _directions = new List<string>
        {
            "UP", "DOWN", "RIGHT", "LEFT"
        };


        public SwipeGameViewModel()
        {
            Players = new Player[2];
            for (var index = 0; index < Players.Length; index++)
                Players[index] = new Player(0);

            SwipeCommand = new Command<string>(HandleSwipeCommand);
            InitializeDirection();
        }

        private void InitializeDirection()
        {
            if (_waitTime) return;
            while (true)
            {
                var randomDirection = _directions[_random.Next(0, _directions.Count)];
                if (DirectionText != null)
                    if (DirectionText.Contains(randomDirection))
                        continue;

                DirectionText = randomDirection;
                break;
            }

            OnPropertyChanged(nameof(DirectionText));
            Timer(Speed);
        }

        private async void HandleSwipeCommand(string swipe)
        {
            if (_waitTime) return;

            var player = int.Parse(swipe.Substring(0, 1));

            if (swipe.Contains(DirectionText))
                Players[player].Score++;

            else if (Players[player].Score > 0)
                Players[player].Score--;

            if (Players[player].Score >= 10)
                await HandleEndGame(player);

            _waitTime = true;
        }

        private async Task HandleEndGame(int player)
        {
            var configuration = player == 0 ? PlayerRedWinDialog : PlayerBlueWinDialog;
            await MaterialDialog.Instance.ConfirmAsync($"Player {player + 1} Won!",
                "Congratulations", "Got It", string.Empty, configuration);

            foreach (var p in Players)
                p.Score = 0;
        }

        private void Timer(int milliseconds)
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(milliseconds), () =>
            {
                _waitTime = false;

                InitializeDirection();

                return false;
            });
        }

        private int CalculateScoreAverage()
        {
            return Players.Select(player => player.Score).Sum() / 2;
        }

        private int CalculateSpeed()
        {
            if (ScoreAverage == 0) return BaseSpeed;
            return BaseSpeed - ScoreAverage * 100;
        }

        
    }

}