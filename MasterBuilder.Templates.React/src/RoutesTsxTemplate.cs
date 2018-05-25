using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;

namespace MasterBuilder.Templates.React.Src
{
    /// <summary>
    /// Routes.tsx Template
    /// </summary>
    public class RoutesTsxTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public RoutesTsxTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Routes.tsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", };
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
                imports.Add($"import {screen.InternalName}Page from './containers/{screen.InternalName}Page';");
                routes.Add($@"            <Route exact={{true}} path=""/{screen.Path}"" component={{{screen.InternalName}Page}} />");
            }

            return $@"/**
 * Routes
 */

import * as React from 'react';
import {{ Route }} from 'react-router';
import HomePage from './pages/HomePage';
import TodoPage from './pages/TodoPage';
{string.Join(Environment.NewLine, imports)}

class Routes extends React.Component {{

    render() {{
        return (
            <div>
            <Route exact={{true}} path=""/"" component={{HomePage}} />
            <Route exact={{true}} path=""/home"" component={{HomePage}} />
            <Route exact={{true}} path=""/todo"" component={{TodoPage}} />
{string.Join(Environment.NewLine, routes)}
        </div>
        );
    }}
}}

export default Routes;";
        }
    }
}