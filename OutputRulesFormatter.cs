using System;
using System.Data;
using DataTablePrettyPrinter;

namespace RockPaperScissorsHMAC
{
    public static class OutputRulesFormatter
    {
        public static void OutputRules(RuleTable ruleTable)
        {
            DataTable table = new DataTable("Rules");

            table.Columns.Add("Ход:", typeof(string));
            foreach (var move in ruleTable.PosibleMoves)
            {
                table.Columns.Add(move, typeof(string));
                table.SetTitleTextAlignment(TextAlignment.Center);
            }

            foreach (var move in ruleTable.PosibleMoves)
            {
                object[] data = new object[1 + ruleTable.PosibleMoves.Length];
                data[0] = move;
                for (var j = 1; j < data.Length; j++)
                {
                    switch (ruleTable.GetGameResult(move,
                        ruleTable.PosibleMoves[j - 1]))
                    {
                        case GameResult.Win:
                            data[j] = "Победа";
                            break;
                        case GameResult.Lose:
                            data[j] = "Поражение";
                            break;
                        case GameResult.Draw:
                            data[j] = "Ничья";
                            break;
                    }
                }

                table.Rows.Add(data);
            }

            Console.WriteLine(table.ToPrettyPrintedString());
        }
    }
}