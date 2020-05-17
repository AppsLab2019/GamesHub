using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesHub.ViewModels.Games
{
    public sealed class MathViewModel : BaseViewModel
    {
        private readonly int[] _operationNumbers = new int[3];
        private readonly Random _random = new Random();
        private Operations _operation;
        private int _result;
        private int _player1Score;
        private int _player2Score;

        public MathViewModel()
        {
            OperationAnswers = new int[3];
            AnswerCommand = new Command<string>(HandleClick);
            InitializeGame();
        }

        public string Operation { get; private set; }
        public int[] OperationAnswers { get; }
        public ICommand AnswerCommand { get; }
        public int Player1Score { get; private set; }

        public int Player2Score { get;  private set; }
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
                ManageScore(ref _player1Score, OperationAnswers[answerIndex]);
            else
                ManageScore(ref _player2Score, OperationAnswers[answerIndex]);
            NextRound();
        }

        private void ManageScore(ref int score, int answer)
        {
            if (answer == _result)
                score++;
            else if (score > 0)
                score--;
            Player1Score = _player1Score;
            Player2Score = _player2Score;
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
            OperationAnswers[_random.Next(0, OperationAnswers.Length)] = _result;
            for (var i = 0; i < OperationAnswers.Length; i++)
            {
                if (OperationAnswers[i] == _result) continue;
                for (var j = 0; j < 99; j++)
                {
                    var wrongAnswer = 0;
                    if (_random.Next(0, 1) == 1)
                        wrongAnswer = _result + _random.Next(0, 5);
                    else
                        wrongAnswer = _result - _random.Next(0, 5);

                    if (OperationAnswers.Contains(wrongAnswer)) continue;
                    OperationAnswers[i] = wrongAnswer;
                }
            }
        }
        private void NextRound()
        {
            if(_player1Score == 10 || _player2Score == 10)
                GameEnd();
            InitializeGame();
            RaiseAllPropertiesChanged();
        }

        private void GameEnd()
        {

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