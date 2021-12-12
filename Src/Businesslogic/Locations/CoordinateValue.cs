using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Businesslogic.Locations
{
    [DebuggerDisplay("Coordinate = {Coord}")]
    public class CoordinateValue<T> : Coordinate
    {
        public T Value { get; set; }
        public CoordinateValue(int x, int y, T value) : base(x, y)
        {
            Value = value;
        }
    }
}
