using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using static System.String;

namespace GamesHub.ViewModels.Games
{
    internal class ReactionViewModel : BaseViewModel
    { 
        public string StartButtonText { get; private set; }
        public Color StartButtonColor { get; private set; }
        public ICommand ClickCommand => new Command<string>(HandleClick);
        private readonly Random _rnd = new Random();
        private bool _isFlashColor;
        private bool _activeTimer;
        private const string StartText = "start";
        private static readonly Color FlashColor = Color.FromHex("#69F0AE");
        private DateTime _flashStart;

        public ReactionViewModel() =>
            InitializeGame();
        private async void HandleClick(string parameter)
        { 
            if (_activeTimer | _isFlashColor && parameter.Equals(StartText)) return;
            if (parameter.Equals(StartText))
            {
                _activeTimer = true;
                StartButtonText = Empty;
                RaiseAllPropertiesChanged();

                Device.StartTimer(TimeSpan.FromSeconds(_rnd.Next(3, 10)), () =>
               {
                   _isFlashColor = true;
                   StartButtonColor = FlashColor;
                   _flashStart = DateTime.Now;
                   RaiseAllPropertiesChanged();
                   return false;
               });
            }
            else if (_isFlashColor)
            {
                var configuration = parameter == "Player 1" ? PlayerRedWinDialog : PlayerBlueWinDialog;
                var reactTime = DateTime.Now.Subtract(_flashStart).TotalMilliseconds;
                await MaterialDialog.Instance.ConfirmAsync($"{parameter} Won! Your reaction time was {Convert.ToInt32(reactTime)} ms.", 
                   "Congratulations", "Got It", Empty, configuration);
                InitializeGame();
            }
        }

        private void InitializeGame()
        {
            StartButtonColor = Color.Transparent;
            _activeTimer = false;
            _isFlashColor = false;
            StartButtonText = StartText;
            RaiseAllPropertiesChanged();
        }
    }
}