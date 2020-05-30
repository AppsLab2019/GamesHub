using System;
using System.Collections.Generic;
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
            var randomDirection = _directions[_random.Next(0, _directions.Count)];
            DirectionText = randomDirection;
            OnPropertyChanged(nameof(DirectionText));
        }

        private async void HandleSwipeCommand(string swipe)
        {
            var player = int.Parse(swipe.Substring(0, 1));

            if (swipe.Contains(DirectionText))
                Players[player].Score++;
            else if (Players[player].Score > 0)
                Players[player].Score--;
            if (Players[player].Score >= 10)
                await HandleEndGame(player);

            InitializeDirection();
        }

        private async Task HandleEndGame(int player)
        {
            await MaterialDialog.Instance.ConfirmAsync($"Player {player + 1} Won!",
                "Congratulations", "Got It");
            foreach (var p in Players)
                p.Score = 0;
        }
    }

}