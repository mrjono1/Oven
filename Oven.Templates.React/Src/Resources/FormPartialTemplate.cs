using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;
using System;
using System.Linq;
using Humanizer;

namespace Oven.Templates.React.Src.Resources
{
    /// <summary>
    /// xList.jsx Template
    /// </summary>
    public class FormPartialTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;
        private readonly bool IsCreate;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormPartialTemplate(Project project, Screen screen, ScreenSection screenSection, bool isCreate)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
            IsCreate = isCreate;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => $"{(IsCreate ? "Create" : "Edit")}{ScreenSection.InternalName}.jsx";

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath() => new string[] { "src", "resources", Screen.Entity.InternalNamePlural.Camelize() };

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var imports = new List<string>();

            var formSection = new CreateFormSectionPartialTemplate(Screen, ScreenSection, IsCreate);

            if (formSection.Blank)
            {
                return null;
            }

            imports.AddRange(formSection.Imports);

            return $@"import React from 'react';
import {{ {string.Join(", ", imports.Distinct().OrderBy(a => a))} }} from 'react-admin';

const {(IsCreate ? "Create" : "Edit")}{ScreenSection.InternalName} = ({(formSection.Content.Contains("{...props}") ? "props" : "")}) => (
    <div>
{formSection.Content.IndentLines(8)}
    </div>
);

export default {(IsCreate ? "Create" : "Edit")}{ScreenSection.InternalName};";
        }
    }
}