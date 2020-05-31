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
    public sealed class ColorMindViewModel : BaseViewModel
    { 
        public ColorMindViewModel()
        {
            Players = new Player[2];
            for (var index = 0; index < Players.Length; index++)
                Players[index] = new Player(0);
            ColorMind = new ColorModel("", Color.Default);
            ClickCommand = new Command<string>(HandleClick);
            InitializeGame();
        }

        public ColorModel ColorMind { get; }
        public ICommand ClickCommand { get; }
        private readonly Random _random = new Random();

        private void InitializeGame()
        {
            if (_waitTime) return;
            ColorMind.ColorName = _colors[_random.Next(0, _colors.Count)].ColorName;
            ColorMind.Color = _colors[_random.Next(0, _colors.Count)].Color;
            GameLoop();
        }

        private void GameLoop()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1.5), () =>
            {
                InitializeGame();
                return false;
            });
        }

        private bool Winner() =>
            Players.Any(player => player.Score == 10);
        private bool CorrectAnswer() =>
            _colors.Any(color => ColorMind.Equals(color));

        private bool _waitTime;
        public Player[] Players { get; }
        private async void HandleClick(string index)
        {
            if(_waitTime) return;

            var player = int.Parse(index);
            if (CorrectAnswer())
                Players[player].Score++;
            else if (Players[player].Score > 0)
                Players[player].Score--;

            if (Winner())
                await HandleGameEnd(player);

            _waitTime = true;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                _waitTime = false;
                InitializeGame();
                return false;
            });
        }

        private async Task HandleGameEnd(int playerIndex)
        {
            var configuration = playerIndex == 0 ? PlayerRedWinDialog : PlayerBlueWinDialog;
            await MaterialDialog.Instance.ConfirmAsync($"Player {playerIndex + 1} Won!",
                "Congratulations", "Got It", string.Empty, configuration);
            foreach (var player in Players)
                    player.Score = 0;
        }

        private readonly List<ColorModel> _colors = new List<ColorModel>
        {
            new ColorModel("RED", Color.FromHex("#FF5252")), 
            new ColorModel("BLUE", Color.FromHex("#448AFF")), 
            new ColorModel("GREEN", Color.FromHex("#69F0AE")),
            new ColorModel("WHITE", Color.White),
            new ColorModel("ORANGE", Color.FromHex("#FFAB40")),
            new ColorModel("YELLOW", Color.FromHex("#FFFF00"))
        };
    }
}