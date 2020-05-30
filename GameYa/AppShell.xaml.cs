using GameYa.Views.Games;
using Xamarin.Forms;

namespace GameYa
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
            Routing.RegisterRoute("Math", typeof(Math));
            Routing.RegisterRoute("Color Mind", typeof(ColorMind));
        }
    }
}