using Businesslogic;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Pastel;
using Businesslogic.Locations;

namespace Day9
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
            var grid = new Grid();
            grid.Parse(input);
            var lowPoints = grid.GetLowPoints();
            return lowPoints.Sum(i => i.Height) + lowPoints.Count;
        }

        private static int Test2(List<string> input)
        {
            var grid = new Grid();
            grid.Parse(input);

            var lowPoints = grid.GetLowPoints();

            var sets = new List<CoordinateHeightSet>();
            // Now check the adjoining locations
            foreach(var lowpoint in lowPoints)
            {
                var set = new CoordinateHeightSet(lowpoint);
                var queue = new Queue<CoordinateHeight>();
                queue.Enqueue(lowpoint);
                while (queue.TryDequeue(out var current))
                {
                    // Get ajoining based on height
                    var ajoining = grid.GetAdjoiningFiltered(current.X, current.Y, i => i.Height < 9 && i.Height > lowpoint.Height);
                    // Filter ajoining if they already have been tested
                    var unique = ajoining.Where(i => !set.Exists(i)).ToList();
                    // Add to the Surroundings
                    set.Surroundings.AddRange(unique);
                    // Add to the queue to test for the surroundings
                    unique.ForEach(i => queue.Enqueue(i));
                }
                sets.Add(set);
            }
            // Get the basin sizes
            var output = sets.Select(i => i.GetBasinSize()).ToList();
            // Only take the 3 highest and multiply them
            return output.OrderByDescending(i => i).Take(3).Aggregate((result, next) => result *= next);
        }
    }

    public class CoordinateHeightSet
    {
        public List<CoordinateHeight> Surroundings { get; set; } = new List<CoordinateHeight>();
        public CoordinateHeight Lowest { get; set; }
        public CoordinateHeightSet(CoordinateHeight lowest)
        {
            Lowest = lowest;
        }
        public bool Exists(CoordinateHeight coord)
        {
            return Surroundings.Any(i => i.Coord == coord.Coord);
        }
        public int GetBasinSize()
        {
            return Surroundings.Count() + 1;
        }
    }
    public class CoordinateHeight: Coordinate
    {
        public int Height { get; set; }
        public CoordinateHeight(int x, int y, int height):base(x,y)
        {
            Height = height;
        }
    }

    public class Grid
    {
        public List<List<int>> Lines { get; set; }

        public int SizeX { get; private set; }
        public int SizeY { get; private set; }

        public void Parse(List<string> input)
        {
            Lines = input.Select(i => i.Select(i => (int)char.GetNumericValue(i)).ToList()).ToList();
            SizeX = Lines[0].Count;
            SizeY = Lines.Count;
        }
        public bool Exists(int x, int y)
        {
            if (x < 0 || y < 0)
                return false;
            if (x > SizeX - 1 || y > SizeY - 1)
                return false;
            return true;

        }

        public List<CoordinateHeight> GetLowPoints()
        {
            var lowPoints = new List<CoordinateHeight>();
            // Find all low points
            foreach (var y in Enumerable.Range(0, SizeY))
            {
                foreach (var x in Enumerable.Range(0, SizeX))
                {
                    var adjoining = GetAdjoining(x, y).Select(i => i.Height);
                    var current = GetCoordinateHeight(x, y);
                    if (current.Height < adjoining.Min())
                        lowPoints.Add(current);
                }
            }
            return lowPoints;
        }

        public CoordinateHeight GetCoordinateHeight(int x, int y)
        {
            if (!Exists(x, y))
                return new CoordinateHeight(x,y,int.MaxValue);
            return new CoordinateHeight(x, y, Lines[y][x]);
        }

        public List<CoordinateHeight> GetAdjoining(int x, int y)
        {
            return new[] { GetCoordinateHeight(x - 1, y), // Left
                           GetCoordinateHeight(x, y - 1), // Top
                           GetCoordinateHeight(x + 1, y), // Right
                           GetCoordinateHeight(x, y + 1)  // Bottom
                         }.ToList();
        }
        public List<CoordinateHeight> GetAdjoiningFiltered(int x, int y, Func<CoordinateHeight, bool> func)
        {
            return GetAdjoining(x, y).Where(func).ToList();
        }
    }
}
