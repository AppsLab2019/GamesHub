using GamesHub.Views;
using Xamarin.Forms;

namespace GamesHub
{ 
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            MainPage = new  NavigationPage(new MainMenu());
        }
    }
}