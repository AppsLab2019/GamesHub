using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicTacToe.throwaway
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        const string image_x = "x.png";
        const string image_circle = "circle.png";

        const string file_image_x = "File: x.png";
        const string file_image_circle = "File: circle.png";

        int turn = 1;
            int idk = 0;

        private readonly ImageButton[] Buttons;

        public MainPage()
        {
            InitializeComponent();
            Buttons = new[] { ib0, ib1, ib2, ib3, ib4, ib5, ib6, ib7, ib8 };
            foreach (var variable in Buttons)
            {
                variable.Source = "";
            }
        }

        private void Clicked(object sender, EventArgs e)
        { 
            ((ImageButton) sender).Source = turn++ % 2 == 1 ? image_circle : image_x;
            ((ImageButton) sender).IsEnabled = false;
            IsThereWinner();
        }

        private void Reset(object sender, EventArgs e)
        {
            foreach (var button in Buttons)
            {
                button.Source = "";
                button.IsEnabled = true;
            }
            turn = 1;
            idk = 0;
            Page.BackgroundColor = Color.FromHex("#FFDBC7");
        }

        private void IsThereWinner()
        {
            for (var i = 2; i < 7; i += 2)
            {
                if (Buttons[i].Source.ToString() == file_image_circle)
                {
                    idk++;
                }
                if (idk == 3)
                    Page.BackgroundColor = Color.FromHex("#FFFFFF");
                
            }
            
        }
    }
}

