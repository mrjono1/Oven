using System;
using System.Collections.Generic;
using Humanizer;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Controllers
{
    /// <summary>
    /// Contoller Edit Method Template
    /// </summary>
    public class ControllerFormSectionMethodsPartial
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ControllerFormSectionMethodsPartial(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        /// <summary>
        /// GET Verb method
        /// </summary>
        internal string GetMethod()
        {
            var getPropertyMapping = new List<string>();
            foreach (var formField in ScreenSection.FormSection.FormFields)
            {
                switch (formField.PropertyType)
                {
                    case PropertyType.ParentRelationship:
                        getPropertyMapping.Add($"                           {formField.InternalNameCSharp} = item.{formField.InternalNameCSharp}");
                        break;

                    case PropertyType.ReferenceRelationship:

                        getPropertyMapping.Add($"                           {formField.InternalNameCSharp} = item.{formField.InternalNameCSharp}");

                        // TODO: Title should be configurable
                        // TODO: is it faster to do the bool check on the key instead of object?
                        getPropertyMapping.Add($"                           {formField.InternalNameAlternateCSharp} = item.{formField.Property.InternalName} != null ? item.{formField.Property.InternalName}.Title : null");
                        break;
                    case PropertyType.OneToOneRelationship:
                        // TODO
                        break;
                    default:
                        getPropertyMapping.Add($"                           {formField.InternalNameCSharp} = item.{formField.InternalNameCSharp}");
                        break;
                }
            }

            return $@"
        /// <summary>
        /// {ScreenSection.Title} Get
        /// </summary>
        [HttpGet(""{Screen.InternalName}/{{id}}"")]
        [ProducesResponseType(typeof(Models.{Screen.InternalName}Response), 200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> {Screen.InternalName}(Guid id)
        {{
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (id == Guid.Empty)
            {{
                return NotFound();
            }}
            
            var result = await _context.{ScreenSection.FormSection.Entity.InternalNamePlural}
                            .AsNoTracking()
                            .Select(item => new Models.{Screen.InternalName}Response
                            {{
{string.Join(string.Concat(",", Environment.NewLine), getPropertyMapping)}
                            }})
                            .SingleOrDefaultAsync(p => p.Id == id);
            if (result == null)
            {{
                return NotFound();
            }}

            return Ok(result);
        }}";
        }

        /// <summary>
        /// PUT Verb Method, for updating records
        /// </summary>
        internal string PutMethod()
        {
            var propertyMapping = new List<string>();
            foreach (var formField in ScreenSection.FormSection.FormFields)
            {
                switch (formField.PropertyType)
                {
                    case PropertyType.PrimaryKey:
                        // Ignore
                        break;
                    case PropertyType.OneToOneRelationship:
                        // TODO
                        break;
                    default:
                        propertyMapping.Add($"                {formField.InternalNameCSharp} = put.{formField.InternalNameCSharp}");
                        break;
                }
            }

            return $@"
        /// <summary>
        /// {ScreenSection.Title} Update
        /// </summary>
        [HttpPut(""{Screen.InternalName}/{{id}}"")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> {Screen.InternalName}Put([FromRoute]Guid id, [FromBody]{Screen.InternalName}Request put)
        {{
            if (put == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}
            
            var newRecord = new {ScreenSection.FormSection.Entity.InternalName}{{
{string.Join(string.Concat(",", Environment.NewLine), propertyMapping)}
            }};            

            _context.{ScreenSection.FormSection.Entity.InternalNamePlural}.Add(newRecord);
            await _context.SaveChangesAsync();

            return Ok(newRecord.Id);
        }}";
        }

        /// <summary>
        /// POST Verb Method, for adding new records
        /// </summary>
        internal string PostMethod()
        {
            var postPropertyMapping = new List<string>();
            foreach (var formField in ScreenSection.FormSection.FormFields)
            {
                switch (formField.PropertyType)
                {
                    case PropertyType.PrimaryKey:
                        // Ignore
                        break;
                    case PropertyType.OneToOneRelationship:
                        // TODO
                        break;
                    default:
                        postPropertyMapping.Add($"                {formField.InternalNameCSharp} = post.{formField.InternalNameCSharp}");
                        break;
                }
            }

            return $@"
        /// <summary>
        /// {ScreenSection.Title} Add
        /// </summary>
        [HttpPost(""{Screen.InternalName}"")]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> {Screen.InternalName}([FromBody]{Screen.InternalName}Request post)
        {{
            if (post == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}
            
            var newRecord = new {ScreenSection.FormSection.Entity.InternalName}{{
{string.Join(string.Concat(",", Environment.NewLine), postPropertyMapping)}
            }};            

            _context.{ScreenSection.FormSection.Entity.InternalNamePlural}.Add(newRecord);
            await _context.SaveChangesAsync();

            return Ok(newRecord.Id);
        }}";
        }

        /// <summary>
        /// PATCH Verb method, for updating records
        /// </summary>
        internal string PatchMethod()
        {
            var patchEntityOperations = new List<string>();
            foreach (var formField in ScreenSection.FormSection.FormFields)
            {
                switch (formField.PropertyType)
                {
                    case PropertyType.ParentRelationship:
                        patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
                        if (operation.value != null && !string.IsNullOrWhiteSpace(operation.value.ToString()) && Guid.TryParse(operation.value.ToString(), out Guid {formField.InternalNameCSharp.Camelize()}))
                        {{
                            entity.{formField.InternalNameCSharp} = {formField.InternalNameCSharp.Camelize()};
                        }}
                        else
                        {{
                            throw new Exception(""{formField.InternalNameCSharp.Camelize()} value is invalid"");
                        }}
                        entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
                        break;");
                        break;

                    case PropertyType.ReferenceRelationship:
                        patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
                        if (operation.value != null && !string.IsNullOrWhiteSpace(operation.value.ToString()) && Guid.TryParse(operation.value.ToString(), out Guid {formField.InternalNameCSharp.Camelize()}))
                        {{
                            entity.{formField.InternalNameCSharp} = {formField.InternalNameCSharp.Camelize()};
                        }}
                        else
                        {{
                            {(formField.Property.Required ? $@"throw new Exception(""{formField.InternalNameCSharp.Camelize()} value is invalid"");" : $"entity.{formField.InternalNameCSharp} = null;")}
                        }}
                        entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
                        break;");
                        break;

                    case PropertyType.PrimaryKey:
                        break;
                    case PropertyType.String:
                        patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
                        entity.{formField.InternalNameCSharp} = operation.value.ToString();
                        entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
                        break;");
                        break;

                    case PropertyType.Integer:
                        patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
                        int int32Value{formField.InternalNameCSharp};
                        if (operation.value != null && Int32.TryParse(operation.value.ToString(), out int32Value{formField.InternalNameCSharp}))
                        {{
                            entity.{formField.InternalNameCSharp} = int32Value{formField.InternalNameCSharp};
                            entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
                        }}
                        {(formField.Property.Required ? string.Empty : $@"else if (operation.value == null || string.IsNullOrWhiteSpace(operation.value.ToString()))
                        {{
                            entity.{formField.InternalNameCSharp} = null;
                            entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
                        }}")}
                        else
                        {{
                            throw new Exception(""Property: {formField.InternalNameCSharp}, Value:"" + operation.value + "" is not a valid integer value"");
                        }}
                        break;");
                        break;
                    case PropertyType.Double:
                        patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
                        double doubleValue{formField.InternalNameCSharp};
                        if (operation.value != null && Double.TryParse(operation.value.ToString(), out doubleValue{formField.InternalNameCSharp}))
                        {{
                            entity.{formField.InternalNameCSharp} = doubleValue{formField.InternalNameCSharp};
                            entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
                        }}
                        {(formField.Property.Required ? string.Empty : $@"else if (operation.value == null || string.IsNullOrWhiteSpace(operation.value.ToString()))
                        {{
                            entity.{formField.InternalNameCSharp} = null;
                            entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
                        }}")}
                        else
                        {{
                            throw new Exception(""Property: {formField.InternalNameCSharp}, Value:"" + operation.value + "" is not a valid double value"");
                        }}
                        break;");
                        break;
                    case PropertyType.DateTime:
                        patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
                        DateTime dateTimeValue{formField.InternalNameCSharp};
                        if (operation.value != null && DateTime.TryParse(operation.value.ToString(), out dateTimeValue{formField.InternalNameCSharp}))
                        {{
                            entity.{formField.InternalNameCSharp} = dateTimeValue{formField.InternalNameCSharp};
                            entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
                        }}
                        {(formField.Property.Required ? string.Empty : $@"else if (operation.value == null || string.IsNullOrWhiteSpace(operation.value.ToString()))
                        {{
                            entity.{formField.InternalNameCSharp} = null;
                            entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
                        }}")}
                        else
                        {{
                            throw new Exception(""Property: {formField.InternalNameCSharp}, Value:"" + operation.value + "" is not a valid boolean value"");
                        }}
                        break;");
                        break;
                    case PropertyType.Boolean:
                        patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
                        bool booleanValue{formField.InternalNameCSharp};
                        if (operation.value != null && Boolean.TryParse(operation.value.ToString(), out booleanValue{formField.InternalNameCSharp}))
                        {{
                            entity.{formField.InternalNameCSharp} = booleanValue{formField.InternalNameCSharp};
                            entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
                        }}
                        else
                        {{
                            throw new Exception(""Property: {formField.InternalNameCSharp}, Value:"" + operation.value + "" is not a valid boolean value"");
                        }}
                        break;");
                        break;
                    default:
                        break;
                }
            }
            
            //http://benfoster.io/blog/aspnet-core-json-patch-partial-api-updates
            return $@"
        /// <summary>
        /// {ScreenSection.Title} Update
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

            var entity = new Entities.{ScreenSection.FormSection.Entity.InternalName}() {{ Id = id }};
            var entityEntry = _context.{ScreenSection.FormSection.Entity.InternalNamePlural}.Attach(entity);
            
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
    }
}
