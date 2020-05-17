using System.Collections.Generic;
using System.Windows.Input;
using GamesHub.Models;
using Xamarin.Forms;

namespace GamesHub.ViewModels
{
    public sealed class OnePlayerGamesViewModel
    { 
        public OnePlayerGamesViewModel()
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
                Name = "Snake",
                Description = "Want to play snake like you had on your Nokia? Then come here.",
                Image = "snake_icon.png"
            },
            new Game
            {
                Name = "Clicker",
                Description = "Do you feel like you aren't clicking enough? Then click here you won't regret it!",
                Image = "clicker_icon.png"
            }
        };
    }
}