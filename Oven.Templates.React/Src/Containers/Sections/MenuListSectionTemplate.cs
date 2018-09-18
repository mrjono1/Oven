using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oven.Templates.React.Src.Containers.Sections
{
    /// <summary>
    /// Menu List Section Builder
    /// </summary>
    public class MenuListSectionTemplate : ISectionTemplate
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

        public IEnumerable<string> Constructor()
        {
            return new string[] { };
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

                        menuListItems.Add($@"<ListItem component={{Link}} to=""/{screen.Path ?? menuItem.Path}"">
    <ListItemText primary=""{menuItem.Title ?? screen.Title}"" />
</ListItem>");
                    }
                }
            }

            return $@"<List component=""nav"">
{string.Join(Environment.NewLine, menuListItems).IndentLines(4)}
</List>";
        }

        public IEnumerable<string> Imports()
        {
            return new string[]
            {
                "import { List } from '@material-ui/core';",
                "import { ListItem } from '@material-ui/core';",
                "import { ListItemText } from '@material-ui/core';",
                "import { Link } from 'react-router-dom';"
            };
        }

        public IEnumerable<string> Methods()
        {
            return new string[] { };
        }

        public IEnumerable<string> Functions()
        {
            return new string[] { };
        }
    }
}
