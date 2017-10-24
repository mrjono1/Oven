using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Views.Shared
{
    public class LayoutTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, "Shared"), "_Layout.cshtml");
        }

        public static string Evaluate(Project project)
        {
            return $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>@ViewData[""Title""] - {project.Title}</title>
    <base href=""~/"" />

    <link rel=""stylesheet"" href=""~/dist/vendor.css"" asp-append-version=""true"" />
</head>
<body>
    @RenderBody()

    @RenderSection(""scripts"", required: false)
</body>
</html>";
        }
    }
}