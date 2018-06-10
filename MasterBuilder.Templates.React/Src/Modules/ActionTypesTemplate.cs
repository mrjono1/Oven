using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.React.Src.Modules
{
    /// <summary>
    /// actionTypes.js Template
    /// </summary>
    public class ActionTypesTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public ActionTypesTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => "actionTypes.js";

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
            var constants = new List<string>();

            if (hasSearchScreenSection)
            {
                constants.Add(GetConstant(name, "REQUEST_ITEMS"));
                constants.Add(GetConstant(name, "RECEIVE_ITEMS"));
                constants.Add(GetConstant(name, "INVALIDATE_ITEMS"));
            }


            return $@"/**
 * {name} ActionTypes
 */
{string.Join(Environment.NewLine, constants)}";
        }

        private string GetConstant(string name, string actionType)
        {
            return $"export const {actionType} = '{name}/{actionType}';";
        }
    }
}