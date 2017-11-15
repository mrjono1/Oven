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
                            sections.Add(ScreenTypeSearch.SectionTypeSearchHtmlTemplate.Evaluate(project, screen, screenSection));
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

            return $@"{new String(' ', 4)}<div class=""screen-type-edit"">
{new String(' ', 8)}<form *ngIf=""{screen.InternalName.ToCamlCase()}"" [formGroup]=""{screen.InternalName.ToCamlCase()}Form"" #formDir=""ngForm"" (ngSubmit)=""onSubmit()"" novalidate>
{new String(' ', 12)}<button type=""submit"" class=""btn btn-success"">Submit</button>
{string.Join(Environment.NewLine, sections)}
{new String(' ', 8)}</form>
{new String(' ', 4)}</div>";

        }
    }
}