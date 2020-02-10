using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            MainButton.Text = null;
            MainButton.IsEnabled = false; 
            var wait = new System.Random().Next(3000, 8000);
            System.Threading.Thread.Sleep(wait);
            MainButton.BackgroundColor = Color.LimeGreen;
            
        }

        private void Player1Score(object sender, EventArgs e)
        {
            MainButton.BackgroundColor = Color.White;
            DisplayAlert("WINNER", "Player1 Won", "JA VIEM", "DAJ MI POKOJ");
            MainButton.Text = "START";
            MainButton.IsEnabled = true;
        }

        private void Player2Score(object sender, EventArgs e)
        {
            MainButton.BackgroundColor = Color.White;
            DisplayAlert("WINNER", "Player2 Won", "JA VIEM", "DAJ MI POKOJ");
            MainButton.Text = "START";
            MainButton.IsEnabled = true;
        }

    }
}