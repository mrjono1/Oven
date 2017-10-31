using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Search
{
    public class ComponentHtmlTemplate
    {
        public static string FileName(string folder, Screen screen)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, Path.Combine("app", "components", screen.InternalName.ToLowerInvariant())), $"{screen.InternalName.ToLowerInvariant()}.component.html");
        }

        public static string Evaluate(Project project, Entity entity, Screen screen)
        {
            var headings = new List<string>();
            var bindings = new List<string>();
            foreach (var property in entity.Properties)
            {
                headings.Add($"             <th>{property.Title}</th>");
                bindings.Add($"             <td>{{{{ {screen.InternalName.ToCamlCase()}Item.{property.InternalName.ToCamlCase()} }}}}</td>");
            }
            
            return $@"<h1>{screen.Title}</h1>

<p *ngIf=""!response""><em>Loading...</em></p>

<table class='table' *ngIf=""response && response.items"">
    <thead>
        <tr>
{string.Join(Environment.NewLine, headings)}
        </tr>
    </thead>
    <tbody>
        <tr *ngFor=""let {screen.InternalName.ToCamlCase()}Item of response.items"">
{string.Join(Environment.NewLine, bindings)}
        </tr>
    </tbody>
</table>";

        }
    }
}