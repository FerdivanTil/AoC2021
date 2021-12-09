using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Businesslogic.Locations
{
    [DebuggerDisplay("Coordinate = {Coord}")]
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Coord => $"{X},{Y}";
        public Coordinate()
        { }
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static Coordinate Parse(string input)
        {
            var xY = input
                .Split(",")
                .Select(i => Convert.ToInt32(i))
                .ToList();
            return new Coordinate(xY[0], xY[1]);
        }
    }
}
