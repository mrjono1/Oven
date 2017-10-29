using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.app.components.navmenu
{
    public class NavmenuComponentHtmlTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, Path.Combine("app", "components", "navmenu")), "navmenu.component.html");
        }

        public static string Evaluate(Project project)
        {
            var menuItems = new StringBuilder();

            if (project.MenuItems != null)
            {
                foreach (var item in project.MenuItems)
                {
                    var path = item.Path;
                    if (item.ScreenId.HasValue)
                    {
                        path = project.Screens.Where(s => s.Id == item.ScreenId.Value).Select(p => p.Path).FirstOrDefault();
                        // todo if url is null error
                    }
                    menuItems.Append($@"<li [routerLinkActive]=""['link-active']"">
                    <a [routerLink]=""['/{path}']"">
                        <span class='glyphicon glyphicon-home'></span> {item.Title}
                    </a>
                </li>");
                }

            }

            return $@"<div class='main-nav'>
    <div class='navbar navbar-inverse'>
        <div class='navbar-header'>
            <button type='button' class='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                <span class='sr-only'>Toggle navigation</span>
                <span class='icon-bar'></span>
                <span class='icon-bar'></span>
                <span class='icon-bar'></span>
            </button>
            <a class='navbar-brand' [routerLink]=""['/home']"">{project.Title}</a>
        </div>
        <div class='clearfix'></div>
        <div class='navbar-collapse collapse'>
            <ul class='nav navbar-nav'>
{menuItems}
            </ul>
        </div>
    </div>
</div>";
        }
    }
}