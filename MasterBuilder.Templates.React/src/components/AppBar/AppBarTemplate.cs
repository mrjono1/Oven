using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Components.AppBar
{
    /// <summary>
    /// index.tsx Template
    /// </summary>
    public class AppBarTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public AppBarTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "AppBar.tsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "components", "AppBar" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"/**
 * AppBar
 */

import * as React from 'react';
import { AppBar as MuiAppBar } from 'material-ui';

class AppBar extends React.Component {
  public render() {
      return <div>
      <MuiAppBar {...this.props} className=""app-bar"" />
    </div>;
  }
}

export default AppBar;";
        }
    }
}