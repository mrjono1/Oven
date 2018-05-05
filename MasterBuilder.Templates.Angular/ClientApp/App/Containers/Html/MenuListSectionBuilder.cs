using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Containers.Html
{
    /// <summary>
    /// Menu List Section Builder
    /// </summary>
    public class MenuListSectionBuilder
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public MenuListSectionBuilder(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        internal string Evaluate()
        {
            var menuListItems = new List<string>();
            if (ScreenSection.MenuListMenuItems != null)
            {
                foreach (var menuItem in ScreenSection.MenuListMenuItems)
                {
                    if (menuItem.MenuItemType == MenuItemType.ApplicationLink)
                    {
                        var screen = Project.Screens.SingleOrDefault(a => a.Id == menuItem.ScreenId);

                        menuListItems.Add($@"        <a mat-list-item [routerLink]=""['/{screen.Path ?? menuItem.Path}']"">{menuItem.Title ?? screen.Title}</a>");
                    }
                }
            }

            return $@"    <div class=""screen-section-menu container mat-elevation-z2"" fxFlex>
        <mat-nav-list>
{string.Join(Environment.NewLine, menuListItems)}
        </mat-nav-list>
    </div>";
        }
    }
}
