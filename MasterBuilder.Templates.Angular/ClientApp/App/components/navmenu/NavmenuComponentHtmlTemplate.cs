using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Angular.ClientApp.app.components.navmenu
{
    /// <summary>
    /// navmenu component
    /// </summary>
    public class NavmenuComponentHtmlTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public NavmenuComponentHtmlTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "navmenu.component.html";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "components", "navmenu" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        /// <returns></returns>
        public string GetFileContent()
        {
            var menuItems = new StringBuilder();

            if (Project.MenuItems != null)
            {
                foreach (var item in Project.MenuItems)
                {
                    var path = item.Path;
                    if (item.ScreenId.HasValue)
                    {
                        path = Project.Screens.Where(s => s.Id == item.ScreenId.Value).Select(p => p.Path).FirstOrDefault();
                        // todo if url is null error
                    }
                    menuItems.Append($@"<a mat-button [routerLinkActive]=""['link-active']"" [routerLink]=""['/{path}']"">{item.Title}</a>");
                }

            }

            return $@"<nav class=""mat-elevation-z6"">
    <mat-toolbar color=""primary"">
        <mat-toolbar-row>
            <a mat-button [routerLinkActive]=""['link-active']"" [routerLink]=""['/home']"">{Project.Title}</a>
{menuItems}
        </mat-toolbar-row>
    </mat-toolbar>
</nav>";
        }

    }
}