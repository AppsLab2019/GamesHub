using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GameYa.Models;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace GameYa.ViewModels.Games
{ 
    public sealed class ABCPatternViewModel : BaseViewModel
    {
        public ICommand ClickCommand { get; }
        public ABCPatternViewModel()
        {
            ClickCommand = new Command<string>(HandleClick);
            Players = new Player[2];
            for (var index = 0; index < Players.Length; index++)
                Players[index] = new Player(0);
            GenerateCombination();
        } 
        
        public string Combination { get; private set; }
        private string _combination;
        private int _combinationLength;
        private readonly Random _random = new Random();
        private void GenerateCombination()
        {
            ClearPlayerCombination();
            _combination = "";
            _combinationLength = _random.Next(4, 9);
            for (var i = 0; i < _combinationLength; i++)
            {
                var letter = _letters[_random.Next(0, _letters.Count)];
                _combination += letter;
            }
            Combination = _combination;
            RaiseAllPropertiesChanged();
        }

        public Player[] Players { get; }
        private readonly string[] _playerCombinations = new string[2];
        private bool _waitTime;
        private bool Winner() =>
            Players.Any(player => player.Score == 10);
        private bool CorrectAnswer(int player) =>
            _playerCombinations[player] == _combination;
        private bool WrongAnswer(int player) =>
            _playerCombinations[player].Length == _combinationLength;
        private void HandleClick(string input)
        {
            if(_waitTime) return;
            var indexes = input.Split(' ');
            var player = int.Parse(indexes[0]);
            var letter = indexes[1];

            _playerCombinations[player] += letter;

            if (CorrectAnswer(player))
            {
                Players[player].Score++;
                Timer();
            }
            else if (WrongAnswer(player))
            {
                if (Players[player].Score > 0)
                    Players[player].Score--;
                Timer();
            }
            if (Winner()) GameEnd(player);
        }

        private void ClearPlayerCombination()
        {
            for (var i = 0; i < _playerCombinations.Length; i++)
                _playerCombinations[i] = "";
        }

        private void Timer()
        {
            _waitTime = true;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                GenerateCombination();
                _waitTime = false;
                return false;
            });
        }
        private async void GameEnd(int playerIndex)
        {
            var configuration = playerIndex == 0 ? PlayerRedWinDialog : PlayerBlueWinDialog;
            await MaterialDialog.Instance.ConfirmAsync($"Player {playerIndex + 1} Won!",
                "Congratulations", "Got It", string.Empty, configuration);
            foreach (var player in Players)
                player.Score = 0;
            GenerateCombination();
        }
        private readonly List<string> _letters = new List<string>
        {
            "A", "B", "C"
        };
    }
}
