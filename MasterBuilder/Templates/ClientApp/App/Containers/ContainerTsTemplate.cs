using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.ClientApp.App.Containers
{
    public class ContainerTsTemplate: ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        public ContainerTsTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        public string GetFileName()
        {
            return $"{Screen.InternalName.ToLowerInvariant()}.ts";
        }

        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "containers", Screen.InternalName.ToLowerInvariant() };
        }

        public string GetFileContent()
        {
            var imports = new List<string>();
            var classProperties = new List<string>();
            var constructorBodySections = new List<string>();
            var constructorParamerters = new List<string>{
                "private http: HttpClient",
                "private router: Router",
                "private route: ActivatedRoute"
            };

            var hasForm = false;
            foreach (var screenSection in Screen.ScreenSections)
            {
                var entity = Project.Entities.SingleOrDefault(e => e.Id == screenSection.EntityId.Value);

                switch (screenSection.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:

                        imports.Add($"import {{ {screenSection.InternalName} }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}'");
                        imports.Add("import { FormControl, FormGroup, Validators } from '@angular/forms';");

                        classProperties.Add($"public {screenSection.InternalName.ToCamlCase()}: {screenSection.InternalName};");
                        classProperties.Add($"public {screenSection.InternalName.ToCamlCase()}Form: FormGroup;");
                        classProperties.Add("public new: boolean;");

                        hasForm = true;
                        break;
                    case ScreenSectionTypeEnum.Search:

                        imports.Add($"import {{ {screenSection.InternalName}Item }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}Item'");
                        imports.Add($"import {{ {screenSection.InternalName}Request }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}Request'");
                        imports.Add($"import {{ {screenSection.InternalName}Response }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}Response'");

                        constructorBodySections.Add($@"    this.{screenSection.InternalName.ToCamlCase()}Request = new {screenSection.InternalName}Request();
    this.{screenSection.InternalName.ToCamlCase()}Request.page = 1;
    this.{screenSection.InternalName.ToCamlCase()}Request.pageSize = 20;");

                        classProperties.Add($"public {screenSection.InternalName.ToCamlCase()}Response: {screenSection.InternalName}Response;");
                        classProperties.Add($"public {screenSection.InternalName.ToCamlCase()}Request: {screenSection.InternalName}Request;");
                        
                        break;
                    case ScreenSectionTypeEnum.Grid:
                        break;
                    case ScreenSectionTypeEnum.Html:
                        break;
                    default:
                        break;
                }

                if (entity != null)
                {
                    imports.Add($"import {{ {entity.InternalName}Service }} from '../../shared/{entity.InternalName.ToLowerInvariant()}.service'");
                    constructorParamerters.Add($"private {entity.InternalName.ToCamlCase()}Service: {entity.InternalName}Service");
                }
            }
            
            if (hasForm)
            {
                imports.Add($"import {{ Operation }} from '../../models/Operation'");
            }

            string cssFile = null;
            if (!string.IsNullOrEmpty(Screen.Css))
            {
                cssFile = $@",
    styleUrls: ['./{Screen.InternalName.ToLowerInvariant()}.component.css']";
            }

            return $@"import {{ HttpClient }} from '@angular/common/http';
import {{ Component, Inject, OnInit }} from '@angular/core';
import {{ Router, ActivatedRoute }} from '@angular/router';
{string.Join(Environment.NewLine, imports.Distinct())}

@Component({{
    selector: '{Screen.InternalName.ToLowerInvariant()}',
    templateUrl: './{Screen.InternalName.ToLowerInvariant()}.component.html'{cssFile}
}})
export class {Screen.InternalName}Component implements OnInit {{
{string.Join(string.Concat(Environment.NewLine, "    "), classProperties.Distinct())}

    constructor({string.Join(string.Concat(",", Environment.NewLine), constructorParamerters.Distinct())}){{
{string.Join(Environment.NewLine, constructorBodySections)}
    }}

    ngOnInit(){{

    }}


}}";
        }
    }
}
