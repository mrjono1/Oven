using MasterBuilder.Helpers;

namespace MasterBuilder.Templates.ClientApp.App
{
    public class AppComponentHtmlTemplate : ITemplate
    {
        public string GetFileName()
        {
            return "app.component.html";
        }

        public string[] GetFilePath()
        {
            return new[] { "ClientApp", "app" };
        }
        public string GetFileContent()
        {
            return @"<div class=""col-sm-3"">
    <app-nav-menu></app-nav-menu>
</div>
<div class=""col-sm-9 body-content"">
    <router-outlet></router-outlet>
</div>";
        }
    }
}
