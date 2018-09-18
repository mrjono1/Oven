using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;
using System;
using System.Linq;
using Humanizer;

namespace Oven.Templates.React.Src.Resources
{
    /// <summary>
    /// xEdit.jsx Template
    /// </summary>
    public class EditTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public EditTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => $"{Screen.Entity.InternalName}Edit.jsx";

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath() => new string[] { "src", "resources", Screen.Entity.InternalNamePlural.Camelize() };

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var imports = new List<string> { "Edit", "SimpleForm" };

            var screenSections = new List<string>();
            var componentImports = new List<string>();

            foreach (var section in Screen.ScreenSections)
            {
                switch (section.ScreenSectionType)
                {
                    case ScreenSectionType.Form:
                        var formSection = new CreateFormSectionPartialTemplate(Screen, section, false);
                        imports.AddRange(formSection.Imports);
                        if (!string.IsNullOrEmpty(formSection.Content))
                        {
                            screenSections.Add(formSection.Content);
                        }
                        break;
                    case ScreenSectionType.Search:
                        screenSections.Add($@"<{section.InternalName} {{...props}} />");
                        componentImports.Add($@"import {section.InternalName} from './{section.InternalName}';");
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

const DynamicTitle = ({{ record }}) => {{
    return <span>{Screen.Title} {{record ? ` - ${{record.title}}` : ''}}</span>;
}};

const {Screen.Entity.InternalName}Edit = (props) => (
    <Edit {{...props}} title={{< DynamicTitle />}}>
        <SimpleForm>
{string.Join(Environment.NewLine, screenSections).IndentLines(12)}
        </SimpleForm>
    </Edit>
);

export default {Screen.Entity.InternalName}Edit;";
        }
    }
}