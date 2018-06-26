using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.React.Src.Modules
{
    /// <summary>
    /// actions.js Template
    /// </summary>
    public class ActionsTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public ActionsTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => "actions.js";

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath() => new string[] { "src", "modules", Entity.InternalName.Camelize() };

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var name = Entity.InternalName.Camelize();
            var hasSearchScreenSection = (from screen in Project.Screens
                                          from screenSection in screen.ScreenSections
                                          where screenSection.ScreenSectionType == ScreenSectionType.Search &&
                                          screenSection.EntityId == Entity.Id
                                          select screen).Any();

            var hasFormScreenSection = (from screen in Project.Screens
                                        from screenSection in screen.ScreenSections
                                        where screenSection.ScreenSectionType == ScreenSectionType.Form &&
                                        screenSection.EntityId == Entity.Id
                                        select screen).Any();

            var functions = new List<string>();
            if (hasSearchScreenSection)
            {
                functions.Add($@"function requestItems() {{
    return {{
        type: actionTypes.REQUEST_ITEMS
    }};
}}");
                functions.Add($@"function receiveItems(json) {{
    return {{
        type: actionTypes.RECEIVE_ITEMS,
        items: json.items,
        receivedAt: Date.now()
    }};
}}");
                functions.Add($@"function fetchItems() {{
    return dispatch => {{
        dispatch(requestItems());
        return fetch('/api/{Entity.InternalNamePlural}/search', {{
            method: 'POST', 
            body: JSON.stringify({{ page: 1, pageSize: 10 }}),
            headers: {{
                'Content-Type': 'application/json'
            }}
        }})
      .then(response => response.json())
      .then(json => dispatch(receiveItems(json)));
    }}
}}

function shouldFetchItems(state) {{
    const items = state.items;
    if (!items) {{
        return true;
    }} else if (items.isFetching) {{
        return false;
    }} else {{
        return items.didInvalidate;
    }}
}}

export function fetchItemsIfNeeded() {{
    // Note that the function also receives getState()
    // which lets you choose what to dispatch next.
    // This is useful for avoiding a network request if
    // a cached value is already available.

    return (dispatch, getState) => {{
        if (shouldFetchItems(getState())) {{
            // Dispatch a thunk from thunk!
            return dispatch(fetchItems());
        }} else {{
            // Let the calling code know there's nothing to wait for.
            return Promise.resolve();
        }}
    }};
}}");
            }

            if (hasFormScreenSection)
            {
                functions.Add($@"function fetchItem(id) {{
    return dispatch => {{
        dispatch(requestItem(id));
        return fetch(`/api/{Entity.InternalNamePlural}/${{id}}`, {{
            method: 'GET',
            headers: {{
                'Content-Type': 'application/json'
            }}
        }})
        .then(response => response.json())
        .then(json => dispatch(receiveItem(id, json)));
    }}
}}
function shouldFetchItem(state, id) {{
    const item = state.{Entity.InternalNamePlural.Camelize()}.byId[id];
    if (!item || item.$default) {{
        return true;
    }} else if (item.isFetching) {{
        return false;
    }} else {{
        return item.didInvalidate;
    }}
}}");
                functions.Add(@"
function beforeRequestItem(id) {
    return {
        type: actionTypes.BEFORE_REQUEST_ITEM,
        id: id
    };
}
function requestItem(id) {
    return {
        type: actionTypes.REQUEST_ITEM,
        id: id
    };
}
function receiveItem(id, json) {
    return {
        type: actionTypes.RECEIVE_ITEM,
        id: id,
        item: json,
        receivedAt: Date.now()
    };
}

export function fetchItemIfNeeded(id) {
    return (dispatch, getState) => {
        dispatch(beforeRequestItem(id));
        if (shouldFetchItem(getState(), id)) {
            // Dispatch a thunk from thunk!
            return dispatch(fetchItem(id));
        } else {
            // Let the calling code know there's nothing to wait for.
            return Promise.resolve();
        }
    };
}");
            }


            return $@"import fetch from 'cross-fetch';
import * as actionTypes from './actionTypes';

{string.Join(Environment.NewLine, functions)}";
        }
    }
}