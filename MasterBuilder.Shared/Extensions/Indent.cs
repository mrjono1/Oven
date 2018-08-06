using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class Indent
    {
        public static string IndentLines(this string value, int numberOfSpaces)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var lines = new List<string>();
            foreach (var line in value.Split(Environment.NewLine))
            {
                lines.Add(string.Concat(new string(' ', numberOfSpaces), line));
            }
            return string.Join(Environment.NewLine, lines);
        }
    }
}
