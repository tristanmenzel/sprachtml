using System.Collections.Generic;
using System.Linq;

namespace Sprachtml.Extensions
{
    public static class EnumerableCharExtensions
    {
        public static string AsString(this IEnumerable<char> input)
        {
            return new string(input.ToArray());
        }
    }
}