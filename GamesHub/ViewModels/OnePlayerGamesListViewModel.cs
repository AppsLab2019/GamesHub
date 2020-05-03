using System.Collections.Generic;
using System.Windows.Input;
using GamesHub.Models;
using Xamarin.Forms;

namespace GamesHub.ViewModels
{
    public sealed class OnePlayerGamesListViewModel
    { 
        public OnePlayerGamesListViewModel()
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
                Description = "So who's the mastermind? Compare with your friends and find out!",
                Image = "circle.png"
            },
            new Game
            {
                Name = "Clicker",
                Description = "So who's the mastermind? Compare with your friends and find out!",
                Image = "circle.png"
            }
        };
    }
}