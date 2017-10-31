using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder
{
    public static class StringExtensions
    {
        public static string ToCamlCase(this string value)
        {
            return Char.ToLowerInvariant(value[0]) + value.Substring(1);
        }
    }
}
