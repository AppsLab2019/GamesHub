using Xamarin.Forms;

namespace GamesHub.Models
{
    public class MathButton : BaseModel
    {
        private int _text;
        private Color _color;
        public MathButton(int text, Color color)
        {
            _color = color;
            _text = text;
        }
        public Color Color
        {
            get => _color;
            set => OnPropertyChanged(ref _color, value);
        }

        public int Text
        {
            get => _text;
            set => OnPropertyChanged(ref _text, value);
        }
    }
}
