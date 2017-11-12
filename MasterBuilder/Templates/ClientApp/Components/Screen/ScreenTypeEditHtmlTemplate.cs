using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Screen
{
    public class ScreenTypeEditHtmlTemplate
    {
        public static string Evaluate(Project project, Request.Screen screen)
        {
            var sections = new List<string>();
            if (screen.ScreenSections != null)
            {
                foreach (var screenSection in screen.ScreenSections)
                {
                    switch (screenSection.ScreenSectionType)
                    {
                        case ScreenSectionTypeEnum.Form:
                            sections.Add(ScreenTypeEdit.SectionTypeFormHtmlTemplate.Evaluate(project, screen, screenSection));
                            break;
                        case ScreenSectionTypeEnum.Search:
                            break;
                        case ScreenSectionTypeEnum.Grid:
                            break;
                        case ScreenSectionTypeEnum.Html:
                            break;
                        default:
                            break;
                    }
                }
            }

            return $@"<div class=""screen-type-edit"">
    <form *ngIf=""{screen.InternalName.ToCamlCase()}"" [formGroup]=""{screen.InternalName.ToCamlCase()}Form"" #formDir=""ngForm"" (ngSubmit)=""onSubmit()"" novalidate>
{string.Join(Environment.NewLine, sections)}
      <button type=""submit"" class=""btn btn-success"">Submit</button>
    </form>
</div>";

        }
    }
}