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

            return $@"this.route.params.subscribe(params => {{
            if (params['id']) {{
                this.new = false;
                this.http.get('api/{entity.InternalName}/{screenSection.InternalName}/' + params['id']).subscribe(result => {{
                    this.{screenSection.InternalName.ToCamlCase()} = result.json() as {screenSection.InternalName};
                    this.setupForm();
                }}, error => console.error(error));
            }} else {{
                this.new = true;
                this.{screenSection.InternalName.ToCamlCase()} = new {screenSection.InternalName}();
                this.setupForm();
            }}
        }});";
        }

        internal static IEnumerable<string> ConstructorParameters(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            return new string[] {
                "private http: Http",
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
                properties.Add($"   {property.InternalName.ToCamlCase()}: {property.TsType};");
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
