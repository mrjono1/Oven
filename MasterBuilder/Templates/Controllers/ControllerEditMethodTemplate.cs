using System;
using System.Collections.Generic;
using Humanizer;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Controllers
{
    /// <summary>
    /// Contoller Edit Method Template
    /// </summary>
    public class ControllerEditMethodTemplate
    {
        internal static string Evaluate(Project project, Screen screen, ScreenSection screenSection)
        {
            var getPropertyMapping = new List<string>();
            var postPropertyMapping = new List<string>();
            var patchEntityOperations = new List<string>();
            foreach (var formField in screenSection.FormSection.FormFields)
            {
                if (formField.PropertyType != PropertyTypeEnum.ParentRelationship &&
                    formField.PropertyType != PropertyTypeEnum.ReferenceRelationship &&
                    formField.PropertyType != PropertyTypeEnum.OneToOneRelationship)
                {
                    getPropertyMapping.Add($"                           {formField.InternalNameCSharp} = item.{formField.InternalNameCSharp}");
                }

                switch (formField.PropertyType)
                {
                    case PropertyTypeEnum.ParentRelationship:
                        postPropertyMapping.Add($"                {formField.InternalNameCSharp} = post.{formField.InternalNameCSharp}");
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

                        getPropertyMapping.Add($"                           {formField.InternalNameCSharp} = item.{formField.InternalNameCSharp}");
                        break;

                    case PropertyTypeEnum.ReferenceRelationship:
                        postPropertyMapping.Add($"                {formField.InternalNameCSharp} = post.{formField.InternalNameCSharp}");

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

                        getPropertyMapping.Add($"                           {formField.InternalNameCSharp} = item.{formField.InternalNameCSharp}");

                        // TODO: Title should be configurable
                        // TODO: is it faster to do the bool check on the key instead of object?
                        getPropertyMapping.Add($"                           {formField.InternalNameAlternateCSharp} = item.{formField.Property.InternalName} != null ? item.{formField.Property.InternalName}.Title : null");
                        break;

                    case PropertyTypeEnum.PrimaryKey:
                        break;
                    case PropertyTypeEnum.String:
                        postPropertyMapping.Add($"                {formField.InternalNameCSharp} = post.{formField.InternalNameCSharp}");
                        patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
                        entity.{formField.InternalNameCSharp} = operation.value.ToString();
                        entityEntry.Property(p => p.{formField.InternalNameCSharp}).IsModified = true;
                        break;");
                        break;
                    case PropertyTypeEnum.Integer:
                        postPropertyMapping.Add($"                {formField.InternalNameCSharp} = post.{formField.InternalNameCSharp}");
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
                    case PropertyTypeEnum.Double:
                        postPropertyMapping.Add($"                {formField.InternalNameCSharp} = post.{formField.InternalNameCSharp}");
                        patchEntityOperations.Add($@"                     case ""/{formField.InternalNameCSharp.Camelize()}"":
                        int doubleValue{formField.InternalNameCSharp};
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
                    case PropertyTypeEnum.DateTime:
                        postPropertyMapping.Add($"                {formField.InternalNameCSharp} = post.{formField.InternalNameCSharp}");
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
                    case PropertyTypeEnum.Boolean:
                        postPropertyMapping.Add($"                {formField.InternalNameCSharp} = post.{formField.InternalNameCSharp}");
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

            var get = $@"
        /// <summary>
        /// {screenSection.Title} Get
        /// </summary>
        [HttpGet(""{screen.InternalName}/{{id}}"")]
        public async Task<IActionResult> {screen.InternalName}(Guid id)
        {{
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (id == Guid.Empty)
            {{
                return NotFound();
            }}
            
            var result = await _context.{screenSection.FormSection.Entity.InternalNamePlural}
                            .AsNoTracking()
                            .Select(item => new Models.{screen.InternalName}Response
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


            var post = $@"
        /// <summary>
        /// {screenSection.Title} Add
        /// </summary>
        [HttpPost(""{screen.InternalName}"")]
        public async Task<IActionResult> {screen.InternalName}([FromBody]{screen.InternalName}Request post)
        {{
            if (post == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}
            
            var newRecord = new {screenSection.FormSection.Entity.InternalName}{{
{string.Join(string.Concat(",", Environment.NewLine), postPropertyMapping)}
            }};            

            _context.{screenSection.FormSection.Entity.InternalNamePlural}.Add(newRecord);
            await _context.SaveChangesAsync();

            return Ok(newRecord.Id);
        }}";

            //http://benfoster.io/blog/aspnet-core-json-patch-partial-api-updates
            var patch = $@"
        /// <summary>
        /// {screenSection.Title} Update
        /// </summary>
        [HttpPatch(""{screen.InternalName}/{{id}}"")]
        public async Task<IActionResult> {screen.InternalName}([FromRoute]Guid id, [FromBody]JsonPatchDocument<{screen.InternalName}Request> patch)
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

            var entity = new Entities.{screenSection.FormSection.Entity.InternalName}() {{ Id = id }};
            var entityEntry = _context.{screenSection.FormSection.Entity.InternalNamePlural}.Attach(entity);
            
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

            // todo delete

            return string.Concat(get, post, patch);
        }
    }
}
