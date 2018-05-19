using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Components.TopAppBar
{
    /// <summary>
    /// index.tsx Template
    /// </summary>
    public class TopAppBarTemplate : ITemplate
    {
        private readonly Project Project;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public TopAppBarTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "TopAppBar.tsx";
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

import * as React from 'react';
import PropTypes from 'prop-types';
import {{ withStyles }} from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';

class TopAppBar extends React.Component {{
  public render() {{
      return <div>
      <AppBar position=""static"">
        <Toolbar>
          <Typography variant=""title"" color=""inherit"">
            {Project.Title}
          </Typography>
        </Toolbar>
      </AppBar>
    </div>;
  }}
}}

export default TopAppBar;";
        }
    }
}