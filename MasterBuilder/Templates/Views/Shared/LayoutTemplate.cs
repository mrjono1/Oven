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
<html lang=""en"">
  <head>
    <base href=""@(Url.Content(""~/""))"" />
    <title>@ViewData[""Title""]</title>

    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <link rel=""apple-touch-icon"" sizes=""180x180"" href=""assets/favicons/apple-touch-icon.png"">
    <link rel=""icon"" type=""image/png"" sizes=""32x32"" href=""assets/favicons/favicon-32x32.png"">
    <link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""assets/favicons/favicon-16x16.png"">
    <link rel=""manifest"" href=""dist/vendor-manifest.json"">
    <link rel=""mask-icon"" href=""assets/favicons/safari-pinned-tab.svg"" color=""#5bbad5"">
    <link rel=""shortcut icon"" href=""assets/favicons/favicon.ico"">
    <meta name=""msapplication-config"" content=""assets/favicons/browserconfig.xml"">
    <meta name=""theme-color"" content=""#ffffff"">
    @Html.Raw(ViewData[""Meta""])
    @Html.Raw(ViewData[""Links""])

    <link href=""https://fonts.googleapis.com/icon?family=Material+Icons"" rel=""stylesheet"" media=""none"" onload=""if(media!='all')media='all'"">
    <link href=""https://fonts.googleapis.com/css?family=Roboto:300,400,500|Roboto+Mono:300"" rel=""stylesheet"" media=""none"" onload=""if(media!='all')media='all'"">
    <!-- <link rel=""stylesheet"" href=""~/dist/vendor.css"" asp-append-version=""true"" /> -->

    @Html.Raw(ViewData[""Styles""])

  </head>
  <body>
    <noscript><p>Please enable javascript to use this site</p></noscript>
    @RenderBody()
    {(Project.ServerSideRendering ? $@"
    <!-- Here we're passing down any data to be used by grabbed and parsed by Angular -->
    @Html.Raw(ViewData[""TransferData""])
    @Html.Raw(ViewData[""Scripts""])" : string.Empty )}

    @RenderSection(""scripts"", required: false)

  </body>
</html>";
        }
    }
}