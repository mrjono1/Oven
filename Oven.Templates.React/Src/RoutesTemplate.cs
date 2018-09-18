using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.React.Src
{
    /// <summary>
    /// Routes.jsx Template
    /// </summary>
    public class RoutesTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public RoutesTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Routes.jsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var imports = new List<string>();
            var routes = new List<string>();
            foreach (var screen in Project.Screens)
            {
                imports.Add($"import {screen.InternalName}Page from '../containers/{screen.InternalName}Page';");
                if (screen.ScreenType == ScreenType.Form)
                {
                    routes.Add($@"<Route exact path=""/{screen.Path}/:id"" component={{{screen.InternalName}Page}} />".IndentLines(4));
                }
                else
                {
                    routes.Add($@"<Route exact path=""/{screen.Path}"" component={{{screen.InternalName}Page}} />".IndentLines(4));
                }
            }

            if (Project.DefaultScreenId.HasValue)
            {
                var defaultScreen = Project.Screens.Single(a => a.Id == Project.DefaultScreenId.Value);
                routes.Insert(0, $@"<Route exact path=""/"" component={{{defaultScreen.InternalName}Page}} />".IndentLines(4));
                // Import should already be added above
            }

            return $@"/**
 * App Routes
 */

import React from 'react';
import {{ Route }} from 'react-router-dom';
{string.Join(Environment.NewLine, imports.OrderBy(a => a))}

export default [
{string.Join(string.Concat(",", Environment.NewLine), routes.OrderBy(a => a))}
];";
        }
    }
}