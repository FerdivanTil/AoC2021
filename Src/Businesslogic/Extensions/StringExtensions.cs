using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Businesslogic.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAny(this string self, IEnumerable<char> items)
        {
            return self.ToArray().Any(x => items.Contains(x));
        }
    }
}
