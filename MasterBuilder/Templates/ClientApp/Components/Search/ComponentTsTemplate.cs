using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Search
{
    public class ComponentTsTemplate
    {
        public static string FileName(string folder, Screen screen)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, Path.Combine("app", "components", screen.InternalName.ToLowerInvariant())), $"{screen.InternalName.ToLowerInvariant()}.component.ts");
        }

        public static string Evaluate(Project project, Entity entity, Screen screen)
        {
            var properties = new List<string>();
            foreach (var property in entity.Properties)
            {
                properties.Add($"   {property.InternalName.ToCamlCase()}: {property.TsType};");
            }

            string cssFile = null;
            if (!string.IsNullOrEmpty(screen.Css))
            {
                cssFile = $@",
    styleUrls: ['./{screen.InternalName.ToLowerInvariant()}.component.css']";
            }

            return $@"import {{ Component, Inject }} from '@angular/core';
import {{ Http }} from '@angular/http';

@Component({{
    selector: '{screen.InternalName.ToLowerInvariant()}',
    templateUrl: './{screen.InternalName.ToLowerInvariant()}.component.html'{cssFile}
}})
export class {screen.InternalName}Component {{
    public response: {screen.InternalName};
    public request: {screen.InternalName}Request;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {{
        this.request = new {screen.InternalName}Request();
        this.request.page = 1;
        this.request.pageSize = 20;

        http.post(baseUrl + 'api/{entity.InternalName}/{screen.InternalName}', this.request).subscribe(result => {{
            this.response = result.json() as {screen.InternalName};
        }}, error => console.error(error));
    }}
}}

export class {screen.InternalName}Request {{
    page: number;
    pageSize: number;
}}

interface {screen.InternalName} {{
    items: {screen.InternalName}Item[];
    totalPages: number;
    totalItems: number;
}}


interface {screen.InternalName}Item {{
{string.Join(Environment.NewLine, properties)}
}}

";
        }
    }
}