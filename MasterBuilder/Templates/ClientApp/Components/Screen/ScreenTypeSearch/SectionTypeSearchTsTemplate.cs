using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Screen.ScreenTypeSearch
{
    public class SectionTypeSearchTsTemplate
    {
        public static string ConstructorBody(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            var entity = project.Entities.SingleOrDefault(p => p.Id == screenSection.EntityId);

            return $@"    this.{screenSection.InternalName.ToCamlCase()}Request = new {screenSection.InternalName}Request();
    this.{screenSection.InternalName.ToCamlCase()}Request.page = 1;
    this.{screenSection.InternalName.ToCamlCase()}Request.pageSize = 20;";
        }

        public static string NgOnInitBody(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            var entity = project.Entities.SingleOrDefault(p => p.Id == screenSection.EntityId);

            return $@"
        this.{screenSection.InternalName.ToCamlCase()}Request = new {screenSection.InternalName}Request();
        this.{screenSection.InternalName.ToCamlCase()}Request.page = 1;
        this.{screenSection.InternalName.ToCamlCase()}Request.pageSize = 20;

        this.http.post('api/{entity.InternalName}/{screen.InternalName}{screenSection.InternalName}', this.{screenSection.InternalName.ToCamlCase()}Request).subscribe(result => {{
            this.{screenSection.InternalName.ToCamlCase()}Response = result.json() as {screenSection.InternalName};
        }}, error => console.error(error));";
        }

        internal static IEnumerable<string> ConstructorParameters(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            return new string[] {
                "private http: Http"
            };
        }
        internal static IEnumerable<string> Imports(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            return new string[] { };
        }

        internal static IEnumerable<string> ClassProperties(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            return new string[] {
                $"public {screenSection.InternalName.ToCamlCase()}Response: {screenSection.InternalName};",
                $"public {screenSection.InternalName.ToCamlCase()}Request: {screenSection.InternalName}Request;"
            };
        }

        internal static IEnumerable<string> Classes(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            var entity = project.Entities.SingleOrDefault(p => p.Id == screenSection.EntityId);

            var properties = new List<string>();
            foreach (var property in entity.Properties)
            {
                if (property.Type == PropertyTypeEnum.ParentRelationship)
                {
                    continue;
                }
                properties.Add($"   {property.InternalName.ToCamlCase()}: {property.TsType};");
            }

            return new string[]
            {
                $@"export class {screenSection.InternalName}Request {{
    page: number;
    pageSize: number;
}}",

$@"interface {screenSection.InternalName} {{
    items: {screenSection.InternalName}Item[];
    totalPages: number;
    totalItems: number;
}}

interface {screenSection.InternalName}Item {{
{string.Join(Environment.NewLine, properties)}
}}"
            };
        }
    }
}