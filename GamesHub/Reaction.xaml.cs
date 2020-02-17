using System;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GamesHub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reaction : ContentPage
    {
        private static Timer _timer;

        public Reaction()
        {
            InitializeComponent();
        }

        private void StartButton(object sender, EventArgs e)
        {
            MainButton.Text = "";
            SetTimer();        
        }

        private void Player1Score(object sender, EventArgs e)
        {
            if (MainButton.BackgroundColor != Color.LimeGreen) return;
            MainButton.BackgroundColor = Color.White;
            DisplayAlert("WINNER", "Player1 Won", "JA VIEM", "DAJ MI POKOJ");
        }

        private void Player2Score(object sender, EventArgs e)
        {
            if (MainButton.BackgroundColor != Color.LimeGreen) return;
            MainButton.BackgroundColor = Color.White;
            DisplayAlert("WINNER", "Player2 Won", "JA VIEM", "DAJ MI POKOJ");
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            MainButton.BackgroundColor = Color.LimeGreen;
        }
        private void SetTimer()
        {
            MainButton.IsEnabled = false;
            var wait = new Random().Next(3000, 8000);
            _timer = new Timer(wait);
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
        }
    }
}