using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesHub.ViewModels
{
    public sealed class TicTacToeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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

        private readonly ImageSource[] _sourceArr;
        private readonly ImageSource _imageCross = "x.png";
        private readonly ImageSource _imageCircle = "circle.png";
        private int _turn;
        
        public TicTacToeViewModel()
        {
            _turn = 1;
            _sourceArr = new ImageSource[9];
            ClickCommand = new Command<string>(HandleClick);
        }

        private void HandleClick(string btnIndex)
        {
            var index = int.Parse(btnIndex);
            if(_sourceArr[index] != null) return;
            _sourceArr[index] = _turn++ % 2 == 1 ? _imageCircle : _imageCross;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(default));
        }
    }
}
