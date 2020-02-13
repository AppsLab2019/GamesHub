using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GamesHub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reaction : ContentPage
    {
        private static Timer timer;

        public Reaction()
        {
            InitializeComponent();     
        }

        private void StartButton(object sender, EventArgs e)
        {
            MainButton.Text = "";
            var wait = new Random().Next(3000, 8000);
            timer = new Timer(wait);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;               
        }

        private void Player1Score(object sender, EventArgs e)
        {
            if (MainButton.BackgroundColor == Color.LimeGreen)
            {
                MainButton.BackgroundColor = Color.White;
                DisplayAlert("WINNER", "Player1 Won", "JA VIEM", "DAJ MI POKOJ");                            
            }
        }

        private void Player2Score(object sender, EventArgs e)
        {
            if (MainButton.BackgroundColor == Color.LimeGreen)
            {
                MainButton.BackgroundColor = Color.White;
                DisplayAlert("WINNER", "Player1 Won", "JA VIEM", "DAJ MI POKOJ");
            }
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {          
            MainButton.BackgroundColor = Color.LimeGreen;
        }

    }
}