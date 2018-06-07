using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;

namespace MasterBuilder.Templates.React.Src.Reducers
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
        public string GetFileName() => "index.js";

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath() => new string[] { "src", "reducers" };

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var imports = new List<string>();
            var reducerNames = new List<string>();

            foreach (var screen in Project.Screens)
            {
                var name = new PageReducerTemplate(Project, screen).FunctionName;

                imports.Add($"import {name} from './{name}';");
                reducerNames.Add($"  {name}");
            }

            return $@"import {{ combineReducers }} from 'redux';
{string.Join(Environment.NewLine, imports)}

const rootReducer = combineReducers({{
{string.Join(string.Concat(",", Environment.NewLine), reducerNames)}
}})

export default rootReducer;";
        }
    }
}