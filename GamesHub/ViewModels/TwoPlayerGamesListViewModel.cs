using System;
using System.Collections.Generic;
using System.Windows.Input;
using GamesHub.Models;
using GamesHub.Views.Games;
using Xamarin.Forms;

namespace GamesHub.ViewModels
{
    public sealed class TwoPlayerGamesListViewModel
    {
        public TwoPlayerGamesListViewModel()
        {
            Games = _games;
            GameSelectCommand = new Command<Game>(HandleGameSelect);
        }

        public List<Game> Games { get; }

        public ICommand GameSelectCommand { get; }

        private static async void HandleGameSelect(Game game)
        {
            var page = (Page) Activator.CreateInstance(game.GameViewType);
            await Shell.Current.Navigation.PushAsync(page);
        }
        private readonly List<Game> _games = new List<Game>
        {
            new Game
            {
                Name = "Tic-Tac-Toe",
                Description = "So who's the mastermind? Compare with your friends and find out!",
                Image = "circle.png",
                GameViewType = typeof(TicTacToe)
            },
            new Game
            {
                Name = "Reaction",
                Description = "So who's the mastermind? Compare with your friends and find out!",
                Image = "circle.png",
                GameViewType = typeof(Reaction)
            }
        };
    }
}