using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;
using System;
using System.Linq;
using MasterBuilder.Templates.React.Src.Containers.Sections;
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
        public string GetFileName() => $"{Screen.Entity.InternalName}Create.js";

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
            foreach (var field in formFields)
            {
                fields.Add($@"<TextInput source=""{field.InternalNameJavaScript}""/>");
            }

            return $@"import React from 'react';
import {{ Create, SimpleForm, TextInput }} from 'react-admin';

export const {Screen.Entity.InternalName}Create = (props) => (
    <Create {{...props}}>
        <SimpleForm>
{string.Join(Environment.NewLine, fields).IndentLines(12)}
        </SimpleForm>
    </Create>
);";
        }
    }
}