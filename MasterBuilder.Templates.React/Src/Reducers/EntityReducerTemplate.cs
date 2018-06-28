using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.React.Src.Reducers
{
    public class EntityReducerTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityReducerTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => "entityReducer.js";

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath() => new string[] { "src", "reducers" };
        
        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"export function createEntityReducer(entityName = '', entityNameUpper = '') {

    const initialState = {
        isFetching: false,
        didInvalidate: false,
        items: [],
        byId: []
    };
    
    return function reducer(state = initialState, action) {
        switch(action.type) {
            // Search Reducer Actions
            case `${entityNameUpper}_INVALIDATE_ITEMS`:
                return {
                    ...state,
                    didInvalidate: true
                };
            case `${entityNameUpper}_REQUEST_ITEMS`:
                return {
                    ...state,
                    isFetching: true,
                    didInvalidate: false
                };
            case `${entityNameUpper}_RECEIVE_ITEMS`:
                return {
                    ...state,
                    isFetching: false,
                    didInvalidate: false,
                    items: action.items,
                    lastUpdated: action.receivedAt
                };

            // Form Reducer Actions
            case `${entityNameUpper}_INVALIDATE_ITEM`:
                return {
                    ...state,
                    didInvalidate: true
                };
            case `${entityNameUpper}_BEFORE_REQUEST_ITEM`: {
                let newState = { ...state };
                if (!newState.byId[action.id]) {
                    // Default Values if null
                    newState.byId[action.id] = {
                        $default: true
                        // TODO: Default Values here
                    };
                }
                return newState;
            }
            case `${entityNameUpper}_REQUEST_ITEM`:
                return {
                    ...state,
                    isFetching: true,
                    didInvalidate: false
                };
            case `${entityNameUpper}_RECEIVE_ITEM`: {
                let newState = {
                    ...state,
                    isFetching: false,
                    didInvalidate: false,
                    lastUpdated: action.receivedAt
                };
                newState.byId[action.id] = {
                    ...newState.byId[action.id],
                    ...action.item,
                    $default: false
                };
                return newState;
            }
            default:
                return state;
        }
    }

}";
        }
    }
}