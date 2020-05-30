using System.Collections.Generic;
using System.Windows.Input;
using GameYa.Models;
using Xamarin.Forms;

namespace GameYa.ViewModels
{
    public sealed class GamesViewModel
    { 
        public GamesViewModel()
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
                Image = "colormind_icon.png"
            },
            new Game
            {
                Name = "SwipeGame",
                Description = "Ever wanted to compare your minds? Then come here!",
                Image = ""
            }
        };
    }
}