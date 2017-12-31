using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.app.components.navmenu
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
                    menuItems.Append($@"<li [routerLinkActive]=""['link-active']"" (click)=""collapseMenu()"">
                    <a [routerLink]=""['/{path}']"">
                        <span class='{item.Icon}'></span> {item.Title}
                    </a>
                </li>");
                }

            }

            return $@"<div class='main-nav'>
    <nav class='navbar navbar-inverse'>
        <div class='navbar-header'>
            <button type='button' class='navbar-toggle' (click)=""collapseNavbar()"">
                <span class='sr-only'>Toggle navigation</span>
                <span class='icon-bar'></span>
                <span class='icon-bar'></span>
                <span class='icon-bar'></span>
            </button>
            <a class='navbar-brand' [routerLink]=""['/home']"">{Project.Title}</a>
        </div>
        <div class='navbar-collapse {{{{collapse}}}}'>
            <ul class='nav navbar-nav'>
{menuItems}
            </ul>
        </div>
    </nav>
</div>";
        }

    }
}