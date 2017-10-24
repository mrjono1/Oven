using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Views
{
    public class ViewStartTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(folder, "_ViewStart.cshtml");
        }

        public static string Evaluate()
        {
            return @"@{
    Layout = ""_Layout"";
}
";
        }
    }
}
