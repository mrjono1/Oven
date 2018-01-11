using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Ts
{
    /// <summary>
    /// Container Ts Template
    /// </summary>
    public class ContainerTsTemplate: ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContainerTsTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Screen.InternalName.ToLowerInvariant()}.component.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "containers", Screen.InternalName.ToLowerInvariant() };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var imports = new List<string>();
            var classProperties = new List<string>();
            var constructorBodySections = new List<string>();
            var onNgInitBodySections = new List<string>();
            var functions = new List<string>();
            var constructorParamerters = new List<string>{
                "private http: HttpClient",
                "private router: Router",
                "private route: ActivatedRoute"
            };

            var hasForm = false;
            if (Screen.ScreenSections == null)
            {
                Screen.ScreenSections = new ScreenSection[] { };
            }
            foreach (var screenSection in Screen.ScreenSections)
            {
                Entity entity = null;
                Entity formEntity = null;
                if (screenSection.EntityId.HasValue) {
                    entity = Project.Entities.SingleOrDefault(e => e.Id == screenSection.EntityId.Value);
                }

                Property parentProperty = null;
                switch (screenSection.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:

                        formEntity = entity;

                        imports.Add($"import {{ {screenSection.InternalName} }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}';");

                        classProperties.Add($"public {screenSection.InternalName.Camelize()}: {screenSection.InternalName};");
                        classProperties.Add($"public {screenSection.InternalName.Camelize()}Form: FormGroup;");
                        classProperties.Add("public new: boolean;");

                        parentProperty = (from p in entity.Properties
                                              where p.PropertyType == PropertyTypeEnum.ParentRelationship
                                              select p).SingleOrDefault();
                        string setParentProperty = null;
                        if (parentProperty != null)
                        {
                            var parentEnitity = (from e in Project.Entities
                                                 where e.Id == parentProperty.ParentEntityId
                                                 select e).SingleOrDefault();
                            setParentProperty = $"this.{screenSection.InternalName.Camelize()}.{parentProperty.InternalName.Camelize()}Id = params['{parentEnitity.InternalName.Camelize()}Id'];";
                        }

                        onNgInitBodySections.Add($@"        this.route.params.subscribe(params => {{
            if (params['{Screen.InternalName.Camelize()}Id']) {{
                this.new = false;
                this.http.get<{screenSection.InternalName}>('api/{entity.InternalName}/{screenSection.InternalName}/' + params['{entity.InternalName.Camelize()}Id']).subscribe(result => {{
                    this.{screenSection.InternalName.Camelize()} = result;
                    this.setupForm();
                }}, error => console.error(error));
            }} else {{
                this.new = true;
                this.{screenSection.InternalName.Camelize()} = new {screenSection.InternalName}();
                {setParentProperty}
                this.setupForm();
            }}
        }});");
                        hasForm = true;
                        break;
                    case ScreenSectionTypeEnum.Search:

                        imports.Add($"import {{ {screenSection.InternalName}Item }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}Item';");
                        imports.Add($"import {{ {screenSection.InternalName}Request }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}Request';");
                        imports.Add($"import {{ {screenSection.InternalName}Response }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}Response';");
                        imports.Add("import { MatTableDataSource } from '@angular/material';");

                        constructorBodySections.Add($@"        this.{screenSection.InternalName.Camelize()}Request = new {screenSection.InternalName}Request();
        this.{screenSection.InternalName.Camelize()}Request.page = 1;
        this.{screenSection.InternalName.Camelize()}Request.pageSize = 20;");

                        classProperties.Add($"public {screenSection.InternalName.Camelize()}Response: {screenSection.InternalName}Response;");
                        classProperties.Add($"public {screenSection.InternalName.Camelize()}Request: {screenSection.InternalName}Request;");

                        var propertiesToDisplay = new List<string>();
                        foreach (var property in entity.Properties)
                        {
                            switch (property.PropertyType)
                            {
                                case PropertyTypeEnum.PrimaryKey:
                                    break;
                                case PropertyTypeEnum.ParentRelationship:
                                    break;
                                case PropertyTypeEnum.ReferenceRelationship:
                                    propertiesToDisplay.Add($"'{property.InternalName.Camelize()}Title'");
                                    break;
                                default:
                                    propertiesToDisplay.Add($"'{property.InternalName.Camelize()}'");
                                    break;
                            }
                        }
                        

                        classProperties.Add($"{screenSection.InternalName.Camelize()}Columns = [{string.Join(",", propertiesToDisplay)}];");
                        classProperties.Add($"{screenSection.InternalName.Camelize()}DataSource = new MatTableDataSource<{screenSection.InternalName}Item>();");

                        string parentPropertyFilterString = null;
                        Entity parentEntity = null;
                        if (entity != null)
                        {
                            parentProperty = (from p in entity.Properties
                                                  where p.PropertyType == PropertyTypeEnum.ParentRelationship
                                                  select p).SingleOrDefault();
                            if (parentProperty != null)
                            {
                                parentEntity = (from s in Project.Entities
                                                where s.Id == parentProperty.ParentEntityId
                                                select s).SingleOrDefault();
                                parentPropertyFilterString = $"this.{screenSection.InternalName.Camelize()}Request.{parentEntity.InternalName.Camelize()}Id = params['{parentEntity.InternalName.Camelize()}Id'];";
                            }
                        }

                        onNgInitBodySections.Add($@"        this.route.params.subscribe(params => {{
            this.{screenSection.InternalName.Camelize()}Request = new {screenSection.InternalName}Request();
            this.{screenSection.InternalName.Camelize()}Request.page = 1;
            this.{screenSection.InternalName.Camelize()}Request.pageSize = 20;
            {parentPropertyFilterString}

             this.{entity.InternalName.Camelize()}Service.get{Screen.InternalName}{screenSection.InternalName}(this.{screenSection.InternalName.Camelize()}Request).subscribe( result => {{
                this.{screenSection.InternalName.Camelize()}Response = result;
                this.{screenSection.InternalName.Camelize()}DataSource = new MatTableDataSource<{screenSection.InternalName}Item>(result.items);
            }});
        }});");


                        break;
                    case ScreenSectionTypeEnum.MenuList:
                        // Nothing at the moment
                        break;
                    case ScreenSectionTypeEnum.Html:
                        break;
                    default:
                        break;
                }

                if (entity != null)
                {
                    imports.Add($"import {{ {entity.InternalName}Service }} from '../../shared/{entity.InternalName.ToLowerInvariant()}.service';");
                    constructorParamerters.Add($"private {entity.InternalName.Camelize()}Service: {entity.InternalName}Service");
                }

                if (screenSection.MenuItems != null)
                {
                    foreach (var menuItem in screenSection.MenuItems)
                    {
                        switch (menuItem.MenuItemType)
                        {
                            case MenuItemTypeEnum.ApplicationLink:
                                break;
                            case MenuItemTypeEnum.New:
                                break;
                            case MenuItemTypeEnum.ServerFunction:
                                functions.Add($@"public {menuItem.InternalName.Camelize()}(){{
        this.{entity.InternalName.Camelize()}Service.get{menuItem.InternalName}(this.{Screen.InternalName.Camelize()}.id).subscribe( result => {{
            alert(result);
        }});
    }}");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            
            if (hasForm)
            {
                var form = new BuildForm(Project, Screen);
                functions.Add(form.GetFunctions());
                imports.AddRange(form.GetImports());
                constructorParamerters.AddRange(form.GetConstructorParamerters());
                onNgInitBodySections.AddRange(form.GetngOnInit());
            }

            if (Screen.InternalName.Equals("home", StringComparison.OrdinalIgnoreCase))
            {
                imports.Add("import { TranslateService } from '@ngx-translate/core';");
                classProperties.Add($"title: string = '{Project.Title}'");
                constructorParamerters.Add("public translate: TranslateService");
                functions.Add(@"public setLanguage(lang) {
        this.translate.use(lang);
    }");
            }
            
            return $@"import {{ HttpClient }} from '@angular/common/http';
import {{ Component, Inject, OnInit }} from '@angular/core';
import {{ Router, ActivatedRoute }} from '@angular/router';
{string.Join(Environment.NewLine, imports.Distinct())}

@Component({{
    selector: '{Screen.InternalName.ToLowerInvariant()}',
    templateUrl: './{Screen.InternalName.ToLowerInvariant()}.component.html'
}})
export class {Screen.InternalName}Component implements OnInit {{
    {string.Join(string.Concat(Environment.NewLine, "    "), classProperties.Distinct())}

    constructor({string.Join(string.Concat(",", Environment.NewLine, "      "), constructorParamerters.Distinct())}){{
{string.Join(Environment.NewLine, constructorBodySections)}
    }}

    ngOnInit(){{
{string.Join(Environment.NewLine, onNgInitBodySections.Distinct())}
    }}

{string.Join(Environment.NewLine, functions)}
}}";
        }
    }
}
