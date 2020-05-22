using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace GamesHub.ViewModels.Games
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
            Answers = new int[3];
            AnswerCommand = new Command<string>(HandleClick);
            InitializeGame();
        }

        public string Operation { get; private set; }
        public int[] Answers { get; private set; }
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
            var indexes = playerAndButtonIndex.Split(' ');
            var player = int.Parse(indexes[0]);
            var answerIndex = int.Parse(indexes[1]);

            if (player == 1)
                ManageScore(ref _playerRedScore, Answers[answerIndex]);
            else
                ManageScore(ref _playerBlueScore, Answers[answerIndex]);
            NextRoundAndCheckForWinner();
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
            for (var i = 0; i < Answers.Length; i++)
            {
                int answer;
                do
                {
                    var randomNumber = _random.Next(1, 6);
                    answer = _random.Next(0, 2) == 0 ? _result + randomNumber : _result - randomNumber;
                } while (!answers.Add(answer));
            }
            Answers = answers.ToArray();
            Answers[_random.Next(0, Answers.Length)] = _result;
        }
        private async void NextRoundAndCheckForWinner()
        {
            if(_playerRedScore == 10)
                await HandleGameEnd("Red");
            else if(_playerBlueScore == 10)
                await HandleGameEnd("Blue");
            InitializeGame();
            RaiseAllPropertiesChanged();
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