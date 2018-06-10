using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.React.Src.Modules
{
    public class ReducerTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public ReducerTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => "reducer.js";

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath() => new string[] { "src", "modules", Entity.InternalName.Camelize() };
        
        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var hasSearchScreenSection = (from screen in Project.Screens
                                          from screenSection in screen.ScreenSections
                                          where screenSection.ScreenSectionType == ScreenSectionType.Search &&
                                          screenSection.EntityId == Entity.Id
                                          select screen).Any();
            var statements = new List<string>();
            if (hasSearchScreenSection)
            {
                statements.Add($@"  case actionTypes.INVALIDATE_ITEMS:
    return Object.assign({{}}, state, {{
      didInvalidate: true
    }});
  case actionTypes.REQUEST_ITEMS:
    return Object.assign({{}}, state, {{
      isFetching: true,
      didInvalidate: false
    }});
  case actionTypes.RECEIVE_ITEMS:
    return Object.assign({{}}, state, {{
      isFetching: false,
      didInvalidate: false,
      items: action.posts,
      lastUpdated: action.receivedAt
    }});");
            }
                return $@"import * as actionTypes from './actionTypes';

const initialState = {{
  isFetching: false,
  didInvalidate: false,
  items: []
}};

const reducer = (state = initialState, action) => {{
  switch(action.type) {{
{string.Join(Environment.NewLine, statements)}
    default:
      return state;
  }}
}}

export default reducer;";
        }
    }
}