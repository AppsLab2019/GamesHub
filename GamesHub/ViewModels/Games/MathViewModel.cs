using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesHub.ViewModels.Games
{
    public sealed class MathViewModel : INotifyPropertyChanged
    {
        private readonly int[] _operationNumbers = new int[3];
        private readonly Random _random = new Random();
        private Operations _operation;
        private int _result;

        public MathViewModel()
        {
            OperationAnswers = new string[3];
            AnswerCommand = new Command<string>(HandleClick);
            GenerateRandomNumbers();
            GenerateRandomOperation();
            _operation(_operationNumbers[0], _operationNumbers[1], _operationNumbers[2]);
            GenerateAnswers();
        }

        public string Operation { get; private set; }
        public string[] OperationAnswers { get; }
        public ICommand AnswerCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void HandleClick(string s)
        {
            if (OperationAnswers[int.Parse(s)] == _result.ToString())
            {
            }
        }

        private void GenerateRandomNumbers()
        {
            for (var i = 0; i < _operationNumbers.Length; i++) _operationNumbers[i] = _random.Next(0, 30);
        }

        private void GenerateRandomOperation()
        {
            var operations = new Operations[]
                {Addition, Subtraction, AdditionAndSubtraction, SubtractionAndAddition};
            _operation = operations[_random.Next(0, operations.Length)];
        }

        private void GenerateAnswers()
        {
            OperationAnswers[_random.Next(0, OperationAnswers.Length)] = _result.ToString();
            for (var i = 0; i < OperationAnswers.Length; i++)
            {
                if (OperationAnswers[i] == _result.ToString()) continue;
                for (var j = 0; j < 99; j++)
                {
                    var wrongAnswer = "";
                    if (_random.Next(0, 1) == 1)
                        wrongAnswer = _result + _random.Next(0, 5).ToString();
                    else
                        wrongAnswer = (_result - _random.Next(0, 5)).ToString();

                    if (OperationAnswers.Contains(wrongAnswer)) continue;
                    OperationAnswers[i] = wrongAnswer;
                }
            }
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