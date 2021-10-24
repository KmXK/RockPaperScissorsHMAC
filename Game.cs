using System;
using System.Linq;
using System.Security.Cryptography;

namespace RockPaperScissorsHMAC
{
    public class Game
    {
        private RuleTable _ruleTable;
        private byte[] _key;
        private string _hmac;
        private string _pcChoice;

        public Game()
        { }

        public void Start(string[] args)
        {
            if (!AreMovesValid(args)) return;

            _ruleTable = new RuleTable(args);
            _key = HMACKeyGenerator.GenerateHMACKey();

            _pcChoice = GenerateComputerMove();

            _hmac = HMACGenerator.CreateHMAC(_key, _pcChoice);

            Console.WriteLine($"HMAC: {_hmac}");

            bool gameEnd = false;
            string playerChoice = "";

            while (!gameEnd)
            {
                var error = false;
                ShowMenu();
                Console.Write("Введите ход: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int number))
                {
                    if (number == 0)
                        return;
                    if (number >= 1 && number <= _ruleTable.PosibleMoves.Length)
                    {
                        playerChoice = _ruleTable.PosibleMoves[number - 1];
                        gameEnd = true;
                    }
                    else error = true;
                }
                else if (input == "?")
                    ShowHelp();
                else error = true;

                if(error)
                    Console.WriteLine("Ошибка! Выбран несуществующий пункт меню. Попробуйте ещё раз.");
            }

            Console.WriteLine($"Ваш ход: {playerChoice}");
            Console.WriteLine($"Ход компьютера: {_pcChoice}");
            switch (_ruleTable.GetGameResult(playerChoice, _pcChoice))
            {
                case GameResult.Win:
                    Console.WriteLine("Вы победили!");
                    break;
                case GameResult.Lose:
                    Console.WriteLine("Вы проиграли!");
                    break;
                case GameResult.Draw:
                    Console.WriteLine("Ничья!");
                    break;
            }
            Console.WriteLine($"HMAC ключ: {BitConverter.ToString(_key).Replace("-", string.Empty).ToLower()}");
        }

        private void ShowHelp()
        {
            OutputRulesFormatter.OutputRules(_ruleTable);
        }

        private void ShowMenu()
        {
            Console.WriteLine("Возможные ходы:");
            for (var i = 0; i < _ruleTable.PosibleMoves.Length; i++)
                Console.WriteLine($"{i}. {_ruleTable.PosibleMoves[i]}");
            Console.WriteLine("0. Выход");
            Console.WriteLine("?. Помощь");
        }

        private string GenerateComputerMove()
        {
            int index = RandomNumberGenerator.GetInt32(0, _ruleTable.PosibleMoves.Length);
            string pcChoice = _ruleTable.PosibleMoves[index];
            return pcChoice;
        }

        private static bool AreMovesValid(string[] moves)
        {
            if (moves.Length % 2 == 0 || moves.Length == 1)
            {
                Console.WriteLine("Ошибка! Количество возможных ходов должно быть нечётным числом больше 1.");
                return false;
            }

            if (moves.GroupBy(m => m).Any(m => m.Count() > 1))
            {
                Console.WriteLine("Ошибка! В возможных ходах не должно быть повторений.");
                return false;
            }

            return true;
        }
    }
}