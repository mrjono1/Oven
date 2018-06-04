using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.React.Src
{
    /// <summary>
    /// LeftDrawer.jsx Template
    /// </summary>
    public class LeftDrawerTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public LeftDrawerTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "LeftDrawer.jsx";
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
            return $@"                    <ListItem component={{Link}} to='/{path}'>
                        <ListItemIcon>
                            <StarIcon />
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
import StarIcon from '@material-ui/icons/Star';
import HomeIcon from '@material-ui/icons/Home';
import {{ Link }} from 'react-router-dom';

class LeftDrawer extends React.Component {{

    render() {{
        return (
                <List>
                    <ListItem component={{Link}} to='/'>
                        <ListItemIcon>
                            <HomeIcon />
                        </ListItemIcon>
                        <ListItemText primary=""Home"" />
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