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
                functions.Add($@"function requestItems(filter) {{
  return {{
    type: actionTypes.REQUEST_ITEMS,
    filter
  }}
}}");
                functions.Add($@"function receiveItems(filter, json) {{
  return {{
    type: RECEIVE_ITEMS,
    filter,
    items: json.data.children.map(child => child.data),
    receivedAt: Date.now()
  }}
}}");
            }
            return $@"import fetch from 'cross-fetch';
import * as actionTypes from './actionTypes';

{string.Join(Environment.NewLine, functions)}";
        }
    }
}