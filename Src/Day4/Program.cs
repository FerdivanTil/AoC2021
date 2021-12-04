using Businesslogic;
using Businesslogic.Enums;
using Businesslogic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper.WriteResult(Test1, FileType.Test1Sample);
            Helper.WriteResult(Test1, FileType.Test1);
            Helper.WriteResult(Test2, FileType.Test1Sample);
            Helper.WriteResult(Test2, FileType.Test1);
        }

        private static int Test1(List<string> input)
        {
            var bingo = new Bingo();
            bingo.Parse(input);
            return bingo.GetBingo(i => i.Min(x => x.BingoAt));
        }

        private static int Test2(List<string> input)
        {
            var bingo = new Bingo();
            bingo.Parse(input);
            return bingo.GetBingo(i => i.Max(x => x.BingoAt));
        }
    }

    public class Bingo
    {
        public List<int> Draws { get; set; }
        public List<Board> Boards { get; set; } = new List<Board>();
        public void Parse(List<string> input)
        {
            Draws = input[0].Split(',').Select(i => int.Parse(i)).ToList();

            var values = new List<int>();
            foreach(var row in input.Skip(2))
            {
                if(string.IsNullOrWhiteSpace(row))
                {
                    // Close the board
                    Boards.Add(new Board { Numbers= new List<int>(values) });
                    values.Clear();
                    continue;
                }
                values.AddRange(row.Split(" ").Where(i => !string.IsNullOrWhiteSpace(i)).Select(i => int.Parse(i)));
            }
        }

        public int GetBingo(Func<List<Board>, int> filter)
        {
            // Check boards for the first bingo
            foreach (var board in Boards)
            {
                board.CheckBingo(Draws);
            }
            // Get the correct board
            var foundBoard = Boards.First(i => i.BingoAt == filter(Boards));
            // Calculate the result
            return Draws[foundBoard.BingoAt] * foundBoard.Numbers.Except(Draws.Take(foundBoard.BingoAt + 1)).Sum();
        }
    }

    public class Board
    {
        public List<int> Numbers { get; set; } = new List<int>();
        public int RowCount => (int)Math.Sqrt(Numbers.Count);
        public int BingoAt { get; private set; } = int.MaxValue;
        public void CheckBingo(List<int> draws)
        {
            CheckBingoLines(draws, GetVerticals());
            CheckBingoLines(draws, GetHorizontals());
        }
        protected List<List<int>> GetVerticals()
        {
            return Enumerable.Range(0, RowCount).Select(i => Numbers.Skip(i * RowCount).Take(RowCount).ToList()).ToList();
        }
        protected List<List<int>> GetHorizontals()
        {
            return Enumerable.Range(0, RowCount).Select(i => Numbers.GetNthElement(5, i).ToList()).ToList();
        }

        protected void CheckBingoLines(List<int> draws, List<List<int>> lines)
        {
            foreach (var row in lines)
            {
                var found = -1;
                foreach (var item in row)
                {
                    var current = draws.IndexOf(item);
                    if (current > found)
                    {
                        found = current;
                    }
                }
                if (found < BingoAt)
                {
                    BingoAt = found;
                }
            }
        }
    }
}
