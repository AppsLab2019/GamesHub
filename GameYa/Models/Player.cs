namespace GameYa.Models
{
    public class Player : BaseModel
    {
        private int _score;

        public Player(int score)
        {
            _score = score;
        }

        public int Score
        {
            get => _score;
            set => OnPropertyChanged(ref _score, value);
        }
    }
}