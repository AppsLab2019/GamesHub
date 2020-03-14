using System.Windows.Input;
using GamesHub.Views;
using Xamarin.Forms;

namespace GamesHub.ViewModels
{
    class MainMenuViewModel
    {
        public ICommand TicTacToeCommand { get; }
        public ICommand ReactionCommand { get; }
        public ICommand SnakeCommand { get; }
        public MainMenuViewModel()
        {
            TicTacToeCommand = new Command(HandleTicTacToeClick);
            ReactionCommand = new Command(HandleReactionClick);
            SnakeCommand = new Command(HandleSnakeClick);
        }

        private void HandleTicTacToeClick()
        {
            ((NavigationPage) ((App) Application.Current).MainPage).PushAsync(new TicTacToe());

        }
        private void HandleReactionClick()
        {
            ((NavigationPage)((App)Application.Current).MainPage).PushAsync(new Reaction());
        }

        private void HandleSnakeClick()
        {
            ((NavigationPage)((App)Application.Current).MainPage).PushAsync(new Snake());
        }
    }
}
