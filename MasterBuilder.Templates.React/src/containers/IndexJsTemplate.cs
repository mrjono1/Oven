using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Containers
{
    /// <summary>
    /// index.js Template
    /// </summary>
    public class IndexJsTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public IndexJsTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Screen.InternalName}Page.jsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "containers" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"import React, {{ Component }} from 'react';

export default class {Screen.InternalName}Page extends Component {{
  constructor(props) {{
    super(props);
  }}

  render() {{
    return (
      <div>
        Screen: {Screen.Title}
      </div>
    );
  }}
}}";
        }
    }
}