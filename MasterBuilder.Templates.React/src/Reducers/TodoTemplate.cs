using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Reducers
{
    /// <summary>
    /// todo.ts Template
    /// </summary>
    public class TodoTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public TodoTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "todo.ts";
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
            return @"import createReducer from './createReducer';
import { Action, ActionType, Todo } from '../model/model';

export const todoList = createReducer([], {
    [ActionType.ADD_TODO](state: Array<Todo>, action: Action<Todo>) {
        return [...state, action.payload];
    },
    [ActionType.COMPLETE_TODO](state: Array<Todo>, action: Action<number>) {
        // search after todo item with the given id and set completed to true
        return state.map(t => t.id === action.payload ? { ...t, completed: true } : t);
    },
    [ActionType.UNCOMPLETE_TODO](state: Array<Todo>, action: Action<number>) {
        // search after todo item with the given id and set completed to false
        return state.map(t => t.id === action.payload ? { ...t, completed: false } : t);
    },
    [ActionType.DELETE_TODO](state: Array<Todo>, action: Action<number>) {
        // remove all todos with the given id
        return state.filter(t => t.id !== action.payload);
    },
});";
        }
    }
}