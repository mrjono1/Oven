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
            return @"export function createEntityReducer(entityName = '', entityNameUpper = '', defaultEntity = {}) {

    const initialState = {
        byId: [],
        byIdMetadata: [],
        all: {
            $isFetching: false,
            $didInvalidate: true,
            $lastUpdated: null,
            page: 1,
            pageSize: 10,
            items: []
        }
    };
    
    return function reducer(state = initialState, action) {
        switch(action.type) {
            // Search Reducer Actions
            case `${entityNameUpper}_INVALIDATE_ITEMS`:
                return {
                    ...state,
                    all: {
                        ...state.all,
                        $didInvalidate: true
                    }
                };
            case `${entityNameUpper}_REQUEST_ITEMS`:
                return {
                    ...state,
                    all: {
                        ...state.all,
                        $isFetching: true,
                        $didInvalidate: false
                    }
                };
            case `${entityNameUpper}_RECEIVE_ITEMS`:
                return {
                    ...state,
                    all: {
                        ...state.all,
                        $isFetching: false,
                        $didInvalidate: false,
                        items: action.items,
                        $lastUpdated: action.receivedAt
                    }
                };

            // Form Reducer Actions
            case `${entityNameUpper}_NEW_ITEM`: {
                return {
                    ...state,
                    byId: {
                        ...state.byId,
                        [action.id]: {
                            ...state.byId[action.id],
                            id: action.id,
                            ...defaultEntity
                        }
                    },
                    byIdMetadata: {
                        ...state.byIdMetadata,
                        [action.id]: {
                            ...state.byIdMetadata[action.id],
                            default: false,
                            new: true,
                            lastUpdated: action.receivedAt
                        }
                    }
                };
            }
            case `${entityNameUpper}_INVALIDATE_ITEM`:
                return {
                    ...state,
                    byIdMetadata: {
                        ...state.byIdMetadata,
                        [action.id]: {
                            didInvalidate: true
                        }
                    }
                };
            case `${entityNameUpper}_BEFORE_REQUEST_ITEM`: {

                if (state.byIdMetadata[action.id]) {
                    return state;
                }

                return {
                    ...state,
                    byId: {
                        ...state.byId,
                        [action.id]: {
                            /* Create new empty item*/
                        }
                    },
                    byIdMetadata: {
                        ...state.byIdMetadata,
                        [action.id]: {
                            default: true
                        }
                    }
                };
            }
            case `${entityNameUpper}_REQUEST_ITEM`:
                return {
                    ...state,
                    byId: {
                        ...state.byId,
                        [action.id]: {
                            ...state.byId[action.id]
                        }
                    },
                    byIdMetadata: {
                        ...state.byIdMetadata,
                        [action.id]: {
                            ...state.byIdMetadata[action.id],
                            isFetching: true,
                            didInvalidate: false
                        }
                    }
                };
            case `${entityNameUpper}_RECEIVE_ITEM`:
                return {
                    ...state,
                    byId: {
                        ...state.byId,
                        [action.id]: {
                            ...state.byId[action.id],
                            ...action.item
                        }
                    },
                    byIdMetadata: {
                        ...state.byIdMetadata,
                        [action.id]: {
                            ...state.byIdMetadata[action.id],
                            default: false,
                            isFetching: false,
                            didInvalidate: false,
                            lastUpdated: action.receivedAt
                        }
                    }
                };
            case `${entityNameUpper}_BEFORE_UPDATE_ITEM`:
                return {
                    ...state,
                    byIdMetadata: {
                        ...state.byIdMetadata,
                        [action.id]: {
                            ...state.byIdMetadata[action.id],
                            updating: false
                        }
                    }
                };
            default:
                return state;
        }
    }
}";
        }
    }
}