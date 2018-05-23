using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Actions
{
    /// <summary>
    /// todo.ts Template
    /// </summary>
    public class TodoTsxTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public TodoTsxTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "todo.tsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "actions" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"import { Action, ActionType, Todo } from '../model/model';

export function addTodo(todo: Todo): Action<Todo> {

    return {
        type: ActionType.ADD_TODO,
        payload: todo
    };
}

// Async Function expample with redux-thunk
export function completeTodo(todoId: number) {

    // here you could do API eg

    return (dispatch: Function, getState: Function) => {

        dispatch({ type: ActionType.COMPLETE_TODO, payload: todoId });
    };
}

export function uncompleteTodo(todoId: number): Action<number> {

    return {
        type: ActionType.UNCOMPLETE_TODO,
        payload: todoId
    };
}

export function deleteTodo(todoId: number): Action<number> {

    return {
        type: ActionType.DELETE_TODO,
        payload: todoId
    };
}";
        }
    }
}