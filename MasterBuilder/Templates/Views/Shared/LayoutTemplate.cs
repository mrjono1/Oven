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
    <base href=""@(Url.Content(""~/""))"" />
    <title>@ViewData[""Title""]</title>

    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    @Html.Raw(ViewData[""Meta""])
    @Html.Raw(ViewData[""Links""])

    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/0.8.2/css/flag-icon.min.css"" />
    <!-- <link rel=""stylesheet"" href=""~/dist/vendor.css"" asp-append-version=""true"" /> -->

    @Html.Raw(ViewData[""Styles""])

  </head>
  <body>
    @RenderBody()

    <!-- Here we're passing down any data to be used by grabbed and parsed by Angular -->
    @Html.Raw(ViewData[""TransferData""])
    @Html.Raw(ViewData[""Scripts""])

    @RenderSection(""scripts"", required: false)

  </body>
</html>";
        }
    }
}