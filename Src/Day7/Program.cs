using Businesslogic;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;

namespace Day7
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
            // Get all positions
            var positions = input[0].Split(",").Select(i => Convert.ToInt32(i)).ToList();
            // Get all fuel costs
            var result = Enumerable.Range(positions.Min(), positions.Max()).Select(i => new { target = i, Fuel = GetFuel(positions, i) });
            // Find the cheapest
            return result.Min(i => i.Fuel);
        }

        private static int Test2(List<string> input)
        {
            // Get all positions
            var positions = input[0].Split(",").Select(i => Convert.ToInt32(i)).ToList();
            // Get all fuel costs
            var result = Enumerable.Range(positions.Min(), positions.Max()).Select(i => new { target = i, Fuel = GetFuelExpensive(positions, i) });
            // Find the cheapest
            return result.Min(i => i.Fuel);
        }

        protected static int GetFuel(List<int> positions, int position)
        {
            // Gets the fuel cost of all the movement.
            return positions.Select(i => Math.Abs(i - position)).Sum();
        }
        protected static int GetFuelExpensive(List<int> positions, int position)
        {
            var travelRanges = positions.Select(i => Math.Abs(i - position));

            // Get all the movement costs
            // TODO: This can be optimized by calculating this once for all positions, but it is fast enough. 
            var costs = new List<long>() { 0 };
            foreach (var i in Enumerable.Range(1, travelRanges.Max()))
            {
                costs.Add(costs[i-1] + i);
            }

            return (int)travelRanges.Select(i => costs[i]).Sum();
        }
    }
}
