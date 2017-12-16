using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Shared
{
    public class ServiceTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        public ServiceTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        public string GetFileName()
        {            
            return $"{Entity.InternalName.ToCamlCase()}.service.ts";
        }

        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "shared"};
        }

        public string GetFileContent()
        {
            var imports = new List<string>();
            var methods = new List<string>();

            foreach (var screen in Project.Screens.Where(s => s.EntityId == Entity.Id))
            {
                var hasForm = false;
                foreach (var screenSection in screen.ScreenSections)
                {
                    switch (screenSection.ScreenSectionType)
                    {
                        case ScreenSectionTypeEnum.Form:

                            imports.Add($"import {{ {screenSection.InternalName} }} from '../models/{screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}'");

                            methods.Add($@"get{screen.InternalName}{screenSection.InternalName}(id: string){{
    return this.http.get<{screenSection.InternalName}>(`${{this.baseUrl}}/api/{Entity.InternalName}/{screen.InternalName}{screenSection.InternalName}/${{id}}`);
}}");
                            hasForm = true;
                            break;
                        case ScreenSectionTypeEnum.Search:

                            imports.Add($"import {{ {screenSection.InternalName}Item }} from '../models/{screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}Item'");
                            imports.Add($"import {{ {screenSection.InternalName}Request }} from '../models/{screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}Request'");
                            imports.Add($"import {{ {screenSection.InternalName}Response }} from '../models/{screen.InternalName.ToLowerInvariant()}/{screenSection.InternalName}Response'");

                            methods.Add($@"get{screen.InternalName}{screenSection.InternalName}(request: {screenSection.InternalName}Request){{
    return this.http.post<{screenSection.InternalName}Response>(`${{this.baseUrl}}/api/{Entity.InternalName}/{screen.InternalName}{screenSection.InternalName}`, request);
}}");

                            break;
                        case ScreenSectionTypeEnum.Grid:
                            break;
                        case ScreenSectionTypeEnum.Html:
                            break;
                        default:
                            break;
                    }
                }

                if (hasForm)
                {
                    imports.Add($"import {{ Operation }} from '../models/Operation'");


                    methods.Add($@"add{Entity.InternalName}{screen.InternalName}(request: any){{
    return this.http.post<string>(`${{this.baseUrl}}/api/{Entity.InternalName}/{screen.InternalName}`, request);
}}");

                    methods.Add($@"update{Entity.InternalName}{screen.InternalName}(id: string, operations: Operation[]){{
    return this.http.post<string>(`${{this.baseUrl}}/api/{Entity.InternalName}/{screen.InternalName}/${{id}}`, operations);
}}");
                }
            }

            return $@"import {{ Injectable, Inject, Injector }} from '@angular/core';
import {{ HttpClient }} from '@angular/common/http';
import {{ Http, URLSearchParams }} from '@angular/http';
import {{ APP_BASE_HREF }} from '@angular/common';
import {{ ORIGIN_URL }} from '@nguniversal/aspnetcore-engine';
{string.Join(Environment.NewLine, imports)}
import {{ Observable }} from 'rxjs/Observable';

@Injectable()
export class {Entity.InternalName}Service {{

    private baseUrl: string;

    constructor(
      private http: HttpClient,
      private injector: Injector
    ) {{
        this.baseUrl = this.injector.get(ORIGIN_URL);
    }}

{string.Join(Environment.NewLine, methods)}

}}
";   
        }
    }
}
