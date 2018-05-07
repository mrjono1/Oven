using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Components.AppBar
{
    /// <summary>
    /// index.js Template
    /// </summary>
    public class IndexJsTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public IndexJsTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "index.js";
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

import React                   from 'react';
import { AppBar as MuiAppBar } from 'material-ui';

/* component styles */
import { styles } from './styles.scss';

export default function AppBar(props) {
  return (
    <div className={styles}>
      <MuiAppBar {...props} className=""app-bar"" />
    </div>
  );
}";
        }
    }
}