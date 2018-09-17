using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;
using System;
using System.Linq;
using Humanizer;

namespace MasterBuilder.Templates.React.Src.Resources
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
            var formFields = (from screenSection in Screen.ScreenSections
                                 where screenSection.ScreenSectionType == ScreenSectionType.Form &&
                                 screenSection.ParentScreenSectionId == null
                                 select screenSection).First().FormSection.FormFields;

            var fields = new List<string>();
            var imports = new List<string> { "Edit", "SimpleForm" };
            foreach (var field in formFields)
            {
                if (field.PropertyType == PropertyType.PrimaryKey)
                {
                    // dont render primary key
                    continue;
                }
                else if(field.PropertyType == PropertyType.ParentRelationshipOneToMany)
                {
                    // dont render parent relationship
                    continue;
                }
                var template = new CreateEditInputPartialTemplate(Screen, field, false);
                fields.Add(template.Content());
                imports.AddRange(template.ReactAdminImports());
                if (template.WrapInFormDataConsumer)
                {
                    imports.Add("FormDataConsumer");
                }
            }

            var searchSectionFields = new List<string>();
            var componentImports = new List<string>();
            var searchSections = (from screenSection in Screen.ScreenSections
                              where screenSection.ScreenSectionType == ScreenSectionType.Search
                              select screenSection);
            foreach (var searchSection in searchSections)
            {
                searchSectionFields.Add($@"<{searchSection.InternalName} {{...props}} />");
                componentImports.Add($@"import {searchSection.InternalName} from './{searchSection.InternalName}';");
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
{string.Join(Environment.NewLine, fields).IndentLines(12)}
{string.Join(Environment.NewLine, searchSectionFields).IndentLines(12)}
        </SimpleForm>
    </Edit>
);

export default {Screen.Entity.InternalName}Edit;";
        }
    }
}