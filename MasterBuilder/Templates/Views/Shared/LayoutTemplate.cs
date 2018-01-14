using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Views.Shared
{
    /// <summary>
    /// Layout
    /// </summary>
    public class LayoutTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public LayoutTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "_Layout.cshtml";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Views", "Shared" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
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

    <link href=""https://fonts.googleapis.com/icon?family=Material+Icons"" rel=""stylesheet"">
    <link href=""https://fonts.googleapis.com/css?family=Roboto:300,400,500|Roboto+Mono:300"" rel=""stylesheet"">
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