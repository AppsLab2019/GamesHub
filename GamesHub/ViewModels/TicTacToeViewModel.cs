using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesHub.ViewModels
{
    public sealed class TicTacToeViewModel : INotifyPropertyChanged
    {
        private readonly ImageSource _imageCircle = "circle.png";
        private readonly ImageSource _imageCross = "x.png";

        private readonly ImageSource[] _sourceArr;
        private int _turn;

        public TicTacToeViewModel()
        {
            _turn = 1;
            _sourceArr = new ImageSource[9];
            ClickCommand = new Command<string>(HandleClick);
        }

        public ICommand ClickCommand { get; }
        public ImageSource FirstSource => _sourceArr[0];
        public ImageSource SecondSource => _sourceArr[1];
        public ImageSource ThirdSource => _sourceArr[2];
        public ImageSource FourthSource => _sourceArr[3];
        public ImageSource FifthSource => _sourceArr[4];
        public ImageSource SixthSource => _sourceArr[5];
        public ImageSource SeventhSource => _sourceArr[6];
        public ImageSource EighthSource => _sourceArr[7];
        public ImageSource NinthSource => _sourceArr[8];
        public event PropertyChangedEventHandler PropertyChanged;

        private void HandleClick(string btnIndex)
        {
            var index = int.Parse(btnIndex);
            if (_sourceArr[index] != null) return;
            _sourceArr[index] = _turn++ % 2 == 1 ? _imageCircle : _imageCross;

            HandleWin();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(default));
        }

        private ImageSource GetWinner()
        {
            //rows
            for (var i = 0; i <= 6; i += 3)
                if (_sourceArr[i] != null && _sourceArr[i] == _sourceArr[i + 1] &&
                    _sourceArr[i + 1] == _sourceArr[i + 2])
                    return _sourceArr[i];
            //columns
            for (var j = 0; j <= 2; j++)
                if (_sourceArr[j] != null && _sourceArr[j] == _sourceArr[j + 3] &&
                    _sourceArr[j + 3] == _sourceArr[j + 6])
                    return _sourceArr[j];

            //diagonal check; from upper right corner
            const int k = 2;
            if (_sourceArr[k] != null && _sourceArr[k] == _sourceArr[k + 2] &&
                _sourceArr[k + 2] == _sourceArr[k + 4]) return _sourceArr[k];

            //diagonal check; from upper left corner
            const int l = 0;
            if (_sourceArr[l] != null && _sourceArr[l] == _sourceArr[l + 4] &&
                _sourceArr[l + 4] == _sourceArr[l + 8]) return _sourceArr[l];

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
            for (var i = 0; i < _sourceArr.Length; i++)
                _sourceArr[i] = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(default));
        }
    }
}