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

            var hasForm = false;

            foreach (var group in (from s in Project.Screens
                                           from ss in s.ScreenSections
                                           where ss.EntityId == Entity.Id
                                           select new
                                           {
                                               Screen = s,
                                               ScreenSection = ss
                                           }))
            {
                switch (group.ScreenSection.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:

                        imports.Add($"import {{ {group.ScreenSection.InternalName} }} from '../models/{group.Screen.InternalName.ToLowerInvariant()}/{group.ScreenSection.InternalName}'");

                        methods.Add($@"get{group.Screen.InternalName}{group.ScreenSection.InternalName}(id: string){{
    return this.http.get<{group.ScreenSection.InternalName}>(`${{this.baseUrl}}/api/{Entity.InternalName}/{group.Screen.InternalName}{group.ScreenSection.InternalName}/${{id}}`);
}}");
                        methods.Add($@"add{Entity.InternalName}{group.Screen.InternalName}(request: any){{
    return this.http.post<string>(`${{this.baseUrl}}/api/{Entity.InternalName}/{group.Screen.InternalName}`, request);
}}");

                        methods.Add($@"update{Entity.InternalName}{group.Screen.InternalName}(id: string, operations: Operation[]){{
    return this.http.post<string>(`${{this.baseUrl}}/api/{Entity.InternalName}/{group.Screen.InternalName}/${{id}}`, operations);
}}");
                        hasForm = true;
                        break;
                    case ScreenSectionTypeEnum.Search:

                        imports.Add($"import {{ {group.ScreenSection.InternalName}Item }} from '../models/{group.Screen.InternalName.ToLowerInvariant()}/{group.ScreenSection.InternalName}Item'");
                        imports.Add($"import {{ {group.ScreenSection.InternalName}Request }} from '../models/{group.Screen.InternalName.ToLowerInvariant()}/{group.ScreenSection.InternalName}Request'");
                        imports.Add($"import {{ {group.ScreenSection.InternalName}Response }} from '../models/{group.Screen.InternalName.ToLowerInvariant()}/{group.ScreenSection.InternalName}Response'");

                        methods.Add($@"get{group.Screen.InternalName}{group.ScreenSection.InternalName}(request: {group.ScreenSection.InternalName}Request){{
    return this.http.post<{group.ScreenSection.InternalName}Response>(`${{this.baseUrl}}/api/{Entity.InternalName}/{group.Screen.InternalName}{group.ScreenSection.InternalName}`, request);
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
            }
            

            return $@"import {{ Injectable, Inject, Injector }} from '@angular/core';
import {{ HttpClient }} from '@angular/common/http';
import {{ Http, URLSearchParams }} from '@angular/http';
import {{ APP_BASE_HREF }} from '@angular/common';
import {{ ORIGIN_URL }} from '@nguniversal/aspnetcore-engine';
{string.Join(Environment.NewLine, imports.Distinct())}
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

{string.Join(Environment.NewLine, methods.Distinct())}

}}
";   
        }
    }
}
