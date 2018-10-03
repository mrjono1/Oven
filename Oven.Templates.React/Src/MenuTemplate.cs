using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.React.Src
{
    /// <summary>
    /// Menu.jsx Template
    /// </summary>
    public class MenuTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public MenuTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Menu.jsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var menuListItems = new List<string>();
            if (Project.MenuItems != null)
            {
                foreach (var menuItem in Project.MenuItems)
                {
                    var screen = Project.Screens.Single(a => a.Id == menuItem.ScreenId);
                    var path = screen.Path ?? menuItem.Path;
                    if (screen.ScreenType == ScreenType.Search)
                    {
                        path = screen.Entity.InternalNamePlural;
                    }
                    menuListItems.Add($@"<MenuItemLink 
    to=""/{path}""
    primaryText=""{menuItem.Title ?? screen.Title}""
    leftIcon={{<LabelIcon />}}
    onClick={{onMenuClick}}
/>");
                }
            }

            return $@"import React from 'react';
import {{ MenuItemLink }} from 'react-admin';
import {{ withRouter }} from 'react-router-dom';
import LabelIcon from '@material-ui/icons/Label';

const CustomMenu = ({{ onMenuClick }}) => (
    <div>
{string.Join(Environment.NewLine, menuListItems).IndentLines(8)}
    </div>
);

export default withRouter(CustomMenu);";
        }
    }
}