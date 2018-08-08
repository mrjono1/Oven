using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Components.AppBar
{
    /// <summary>
    /// index.jsx Template
    /// </summary>
    public class IndexTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public IndexTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "index.jsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "components", "TopAppBar" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"/**
 * AppBar
 */

import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';

export default function TopAppBar() {{
    return (
        <div>
            <AppBar position=""static"">
                <Toolbar>
                    <IconButton color=""inherit"" aria-label=""Menu"">
                        <MenuIcon />
                    </IconButton>
                    <Typography variant=""title"" color=""inherit"">
                        {Project.Title}
                    </Typography>
                </Toolbar>
            </AppBar>
        </div>
    );
}}";
        }
    }
}