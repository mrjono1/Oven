using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Screen
{
    public class ScreenTypeSearchTsTemplate
    {
        public static string Evaluate(Project project, Request.Screen screen)
        {
            var entity = project.Entities.SingleOrDefault(p => p.Id == screen.EntityId);
                        
            return $@"
    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {{
        this.request = new {screen.InternalName}Request();
        this.request.page = 1;
        this.request.pageSize = 20;

        http.post(baseUrl + 'api/{entity.InternalName}/{screen.InternalName}', this.request).subscribe(result => {{
            this.response = result.json() as {screen.InternalName};
        }}, error => console.error(error));
    }}

    ngOnInit(){{
    }}";
        }

        internal static IEnumerable<string> Imports(Project project, Request.Screen screen)
        {
            return new string[] { };
        }

        internal static IEnumerable<string> ClassProperties(Project project, Request.Screen screen)
        {
            return new string[] {
                $"public response: {screen.InternalName};",
                $"public request: {screen.InternalName}Request;"
            };
        }
        
        internal static IEnumerable<string> Classes(Project project, Request.Screen screen)
        {
            var entity = project.Entities.SingleOrDefault(p => p.Id == screen.EntityId);

            var properties = new List<string>();
            foreach (var property in entity.Properties)
            {
                properties.Add($"   {property.InternalName.ToCamlCase()}: {property.TsType};");
            }

            return new string[]
            {
                $@"export class {screen.InternalName}Request {{
    page: number;
    pageSize: number;
}}",

$@"interface {screen.InternalName} {{
    items: {screen.InternalName}Item[];
    totalPages: number;
    totalItems: number;
}}

interface {screen.InternalName}Item {{
{string.Join(Environment.NewLine, properties)}
}}"
            };
        }
    }
}