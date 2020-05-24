using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace GamesHub.ViewModels.Games
{
    public sealed class TicTacToeViewModel : BaseViewModel
    {
        private readonly ImageSource _imageCircle = "tictactoe_circle.png";
        private readonly ImageSource _imageCross = "tictactoe_cross.png";

        public ImageSource[] SourceArr { get; }
        private int _turn;
        private readonly Random _random = new Random();

        public TicTacToeViewModel()
        {
            for (var i = 0; i < _score.Length; i++) _score[i] = 0;
            Score = _score;
            _turn = _random.Next(0, 2);
            UpdateTurnImage();
            SourceArr = new ImageSource[9];
            ClickCommand = new Command<string>(HandleClick);
            ResetCommand = new Command(HandleReset);
        }

        public ICommand ClickCommand { get; }
        public ImageSource NextTurn { get; private set; }

        private void HandleClick(string btnIndex)
        {
            var index = int.Parse(btnIndex);
            if (SourceArr[index] != null) return;
            SourceArr[index] = _turn++ % 2 == 1 ? _imageCircle : _imageCross;
            UpdateTurnImage();
            HandleWin(GetWinner());
            RaiseAllPropertiesChanged();
        }
        private void UpdateTurnImage()
        {
            NextTurn = _turn % 2 == 1 ? _imageCircle : _imageCross;
        }

        private ImageSource GetWinner()
        {
            //rows
            for (var i = 0; i <= 6; i += 3)
                if (SourceArr[i] != null && SourceArr[i] == SourceArr[i + 1] &&
                    SourceArr[i + 1] == SourceArr[i + 2])
                    return SourceArr[i];
            //columns
            for (var j = 0; j <= 2; j++)
                if (SourceArr[j] != null && SourceArr[j] == SourceArr[j + 3] &&
                    SourceArr[j + 3] == SourceArr[j + 6])
                    return SourceArr[j];

            //diagonal check; from upper right corner
            const int k = 2;
            if (SourceArr[k] != null && SourceArr[k] == SourceArr[k + 2] &&
                SourceArr[k + 2] == SourceArr[k + 4]) return SourceArr[k];

            //diagonal check; from upper left corner
            const int l = 0;
            if (SourceArr[l] != null && SourceArr[l] == SourceArr[l + 4] &&
                SourceArr[l + 4] == SourceArr[l + 8]) return SourceArr[l];

            return null;
        }

        private bool PossibleDraw()
        {
            var cellsFilled = SourceArr.Count(image => image != null);
            return cellsFilled == 9;
        }

        private async void HandleWin(ImageSource result)
        {
            var player = result ==_imageCircle ? "Circle" : "Cross";
            if (PossibleDraw() && result == null)
            {
                await MaterialDialog.Instance.ConfirmAsync("Well that happened...",
                    "Draw", "Got It", string.Empty, BasicDialog);
                UpdateScore("Draw");
                Reset();
            }
            if (result == null) return;
            var configuration = player.Equals("Circle") ? PlayerBlueWinDialog : PlayerRedWinDialog;
            await MaterialDialog.Instance.ConfirmAsync($"Player {player} Won!",
                "Congratulations", "Got It", string.Empty, configuration);
            UpdateScore(player);
            Reset();
        }
        public ICommand ResetCommand { get; }
        public int[] Score { get; private set; }
        private readonly int[] _score = new int[3];
        private void Reset()
        {
            _turn = _random.Next(0, 2);
            UpdateTurnImage();  
            for (var i = 0; i < SourceArr.Length; i++)
                SourceArr[i] = null;
            RaiseAllPropertiesChanged();
        }

        private void HandleReset()
        {
            for (var i = 0; i < _score.Length; i++)
                _score[i] = 0;
            Score = _score;
            Reset();
        }

        private void UpdateScore(string winner)
        {
            switch (winner)
            {
                case "Draw":
                    _score[1]++;
                    break;
                case "Circle":
                    _score[2]++;
                    break;
                case "Cross":
                    _score[0]++;
                    break;
            }
            Score = _score;
        }
    }
}