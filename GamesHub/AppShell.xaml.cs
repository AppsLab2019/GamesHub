using GamesHub.Views.Games;
using Xamarin.Forms;

namespace GamesHub
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }
        protected override bool OnBackButtonPressed() { Current.Navigation.PopAsync(false); return true; }
        private void RegisterRoutes()
        {
            Routing.RegisterRoute("Tic-Tac-Toe", typeof(TicTacToe));
            Routing.RegisterRoute("Reaction", typeof(Reaction));
            Routing.RegisterRoute("Snake", typeof(Snake));
        }
    }
}