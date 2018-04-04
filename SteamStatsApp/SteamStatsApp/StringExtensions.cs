using System;

namespace SteamStatsApp
{
    public static class StringExtensions
    {
        public static bool Contains(this string str, string toCompare, StringComparison stringComparison)
        {
            return str != null && toCompare != null && str.IndexOf(toCompare, stringComparison) >= 0;
        }
    }
}
