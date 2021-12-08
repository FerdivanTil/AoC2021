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
            var codes = Enumerable.Range(0,10).Select(i => "").ToList();
            var positions = new List<string>();

            // Get get the easy ones first
            // Lets decode the 1
            codes[1] = totalList.First(i => i.Length == 2);
            // Lets decode the 7
            codes[7] = totalList.First(i => i.Length == 3);
            // Lets decode the 4
            codes[4] = totalList.First(i => i.Length == 4);
            // Lets decode the 8
            codes[8] = totalList.First(i => i.Length == 7);
            //positions[1] = codes[7].ToArray().Where(i => !codes[1].ToArray().Contains(i)).First().ToString();

            // Lets decode the 9
            codes[9] = totalList.First(i => i.Length == 6 && i.ToArray().Intersect(codes[4].ToArray()).Count() == 4);

            // Lets decode the 5
            codes[5] = totalList.First(i => i.Length == 5 
                                        && i.ToArray()
                                           .Intersect(codes[4].ToArray()
                                                              .Except(codes[1].ToArray())
                                                     )
                                           .Count() == 2);

            // Lets decode the 3 => not 5 and intersect (1) == 2
            codes[3] = totalList.First(i => i.Length == 5
                                        && i != codes[5]
                                        && i.ToArray()
                                           .Intersect(codes[1].ToArray()
                                                     )
                                           .Count() == 2);

            // Lets decode the 2 => not 3 and not 5
            codes[2] = totalList.First(i => i.Length == 5 && i != codes[3] && i != codes[5]);

            // Lets decode the 6 => not 9 and same as 5
            codes[6] = totalList.First(i => i.Length == 6 
                                        && i != codes[9]
                                        && i.ToArray()
                                           .Intersect(codes[5].ToArray())
                                           .Count() == 5);

            // Lets decode the 0 => not 9 and same as 5
            codes[0] = totalList.First(i => i.Length == 6
                                        && i != codes[6]
                                        && i != codes[9]);

            //Calculate result
            Result += codes.IndexOf(codes.Where(i => i.Length == OutputSet[0].Length && i.Intersect(OutputSet[0]).Count() == OutputSet[0].Length).First()) * 1000;
            Result += codes.IndexOf(codes.Where(i => i.Length == OutputSet[1].Length && i.Intersect(OutputSet[1]).Count() == OutputSet[1].Length).First()) * 100;
            Result += codes.IndexOf(codes.Where(i => i.Length == OutputSet[2].Length && i.Intersect(OutputSet[2]).Count() == OutputSet[2].Length).First()) * 10;
            Result += codes.IndexOf(codes.Where(i => i.Length == OutputSet[3].Length && i.Intersect(OutputSet[3]).Count() == OutputSet[3].Length).First());
        }
    }
}
