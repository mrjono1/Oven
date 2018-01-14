using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ClientApp.App
{
    /// <summary>
    /// app.component.scss used for application styling
    /// </summary>
    public class AppComponentScssTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "app.component.scss";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new[] { "ClientApp", "app" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"// Material UI Theme
@import ""~@angular/material/prebuilt-themes/deeppurple-amber.css"";

body {
    margin: 0;
}

.container {
    max-width: 992px;
    width: 100%;
    padding: 20px;
    margin: 8px;
}";
        }
    }
}
