﻿using System.Windows.Input;
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

        public TicTacToeViewModel()
        {
            _turn = 1;
            SourceArr = new ImageSource[9];
            ClickCommand = new Command<string>(HandleClick);
        }

        public ICommand ClickCommand { get; }

        private void HandleClick(string btnIndex)
        {
            var index = int.Parse(btnIndex);
            if (SourceArr[index] != null) return;
            SourceArr[index] = _turn++ % 2 == 1 ? _imageCircle : _imageCross;
            HandleWin(GetWinner());
            RaiseAllPropertiesChanged();
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

        private async void HandleWin(ImageSource result)
        {
            var player = result ==_imageCircle ? "Circle" : "Cross";
            if (result == null && _turn == 10)
            {
                await MaterialDialog.Instance.ConfirmAsync("Well that happened...",
                    "Draw", "Got It", string.Empty, BasicDialog);
                Reset();
            }
            if (result == null) return;

            var configuration = player.Equals("Circle") ? PlayerBlueWinDialog : PlayerRedWinDialog;
            await MaterialDialog.Instance.ConfirmAsync($"Player {player} Won!",
                "Congratulations", "Got It", string.Empty, configuration);
            Reset();
        }

        private void Reset()
        {
            _turn = 1;
            for (var i = 0; i < SourceArr.Length; i++)
                SourceArr[i] = null;
            RaiseAllPropertiesChanged();
        }
    }
}