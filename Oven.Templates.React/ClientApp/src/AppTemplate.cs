using Humanizer;
using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.React.ClientApp.Src
{
    /// <summary>
    /// App Template
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
        public string GetFileName() => "App.js";

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath() => new string[] { "ClientApp", "src" };

        private bool GetResource(Entity entity, out string[] imports, out string resource)
        {
            var createAndEdit = Project.Screens.Any(s => s.EntityId == entity.Id && s.ScreenType == ScreenType.Form);
            var list = Project.Screens.Any(s => s.EntityId == entity.Id && s.ScreenType == ScreenType.Search);
            if (createAndEdit && list)
            {
                imports = new string[] {
                    $"import {entity.InternalName}List from './resources/{entity.InternalNamePlural.Camelize()}/{entity.InternalName}List';",
                    $"import {{ {entity.InternalName}Create, {entity.InternalName}Edit }} from './resources/{entity.InternalNamePlural.Camelize()}/{entity.InternalName}Form';"
                };
                resource = $@"<Resource name=""{entity.InternalNamePlural}"" options={{{{ label: '{entity.Title}' }}}} list={{{entity.InternalName}List}} create={{{entity.InternalName}Create}} edit={{{entity.InternalName}Edit}} />".IndentLines(8);
            }
            else if (createAndEdit)
            {
                imports = new string[] { $"import {{ {entity.InternalName}Create, {entity.InternalName}Edit }} from './resources/{entity.InternalNamePlural.Camelize()}/{entity.InternalName}Form';"};
                resource = $@"<Resource name=""{entity.InternalNamePlural}"" options={{{{ label: '{entity.Title}' }}}} create={{{entity.InternalName}Create}} edit={{{entity.InternalName}Edit}} />".IndentLines(8);
            }
            else if (list)
            {
                imports = new string[] { $"import {entity.InternalName}List from './resources/{entity.InternalNamePlural.Camelize()}{entity.InternalName}List';" };
                resource = $@"<Resource name=""{entity.InternalNamePlural}"" options={{{{ label: '{entity.Title}' }}}} list={{{entity.InternalName}List}} />".IndentLines(8);
            }
            else
            {
                imports = new string[] { };
                resource = null;
                return false;
            }

            return true;
        }


        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var resources = new List<string>();
            var imports = new List<string>();

            Entity defaultEntity = null;
            foreach (var entity in Project.Entities.OrderBy(a => a.InternalName))
            {
                if (Project.DefaultScreen != null && Project.DefaultScreen.EntityId.HasValue && Project.DefaultScreen.EntityId == entity.Id)
                {
                    defaultEntity = entity;
                    continue;
                }

                if (GetResource(entity, out string[] outImports, out string outResource))
                {
                    imports.AddRange(outImports);
                    resources.Add(outResource);
                }
            }
            if (defaultEntity != null && GetResource(defaultEntity, out string[] outDefaultImports, out string outDefaultResource))
            {
                imports.AddRange(outDefaultImports);
                resources.Insert(0, outDefaultResource);
            }

            return $@"import React from 'react';
import {{ Admin, Resource }} from 'react-admin';
import CustomLayout from './Layout';
import jsonServerProvider from './RestProvider';
import createHistory from 'history/createBrowserHistory';
import customRoutes from './Routes';
{string.Join(Environment.NewLine, imports)}

const history = createHistory();

const App = () => 
    <Admin 
        appLayout={{CustomLayout}}
        dataProvider={{jsonServerProvider('/api')}}
        customRoutes={{customRoutes}}
        history={{history}}
        locale=""en"" 
        title=""{Project.Title}"">
{string.Join(Environment.NewLine, resources)}
    </Admin>;

export default App;";
        }
    }
}