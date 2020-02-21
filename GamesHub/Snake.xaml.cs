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
    public partial class Snake : ContentPage
    {
        private readonly Image[] Image;
        public Snake()
        {
            InitializeComponent();
            Image = new[] {img1, img2, img3};
        }
        private void Button_OnClicked(object sender, EventArgs e)
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                var rnd = new Random();
                var i = rnd.Next(0, 3);
                Image[i].BackgroundColor = Color.LimeGreen;
                return true;
            });
            

        }
    }
}   