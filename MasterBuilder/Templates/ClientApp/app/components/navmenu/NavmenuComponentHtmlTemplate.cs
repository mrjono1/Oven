using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
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
                <li [routerLinkActive]=""['link-active']"">
                    <a [routerLink]=""['/home']"">
                        <span class='glyphicon glyphicon-home'></span> Home
                    </a>
                </li>
                <li [routerLinkActive]=""['link-active']"">
                    <a [routerLink]=""['/counter']"">
                        <span class='glyphicon glyphicon-education'></span> Counter
                    </a>
                </li>
                <li [routerLinkActive]=""['link-active']"">
                    <a [routerLink]=""['/fetch-data']"">
                        <span class='glyphicon glyphicon-th-list'></span> Fetch data
                    </a>
                </li>
            </ul>
        </div>
    </div>
</div>";
        }
    }
}