using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.React.Src.Components
{
    /// <summary>
    /// AppRoutes.jsx Template
    /// </summary>
    public class AppRoutesTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public AppRoutesTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "AppRoutes.jsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "components" };
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
                routes.Add($@"            <Route exact path=""/{screen.Path}"" component={{{screen.InternalName}Page}} />");
            }

            if (Project.DefaultScreenId.HasValue)
            {
                var defaultScreen = Project.Screens.Single(a => a.Id == Project.DefaultScreenId.Value);
                routes.Insert(0, $@"            <Route exact path=""/"" component={{{defaultScreen.InternalName}Page}} />");
                // Import should already be added above
            }

            return $@"/**
 * App Routes
 */

import React, {{ Component }} from 'react';
import {{ Route }} from 'react-router-dom';
import {{ Router }} from 'react-router';
import {{ createBrowserHistory }} from 'history';
{string.Join(Environment.NewLine, imports)}

const history = createBrowserHistory();

class Routes extends Component {{

  render() {{
    return (
      <Router history={{history}}>      
        <div>
{string.Join(Environment.NewLine, routes)}
        </div>
      </Router>
    );
  }}
}}

export default Routes;";
        }
    }
}