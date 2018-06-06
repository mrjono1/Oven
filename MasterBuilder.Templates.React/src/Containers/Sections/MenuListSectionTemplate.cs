using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.React.src.Containers.Sections
{
    /// <summary>
    /// Menu List Section Builder
    /// </summary>
    public class MenuListSectionTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public MenuListSectionTemplate(Project project, Screen screen, ScreenSection screenSection)
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
                        var screen = Project.Screens.Single(a => a.Id == menuItem.ScreenId);

                        menuListItems.Add($@"        <ListItem component={{Link}} to='/{screen.Path ?? menuItem.Path}'>
          <ListItemText primary=""{menuItem.Title ?? screen.Title}"" />
        </ListItem>");
                    }
                }
            }

            return $@"    <List component=""nav"">
{string.Join(Environment.NewLine, menuListItems)}
    </List>";
        }

        internal IEnumerable<string> Imports()
        {
            return new string[]
            {
                "import { List } from '@material-ui/core';",
                "import { ListItem } from '@material-ui/core';",
                "import { ListItemText } from '@material-ui/core';",
                "import { Link } from 'react-router-dom';"
            };
        }
    }
}
