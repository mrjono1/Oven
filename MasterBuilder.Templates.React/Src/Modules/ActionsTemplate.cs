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
    type: RECEIVE_ITEMS,
    items: json.data.children.map(child => child.data),
    receivedAt: Date.now()
  }};
}}");
                functions.Add($@"function fetchItems() {{
  return dispatch => {{
    dispatch(requestItems());
    return fetch('/api/{Entity.InternalNamePlural}')
      .then(response => response.json())
      .then(json => dispatch(receiveItems(json)));
  }}
}}
​
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
​
export function fetchItemsIfNeeded() {{
  // Note that the function also receives getState()
  // which lets you choose what to dispatch next.
​
  // This is useful for avoiding a network request if
  // a cached value is already available.
​
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
            return $@"import fetch from 'cross-fetch';
import * as actionTypes from './actionTypes';

{string.Join(Environment.NewLine, functions)}";
        }
    }
}