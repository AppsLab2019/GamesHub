using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

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
            HandleWin();
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

        private async void HandleWin()
        {
            if (GetWinner() == null && _turn == 10)
            {
                await Application.Current.MainPage.DisplayAlert("Congratulations", "It's a draw", "Ok");
                Reset();
            }

            if (GetWinner() == null) return;

            await Application.Current.MainPage.DisplayAlert("Congratulations",
                $"Player {GetWinner().ToString().Replace("File: ", "").Replace(".png", "")} won!", "Ok");
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