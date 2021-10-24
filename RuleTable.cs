using System;
using System.Linq;

namespace RockPaperScissorsHMAC
{
    public class RuleTable
    {
        public string[] PosibleMoves { get; }
        private GameResult[,] _rules;

        public RuleTable(string[] posibleMoves)
        {
            int delta = posibleMoves.Length / 2;
            PosibleMoves = posibleMoves;

            _rules = new GameResult[posibleMoves.Length, posibleMoves.Length];
            for (var i = 0; i < posibleMoves.Length; i++)
            {
                _rules[i, i] = GameResult.Draw;
                for (var j = 0; j < 2 * delta; j++) 
                    _rules[i, (i + j + 1) % posibleMoves.Length] = 
                        j < delta ? GameResult.Lose : GameResult.Win;
            }
        }

        public GameResult GetGameResult(string playerMove, string pcMove)
        {
            if (!PosibleMoves.Contains(playerMove) || !PosibleMoves.Contains(pcMove))
                throw new ArgumentException("Move is not valid.");

            int index1 = Array.IndexOf(PosibleMoves, playerMove);
            int index2 = Array.IndexOf(PosibleMoves, pcMove);

            return _rules[index1, index2] ;
        }
    }

    public enum GameResult
    {
        Win,
        Lose,
        Draw
    };
}