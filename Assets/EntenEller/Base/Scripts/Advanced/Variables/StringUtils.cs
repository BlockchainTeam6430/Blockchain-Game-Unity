using System;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class StringUtils
    {
        [Serializable]
        public class KeyValueStrings
        {
            public string Key;
            public string Value;
        }
        
        public static string GetBetween(this string source, string start, string end)
        {
            if (!source.Contains(source) || !source.Contains(end)) return string.Empty;
            var i0 = source.IndexOf(start, 0, StringComparison.Ordinal) + start.Length;
            var i1 = source.IndexOf(end, i0, StringComparison.Ordinal);
            return source.Substring(i0, i1 - i0);
        }
    }
}
