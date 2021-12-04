using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Businesslogic.Extensions
{
    public static class LinqExtensions
    {
        public static List<T> GetNthElement<T>(this List<T> list, int n, int offset = 0)
        {
            return list.Select((item, index) => (index, item))
                       .Where(i => (i.index % n) - offset == 0)
                       .Select(i => i.item)
                       .ToList();

        }
    }
}
