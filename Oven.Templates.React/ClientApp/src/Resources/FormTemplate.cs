using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;
using System;
using System.Linq;
using Humanizer;

namespace Oven.Templates.React.ClientApp.Src.Resources
{
    /// <summary>
    /// xEdit.jsx Template
    /// </summary>
    public class FormTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => $"{Screen.Entity.InternalName}Form.jsx";

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath() => new string[] { "ClientApp", "src", "resources", Screen.Entity.InternalNamePlural.Camelize() };

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var imports = new List<string> { "Edit", "SimpleForm", "Create" };

            var screenSections = new List<string>();
            var componentImports = new List<string>();
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
                            if (!formSection.Blank)
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
                    case ScreenSectionType.Search:
                        {
                            var expression = "formData.id";
                            if (section.VisibilityExpression != null)
                            {
                                var expressionHelper = new Helpers.ExpressionHelper(Screen);
                                expression = string.Concat(expression, " && ", expressionHelper.GetExpression(section.VisibilityExpression, "formData"));
                            }

                            screenSections.Add($@"<FormDataConsumer>
    {{({{ formData, ...rest }}) => 
        {expression} &&
            <{section.InternalName} {{...props}} {{...rest}} />
    }}
</FormDataConsumer>");
                            imports.Add("FormDataConsumer");

                            componentImports.Add($@"import {section.InternalName} from './{section.InternalName}';");
                        }
                        break;
                    case ScreenSectionType.MenuList:
                        break;
                    case ScreenSectionType.Html:
                        break;
                    default:
                        break;
                }
            }

            return $@"import React from 'react';
import {{ {string.Join(", ", imports.Distinct().OrderBy(a => a))} }} from 'react-admin';
{string.Join(Environment.NewLine, componentImports)}

{string.Join(Environment.NewLine, constants)}
const DynamicTitle = ({{ record }}) => {{
    return <span>{Screen.Title} {{record ? ` - ${{record.title}}` : ''}}</span>;
}};

const {Screen.Entity.InternalName}Edit = (props) => (
    <Edit undoable={{ false }} {{...props}} title={{< DynamicTitle />}}>
        <Form {{...props}} />
    </Edit>
);

const {Screen.Entity.InternalName}Create = (props) => (
    <Create {{...props}} title=""Create {Screen.Title}"">
        <Form {{...props}} />
    </Create>
);

const Form = (props) => (
    <SimpleForm {{...props}} redirect=""edit"">
{string.Join(Environment.NewLine, screenSections).IndentLines(8)}
    </SimpleForm>
);

export {{ {Screen.Entity.InternalName}Edit, {Screen.Entity.InternalName}Create }};";
        }
    }
}