using Humanizer;
using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.React.Src
{
    /// <summary>
    /// App.jsx Template
    /// </summary>
    public class AppTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public AppTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "App.jsx";
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
            var resources = new List<string>();
            var imports = new List<string>();
            foreach (var entity in Project.Entities.OrderBy(a => a.InternalName))
            {
                var createAndEdit = Project.Screens.Any(s => s.EntityId == entity.Id && s.ScreenType == ScreenType.Form);
                var list = Project.Screens.Any(s => s.EntityId == entity.Id && s.ScreenType == ScreenType.Search);
                if (createAndEdit && list)
                {
                    imports.Add($"import {entity.InternalName}List from './resources/{entity.InternalNamePlural.Camelize()}/{entity.InternalName}List';");
                    imports.Add($"import {entity.InternalName}Create from './resources/{entity.InternalNamePlural.Camelize()}/{entity.InternalName}Create';");
                    imports.Add($"import {entity.InternalName}Edit from './resources/{entity.InternalNamePlural.Camelize()}/{entity.InternalName}Edit';");
                    resources.Add($@"<Resource name=""{entity.InternalNamePlural}"" options={{{{ label: '{entity.Title}' }}}} list={{{entity.InternalName}List}} create={{{entity.InternalName}Create}} edit={{{entity.InternalName}Edit}} />".IndentLines(8));
                }
                else if (createAndEdit)
                {
                    imports.Add($"import {entity.InternalName}Create from './resources/{entity.InternalNamePlural.Camelize()}/{entity.InternalName}Create';");
                    imports.Add($"import {entity.InternalName}Edit from './resources/{entity.InternalNamePlural.Camelize()}/{entity.InternalName}Edit';");
                    resources.Add($@"<Resource name=""{entity.InternalNamePlural}"" options={{{{ label: '{entity.Title}' }}}} create={{{entity.InternalName}Create}} edit={{{entity.InternalName}Edit}} />".IndentLines(8));
                }
                else if (list)
                {
                    imports.Add($"import {entity.InternalName}List from './resources/{entity.InternalNamePlural.Camelize()}{entity.InternalName}List';");
                    resources.Add($@"<Resource name=""{entity.InternalNamePlural}"" options={{{{ label: '{entity.Title}' }}}} list={{{entity.InternalName}List}} />".IndentLines(8));
                }
            }

            return $@"import React from 'react';
import {{ Admin, Resource }} from 'react-admin';
import CustomLayout from './Layout';
import jsonServerProvider from './RestProvider';
{string.Join(Environment.NewLine, imports)}

const App = () => 
    <Admin 
        title=""{Project.Title}""
        dataProvider={{jsonServerProvider('/api')}}
        locale=""en"" 
        appLayout={{CustomLayout}}>
{string.Join(Environment.NewLine, resources)}
    </Admin>;

export default App;";
        }
    }
}