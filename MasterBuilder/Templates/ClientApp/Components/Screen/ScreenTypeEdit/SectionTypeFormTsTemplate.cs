using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humanizer;

namespace MasterBuilder.Templates.ClientApp.Components.Screen.ScreenTypeEdit
{
    public class SectionTypeFormTsTemplate
    {
        public static IEnumerable<string> Imports(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            var imports = new List<string>
            {
                "import { FormControl, FormGroup, Validators } from '@angular/forms';"
            };

            return imports;
        }
        public static string ConstructorBody(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            return null;
        }

        public static string NgOnInitBody(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            var entity = project.Entities.SingleOrDefault(p => p.Id == screenSection.EntityId);

            var parentProperty = (from p in entity.Properties
                                  where p.Type == PropertyTypeEnum.ParentRelationship
                                  select p).SingleOrDefault();
            string setParentProperty = null;
            if (parentProperty != null)
            {
                var parentEnitity = (from e in project.Entities
                                     where e.Id == parentProperty.ParentEntityId
                                     select e).SingleOrDefault();
                setParentProperty = $"this.{screenSection.InternalName.ToCamlCase()}.{parentProperty.InternalName.ToCamlCase()}Id = params['{parentEnitity.InternalName.ToCamlCase()}Id'];";
            }

            return $@"this.route.params.subscribe(params => {{
            if (params['{screen.InternalName.ToCamlCase()}Id']) {{
                this.new = false;
                this.http.get<{screenSection.InternalName}>('api/{entity.InternalName}/{screenSection.InternalName}/' + params['{entity.InternalName.ToCamlCase()}Id']).subscribe(result => {{
                    this.{screenSection.InternalName.ToCamlCase()} = result;
                    this.setupForm();
                }}, error => console.error(error));
            }} else {{
                this.new = true;
                this.{screenSection.InternalName.ToCamlCase()} = new {screenSection.InternalName}();
                {setParentProperty}
                this.setupForm();
            }}
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

        internal static IEnumerable<string> ClassProperties(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            return new string[] {
                $@"public {screenSection.InternalName.ToCamlCase()}: {screenSection.InternalName};",
                $"public {screenSection.InternalName.ToCamlCase()}Form: FormGroup;"
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
                    properties.Add($"   {property.InternalName.ToCamlCase()}Id: {property.TsType};");
                }
                else
                {
                    properties.Add($"   {property.InternalName.ToCamlCase()}: {property.TsType};");
                }
            }

            return new string[]
            {
                $@"export class {screenSection.InternalName} {{
{string.Join(Environment.NewLine, properties)}
}}"
            };
        }
    }
}
