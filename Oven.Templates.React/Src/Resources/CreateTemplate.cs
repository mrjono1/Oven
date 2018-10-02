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
            var componentImports = new List<string>();
            var screenSections = new List<string>();
            var constants = new List<string>();

            ScreenSection rootScreenSection = null;
            foreach (var section in Screen.ScreenSections)
            {
                switch (section.ScreenSectionType)
                {
                    case ScreenSectionType.Form:
                        if (rootScreenSection == null)
                        {
                            rootScreenSection = section;
                            var formSection = new CreateFormSectionPartialTemplate(Screen, section);
                            imports.AddRange(formSection.Imports);
                            constants.AddRange(formSection.Constants);
                            if (!string.IsNullOrEmpty(formSection.Content))
                            {
                                screenSections.Add(formSection.Content);
                            }
                        }
                        else
                        {
                            var formSection = new CreateFormSectionPartialTemplate(Screen, section);
                            constants.AddRange(formSection.Constants);
                            if (formSection.Blank)
                            {
                                continue;
                            }
                            if (section.VisibilityExpression == null)
                            {
                                screenSections.Add($@"<{section.InternalName} {{...props}} />");
                            }
                            else
                            {
                                var expressionHelper = new Helpers.ExpressionHelper(Screen);
                                var expression = expressionHelper.GetExpression(section.VisibilityExpression, "formData");
                                screenSections.Add($@"<FormDataConsumer>
    {{({{ formData, ...rest }}) => 
        {expression} &&
            <{section.InternalName} {{...props}} {{...rest}} />
    }}
</FormDataConsumer>");
                                imports.Add("FormDataConsumer");
                            }
                            componentImports.Add($@"import {section.InternalName} from './{section.InternalName}';");
                        }
                        break;
                }
            }

            if (imports.Any())
            {
                componentImports.Add($@"import {{ {string.Join(", ", imports.Distinct().OrderBy(a => a))} }} from 'react-admin';");
            }

            return $@"import React from 'react';
{string.Join(Environment.NewLine, componentImports)}

{string.Join(Environment.NewLine, constants)}

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