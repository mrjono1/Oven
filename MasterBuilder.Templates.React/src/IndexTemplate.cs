using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src
{
    /// <summary>
    /// index.js Template
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
            return new string[] { "src" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"import React from 'react';
import ReactDOM from 'react-dom';
import {{ Provider }} from 'react-redux';
import CssBaseline from '@material-ui/core/CssBaseline';
import configureStore from 'store/configureStore';
import App from 'containers/core/App';

const store = configureStore();

ReactDOM.render(
    <Provider store={{store}}>
        <React.Fragment>
            <CssBaseline />
            <App/>
        </React.Fragment>
    </Provider>,
    document.getElementById('root')
);";
        }
    }
}