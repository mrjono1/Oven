using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Oven.Helpers;
using Oven.Request;

namespace Oven.Templates.DataAccessLayer.Services
{
    /// <summary>
    /// Contoller Edit Method Template
    /// </summary>
    public class UpdateMethodTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly IEnumerable<ScreenSection> ScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public UpdateMethodTemplate(Project project, Screen screen, IEnumerable<ScreenSection> screenSections)
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

                    properties.Add($@"                            {new string(' ', 4 * level)}{childEntityFormFieldEntity.InternalName} = {childObjectName} == null || !{childObjectName}.{parentPropertyInternalName}Id.HasValue ? null : new {childEntityFormFieldEntity.InternalName}Response{{
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
        
        private IEnumerable<string> Property(ScreenSectionEntityFormFields entityFormFieldEntity, IEnumerable<ScreenSectionEntityFormFields> effes, string requestObjectName = "request", string existingObjectName = "", int level = 0, bool add = false)
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
                    case PropertyType.ParentRelationshipOneToMany:
                    case PropertyType.ReferenceRelationship:
                        properties.Add($"            {new string(' ', 4 * level)}updates.Add(update.Set(p => p.{existingObjectName}{formField.InternalNameCSharp}, {requestObjectName}.{formField.InternalNameCSharp}));");
                        break;
                    default:
                        properties.Add($"            {new string(' ', 4 * level)}updates.Add(update.Set(p => p.{existingObjectName}{formField.InternalNameCSharp}, {requestObjectName}.{formField.InternalNameCSharp}));");
                        break;
                }
            }

            if (entityFormFieldEntity.ChildEntities != null)
            {
                foreach (var childEntityFormFieldEntity in entityFormFieldEntity.ChildEntities)
                {
                    var childProperties = new List<string>();
                    var childObjectName = $"{requestObjectName}.{childEntityFormFieldEntity.InternalName}";
                    var childExistingObjectName = $"{(string.IsNullOrEmpty(existingObjectName) ? "" : $"{existingObjectName}.")}{childEntityFormFieldEntity.InternalName}.";
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
                update.Set(p => p{existingObjectName}.{childEntityFormFieldEntity.InternalName}, new {childEntityFormFieldEntity.InternalName}());
{string.Join(Environment.NewLine, childProperties)}
            }}");
                    }
                    else
                    {
                        properties.Add($@"            if ({childObjectName} == null)
            {{
                updates.Add(update.Set(p => p{existingObjectName}.{childEntityFormFieldEntity.InternalName}, null));
            }}
            else
            {{
                //if ({requestObjectName}{existingObjectName}.{childEntityFormFieldEntity.InternalName} == null || !{requestObjectName}{existingObjectName}.{childEntityFormFieldEntity.InternalName}.{parentPropertyInternalName}Id.HasValue)
                //{{
                    updates.Add(update.Set(p => p{existingObjectName}.{childEntityFormFieldEntity.InternalName}, new {childEntityFormFieldEntity.InternalName}()));
                //}}
{string.Join(Environment.NewLine, childProperties)}
            }}");
                    }
                }
            }
            return properties;
        }

        /// <summary>
        /// PUT Verb Method, for updating records
        /// </summary>
        internal string PutMethod()
        {
            // TODO: Phase 2 get screen section properties that are appropriate using required expression
            var effes = RequestTransforms.GetScreenSectionEntityFields(Screen);

            var properties = new List<string>();
            foreach (var effe in effes)
            {
                if (effe.Entity.Id == Screen.EntityId)
                {
                    properties.AddRange(Property(effe, effes));
                }
            }

            return $@"
        /// <summary>
        /// {Screen.Title} Update
        /// </summary>
        public virtual async Task<ObjectId> UpdateAsync(ObjectId id, {Screen.InternalName}Request request)
        {{
            return await UpsertAsync(id, request, false);
        }}

        public virtual async Task<ObjectId> UpsertAsync(ObjectId id, {Screen.InternalName}Request request, bool upsert = false)
        {{
            if (id == ObjectId.Empty)
            {{
                throw new ArgumentException(""Invalid ObjectId"", ""id"");
            }}
            
            var filter = Builders<{Screen.Entity.InternalName}>.Filter.Eq(s => s.Id, id);
            var update = Builders<{Screen.Entity.InternalName}>.Update;
            var updates = new List<UpdateDefinition<{Screen.Entity.InternalName}>>{{ }};
            
{string.Join(Environment.NewLine, properties)}
                
            var result = await _context.{Screen.Entity.InternalNamePlural}.UpdateOneAsync(filter, update.Combine(updates), new UpdateOptions {{ IsUpsert = upsert }});
            if (result.IsAcknowledged)
            {{
                return id;
            }}
            return ObjectId.Empty;
        }}";
        }
    }
}
