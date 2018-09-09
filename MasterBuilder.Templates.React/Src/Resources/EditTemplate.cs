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
            foreach (var field in formFields)
            {
                if (field.PropertyType == PropertyType.PrimaryKey)
                {
                    continue;
                }
                fields.Add($@"<TextInput source=""{field.InternalNameJavaScript}"" />");
            }
            
            return $@"import React from 'react';
import {{ Edit, SimpleForm, TextInput }} from 'react-admin';

const {Screen.Entity.InternalName}Edit = (props) => (
    <Edit {{...props}}>
        <SimpleForm>
{string.Join(Environment.NewLine, fields).IndentLines(12)}
        </SimpleForm>
    </Edit>
);

export default {Screen.Entity.InternalName}Edit;";
        }
    }
}