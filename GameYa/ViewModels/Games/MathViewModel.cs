using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GameYa.Models;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace GameYa.ViewModels.Games
{
    public sealed class MathViewModel : BaseViewModel
    {
        private readonly int[] _operationNumbers = new int[3];
        private readonly Random _random = new Random();
        private Operations _operation;
        private int _result;
        private int _playerRedScore;
        private int _playerBlueScore;

        public MathViewModel()
        {
            Buttons = new MathButton[3];
            for (var i = 0; i < Buttons.Length; i++)
                Buttons[i] = new MathButton(0, (Color)Resources["Color.Surface"]);
            InitializeGame();
            AnswerCommand = new Command<string>(HandleClick);
        }

        public string Operation { get; private set; }
        public MathButton[] Buttons { get; }
        public ICommand AnswerCommand { get; }
        public int PlayerRedScore { get; private set; }

        public int PlayerBlueScore { get;  private set; }
        private void InitializeGame()
        {
            GenerateMathProblem();
            GenerateAnswers();
        }
        private void HandleClick(string playerAndButtonIndex)
        {
            if(_waitTime) return;
            var indexes = playerAndButtonIndex.Split(' ');
            var player = int.Parse(indexes[0]);
            var answerIndex = int.Parse(indexes[1]);

            if (player == 1)
                ManageScore(ref _playerRedScore, Buttons[answerIndex].Text);
            else
                ManageScore(ref _playerBlueScore, Buttons[answerIndex].Text);
            
            HandleAnswer(answerIndex);
            CheckForWinner();
        }

        private bool _waitTime;
        private void HandleAnswer(int index)
        {
            var color = Buttons[index].Text == _result ? (Color) Resources["Color.PrimaryGreen"] : (Color) Resources["Color.PrimaryRed"];
            _waitTime = true;
            Buttons[index].Color = color;
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                _waitTime = false;
                Buttons[index].Color = (Color) Resources["Color.Surface"];
                InitializeGame();
                RaiseAllPropertiesChanged();
                return false;
            });
        }
        private void ManageScore(ref int score, int answer)
        {
            if (answer == _result)
                score++;
            else if (score > 0)
                score--;
            PlayerRedScore = _playerRedScore;
            PlayerBlueScore = _playerBlueScore;
            RaiseAllPropertiesChanged();
        }
        private void GenerateMathProblem()
        {
            var operations = new Operations[]
                {Addition, Subtraction, AdditionAndSubtraction, SubtractionAndAddition};
            _operation = operations[_random.Next(0, operations.Length)];

            for (var i = 0; i < _operationNumbers.Length; i++) _operationNumbers[i] = _random.Next(0, 30);
            _operation(_operationNumbers[0], _operationNumbers[1], _operationNumbers[2]);
        }

        private void GenerateAnswers()
        {
            var answers = new HashSet<int>();
            for (var i = 0; i < Buttons.Length; i++)
            {
                int answer;
                do
                {
                    var randomNumber = _random.Next(1, 6);
                    answer = _random.Next(0, 2) == 0 ? _result + randomNumber : _result - randomNumber;
                } while (!answers.Add(answer));
            }
            var array = answers.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                Buttons[i].Text = array[i];
            }
            Buttons[_random.Next(0, Buttons.Length)].Text = _result;
        }
        private async void CheckForWinner()
        {
            if(_playerRedScore == 10)
                await HandleGameEnd("Red");
            else if(_playerBlueScore == 10)
                await HandleGameEnd("Blue");
        }

        private async Task HandleGameEnd(string player)
        {
            var configuration = player == "Red" ? PlayerRedWinDialog : PlayerBlueWinDialog;
            await MaterialDialog.Instance.ConfirmAsync($"Player {player} Won!",
                "Congratulations", "Got It", string.Empty, configuration);

            PlayerBlueScore = 0;
            PlayerRedScore = 0;
            _playerBlueScore = 0;
            _playerRedScore = 0;
        }

        private void Addition(int firstNumber, int secondNumber, int thirdNumber)
        {
            _result = firstNumber + secondNumber + thirdNumber;
            Operation = $"{firstNumber} + {secondNumber} + {thirdNumber}";
        }

        private void Subtraction(int firstNumber, int secondNumber, int thirdNumber)
        {
            _result = firstNumber - secondNumber - thirdNumber;
            Operation = $"{firstNumber} - {secondNumber} - {thirdNumber}";
        }

        private void AdditionAndSubtraction(int firstNumber, int secondNumber, int thirdNumber)
        {
            _result = firstNumber + secondNumber - thirdNumber;
            Operation = $"{firstNumber} + {secondNumber} - {thirdNumber}";
        }

        private void SubtractionAndAddition(int firstNumber, int secondNumber, int thirdNumber)
        {
            _result = firstNumber - secondNumber + thirdNumber;
            Operation = $"{firstNumber} - {secondNumber} + {thirdNumber}";
        }

        private delegate void Operations(int firstOperand, int secondOperand, int thirdOperand);
    }
}