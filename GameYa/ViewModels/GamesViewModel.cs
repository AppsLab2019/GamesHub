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
                Description = "We’ve dug all over, but we can’t find any treasure.",
                Image = "tictactoe_icon.png"
            },
            new Game
            {
                Name = "Reaction",
                Description = "So you are faster than light? Let's prove that!",
                Image = "reaction_icon.png"
            },
            new Game
            {
                Name = "Math",
                Description = "Let's see you prove you have a big brain.",
                Image = "math_icon.png"
            },
            new Game
            {
                Name = "Color Mind",
                Description = "Be careful! Don't play for too long, because your brain may explode!",
                Image = "colormind_icon.png"
            },
            new Game
            {
                Name = "Swipe Game",
                Description = "There's only one rule. Just keep swiping!",
                Image = "swipegame_icon.png"
            },
            new Game
            {
                Name = "ABC Pattern",
                Description = "Yep, another game as simple as this description!",
                Image = "letterpattern_icon.png"
            }
        };
    }
}