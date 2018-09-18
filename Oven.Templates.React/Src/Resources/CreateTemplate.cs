using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;
using System;
using System.Linq;
using Humanizer;

namespace Oven.Templates.React.Src.Resources
{
    /// <summary>
    /// xCreate.jsx Template
    /// </summary>
    public class CreateTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => $"{Screen.Entity.InternalName}Create.jsx";

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath() => new string[] { "src", "resources", Screen.Entity.InternalNamePlural.Camelize() };

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var imports = new List<string> { "Create", "SimpleForm" };
            var screenSections = new List<string>();

            foreach (var section in Screen.ScreenSections)
            {
                switch (section.ScreenSectionType)
                {
                    case ScreenSectionType.Form:
                        var formSection = new CreateFormSectionPartialTemplate(Screen, section, true);
                        imports.AddRange(formSection.Imports);
                        if (!string.IsNullOrEmpty(formSection.Content))
                        {
                            screenSections.Add(formSection.Content);
                        }
                        break;
                }
            }

            return $@"import React from 'react';
import {{ {string.Join(", ", imports.Distinct().OrderBy(a => a))} }} from 'react-admin';

const {Screen.Entity.InternalName}Create = (props) => (
    <Create {{...props}} title=""Create {Screen.Title}"">
        <SimpleForm>
{string.Join(Environment.NewLine, screenSections).IndentLines(12)}
        </SimpleForm>
    </Create>
);

export default {Screen.Entity.InternalName}Create;";
        }
    }
}