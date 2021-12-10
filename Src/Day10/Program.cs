using Businesslogic;
using Businesslogic.Extensions;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;
using System.Diagnostics;

namespace Day10
{
    class Program
    {
        public static List<string> Tags = new List<string>() { "<>", "{}", "()", "[]" };
        public static List<char> ClosingTags = new List<char>() { '>', '}', ')', ']' };
        public static Dictionary<char, int> Points1 = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        public static Dictionary<char, int> Points2 = new Dictionary<char, int>() { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };


        static void Main(string[] args)
        {
            Helper.WriteResult(Test1, FileType.Test1Sample);
            Helper.WriteResult(Test1, FileType.Test1);
            Helper.WriteResult(Test2, FileType.Test1Sample);
            Helper.WriteResult(Test2, FileType.Test1);
        }

        private static long Test1(List<string> input)
        {
            var resultList = input.Select(i => GetCorruptedPoints(i));
            return resultList.Sum(i => (long)i);
        }

        private static long Test2(List<string> input)
        {
            var watch = new Stopwatch();
            watch.Start();
            var resultList = input.Select(i => GetIncompletePoints(i)).Where(i => i > 0).ToList();
            var result =  resultList.OrderBy(i => i).Skip(resultList.Count / 2).First();
            watch.Stop();
            Console.WriteLine($"It took {watch.ElapsedMilliseconds.ToString().Pastel(Color.Green)} ms");
            return result;
        }
        public static long GetIncompletePoints(string input)
        {
            var current = GetTagResult(input);

            if (current.ContainsAny(ClosingTags))
                return 0;

            // Invert list
            return current.ToArray().Reverse().Select(i => Points2[i]).Aggregate(0L,(result, next) => result = result * 5 + next);
        }
        protected static string GetTagResult(string input)
        {
            var current = input;
            while (true)
            {
                var temp = current;
                foreach (var tag in Tags)
                {
                    current = current.Replace(tag, string.Empty);
                }
                if (temp == current)
                    break;
            }
            return current;
        }

        public static int GetCorruptedPoints(string input)
        {
            var current = GetTagResult(input);

            if (!current.ContainsAny(ClosingTags))
                return 0;
            var error = ClosingTags
            .Select(i => new { Tag = i, Pos = current.IndexOf(i) })
            .Where(i => i.Pos != -1)
            .OrderBy(i => i.Pos).First();

            return Points1[error.Tag];
        }
    }
}
