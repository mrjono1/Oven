using Humanizer;
using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.Src.Reducers
{
    public class PageReducerTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public PageReducerTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => $"{FunctionName}.js";

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath() => new string[] { "src", "reducers" };

        /// <summary>
        /// Function Name
        /// </summary>
        public string FunctionName { get => $"{Screen.InternalName.Camelize()}Reducer"; }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"//import {{ browserHistory }} from 'react-router';

const initialState = {{
}};

export default function {FunctionName}(state = initialState, action) {{
  switch(action.type) {{
    default:
      return state;
  }}
}}";
        }
    }
}