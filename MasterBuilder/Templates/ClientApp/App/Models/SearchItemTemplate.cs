using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    /// <summary>
    /// Search Item Template
    /// </summary>
    public class SearchItemTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchItemTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{ScreenSection.SearchSection.SearchItemClass}.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "models", $"{Screen.InternalName.ToLowerInvariant()}" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var properties = new List<string>();

            foreach (var searchColumn in ScreenSection.SearchSection.SearchColumns)
            {
                properties.Add($"    {searchColumn.InternalNameCSharp.Camelize()}: {searchColumn.TypeTs};");
            }
            
            return $@"export interface {ScreenSection.SearchSection.SearchItemClass} {{
{string.Join(Environment.NewLine, properties)}
}}";
        }
    }
}
