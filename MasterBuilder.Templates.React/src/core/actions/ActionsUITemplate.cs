using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Core.Actions
{
    /// <summary>
    /// actions ui Template
    /// </summary>
    public class ActionsUITemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ActionsUITemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "actions-ui.js";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "core", "actions" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"import constants from 'core/types';

/**
 * openNav - Open the side nav
 */
export function openNav() {
    return {
        type: constants.OPEN_NAV
    };
}

/**
 * closeNav - Close the side nav
 */
export function closeNav() {
    return {
        type: constants.CLOSE_NAV
    };
}";
        }
    }
}