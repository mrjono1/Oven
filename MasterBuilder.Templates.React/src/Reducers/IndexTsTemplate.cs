using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Reducers
{
    /// <summary>
    /// index.ts Template
    /// </summary>
    public class IndexTsTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public IndexTsTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "index.ts";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "reducers" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            // TODO: add reducers here
            return $@"import {{ combineReducers, Reducer }} from 'redux';
//import todos from './todos';

export interface RootState {{
  //todos: TodoStoreState;
}}

export default combineReducers<RootState>({{
  //todos
}});
";
        }
    }
}