using System.Collections.Generic;
using System.Windows.Input;
using GamesHub.Models;
using Xamarin.Forms;

namespace GamesHub.ViewModels
{
    public sealed class TwoPlayerGamesViewModel
    { 
        public TwoPlayerGamesViewModel()
        {
            Games = _games;
            GameSelectCommand = new Command<string>(HandleGameSelect);
        }

        public List<Game> Games { get; }

        public ICommand GameSelectCommand { get; }

        private static async void HandleGameSelect(string page)
        {
            await Shell.Current.GoToAsync(page);
        }
        private readonly List<Game> _games = new List<Game>
        {
            new Game
            {
                Name = "Tic-Tac-Toe",
                Description = "Who's the mastermind? Play with your friend and find out!",
                Image = "tictactoe_icon.png"
            },
            new Game
            {
                Name = "Reaction",
                Description = "Who's faster? Play with your friend and find out!",
                Image = "reaction_icon.png"
            },
            new Game
            {
                Name = "Math",
                Description = "Who's better in math? First to 10 wins!",
                Image = "math_icon.png"
            },
            new Game
            {
                Name = "Color Mind",
                Description = "Ever wanted to compare your minds? Then come here!",
                Image = "math_icon.png"
            }
        };
    }
}