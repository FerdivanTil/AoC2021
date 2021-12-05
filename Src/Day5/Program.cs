using Businesslogic;
using Businesslogic.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day5
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
            var lines = Parse(input);
            var coords = lines.Where(i => i.Direction != Line.DirectionType.Diagonal)
                               .SelectMany(i => i.GetCoordinates())
                               .ToList();
            return coords.GroupBy(i => i.Coord).Where(i => i.Count() > 1).Count();
        }

        private static int Test2(List<string> input)
        {
            var lines = Parse(input);
            var coords = lines.SelectMany(i => i.GetCoordinates())
                              .ToList();
            return coords.GroupBy(i => i.Coord).Where(i => i.Count() > 1).Count();
        }

        protected static IEnumerable<Line> Parse(List<string> input)
        {
            foreach (var line in input)
            {
                var pair = line.Split(" -> ");
                var first = Coordinate.Parse(pair[0]);
                var last = Coordinate.Parse(pair[1]);
                yield return new Line(first, last);
            }
        }

        [DebuggerDisplay("First = {First} and Last = {Last}")]
        protected class Line
        {
            public Coordinate First { get; set; }
            public Coordinate Last { get; set; }

            public DirectionType Direction { get; private set; }
            public IEnumerable<Coordinate> GetCoordinates()
            {
                if (Direction == DirectionType.Vertical)
                {
                    var low = First.X < Last.X ? First.X : Last.X;
                    return Enumerable.Range(low, Math.Abs(First.X - Last.X) + 1)
                        .Select(x => new Coordinate(x, First.Y))
                        .ToList();
                }
                else if(Direction == DirectionType.Horizontal)
                {
                    var yLow = First.Y < Last.Y ? First.Y : Last.Y;
                    return Enumerable.Range(yLow, Math.Abs(First.Y - Last.Y) + 1)
                        .Select(y => new Coordinate(First.X, y))
                        .ToList();
                }
                else
                {
                    var low = First.X < Last.X ? First : Last;
                    var high = First.X > Last.X ? First : Last;
                    return Enumerable.Range(0, Math.Abs(First.X - Last.X) + 1)
                        .Select(i => new Coordinate(low.X + i, low.Y > high.Y ? low.Y - i : low.Y + i))
                        .ToList();
                }

            }
            public Line(Coordinate first, Coordinate last)
            {
                First = first;
                Last = last;
                if (first.X == last.X)
                    Direction = DirectionType.Horizontal;
                else if (first.Y == last.Y)
                    Direction = DirectionType.Vertical;
                else
                    Direction = DirectionType.Diagonal;
            }
            //public static Line GetLine(Coordinate first, Coordinate last)
            //{
            //    return new Line(first, last);
            //}
            public enum DirectionType
            {
                Horizontal,
                Vertical,
                Diagonal
            }
        }

        [DebuggerDisplay("Coordinate = {Coord}")]
        protected class Coordinate
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
}
