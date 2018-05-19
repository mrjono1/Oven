using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Shared
{
    /// <summary>
    /// Service Template
    /// </summary>
    public class ServiceTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {            
            return $"{Screen.InternalName.ToLowerInvariant()}.service.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "shared"};
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var imports = new List<string>();
            var methods = new List<string>();
            var referenceFormFields = new List<FormField>();
            var properties = new List<string>();
            var constructorExperssions = new List<string>();

            var hasForm = false;

            foreach (var screenSection in Screen.ScreenSections.Where(e => e.EntityId.HasValue))
            {
                switch (screenSection.ScreenSectionType)
                {
                    case ScreenSectionType.Form:

                        hasForm = true;
                        referenceFormFields.AddRange(screenSection.FormSection.FormFields.Where(a => a.PropertyType == PropertyType.ReferenceRelationship));

                        break;
                    case ScreenSectionType.Search:

                        imports.Add($"import {{ {screenSection.SearchSection.SearchItemClass} }} from '../models/{Screen.InternalName.ToLowerInvariant()}/{screenSection.SearchSection.SearchItemClass}';");
                        imports.Add($"import {{ {screenSection.SearchSection.SearchRequestClass} }} from '../models/{Screen.InternalName.ToLowerInvariant()}/{screenSection.SearchSection.SearchRequestClass}';");
                        imports.Add($"import {{ {screenSection.SearchSection.SearchResponseClass} }} from '../models/{Screen.InternalName.ToLowerInvariant()}/{screenSection.SearchSection.SearchResponseClass}';");

                        var privateProperty = $"_{screenSection.SearchSection.SearchItemClass.Camelize()}";
                        var dataStore = $"{screenSection.SearchSection.SearchItemClass.Camelize()}DataStore";
                        var dataStoreProperty = $"{screenSection.Entity.InternalName.Camelize().Pluralize()}";

                        properties.Add($@"    private {privateProperty}: BehaviorSubject<{screenSection.SearchSection.SearchItemClass}[]>;");
                        properties.Add($@"    private {dataStore}: {{
        {dataStoreProperty}: {screenSection.SearchSection.SearchItemClass}[]
    }};");
                        constructorExperssions.Add($@"        this.{dataStore} = {{ {dataStoreProperty}: [] }};");
                        constructorExperssions.Add($@"        this.{privateProperty} = <BehaviorSubject<{screenSection.SearchSection.SearchItemClass}[]>>new BehaviorSubject([]);");

                        // Get property
                        methods.Add($@"    /**
     * Gets an observable list of {screenSection.SearchSection.SearchItemClass.Camelize()}
     */
    get {Screen.InternalName.Camelize()}{screenSection.InternalName.Pluralize()}() {{
        return this.{privateProperty}.asObservable();
    }}");
                        var validation = string.Empty;
                        var parentProperties = (from prop in screenSection.Entity.Properties
                                                where prop.PropertyType == PropertyType.ParentRelationshipOneToMany
                                                select $"!request.{prop.InternalName.Camelize()}Id");
                        if (parentProperties.Any())
                        {
                            validation = $@"
        if ({string.Join(" && ", parentProperties)}){{
            return;
        }}";
                        }

                        // Load method
                        methods.Add($@"    load{Screen.InternalName}{screenSection.InternalName}(request: {screenSection.SearchSection.SearchRequestClass}) {{{validation}
        this.http.post<{screenSection.SearchSection.SearchResponseClass}>(`${{this.baseUrl}}/api/{Screen.InternalName}/{Screen.InternalName}{screenSection.InternalName}`, request).subscribe(data => {{
            this.{dataStore}.{dataStoreProperty} = data.items;
            this.{privateProperty}.next(Object.assign({{}}, this.{dataStore}).{dataStoreProperty});
        }}, error => console.log('Could not load {screenSection.SearchSection.SearchResponseClass}'));
    }}");
                        break;
                    case ScreenSectionType.MenuList:
                        // None
                        break;
                    case ScreenSectionType.Html:
                        // None
                        break;
                    default:
                        break;
                }

                if (screenSection.MenuItems != null)
                {
                    foreach (var menuItem in screenSection.MenuItems)
                    {
                        switch (menuItem.MenuItemType)
                        {
                            case MenuItemType.ServerFunction:
                                methods.Add($@"    get{menuItem.InternalName}(id: string){{
        return this.http.get(`${{this.baseUrl}}/api/{Screen.InternalName}/{Screen.InternalName}{screenSection.InternalName}{menuItem.InternalName}/${{id}}`);
    }}");
                                break;
                        }
                    }
                }
            }

            if (hasForm)
            {
                imports.Add($"import {{ {Screen.InternalName} }} from '../models/{Screen.InternalName.ToLowerInvariant()}/{Screen.InternalName}';");
                imports.Add($"import {{ Operation }} from '../models/Operation';");

                methods.Add($@"    get{Screen.InternalName}(id: string){{
        return this.http.get<{Screen.InternalName}>(`${{this.baseUrl}}/api/{Screen.InternalName}/{Screen.InternalName}/${{id}}`);
    }}");
                methods.Add($@"    add{Screen.InternalName}(request: any){{
        return this.http.post<string>(`${{this.baseUrl}}/api/{Screen.InternalName}/{Screen.InternalName}`, request);
    }}");
                if (Project.UsePutForUpdate)
                {
                    methods.Add($@"    update{Screen.InternalName}(id: string, request: any){{
        return this.http.put<string>(`${{this.baseUrl}}/api/{Screen.InternalName}/{Screen.InternalName}/${{id}}`, request);
    }}");
                }
                else
                {
                    methods.Add($@"    update{Screen.InternalName}(id: string, operations: Operation[]){{
        return this.http.patch<string>(`${{this.baseUrl}}/api/{Screen.InternalName}/{Screen.InternalName}/${{id}}`, operations);
    }}");
                }
            }

            foreach (var referenceFormField in referenceFormFields)
            {
                var serviceReferenceMethodTemplate = new ServiceReferenceMethodTemplate(Project, Screen, referenceFormField);
                imports.AddRange(serviceReferenceMethodTemplate.Imports());
                methods.AddRange(serviceReferenceMethodTemplate.Method());
                properties.AddRange(serviceReferenceMethodTemplate.Properties());
                constructorExperssions.AddRange(serviceReferenceMethodTemplate.ConstructorExpressions());
            }

            return $@"import {{ Injectable, Inject, Injector }} from '@angular/core';
import {{ HttpClient }} from '@angular/common/http';
import {{ Http, URLSearchParams }} from '@angular/http';
import {{ APP_BASE_HREF }} from '@angular/common';
import {{ ORIGIN_URL }} from '@nguniversal/aspnetcore-engine/tokens';
{string.Join(Environment.NewLine, imports.Distinct())}
import {{ Observable }} from 'rxjs/Observable';
import {{ BehaviorSubject }} from 'rxjs/BehaviorSubject';

@Injectable()
export class {Screen.InternalName}Service {{    
    private baseUrl: string;
{string.Join(Environment.NewLine, properties)}

    constructor(
      private http: HttpClient,
      private injector: Injector
    ) {{
        this.baseUrl = this.injector.get(ORIGIN_URL);
{string.Join(Environment.NewLine, constructorExperssions)}
    }}

{string.Join(Environment.NewLine, methods.Distinct())}
}}
";   
        }
    }
}