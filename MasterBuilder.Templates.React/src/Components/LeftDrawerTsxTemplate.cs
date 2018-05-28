using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.React.Src
{
    /// <summary>
    /// LeftDrawer.tsx Template
    /// </summary>
    public class LeftDrawerTsxTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public LeftDrawerTsxTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "LeftDrawer.tsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "components" };
        }

        private string ListItem(MenuItem menuItem)
        {
            var path = menuItem.Path;
            if (menuItem.ScreenId.HasValue)
            {
                path = Project.Screens.Where(s => s.Id == menuItem.ScreenId.Value).Select(p => p.Path).SingleOrDefault();
                // TODO: if url is null error
            }
            return $@"                    <ListItem button onClick={{() => history.push('/{path}')}}>
                        <ListItemIcon>
                            {{this.renderTodoIcon()}}
                        </ListItemIcon>
                        <ListItemText primary=""{menuItem.Title}"" />
                    </ListItem>";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var menuItems = new List<string>();

            if (Project.MenuItems != null)
            {
                foreach (var menuItem in Project.MenuItems)
                {
                    menuItems.Add(ListItem(menuItem));
                }

            }

            return $@"/**
 * Left Drawer
 */

import {{ List, ListItem, ListItemIcon, ListItemText }} from '@material-ui/core';
import * as React from 'react';

class LeftDrawer extends React.Component {{

    render() {{
        return (
                <List>
                    <ListItem button onClick={{() => history.push('/todo')}}>
                        <ListItemIcon>
                            {{this.renderTodoIcon()}}
                        </ListItemIcon>
                        <ListItemText primary=""Todo"" />
                    </ListItem>
{string.Join(Environment.NewLine, menuItems)}
                </List>
        );
    }}
}}

export default LeftDrawer;";
        }
    }
}