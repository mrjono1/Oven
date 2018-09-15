using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;
using System;
using System.Linq;
using Humanizer;

namespace MasterBuilder.Templates.React.Src.Resources
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
            var formFields = (from screenSection in Screen.ScreenSections
                              where screenSection.ScreenSectionType == ScreenSectionType.Form &&
                              screenSection.ParentScreenSectionId == null
                              select screenSection).First().FormSection.FormFields;

            var fields = new List<string>();
            var imports = new List<string> { "Create", "SimpleForm" };
            foreach (var field in formFields)
            {
                if (field.PropertyType == PropertyType.PrimaryKey)
                {
                    continue;
                }
                var template = new CreateEditInputPartialTemplate(field, true);
                fields.Add(template.Content());
                imports.AddRange(template.ReactAdminImports());
            }

            return $@"import React from 'react';
import {{ {string.Join(", ", imports.Distinct().OrderBy(a => a))} }} from 'react-admin';

const {Screen.Entity.InternalName}Create = (props) => (
    <Create {{...props}} title=""Create {Screen.Title}"">
        <SimpleForm>
{string.Join(Environment.NewLine, fields).IndentLines(12)}
        </SimpleForm>
    </Create>
);

export default {Screen.Entity.InternalName}Create;";
        }
    }
}