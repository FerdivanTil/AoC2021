using Businesslogic;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
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
            var bitsize = input.First().Length;
            var items = Parse(input);
            var gamma = 0;
            var epsilon = 0;
            foreach(var bit in Enumerable.Range(0,bitsize).Reverse())
            {
                var currentBit = (int)Math.Pow(2, bit);
                // Get all zeros
                var zeros = items.Select(i => (i & currentBit) == 0).Where(i => i).Count();
                // Get all ones
                var ones = input.Count - zeros;

                // Bitshift to prepaire for the next number
                gamma <<= 1;

                // Add the number
                gamma |= ones > zeros ? 1:0;
            }
            // XOR to get the epsilon
            epsilon = gamma ^ (int)Math.Pow(2,bitsize) - 1;
            return gamma* epsilon;
        }

        private static int Test2(List<string> input)
        {
            var bitsize = input.First().Length;
            var parsedInput = Parse(input);
            var items = parsedInput;
            
            // Get the oxy part
            foreach (var bit in Enumerable.Range(0, bitsize).Reverse())
            {
                var currentBit = (int)Math.Pow(2, bit);
                // Get all zeros
                var zeros = items.Select(i => (i & currentBit) == 0).Where(i => i).Count();

                // Get all ones
                var ones = items.Count - zeros;

                // Filter based on the ones
                if(ones >= zeros)
                    items = items.Where(i => (i & currentBit) > 0).ToList();
                else
                    items = items.Where(i => (i & currentBit) == 0).ToList();
                if (items.Count == 1)
                    break;
            }
            var oxy = items.First();

            // Get the srub part
            items = parsedInput;
            foreach (var bit in Enumerable.Range(0, bitsize).Reverse())
            {
                var currentBit = (int)Math.Pow(2, bit);
                // Get all zeros
                var zeros = items.Select(i => (i & currentBit) == 0).Where(i => i).Count();

                // Get all ones
                var ones = items.Count - zeros;

                // Filter on te zeros
                if (ones < zeros)
                    items = items.Where(i => (i & currentBit) > 0).ToList();
                else
                    items = items.Where(i => (i & currentBit) == 0).ToList();
                if (items.Count == 1)
                    break;
            }
            var scrub = items.First();
            return scrub* oxy;
        }

        private static List<int> Parse(List<string> input)
        {
            return input.Select(i => Convert.ToInt32(i, 2)).ToList();
        }
    }
}
