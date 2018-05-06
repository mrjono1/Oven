using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src
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
            return new string[] { "src" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"import React          from 'react';
import ReactDOM       from 'react-dom';
import {{ Provider }}   from 'react-redux';
import configureStore from 'core/store/configureStore';
import App            from 'containers/App';

const store = configureStore();

ReactDOM.render(
  <Provider store={{store}}>
    <App/>
  </Provider>,
  document.getElementById('root')
);";
        }
    }
}