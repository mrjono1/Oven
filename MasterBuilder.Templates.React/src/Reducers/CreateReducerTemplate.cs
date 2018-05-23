using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Reducers
{
    /// <summary>
    /// createReducers.ts Template
    /// </summary>
    public class CreateReducerTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateReducerTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "createReducer.ts";
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
            return @"/**
 * Created by toni on 12.03.2017.
 */
import { Action } from '../model/model';

export default function createReducer(initialState: Object, handlers: Object) {
    return function reducer(state: Object = initialState, action: Action<any>) {
        if (handlers.hasOwnProperty(action.type)) {
            return handlers[action.type](state, action);
        } else {
            return state;
        }
    };
}";
        }
    }
}