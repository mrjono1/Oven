using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                if (property.PropertyTemplate == PropertyTemplateEnum.PrimaryKey)
                {
                    continue;
                }
                headings.Add($@"             <th data-property-id=""{property.Id}"" >{property.Title}</th>");
                bindings.Add($"             <td>{{{{ {screen.InternalName.ToCamlCase()}Item.{property.InternalName.ToCamlCase()} }}}}</td>");
            }

            var navigateToScreenPath = (from s in project.Screens
                                        where s.Id == screen.NavigateToScreenId
                                        select s.Path).FirstOrDefault();
            
            return $@"<h1>{screen.Title}</h1>
<nav>
    <a [routerLink]=""['/{navigateToScreenPath}']"">New</a>
</nav>
<p *ngIf=""!response""><em>Loading...</em></p>

<table class='table' *ngIf=""response && response.items"">
    <thead>
        <tr>
{string.Join(Environment.NewLine, headings)}
        </tr>
    </thead>
    <tbody>
        <tr [attr.data-id]=""{screen.InternalName.ToCamlCase()}Item.id"" *ngFor=""let {screen.InternalName.ToCamlCase()}Item of response.items""  [routerLink]=""['/{navigateToScreenPath}', {screen.InternalName.ToCamlCase()}Item.id]"">
{string.Join(Environment.NewLine, bindings)}
        </tr>
    </tbody>
</table>";

        }
    }
}