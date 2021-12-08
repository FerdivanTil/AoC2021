using Businesslogic;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            //Helper.WriteResult(Test1, FileType.Test1Sample);
            //Helper.WriteResult(Test1, FileType.Test1);
            Helper.WriteResult(Test2, FileType.Test1Sample);
            Helper.WriteResult(Test2, FileType.Test1);
        }

        private static int Test1(List<string> input)
        {
            var segmentsResult = input.Select(i => i.Split(" | ")[1].Split(" ").ToList());
            var filterLengths = new[] { 2, 3, 4, 7 };
            var results = segmentsResult.Select(i => i.Where(x => filterLengths.Contains(x.Length)).Count());
            

            return results.Sum();
        }

        private static int Test2(List<string> input)
        {
            var segmentsResult = input.Select(i => i.Split(" | ").ToList());
            var decoders = segmentsResult.Select(i => new SegmentDecoder(i[0].Split(" ").ToList(), i[1].Split(" ").ToList())).ToList();
            decoders.ForEach(i => i.Decode());

            return decoders.Sum(i => i.Result);
        }
    }

    public class SegmentDecoder
    {
        protected List<string> InputSet { get; set; }
        protected List<string> OutputSet { get; set; }

        public int Result { get; set; }

        public SegmentDecoder(List<string> inputSet, List<string> outputSet)
        {
            InputSet = inputSet;
            OutputSet = outputSet;
        }
        public void Decode()
        {
            var totalList = InputSet.Union(OutputSet).ToList();
            var codes = Enumerable.Range(0,10).Select(_ => string.Empty).ToList();

            Func<string, string, bool> contains = (first, second ) => first.ToArray().All(x => second.ToArray().Contains(x));
            Func<string, string, string> except = (first, second ) => string.Concat(first.ToArray().Except(second.ToArray()));

            // Get get the easy ones first
            // Lets decode the 1
            codes[1] = totalList.First(i => i.Length == 2);
            // Lets decode the 7
            codes[7] = totalList.First(i => i.Length == 3);
            // Lets decode the 4
            codes[4] = totalList.First(i => i.Length == 4);
            // Lets decode the 8
            codes[8] = totalList.First(i => i.Length == 7);

            // Lets decode the 9 same as 4
            codes[9] = totalList.First(i => i.Length == 6 && contains(codes[4], i));

            // Lets decode the 5 => same as 4 - 1
            codes[5] = totalList.First(i => i.Length == 5 && contains(except(codes[4], codes[1]), i));

            // Lets decode the 3 => not 5 and same as 1
            codes[3] = totalList.First(i => i.Length == 5 && i != codes[5] && contains(codes[1], i));

            // Lets decode the 2 => not 3 and not 5
            codes[2] = totalList.First(i => i.Length == 5 && i != codes[3] && i != codes[5]);

            // Lets decode the 6 => not 9 and same as 5
            codes[6] = totalList.First(i => i.Length == 6 && i != codes[9] && contains(codes[5], i));

            // Lets decode the 0 => not 6 and not 9
            codes[0] = totalList.First(i => i.Length == 6 && i != codes[6] && i != codes[9]);

            Func<List<string>, string, int> find = (input, search) => input.IndexOf(input.First(i => i.Length == search.Length && i.Intersect(search).Count() == search.Length));

            //Calculate result
            Result += find(codes, OutputSet[0]) * 1000;
            Result += find(codes, OutputSet[1]) * 100;
            Result += find(codes, OutputSet[2]) * 10;
            Result += find(codes, OutputSet[3]);
        }
    }
}
