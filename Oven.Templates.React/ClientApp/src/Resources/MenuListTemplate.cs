using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oven.Templates.React.ClientApp.Src.Resources
{
    /// <summary>
    /// Admin Menu Template
    /// </summary>
    public class MenuListTemplate: ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public MenuListTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => $"{Screen.InternalName}.jsx";

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath() => new string[] { "ClientApp", "src", "containers" };

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            // The Tuple is to allow sorting by the display text, that does not have to be unique although should
            // I don't want throw an exception for this

            var menuListItems = new List<Tuple<string, string>>();
            if (ScreenSection.MenuListMenuItems != null)
            {
                foreach (var menuItem in ScreenSection.MenuListMenuItems)
                {
                    if (menuItem.MenuItemType == MenuItemType.ApplicationLink)
                    {
                        var screen = Project.Screens.Single(a => a.Id == menuItem.ScreenId);

                        menuListItems.Add(new Tuple<string, string>(menuItem.Title ?? screen.Title, $@"<MenuItemLink 
    to=""/{screen.Path ?? menuItem.Path}""
    primaryText=""{menuItem.Title ?? screen.Title}""
    leftIcon={{<LabelIcon />}}
    onClick={{onClick}}
/>"));
                    }
                }
            }

            return $@"import React from 'react';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import {{ Title, MenuItemLink, fetchUtils }} from 'react-admin';
import LabelIcon from '@material-ui/icons/Label';
import {{ withRouter }} from 'react-router-dom';
import Button from '@material-ui/core/Button';

async function seedDataAction() {{
    await fetchUtils.fetchJson('api/Administration/seed', {{ method: 'GET' }});
}};

const AdminMenu = ({{ onClick }}) => (
    <Card>
        <Title title=""Administration"" />
        <CardContent>
            <Button color=""primary"" onClick={{seedDataAction}}>Seed Data</Button>
{ string.Join(Environment.NewLine, menuListItems.OrderBy(a => a.Item1).Select(a => a.Item2)).IndentLines(12)}
        </CardContent>
    </Card>
);


export default withRouter(AdminMenu);";
        }
    }
}
