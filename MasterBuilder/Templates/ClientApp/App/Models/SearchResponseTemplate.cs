using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    public class SearchResponseTemplate : ITemplate
    {
        private readonly Project Project;

        private readonly Screen Screen;

        private readonly ScreenSection ScreenSection;

        public SearchResponseTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }
        
        public string GetFileName()
        {
            return $"{ScreenSection.InternalName}Response.ts";
        }

        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "models", $"{Screen.InternalName.ToLowerInvariant()}" };
        }

        public string GetFileContent()
        {
            return $@"import {{ {ScreenSection.InternalName}Item }} from './{ScreenSection.InternalName}Item';

export interface {ScreenSection.InternalName} {{
    items: {ScreenSection.InternalName}Item[];
    totalPages: number;
    totalItems: number;
}}";
        }
    }
}
