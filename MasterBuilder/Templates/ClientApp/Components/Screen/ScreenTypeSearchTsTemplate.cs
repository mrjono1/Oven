using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Screen
{
    public class ScreenTypeSearchTsTemplate
    {
        internal static IEnumerable<string> Imports(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                results.AddRange(ScreenTypeSearch.SectionTypeSearchTsTemplate.Imports(project, screen, section));
            }
            return results;
        }

        internal static IEnumerable<string> ClassProperties(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                results.AddRange(ScreenTypeSearch.SectionTypeSearchTsTemplate.ClassProperties(project, screen, section));
            }
            return results;
        }

        internal static IEnumerable<string> ConstructorParameters(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                results.AddRange(ScreenTypeSearch.SectionTypeSearchTsTemplate.ConstructorParameters(project, screen, section));
            }
            return results;
        }

        internal static IEnumerable<string> ConstructorBody(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                results.Add(ScreenTypeSearch.SectionTypeSearchTsTemplate.ConstructorBody(project, screen, section));
            }
            return results;
        }

        internal static IEnumerable<string> NgInitBody(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                results.Add(ScreenTypeSearch.SectionTypeSearchTsTemplate.NgOnInitBody(project, screen, section));
            }
            return results;
        }
        
        internal static IEnumerable<string> Classes(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                results.AddRange(ScreenTypeSearch.SectionTypeSearchTsTemplate.Classes(project, screen, section));
            }
            return results;
        }
    }
}