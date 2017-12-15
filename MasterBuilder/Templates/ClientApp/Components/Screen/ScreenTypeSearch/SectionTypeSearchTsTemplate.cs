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

            string parentPropertyFilterString = null;
            Entity parentEntity = null;
            if (entity != null)
            {
                var parentProperty = (from p in entity.Properties
                                      where p.Type == PropertyTypeEnum.ParentRelationship
                                      select p).SingleOrDefault();
                if (parentProperty != null)
                {
                    parentEntity = (from s in project.Entities
                                    where s.Id == parentProperty.ParentEntityId
                                    select s).SingleOrDefault();
                    parentPropertyFilterString = $"this.{screenSection.InternalName.ToCamlCase()}Request.{parentEntity.InternalName.ToCamlCase()}Id = params['{parentEntity.InternalName.ToCamlCase()}Id'];";
                }
            }

            return $@"      this.route.params.subscribe(params => {{
            this.{screenSection.InternalName.ToCamlCase()}Request = new {screenSection.InternalName}Request();
            this.{screenSection.InternalName.ToCamlCase()}Request.page = 1;
            this.{screenSection.InternalName.ToCamlCase()}Request.pageSize = 20;
            {parentPropertyFilterString}

            this.http.post<{screenSection.InternalName}>('api/{entity.InternalName}/{screen.InternalName}{screenSection.InternalName}', this.{screenSection.InternalName.ToCamlCase()}Request).subscribe(result => {{
                this.{screenSection.InternalName.ToCamlCase()}Response = result;
            }}, error => console.error(error));
        }});";
        }

        internal static IEnumerable<string> ConstructorParameters(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            return new string[] {
                "private http: HttpClient",
                "private router: Router",
                "private route: ActivatedRoute"
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
            Property parentProperty = null;
            foreach (var property in entity.Properties)
            {
                if (property.Type == PropertyTypeEnum.ParentRelationship)
                {
                    parentProperty = property;
                    continue;
                }
                properties.Add($"   {property.InternalName.ToCamlCase()}: {property.TsType};");
            }

            string parentPropertyFilterString = null;
            Entity parentEntity = null;
            if (entity != null)
            {
                if (parentProperty != null)
                {
                    parentEntity = (from s in project.Entities
                                    where s.Id == parentProperty.ParentEntityId
                                    select s).SingleOrDefault();
                    parentPropertyFilterString = $"{parentEntity.InternalName.ToCamlCase()}Id: string;";
                }
            }

            return new string[]
            {
                $@"export class {screenSection.InternalName}Request {{
    page: number;
    pageSize: number;
    {parentPropertyFilterString}
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