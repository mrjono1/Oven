using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src
{
    /// <summary>
    /// index.tsx Template
    /// </summary>
    public class IndexTsxTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public IndexTsxTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "index.tsx";
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
            return $@"import * as React from 'react';
import * as ReactDOM from 'react-dom';
import ReduxRoot from './ReduxRoot';

const rootEl = document.getElementById('root');
ReactDOM.render(<ReduxRoot />, rootEl);

if (module.hot) {{
    module.hot.accept('./ReduxRoot', () => {{
        const NextApp = require('./ReduxRoot').default;
        ReactDOM.render(
            <NextApp />,
            rootEl
        );
    }});
}}";
        }
    }
}