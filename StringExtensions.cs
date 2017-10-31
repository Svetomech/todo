using System;

namespace Svetomech.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsOrdinal(this string self, string value) => 
            self.Equals(value, StringComparison.Ordinal);
    }
}
