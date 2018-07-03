using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.React.Src.Actions
{
    /// <summary>
    /// entityActions.js Template
    /// </summary>
    public class EntityActionsTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityActionsTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => "entityActions.js";

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath() => new string[] { "src", "actions" };

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"import fetch from 'cross-fetch';
export default function createEntityActions(entityName = '', entityNamePlural = '', entityNameUpper = ''){

    function requestItems() {
        return {
            type: `${entityNameUpper}_REQUEST_ITEMS`
        };
    }

    function receiveItems(json) {
        return {
            type: `${entityNameUpper}_RECEIVE_ITEMS`,
            items: json.items,
            receivedAt: Date.now()
        };
    }
    
    function fetchItems() {
        return dispatch => {
            dispatch(requestItems());
            return fetch(`/api/${entityNamePlural}/search`, {
                method: 'POST', 
                body: JSON.stringify({ page: 1, pageSize: 10 }),
                headers: {
                    'Content-Type': 'application/json'
                }
            })
          .then(response => response.json())
          .then(json => dispatch(receiveItems(json)));
        }
    }
    
    function shouldFetchItems(state) {
        const items = state.items;//[entityName]
        if (!items) {
            return true;
        } else if (items.$isFetching) {
            return false;
        } else {
            return items.$didInvalidate;
        }
    }
    
    function fetchItem(id) {
        return dispatch => {
            dispatch(requestItem(id));
            return fetch(`/api/${entityNamePlural}/${id}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => response.json())
            .then(json => dispatch(receiveItem(id, json)));
        }
    }

    function shouldFetchItem(state, id) {
        const item = state[entityName].byId[id];
        if (!item || item.$default) {
            return true;
        } else if (item.$isFetching) {
            return false;
        } else {
            return item.$didInvalidate;
        }
    }

    function beforeRequestItem(id) {
        return {
            type: `${entityNameUpper}_BEFORE_REQUEST_ITEM`,
            id: id
        };
    }

    function requestItem(id) {
        return {
            type: `${entityNameUpper}_REQUEST_ITEM`,
            id: id
        };
    }

    function receiveItem(id, json) {
        return {
            type: `${entityNameUpper}_RECEIVE_ITEM`,
            id: id,
            item: json,
            receivedAt: Date.now()
        };
    }

    // Functions available externally
    return {
        fetchItemsIfNeeded: function () {
            return (dispatch, getState) => {
                if (shouldFetchItems(getState())) {
                    // Dispatch a thunk from thunk!
                    return dispatch(fetchItems());
                } else {
                    // Let the calling code know there's nothing to wait for.
                    return Promise.resolve();
                }
            }
        },
        fetchItemIfNeeded: function (id) {
            return (dispatch, getState) => {
                dispatch(beforeRequestItem(id));
                if (shouldFetchItem(getState(), id)) {
                    // Dispatch a thunk from thunk!
                    return dispatch(fetchItem(id));
                } else {
                    // Let the calling code know there's nothing to wait for.
                    return Promise.resolve();
                }
            }
        }
    }
}";
        }
    }
}