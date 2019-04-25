using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Oven.Helpers;
using Oven.Request;
using MongoDB.Bson;

namespace Oven.Templates.Api.Services
{
    /// <summary>
    /// Get Method Template
    /// </summary>
    public class GetMethodTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly IEnumerable<ScreenSection> ScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public GetMethodTemplate(Project project, Screen screen, IEnumerable<ScreenSection> screenSections)
        {
            Project = project;
            Screen = screen;
            ScreenSections = screenSections;
        }

        private IEnumerable<string> GetPropertiesRecursive(ScreenSectionEntityFormFields entityFormFieldEntity, IEnumerable<ScreenSectionEntityFormFields> effes, string objectName = "item", int level = 0)
        {
            var properties = new List<string>();
            foreach (var group in entityFormFieldEntity.FormFields.GroupBy(ff => ff.EntityPropertyId))
            {
                var formField = group.First();
                switch (formField.PropertyType)
                {
                    case PropertyType.ReferenceRelationship:

                        properties.Add($"                            {new string(' ', 4 * level)}{formField.InternalNameCSharp} = {objectName}.{formField.InternalNameCSharp}");

                        // TODO: Title should be configurable
                        // TODO: is it faster to do the bool check on the key instead of object?
                        // TODO: needs to work for all levels
                        if (level == 0)
                        {
                            properties.Add($"                            {new string(' ', 4 * level)}{formField.InternalNameAlternateCSharp} = {objectName}.{formField.Property.InternalName} != null ? {objectName}.{formField.Property.InternalName}.Title : null");
                        }

                        break;
                    case PropertyType.ParentRelationshipOneToOne:
                        // TODO
                        break;
                    default:
                        properties.Add($"                            {new string(' ', 4 * level)}{formField.InternalNameCSharp} = {objectName}.{formField.InternalNameCSharp}");
                        break;
                }
            }

            if (entityFormFieldEntity.ChildEntities != null)
            {
                foreach (var childEntityFormFieldEntity in entityFormFieldEntity.ChildEntities)
                {
                    var childProperties = new List<string>();
                    var childObjectName = $"{objectName}.{childEntityFormFieldEntity.InternalName}";
                    foreach (var effe in effes)
                    {
                        if (effe.Entity.Id == childEntityFormFieldEntity.Id)
                        {
                            childProperties.AddRange(GetPropertiesRecursive(effe, effes, childObjectName, level + 1));
                        }
                    }

                    var parentPropertyInternalName = (from p in childEntityFormFieldEntity.Properties
                                                      where p.PropertyType == PropertyType.ParentRelationshipOneToOne
                                                      select p).Single().InternalName;

                    properties.Add($@"                            {new string(' ', 4 * level)}{childEntityFormFieldEntity.InternalName} = {childObjectName} == null || {childObjectName}.{parentPropertyInternalName}Id != null ? null : new {childEntityFormFieldEntity.InternalName}Response{{
{string.Join(string.Concat(",", Environment.NewLine),  childProperties)}
                            {new string(' ', 4 * level)}}}");
                }
            }
            
            return properties;
        }

        private IEnumerable<string> GetParentProperties(Entity entity, string objectName, bool first = false)
        {
            var propertyMapping = new List<string>();


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
                    if (!first)
                    {
                        propertyMapping.Add($"{parentEntity.InternalName}Id = {objectName}.{parentEntity.InternalName}Id".IndentLines(28));
                    }

                    objectName = $"{objectName}.{parentEntity.InternalName}";
                    propertyMapping.AddRange(GetParentProperties(parentEntity, objectName));
                }
            }

            return propertyMapping;
        }

        /// <summary>
        /// GET Verb method
        /// </summary>
        internal string GetMethod()
        {
            // TODO: Phase 2 get screen section properties that are appropriate using required expression
            var effes = RequestTransforms.GetScreenSectionEntityFields(Screen);

            var propertyMapping = new List<string>();
            foreach (var effe in effes)
            {
                if (effe.Entity.Id == Screen.EntityId)
                {
                    propertyMapping.AddRange(GetPropertiesRecursive(effe, effes));
                }
            }

            propertyMapping.AddRange(GetParentProperties(Screen.Entity, "item", true));

            return $@"
        /// <summary>
        /// {Screen.Title} Get
        /// </summary>
        public virtual async Task<{Screen.FormResponseClass}> GetAsync(ObjectId id)
        {{
            if (id == null)
            {{
                throw new ArgumentNullException(); 
            }}
            
            var result = _context.{Screen.Entity.InternalNamePlural}.AsQueryable()
                        .Select(item => new {Screen.FormResponseClass}
                        {{
{string.Join(string.Concat(",", Environment.NewLine), propertyMapping)}
                        }})
                        .SingleOrDefault(p => p.Id == id);

            return result;
        }}";
        }

        private IEnumerable<string> Property(ScreenSectionEntityFormFields entityFormFieldEntity, IEnumerable<ScreenSectionEntityFormFields> effes, string requestObjectName = "put", string existingObjectName = "existingRecord", int level = 0, bool add = false)
        {
            var properties = new List<string>();

            foreach (var group in entityFormFieldEntity.FormFields.GroupBy(ff => ff.EntityPropertyId))
            {
                var formField = group.First();
                switch (formField.PropertyType)
                {
                    case PropertyType.PrimaryKey:
                    case PropertyType.ParentRelationshipOneToOne:
                        // Ignore
                        break;
                    default:
                        properties.Add($"            {new string(' ', 4 * level)}{existingObjectName}.{formField.InternalNameCSharp} = {requestObjectName}.{formField.InternalNameCSharp};");
                        break;
                }
            }

            if (entityFormFieldEntity.ChildEntities != null)
            {
                foreach (var childEntityFormFieldEntity in entityFormFieldEntity.ChildEntities)
                {
                    var childProperties = new List<string>();
                    var childObjectName = $"{requestObjectName}.{childEntityFormFieldEntity.InternalName}";
                    var childExistingObjectName = $"{existingObjectName}.{childEntityFormFieldEntity.InternalName}";
                    foreach (var effe in effes)
                    {
                        if (effe.Entity.Id == childEntityFormFieldEntity.Id)
                        {
                            childProperties.AddRange(Property(effe, effes, childObjectName, childExistingObjectName, level + 1));
                        }
                    }

                    var parentPropertyInternalName = (from p in childEntityFormFieldEntity.Properties
                                                      where p.PropertyType == PropertyType.ParentRelationshipOneToOne
                                                      select p).Single().InternalName;
                    if (add)
                    {
                        properties.Add($@"            if ({childObjectName} != null)
            {{
                {existingObjectName}.{childEntityFormFieldEntity.InternalName} = new {childEntityFormFieldEntity.InternalName}();
{string.Join(Environment.NewLine, childProperties)}
            }}");
                    }
                    else
                    {
                        properties.Add($@"            if ({childObjectName} == null)
            {{
                {existingObjectName}.{childEntityFormFieldEntity.InternalName} = null;
            }}
            else
            {{
                if ({existingObjectName}.{childEntityFormFieldEntity.InternalName} == null || !{existingObjectName}.{childEntityFormFieldEntity.InternalName}.{parentPropertyInternalName}Id.HasValue)
                {{
                    {existingObjectName}.{childEntityFormFieldEntity.InternalName} = new {childEntityFormFieldEntity.InternalName}();
                }}
{string.Join(Environment.NewLine, childProperties)}
            }}");
                    }
                }
            }
            return properties;
        }
    }
}
