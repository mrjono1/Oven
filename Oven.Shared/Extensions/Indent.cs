using System.Collections.Generic;

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
            foreach (var line in value.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                lines.Add(string.Concat(new string(' ', numberOfSpaces), line));
            }
            return string.Join(Environment.NewLine, lines);
        }

        public static string IndentLines(this IEnumerable<string> lines, int tabs, string endOfLineCharacter = "")
        {
            if (lines == null)
            {
                return string.Empty;
            }

            var indent = new string(' ', tabs * 4);
            var subLinesIndented = new List<string>();

            // Indent sub lines but dont add end of line character
            foreach(var line in lines)
            {
                var subLines = line.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                if (subLines.Length > 1)
                {
                    var subLine = string.Join(string.Concat(Environment.NewLine, indent), subLines);
                    subLinesIndented.Add(subLine);
                }
                else
                {
                    subLinesIndented.Add(line);
                }
            }

            return string.Concat(indent, string.Join(string.Concat(endOfLineCharacter, Environment.NewLine, indent), subLinesIndented));
        }
    }
}
