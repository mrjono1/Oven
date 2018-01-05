using MasterBuilder.Helpers;

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
            return @"/* Import Bootstrap & Fonts */
$icon-font-path: '~bootstrap-sass/assets/fonts/bootstrap/';
@import ""~bootstrap-sass/assets/stylesheets/bootstrap"";

// Material UI Theme
@import ""~@angular/material/prebuilt-themes/deeppurple-amber.css"";

// below material testing css to be removed
.example-form {
  min-width: 150px;
  max-width: 500px;
  width: 100%;
}

.example-full-width {
  width: 100%;
}";
        }
    }
}
