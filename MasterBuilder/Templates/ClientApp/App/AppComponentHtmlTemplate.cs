using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ClientApp.App
{
    /// <summary>
    /// app.component template
    /// </summary>
    public class AppComponentHtmlTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "app.component.html";
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
            return @"<div>
    <app-nav-menu></app-nav-menu>
    <div>
        <router-outlet></router-outlet>
    </div>
</div>";
        }
    }
}
