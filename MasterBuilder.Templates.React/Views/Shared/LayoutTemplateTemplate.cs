using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Views.Shared
{
    /// <summary>
    /// Layout Template
    /// </summary>
    public class LayoutTemplateTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public LayoutTemplateTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "_LayoutTemplate.cshtml";
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
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>@ViewData[""Title""] - {Project.Title}</title>
    <base href=""~/"" />
</head>
<body>
    <noscript><p>Please enable javascript to use this site</p></noscript>
    @RenderBody()
    @RenderSection(""scripts"", required: false)
</body>
</html>";
        }
    }
}