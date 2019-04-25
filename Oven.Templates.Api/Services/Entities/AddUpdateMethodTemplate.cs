using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Oven.Helpers;
using Oven.Request;

namespace Oven.Templates.Api.Services
{
    /// <summary>
    /// Contoller Edit Method Template
    /// </summary>
    public class AddUpdateMethodTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly IEnumerable<ScreenSection> ScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public AddUpdateMethodTemplate(Project project, Screen screen, IEnumerable<ScreenSection> screenSections)
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

        #region PUT (Update)
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
        public virtual async Task<ObjectId> UpdateAsync(ObjectId id, {Screen.InternalName}Request put)
        {{
            if (put == null)
            {{
                throw new ArgumentNullException(); 
            }}
            /*
            var existingRecord = await _context.{Screen.Entity.InternalNamePlural}
                .SingleOrDefaultAsync(record => record.Id == id);

            if (existingRecord == null){{
                throw new ArgumentNullException();
            }}

{string.Join(Environment.NewLine, properties)}

            var result = await collection.UpdateOneAsync(filter, update);*/

            return id;
        }}";
        }
        #endregion

        #region Add
        /// <summary>
        /// POST Verb Method, for adding new records
        /// </summary>
        internal string PostMethod()
        {
            // TODO: Phase 2 get screen section properties that are appropriate using required expression
            var effes = RequestTransforms.GetScreenSectionEntityFields(Screen);

            var properties = new List<string>();
            foreach (var effe in effes)
            {
                if (effe.Entity.Id == Screen.EntityId)
                {
                    properties.AddRange(Property(effe, effes, "post", "newRecord", 0, true));
                }
            }

            var newRecord = $"            var newRecord = new {Screen.Entity.InternalName}();";

            if (!string.IsNullOrWhiteSpace(Screen.DefaultObjectJsonData))
            {
                // TODO: add try catch and logging around JsonConvert
                var content = Newtonsoft.Json.JsonConvert.SerializeObject(Screen.DefaultObjectJsonData);
                newRecord = $@"            var content  = {content};
            var newRecord = Newtonsoft.Json.JsonConvert.DeserializeObject<{Screen.Entity.InternalName}>(content);";
            }

            return $@"
        /// <summary>
        /// {Screen.Title} Add
        /// </summary>
        public virtual async Task<ObjectId> CreateAsync({Screen.InternalName}Request post)
        {{
            if (post == null)
            {{
                throw new ArgumentNullException(); 
            }}
{newRecord}

{string.Join(Environment.NewLine, properties)}

            if (post.Id == null)
            {{
                post.Id = ObjectId.GenerateNewId();
            }}
            await _context.{Screen.Entity.InternalNamePlural}.InsertOneAsync(newRecord);

            return post.Id.Value;
        }}";
        }
        #endregion

    }
}
