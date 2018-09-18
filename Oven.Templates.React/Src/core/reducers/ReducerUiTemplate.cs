using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.Src.Core.Reducers
{
    /// <summary>
    /// configure store Template
    /// </summary>
    public class ReducerUiTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ReducerUiTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "reducer-ui.js";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "core", "reducers" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"import constants from 'core/types';

const initialState = {
    leftNavOpen: false
};

export function uiReducer(state = initialState, action) {
    switch (action.type) {

        case constants.OPEN_NAV:
            return Object.assign({}, state, {
                leftNavOpen: true
            });
        
        case constants.CLOSE_NAV:
            return Object.assign({}, state, {
                leftNavOpen: false
            });
        
        default:
            return state;
    }
}";
        }
    }
}