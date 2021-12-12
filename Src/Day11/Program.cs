using Businesslogic;
using Businesslogic.Extensions;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;
using Businesslogic.Locations;

namespace Day11
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
            var steps = 100;
            var grid = new Grid<int>();
            grid.Parse(Parse(input));
            var result = 0;
            foreach (var step in Enumerable.Range(1,steps))
            {
                ProcessStep(ref grid);
                result += ProcessFlashes(grid);
            }

            return result;
        }

        private static int Test2(List<string> input)
        {
            var steps = 3000; // Random large number
            var grid = new Grid<int>();
            grid.Parse(Parse(input));
            var octos = grid.All.Count;
            foreach (var step in Enumerable.Range(1, steps))
            {
                ProcessStep(ref grid);
                var flashes = ProcessFlashes(grid);
                if (octos == flashes)
                    return step;
            }
            return 0;
        }
        public static List<List<int>> Parse(List<string> input)
        {
            return input.Select(i => i.Select(x => (int)char.GetNumericValue(x)).ToList()).ToList();
        }
        public static void ProcessStep(ref Grid<int> grid)
        {
            foreach(var coord in grid.All)
            {
                grid.UpdateValue(coord.x, coord.y, i => i+1);
            }
        }
        public static int ProcessFlashes(Grid<int> grid)
        {
            var result = 0;
            var flashed = grid.GetCoordinatesFiltered(i => i > 9);
            if (!flashed.Any())
                return 0;
            result += flashed.Count;
            flashed.ForEach(i => grid.UpdateValue(i.x, i.y, _ => 0));
            var ajoining = flashed.SelectMany(i => grid.GetAdjoining(i.x, i.y)).Where(i => i.Value != 0).ToList();
            ajoining.ForEach(i => grid.UpdateValue(i.X, i.Y, x => x + 1));
            if (flashed.Count > 0)
            {
                result += ProcessFlashes(grid);
            }
            return result;
        }
    }
}
