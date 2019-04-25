using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;
using System;
using System.Linq;
using Humanizer;

namespace Oven.Templates.React.ClientApp.Src.Resources
{
    /// <summary>
    /// xList.jsx Template
    /// </summary>
    public class FormPartialTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormPartialTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => $"{ScreenSection.InternalName}.jsx";

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath() => new string[] { "ClientApp", "src", "resources", Screen.Entity.InternalNamePlural.Camelize() };

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var imports = new List<string>();
            var constants = new List<string>();

            var formSection = new CreateFormSectionPartialTemplate(Screen, ScreenSection);
            constants.AddRange(formSection.Constants);

            if (formSection.Blank)
            {
                return null;
            }

            imports.AddRange(formSection.Imports);

            var source = "";
            if (ScreenSection.ParentScreenSectionId.HasValue)
            {
                source = $@" source=""{ScreenSection.Entity.InternalName.Camelize()}""";
            }

            return $@"import React from 'react';
import {{ {string.Join(", ", imports.Distinct().OrderBy(a => a))} }} from 'react-admin';

{string.Join(Environment.NewLine, constants)}

const {ScreenSection.InternalName} = ({(formSection.Content.Contains("{...props}") ? "props" : "")}) => (
    <div>
{formSection.Content.IndentLines(8)}
    </div>
);

export default {ScreenSection.InternalName};";
        }
    }
}