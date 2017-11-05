using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Edit
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

            return $@"import {{ Component, Inject, OnInit, KeyValueChanges, KeyValueDiffer, KeyValueDiffers }} from '@angular/core';
import {{ Http }} from '@angular/http';
import {{ ActivatedRoute }} from '@angular/router';

@Component({{
    selector: '{screen.InternalName.ToLowerInvariant()}',
    templateUrl: './{screen.InternalName.ToLowerInvariant()}.component.html'{cssFile}
}})
export class {screen.InternalName}Component implements OnInit {{
    public response: {screen.InternalName};
    public new: boolean;
    private sub: any;
    private submitted: boolean;
    private objectDiffer: KeyValueDiffer<string, any>;
    private postChanges: Operation[];

    constructor(private route: ActivatedRoute, 
                private http: Http,
                private differs: KeyValueDiffers) {{ }}

    ngOnInit(){{
        this.sub = this.route.params.subscribe(params => {{
            if (params['id']) {{
                this.new = false;
                this.http.get('api/{entity.InternalName}/{screen.InternalName}/' + params['id']).subscribe(result => {{
                    this.response = result.json() as {screen.InternalName};
                    this.objectDiffer = this.differs.find(this.response).create();
                    this.postChanges = [];
                }}, error => console.error(error));
            }} else {{
                this.new = true;
                this.response = new {screen.InternalName}();
            }}
        }});
    }}
    
    onSubmit() {{ 
        
        //todo ensure validated
        this.submitted = true;
        
        let request = {{}};
        if (this.new){{
            // Post new
            this.http.post('api/{entity.InternalName}/{screen.InternalName}', this.response).subscribe( results => {{
                alert(results);
            }});
        }} else {{
            // Patch existing
            this.http.patch(api/{entity.InternalName}/{screen.InternalName}/' + this.response.id, this.postChanges).subscribe( results => {{
                alert(results);
            }});
        }}
    }}

    objectChanged(changes: KeyValueChanges<string, any>) {{
        changes.forEachChangedItem((record) => {{
            let change = this.postChanges.filter(function(element, index) {{
                return (element.path === '/' + record.key);
            }})[0];
            if (change) {{
                change.value = record.currentValue;
            }} else {{
                change = new Operation();
                change.op = 'replace';
                change.path = '/' + record.key;
                change.value = record.currentValue;
                this.postChanges.push(change);
            }}
        }});

        console.log('changes');
    }}

    ngDoCheck(): void {{
        if (this.objectDiffer) {{
            const changes = this.objectDiffer.diff(this.response);
            if (changes) {{
                this.objectChanged(changes);
            }}
        }}
    }}
}}

export class {screen.InternalName} {{
{string.Join(Environment.NewLine, properties)}
}}

export class Operation {{
    op: string;
    path: string;
    value: any;
}}

";
        }
    }
}