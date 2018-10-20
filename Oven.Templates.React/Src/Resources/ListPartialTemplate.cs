using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;
using System;
using System.Linq;
using Humanizer;
using Oven.Shared.Extensions;

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
            var defaultValues = new List<string>();
            var defaultValuesString = "";

            // Get parent filter properties
            var filterEntityProperties = new Dictionary<Entity, List<Property>>();
            foreach (var property in ScreenSection.Entity.Properties)
            {
                if (property.PropertyType == PropertyType.ReferenceRelationship &&
                    property.FilterExpression != null &&
                    property.FilterExpression.PropertyId.HasValue &&
                    property.FilterExpression.Property.PropertyType != PropertyType.PrimaryKey)
                {
                    var p = ScreenSection.Entity.Properties.SingleOrDefault(a => a.Id == property.FilterExpression.PropertyId.Value);
                    if (p == null)
                    {
                        var pp = (from projectEntity in Project.Entities
                                  from entityProperty in projectEntity.Properties
                                  where entityProperty.Id == property.FilterExpression.PropertyId.Value
                                  select new
                                  {
                                      Entity = projectEntity,
                                      Property = entityProperty
                                  }).SingleOrDefault();
                        if (pp != null)
                        {
                            if (filterEntityProperties.ContainsKey(pp.Entity))
                            {
                                filterEntityProperties[pp.Entity].Add(pp.Property);
                            }
                            else
                            {
                                filterEntityProperties.Add(pp.Entity, new List<Property> { pp.Property });
                            }
                        }
                    }
                }
            }

            var hasTarget = false;
            var filter = "props.record.id";
            var parentEntities = ScreenSection.Entity.GetParentEntites(Project);
            if (parentEntities.Any())
            {
                foreach (var pe in parentEntities)
                {
                    if (string.IsNullOrEmpty(parentPropertyId))
                    {
                        if (ScreenSection.ParentScreenSection != null && ScreenSection.EntityId != Screen.EntityId)
                        {
                            var entities = new List<string> { "record" };
                            foreach (var item in parentEntities)
                            {
                                if (item.Id == pe.Id)
                                {
                                    break;
                                }
                                else
                                {
                                    entities.Add(item.InternalName.Camelize());
                                }
                            }
                            filter = $@"props.{string.Join(".", entities)}.{pe.InternalName.Camelize()}.id";
                            defaultValues.Add($"{pe.InternalName.Camelize()}Id: {filter}");
                        }
                        else
                        {
                            hasTarget = true;
                        }
                        parentPropertyId = $"{pe.InternalName.Camelize()}Id";
                    }
                    else
                    {
                        defaultValues.Add($"{pe.InternalName.Camelize()}Id: props.record.{pe.InternalName.Camelize()}Id");
                    }

                    
                    if (filterEntityProperties.TryGetValue(pe, out List<Property> properties))
                    {
                        foreach (var prop in properties)
                        {
                            defaultValues.Add($"{prop.InternalNameJavaScript}: props.record.{prop.InternalNameJavaScript}");
                        }
                    }
                }
            }

            if (defaultValues.Any())
            {
                defaultValuesString = $" defaultValues={{{{{string.Join(", ", defaultValues.Distinct())}}}}}";
            }

            return $@"import React from 'react';
import {{ ReferenceManyField, Datagrid, TextField, EditButton }} from 'react-admin';
import CreateButton from './../../components/CreateButton';

const {ScreenSection.InternalName} = (props) => (
    <div>
        <CreateButton record={{!props.record ? {{}} : props.record}} reference=""{ScreenSection.Entity.InternalNamePlural}"" {(hasTarget ? $@"target=""{parentPropertyId}"" " : "")}title=""Create {ScreenSection.Entity.Title}""{defaultValuesString}/>
        <ReferenceManyField
            {{...props}}
            label=""{ScreenSection.Title}""
            reference=""{ScreenSection.Entity.InternalNamePlural}""
            target=""{parentPropertyId}""
            filter={{{{{parentPropertyId}: {filter}}}}}>

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