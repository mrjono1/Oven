using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.Src
{
    /// <summary>
    /// index.jsx Template
    /// </summary>
    public class DevIndexTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public DevIndexTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "devIndex.jsx";
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
            return $@"import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import {{ hot }} from 'react-hot-loader';

const HotApp = hot(module)(App);

ReactDOM.render(<HotApp />, document.getElementById('root'));";
        }
    }
}