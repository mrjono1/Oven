using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.React.ClientApp.Src
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
            return new string[] { "ClientApp", "src" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var imports = new List<string>();
            var routes = new List<string>();
            foreach (var screen in Project.Screens.Where(a => a.ScreenType != ScreenType.Form && a.ScreenType != ScreenType.Search))
            {
                imports.Add($"import {screen.InternalName} from './containers/{screen.InternalName}';");
                routes.Add($@"<Route exact path=""/{screen.Path}"" component={{{screen.InternalName}}} />");
            }

            return $@"/**
 * App Routes
 */

import React from 'react';
import {{ Route }} from 'react-router-dom';
{string.Join(Environment.NewLine, imports.OrderBy(a => a))}

export default [
{string.Join(string.Concat(",", Environment.NewLine), routes.OrderBy(a => a)).IndentLines(4)}
];";
        }
    }
}