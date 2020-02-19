using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GamesHub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reaction : ContentPage
    {

        public Reaction()
        {
            InitializeComponent();
        }

        private void StartButton(object sender, EventArgs e)
        {
            Player1.IsEnabled = true;
            Player2.IsEnabled = true;
            MainButton.Text = "";
            MainButton.IsEnabled = false;
            var wait = new Random().Next(3, 8);
            Device.StartTimer(TimeSpan.FromSeconds(wait), () =>
            { 
                MainButton.BackgroundColor = Color.LimeGreen;
                return false;
            });
        }

        private void Player1Score(object sender, EventArgs e)
        {
            if (MainButton.BackgroundColor != Color.LimeGreen) return;
            Player2.IsEnabled = false;
            MainButton.BackgroundColor = Color.White;
            MainButton.Text = "START";
            MainButton.IsEnabled = true;
            DisplayAlert("WINNER", "Player1 Won", "OK");
        }

        private void Player2Score(object sender, EventArgs e) 
        {
            if (MainButton.BackgroundColor != Color.LimeGreen) return;
            Player1.IsEnabled = false;
            MainButton.BackgroundColor = Color.White;
            MainButton.Text = "START";
            MainButton.IsEnabled = true;
            DisplayAlert("WINNER", "Player2 Won", "OK");
        }

    }
}