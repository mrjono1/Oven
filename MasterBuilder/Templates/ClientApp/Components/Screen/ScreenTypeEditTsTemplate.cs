using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Screen
{
    public class ScreenTypeEditTsTemplate
    {
        public static IEnumerable<string> Imports(Project project, Request.Screen screen)
        {
            var imports = new List<string>
            {
                "import { ActivatedRoute } from '@angular/router';",
                "import { FormControl, FormGroup, Validators } from '@angular/forms';"
            };

            return imports;
        }


        public static string Evaluate(Project project, Request.Screen screen)
        {
            var entity = project.Entities.SingleOrDefault(p => p.Id == screen.EntityId);

            var sectionImports = new List<string>();
            var sections = new List<string>();
            foreach (var section in screen.ScreenSections)
            {

            }
            
            return $@"

    constructor(private route: ActivatedRoute, 
                private http: Http) {{ }}

    ngOnInit(){{
        this.sub = this.route.params.subscribe(params => {{
            if (params['id']) {{
                this.new = false;
                this.http.get('api/{entity.InternalName}/{screen.InternalName}/' + params['id']).subscribe(result => {{
                    this.{screen.InternalName.ToCamlCase()} = result.json() as {screen.InternalName};
                    this.setupForm();
                }}, error => console.error(error));
            }} else {{
                this.new = true;
                this.{screen.InternalName.ToCamlCase()} = new {screen.InternalName}();
                this.setupForm();
            }}
        }});
    }}

    
    private getPatchOperations(): Operation[] {{
        let operations: Operation[] = [];

        Object.keys(this.projectForm.controls).forEach((name) => {{
            let currentControl = this.projectForm.controls[name];

            if (currentControl.dirty) {{
                let operation = new Operation();
                operation.op = 'replace';
                operation.path = '/' + name;
                operation.value = currentControl.value;
                operations.push(operation);
            }}
        }});
        return operations;
    }}

    onSubmit() {{ 
        // Don't submit if nothing has changed
        if (this.projectForm.pristine) {{
            return;
        }}
        
        //todo ensure validated
        this.submitted = true;
        
        let request = {{}};
        if (this.new){{
            // Post new
            this.http.post('api/{entity.InternalName}/{screen.InternalName}', this.{screen.InternalName.ToCamlCase()}).subscribe( results => {{
                alert(results);
            }});
        }} else {{
            // Patch existing
            let operations = this.getPatchOperations();
            this.http.patch('api/{entity.InternalName}/{screen.InternalName}/' + this.{screen.InternalName.ToCamlCase()}.id, operations).subscribe( results => {{
                alert(results);
                this.projectForm.markAsPristine(true);
            }});
        }}
    }}
}}";
        }

        internal static IEnumerable<string> Classes(Project project, Request.Screen screen)
        {
            return new string[]
            {
                @"export class Operation {
    op: string;
    path: string;
    value: any;
}"
            };
        }

        internal static IEnumerable<string> ClassProperties(Project project, Request.Screen screen)
        {
            var properties = new List<string>
            {
                $@"public {screen.InternalName.ToCamlCase()}: {screen.InternalName};",
                $"public {screen.InternalName.ToCamlCase()}Form: FormGroup;",
                "public new: boolean;",
                "private sub: any;",
                "private submitted: boolean;"
            };

            return properties;
        }
    }
}