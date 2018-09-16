using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;
using System;
using System.Linq;
using Humanizer;

namespace MasterBuilder.Templates.React.Src.Resources
{
    /// <summary>
    /// xList.jsx Template
    /// </summary>
    public class ListPartialTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ListPartialTemplate(Project project, Screen screen, ScreenSection screenSection)
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
        public string[] GetFilePath() => new string[] { "src", "resources", Screen.Entity.InternalNamePlural.Camelize() };

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var searchColumns = ScreenSection.SearchSection.SearchColumns;

            var fields = new List<string>();
            foreach (var searchColumn in searchColumns)
            {
                if (searchColumn.PropertyType == PropertyType.PrimaryKey)
                {
                    continue;
                }
                fields.Add($@"<TextField source=""{searchColumn.InternalNameJavascript}"" />");
            }

            string parentPropertyId = null;
            Entity parentEntity = null;

            var parentProperty = (from p in ScreenSection.SearchSection.Entity.Properties
                                  where p.PropertyType == PropertyType.ParentRelationshipOneToMany
                                  select p).SingleOrDefault();
            if (parentProperty != null)
            {
                parentEntity = (from s in Project.Entities
                                where s.Id == parentProperty.ParentEntityId
                                select s).SingleOrDefault();

                parentPropertyId = $"{parentEntity.InternalName.Camelize()}Id";
            }

            return $@"import React from 'react';
import {{ ReferenceManyField, Datagrid, TextField, EditButton }} from 'react-admin';
import CreateButton from './../../components/CreateButton';

const {ScreenSection.InternalName} = (props) => (
    <div>
        <CreateButton record={{props.record}} reference=""{ScreenSection.Entity.InternalNamePlural}"" target=""{parentPropertyId}"" title=""Create {ScreenSection.Entity.Title}""/>
        <ReferenceManyField
            {{...props}}
            label=""{ScreenSection.Title}""
            reference=""{ScreenSection.Entity.InternalNamePlural}""
            target=""{parentPropertyId}""
            filter={{{{{parentPropertyId}: props.id}}}}>

            <Datagrid>
{string.Join(Environment.NewLine, fields).IndentLines(16)}
                <EditButton />
            </Datagrid>
        </ReferenceManyField>
    </div>
);

export default {ScreenSection.InternalName};";
        }
    }
}