using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GamesHub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TicTacToe : ContentPage
    {
        private const string ImageX = "x.png";
        private const string ImageCircle = "circle.png";

        private const string ImageXSource = "File: x.png";
        private const string ImageCircleSource = "File: circle.png";

        private readonly ImageButton[] Buttons;

        private int _turn = 1;

        public TicTacToe()
        {
            InitializeComponent();
            Buttons = new[] {ib0, ib1, ib2, ib3, ib4, ib5, ib6, ib7, ib8};
            foreach (var imageButton in Buttons)
            {
                imageButton.Source = "";
                imageButton.CornerRadius = 0;
            }
        }

        private void Clicked(object sender, EventArgs e)
        {
            ((ImageButton) sender).Source = _turn++ % 2 == 1 ? ImageCircle : ImageX;
            ((ImageButton) sender).IsEnabled = false;
            if (WhoIsWinner() == ImageXSource)
                DisplayAlert("WINNER", "X WON!", "JA VIEM", "DAJ MI POKOJ");
            else if (WhoIsWinner() == ImageCircleSource) DisplayAlert("WINNER", "O WON!", "JA VIEM", "DAJ MI POKOJ");
        }

        private void Reset(object sender, EventArgs e)
        {
            foreach (var button in Buttons)
            {
                button.Source = "";
                button.IsEnabled = true;
            }

            _turn = 1;
        }

        private string WhoIsWinner()
        {
            if (ib0.IsEnabled != true && ib0.Source.ToString() == ib1.Source.ToString() &&
                ib1.Source.ToString() == ib2.Source.ToString()) return ib0.Source.ToString();
            if (ib3.IsEnabled != true && ib3.Source.ToString() == ib4.Source.ToString() &&
                ib4.Source.ToString() == ib5.Source.ToString()) return ib3.Source.ToString();
            if (ib6.IsEnabled != true && ib6.Source.ToString() == ib7.Source.ToString() &&
                ib7.Source.ToString() == ib8.Source.ToString()) return ib6.Source.ToString();
            if (ib0.IsEnabled != true && ib0.Source.ToString() == ib3.Source.ToString() &&
                ib3.Source.ToString() == ib6.Source.ToString()) return ib0.Source.ToString();
            if (ib1.IsEnabled != true && ib1.Source.ToString() == ib4.Source.ToString() &&
                ib4.Source.ToString() == ib7.Source.ToString()) return ib1.Source.ToString();
            if (ib2.IsEnabled != true && ib2.Source.ToString() == ib5.Source.ToString() &&
                ib5.Source.ToString() == ib8.Source.ToString()) return ib2.Source.ToString();
            if (ib0.IsEnabled != true && ib0.Source.ToString() == ib4.Source.ToString() &&
                ib4.Source.ToString() == ib8.Source.ToString()) return ib0.Source.ToString();
            if (ib2.IsEnabled != true && ib2.Source.ToString() == ib4.Source.ToString() &&
                ib4.Source.ToString() == ib6.Source.ToString()) return ib2.Source.ToString();
            return null;
        }
    }
}