using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Containers
{
    /// <summary>
    /// Container Template
    /// </summary>
    public class ContainerTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContainerTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Screen.InternalName}.tsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "containers", Screen.InternalName };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"/**
 * Component - {Screen.Title}
 */

import * as React from 'react';

export default class {Screen.InternalName} extends React.Component {{
  public render() {{
    return <div>
        Screen: {Screen.Title}
      </div>;
  }}
}}";
        }
    }
}