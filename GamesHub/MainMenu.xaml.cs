using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GamesHub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        private void TicTacToe_Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TicTacToe());
        }

        private void Reaction_Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Reaction());
        }

        private void Snake_Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Snake());
        }
    }
}