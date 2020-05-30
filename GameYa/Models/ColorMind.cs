using System;
using Xamarin.Forms;

namespace GameYa.Models
{
    public class ColorMind : BaseModel, IEquatable<ColorMind>
    {
        private Color _color;
        private string _colorName;

        public ColorMind(string colorName, Color color)
        {
            _colorName = colorName;
            _color = color;
        }

        public Color Color
        {
            get => _color;
            set => OnPropertyChanged(ref _color, value);
        }

        public string ColorName
        {
            get => _colorName;
            set => OnPropertyChanged(ref _colorName, value);
        }

        public bool Equals(ColorMind other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _color.Equals(other._color) && _colorName == other._colorName;
        }
    }
}