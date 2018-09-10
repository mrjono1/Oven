using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Api.Controllers
{
    /// <summary>
    /// Contoller Edit Method Template
    /// </summary>
    public class ControllerFormSectionMethodsPartial
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly IEnumerable<ScreenSection> ScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public ControllerFormSectionMethodsPartial(Project project, Screen screen, IEnumerable<ScreenSection> screenSections)
        {
            Project = project;
            Screen = screen;
            ScreenSections = screenSections;
        }

        #region GET
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
            
            return $@"
        /// <summary>
        /// {Screen.Title} Get
        /// </summary>
        [HttpGet(""{{id}}"")]
        [ProducesResponseType(typeof({Screen.FormResponseClass}), 200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> GetAsync(Guid id)
        {{
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (id == Guid.Empty)
            {{
                return NotFound();
            }}
            
            var result = await _context.{Screen.Entity.InternalNamePlural}
                        .AsNoTracking()
                        .Select(item => new Models.{Screen.FormResponseClass}
                        {{
{string.Join(string.Concat(",", Environment.NewLine), propertyMapping)}
                        }})
                        .SingleOrDefaultAsync(p => p.Id == id);

            if (result == null)
            {{
                return NotFound();
            }}

            return Ok(result);
        }}";
        }
        #endregion


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

            // TODO: Includes are not recursive yet
            var includes = new List<string>();
            foreach (var effe in effes)
            {
                if (effe.Entity.Id == Screen.EntityId)
                {
                    properties.AddRange(Property(effe, effes));
                }
                else
                {
                    includes.Add($@"                .Include(p => p.{effe.Entity.InternalName})");
                }
            }
            
            return $@"
        /// <summary>
        /// {Screen.Title} Update
        /// </summary>
        [HttpPut(""{{id}}"")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id, [FromBody]{Screen.InternalName}Request put)
        {{
            if (put == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}
            
            var existingRecord = await _context.{Screen.Entity.InternalNamePlural}{(includes.Any() ? string.Concat(Environment.NewLine, string.Join(Environment.NewLine, includes)) : string.Empty)}
                .SingleOrDefaultAsync(record => record.Id == id);

            if (existingRecord == null){{
                return BadRequest();
            }}

{string.Join(Environment.NewLine, properties)}

            await _context.SaveChangesAsync();

            return Ok(id);
        }}";
        }
        #endregion

        #region POST (Add)
        /// <summary>
        /// POST Verb Method, for adding new records
        /// </summary>
        internal string PostMethod()
        {
            // TODO: Phase 2 get screen section properties that are appropriate using required expression
            var effes = RequestTransforms.GetScreenSectionEntityFields(Screen);

            var properties = new List<string>();
            var propertyMapping = new List<string>();
            foreach (var effe in effes)
            {
                if (effe.Entity.Id == Screen.EntityId)
                {
                    properties.AddRange(Property(effe, effes, "post", "newRecord", 0, true));
                    propertyMapping.AddRange(GetPropertiesRecursive(effe, effes));
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
        [HttpPost]
        [ProducesResponseType(typeof({Screen.FormResponseClass}), 200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> CreateAsync([FromBody]{Screen.InternalName}Request post)
        {{
            if (post == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}
{newRecord}

{string.Join(Environment.NewLine, properties)}

            _context.{Screen.Entity.InternalNamePlural}.Add(newRecord);
            await _context.SaveChangesAsync();

            var result = await _context.{Screen.Entity.InternalNamePlural}
                        .AsNoTracking()
                        .Select(item => new Models.{Screen.FormResponseClass}
                        {{
{string.Join(string.Concat(",", Environment.NewLine), propertyMapping)}
                        }})
                        .SingleOrDefaultAsync(p => p.Id == newRecord.Id);

            if (result == null)
            {{
                return NotFound();
            }}

            return Ok(result);
        }}";
        }
        #endregion

        #region Delete
        /// <summary>
        /// DELETE Verb method
        /// </summary>
        internal string DeleteMethod()
        {
            return $@"
        /// <summary>
        /// {Screen.Title} Delete
        /// </summary>
        [HttpDelete(""{{id}}"")]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {{
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (id == Guid.Empty)
            {{
                return NotFound();
            }}
            
            _context.{Screen.Entity.InternalNamePlural}.Remove(new {Screen.Entity.InternalName}() {{ Id = id }});
            await _context.SaveChangesAsync();

            return Ok();
        }}";
        }
#endregion

        #region PATCH (Update not in use)
        /// <summary>
        /// PATCH Verb method, for updating records
        /// </summary>
        internal string PatchMethod()
        {
            var patchEntityOperations = new List<string>();
            //foreach (var formField in ScreenSection.FormSection.FormFields)
            //{
            //    switch (formField.PropertyType)
            //    {
            //        case PropertyType.ParentRelationship:
            //            patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
            //            if (operation.value != null && !string.IsNullOrWhiteSpace(operation.value.ToString()) && Guid.TryParse(operation.value.ToString(), out Guid {formField.InternalNameCSharp.Camelize()}))
            //            {{
            //                entity.{formField.InternalNameCSharp} = {formField.InternalNameCSharp.Camelize()};
            //            }}
            //            else
            //            {{
            //                throw new Exception(""{formField.InternalNameCSharp.Camelize()} value is invalid"");
            //            }}
            //            entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
            //            break;");
            //            break;

            //        case PropertyType.ReferenceRelationship:
            //            patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
            //            if (operation.value != null && !string.IsNullOrWhiteSpace(operation.value.ToString()) && Guid.TryParse(operation.value.ToString(), out Guid {formField.InternalNameCSharp.Camelize()}))
            //            {{
            //                entity.{formField.InternalNameCSharp} = {formField.InternalNameCSharp.Camelize()};
            //            }}
            //            else
            //            {{
            //                {(formField.Property.Required ? $@"throw new Exception(""{formField.InternalNameCSharp.Camelize()} value is invalid"");" : $"entity.{formField.InternalNameCSharp} = null;")}
            //            }}
            //            entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
            //            break;");
            //            break;

            //        case PropertyType.PrimaryKey:
            //            break;
            //        case PropertyType.String:
            //            patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
            //            entity.{formField.InternalNameCSharp} = operation.value.ToString();
            //            entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
            //            break;");
            //            break;

            //        case PropertyType.Integer:
            //            patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
            //            int int32Value{formField.InternalNameCSharp};
            //            if (operation.value != null && Int32.TryParse(operation.value.ToString(), out int32Value{formField.InternalNameCSharp}))
            //            {{
            //                entity.{formField.InternalNameCSharp} = int32Value{formField.InternalNameCSharp};
            //                entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
            //            }}
            //            {(formField.Property.Required ? string.Empty : $@"else if (operation.value == null || string.IsNullOrWhiteSpace(operation.value.ToString()))
            //            {{
            //                entity.{formField.InternalNameCSharp} = null;
            //                entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
            //            }}")}
            //            else
            //            {{
            //                throw new Exception(""Property: {formField.InternalNameCSharp}, Value:"" + operation.value + "" is not a valid integer value"");
            //            }}
            //            break;");
            //            break;
            //        case PropertyType.Double:
            //            patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
            //            double doubleValue{formField.InternalNameCSharp};
            //            if (operation.value != null && Double.TryParse(operation.value.ToString(), out doubleValue{formField.InternalNameCSharp}))
            //            {{
            //                entity.{formField.InternalNameCSharp} = doubleValue{formField.InternalNameCSharp};
            //                entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
            //            }}
            //            {(formField.Property.Required ? string.Empty : $@"else if (operation.value == null || string.IsNullOrWhiteSpace(operation.value.ToString()))
            //            {{
            //                entity.{formField.InternalNameCSharp} = null;
            //                entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
            //            }}")}
            //            else
            //            {{
            //                throw new Exception(""Property: {formField.InternalNameCSharp}, Value:"" + operation.value + "" is not a valid double value"");
            //            }}
            //            break;");
            //            break;
            //        case PropertyType.DateTime:
            //            patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
            //            DateTime dateTimeValue{formField.InternalNameCSharp};
            //            if (operation.value != null && DateTime.TryParse(operation.value.ToString(), out dateTimeValue{formField.InternalNameCSharp}))
            //            {{
            //                entity.{formField.InternalNameCSharp} = dateTimeValue{formField.InternalNameCSharp};
            //                entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
            //            }}
            //            {(formField.Property.Required ? string.Empty : $@"else if (operation.value == null || string.IsNullOrWhiteSpace(operation.value.ToString()))
            //            {{
            //                entity.{formField.InternalNameCSharp} = null;
            //                entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
            //            }}")}
            //            else
            //            {{
            //                throw new Exception(""Property: {formField.InternalNameCSharp}, Value:"" + operation.value + "" is not a valid boolean value"");
            //            }}
            //            break;");
            //            break;
            //        case PropertyType.Boolean:
            //            patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
            //            bool booleanValue{formField.InternalNameCSharp};
            //            if (operation.value != null && Boolean.TryParse(operation.value.ToString(), out booleanValue{formField.InternalNameCSharp}))
            //            {{
            //                entity.{formField.InternalNameCSharp} = booleanValue{formField.InternalNameCSharp};
            //                entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
            //            }}
            //            else
            //            {{
            //                throw new Exception(""Property: {formField.InternalNameCSharp}, Value:"" + operation.value + "" is not a valid boolean value"");
            //            }}
            //            break;");
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //http://benfoster.io/blog/aspnet-core-json-patch-partial-api-updates
            return $@"
        /// <summary>
        /// {Screen.Title} Update
        /// </summary>
        [HttpPatch(""{Screen.InternalName}/{{id}}"")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> {Screen.InternalName}([FromRoute]Guid id, [FromBody]JsonPatchDocument<{Screen.InternalName}Request> patch)
        {{
            if (patch == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (!patch.Operations.Any())
            {{
                return Ok();
            }}

            var entity = new DataAccessLayer.Entities.{Screen.Entity.InternalName}() {{ Id = id }};
            var entityEntry = _context.{Screen.Entity.InternalNamePlural}.Attach(entity);
            
            // do stuff
            foreach(var operation in patch.Operations)
            {{
                switch (operation.path)
                {{
{string.Join(Environment.NewLine, patchEntityOperations)}
                }}
            }}

            await _context.SaveChangesAsync();
            
            return Ok();
        }}";
        }
        #endregion
    }
}
