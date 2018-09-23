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
            //Entity parentEntity = null;

            //var parentProperty = (from p in ScreenSection.SearchSection.Entity.Properties
            //                      where p.PropertyType == PropertyType.ParentRelationshipOneToMany
            //                      select p).SingleOrDefault();
            //if (parentProperty != null)
            //{
            //    parentEntity = (from s in Project.Entities
            //                    where s.Id == parentProperty.ReferenceEntityId
            //                    select s).SingleOrDefault();

            //    parentPropertyId = $"{parentEntity.InternalName.Camelize()}Id";
            //}

            var defaultValues = new List<string>();
            var defaultValuesString = "";

            var parentEntities = GetParentEntites(ScreenSection.Entity);
            if (parentEntities.Any())
            {
                foreach (var pe in parentEntities)
                {
                    if (string.IsNullOrEmpty(parentPropertyId))
                    {
                        parentPropertyId = $"{pe.InternalName.Camelize()}Id";
                    }
                    else
                    {
                        defaultValues.Add($"{pe.InternalName.Camelize()}Id: props.record.{pe.InternalName.Camelize()}Id");
                    }
                }
                if (defaultValues.Any())
                {
                    defaultValuesString = $" defaultValues={{{{{string.Join(", ", defaultValues)}}}}}";
                }
            }

            return $@"import React from 'react';
import {{ ReferenceManyField, Datagrid, TextField, EditButton }} from 'react-admin';
import CreateButton from './../../components/CreateButton';

const {ScreenSection.InternalName} = (props) => (
    <div>
        <CreateButton record={{props.record}} reference=""{ScreenSection.Entity.InternalNamePlural}"" target=""{parentPropertyId}"" title=""Create {ScreenSection.Entity.Title}""{defaultValuesString}/>
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

        private IEnumerable<Entity> GetParentEntites(Entity entity)
        {
            var parentEnities = new List<Entity>();
            Entity parentEntity = null;

            var parentProperty = (from p in entity.Properties
                                  where p.PropertyType == PropertyType.ParentRelationshipOneToMany
                                  select p).SingleOrDefault();
            if (parentProperty != null)
            {
                parentEntity = (from s in Project.Entities
                                where s.Id == parentProperty.ReferenceEntityId
                                select s).SingleOrDefault();

                if (parentEntity != null)
                {
                    parentEnities.Add(parentEntity);
                    parentEnities.AddRange(GetParentEntites(parentEntity));
                }
            }
            return parentEnities;
        }
    }
}