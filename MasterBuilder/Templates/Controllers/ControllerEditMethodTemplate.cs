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
        internal static string Evaluate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
        {
            var getPropertyMapping = new List<string>();
            var postPropertyMapping = new List<string>();
            var patchEntityOperations = new List<string>();
            foreach (var item in entity.Properties)
            {
                if (item.PropertyType != PropertyTypeEnum.ParentRelationship &&
                    item.PropertyType != PropertyTypeEnum.ReferenceRelationship &&
                    item.PropertyType != PropertyTypeEnum.OneToOneRelationship)
                {
                    getPropertyMapping.Add($"                           {item.InternalName} = item.{item.InternalName}");
                }
                switch (item.PropertyType)
                {
                    case PropertyTypeEnum.ParentRelationship:
                        postPropertyMapping.Add($"                {item.InternalName}Id = post.{item.InternalName}Id");
                        patchEntityOperations.Add($@"                     case ""/{item.InternalName.Camelize()}Id"":
                        if (operation.value != null && !string.IsNullOrWhiteSpace(operation.value.ToString()) && Guid.TryParse(operation.value.ToString(), out Guid {item.InternalName.Camelize()}Id))
                        {{
                            entity.{item.InternalName}Id = {item.InternalName.Camelize()}Id;
                        }}
                        else
                        {{
                            throw new Exception(""{item.InternalName.Camelize()}Id value is invalid"");
                        }}
                        entityEntry.Property(p => p.{item.InternalName}Id).IsModified = true;
                        break;");

                        getPropertyMapping.Add($"                           {item.InternalName}Id = item.{item.InternalName}Id");
                        break;

                    case PropertyTypeEnum.ReferenceRelationship:
                        postPropertyMapping.Add($"                {item.InternalName}Id = post.{item.InternalName}Id");

                        patchEntityOperations.Add($@"                     case ""/{item.InternalName.Camelize()}Id"":
                        if (operation.value != null && !string.IsNullOrWhiteSpace(operation.value.ToString()) && Guid.TryParse(operation.value.ToString(), out Guid {item.InternalName.Camelize()}Id))
                        {{
                            entity.{item.InternalName}Id = {item.InternalName.Camelize()}Id;
                        }}
                        else
                        {{
                            {(item.Required ? $@"throw new Exception(""{item.InternalName.Camelize()}Id value is invalid"");" : $"entity.{item.InternalName}Id = null;")}
                        }}
                        entityEntry.Property(p => p.{item.InternalName}Id).IsModified = true;
                        break;");

                        getPropertyMapping.Add($"                           {item.InternalName}Id = item.{item.InternalName}Id");

                        // TODO: Title should be configurable
                        // TODO: is it faster to do the bool check on the key instead of object?
                        getPropertyMapping.Add($"                           {item.InternalName}Title = item.{item.InternalName} != null ? item.{item.InternalName}.Title : null");
                        break;

                    case PropertyTypeEnum.PrimaryKey:
                        break;
                    case PropertyTypeEnum.String:
                        postPropertyMapping.Add($"                {item.InternalName} = post.{item.InternalName}");
                        patchEntityOperations.Add($@"                     case ""/{item.InternalName.Camelize()}"":
                        entity.{item.InternalName} = operation.value.ToString();
                        entityEntry.Property(p => p.{item.InternalName}).IsModified = true;
                        break;");
                        break;
                    case PropertyTypeEnum.Integer:
                        postPropertyMapping.Add($"                {item.InternalName} = post.{item.InternalName}");
                        patchEntityOperations.Add($@"                     case ""/{item.InternalName.Camelize()}"":
                        int int32Value{item.InternalName};
                        if (operation.value != null && Int32.TryParse(operation.value.ToString(), out int32Value{item.InternalName}))
                        {{
                            entity.{item.InternalName} = int32Value{item.InternalName};
                            entityEntry.Property(p => p.{item.InternalName}).IsModified = true;
                        }}
                        {(item.Required ? string.Empty : $@"else if (operation.value == null || string.IsNullOrWhiteSpace(operation.value.ToString()))
                        {{
                            entity.{item.InternalName} = null;
                            entityEntry.Property(p => p.{item.InternalName}).IsModified = true;
                        }}")}
                        else
                        {{
                            throw new Exception(""Property: {item.InternalName}, Value:"" + operation.value + "" is not a valid integer value"");
                        }}
                        break;");
                        break;
                    case PropertyTypeEnum.Double:
                        postPropertyMapping.Add($"                {item.InternalName} = post.{item.InternalName}");
                        patchEntityOperations.Add($@"                     case ""/{item.InternalName.Camelize()}"":
                        int doubleValue{item.InternalName};
                        if (operation.value != null && Double.TryParse(operation.value.ToString(), out doubleValue{item.InternalName}))
                        {{
                            entity.{item.InternalName} = doubleValue{item.InternalName};
                            entityEntry.Property(p => p.{item.InternalName}).IsModified = true;
                        }}
                        {(item.Required ? string.Empty : $@"else if (operation.value == null || string.IsNullOrWhiteSpace(operation.value.ToString()))
                        {{
                            entity.{item.InternalName} = null;
                            entityEntry.Property(p => p.{item.InternalName}).IsModified = true;
                        }}")}
                        else
                        {{
                            throw new Exception(""Property: {item.InternalName}, Value:"" + operation.value + "" is not a valid double value"");
                        }}
                        break;");
                        break;
                    case PropertyTypeEnum.DateTime:
                        postPropertyMapping.Add($"                {item.InternalName} = post.{item.InternalName}");
                        patchEntityOperations.Add($@"                     case ""/{item.InternalName.Camelize()}"":
                        DateTime dateTimeValue{item.InternalName};
                        if (operation.value != null && DateTime.TryParse(operation.value.ToString(), out dateTimeValue{item.InternalName}))
                        {{
                            entity.{item.InternalName} = dateTimeValue{item.InternalName};
                            entityEntry.Property(p => p.{item.InternalName}).IsModified = true;
                        }}
                        {(item.Required ? string.Empty : $@"else if (operation.value == null || string.IsNullOrWhiteSpace(operation.value.ToString()))
                        {{
                            entity.{item.InternalName} = null;
                            entityEntry.Property(p => p.{item.InternalName}).IsModified = true;
                        }}")}
                        else
                        {{
                            throw new Exception(""Property: {item.InternalName}, Value:"" + operation.value + "" is not a valid boolean value"");
                        }}
                        break;");
                        break;
                    case PropertyTypeEnum.Boolean:
                        postPropertyMapping.Add($"                {item.InternalName} = post.{item.InternalName}");
                        patchEntityOperations.Add($@"                     case ""/{item.InternalName.Camelize()}"":
                        bool booleanValue{item.InternalName};
                        if (operation.value != null && Boolean.TryParse(operation.value.ToString(), out booleanValue{item.InternalName}))
                        {{
                            entity.{item.InternalName} = booleanValue{item.InternalName};
                            entityEntry.Property(p => p.{item.InternalName}).IsModified = true;
                        }}
                        else
                        {{
                            throw new Exception(""Property: {item.InternalName}, Value:"" + operation.value + "" is not a valid boolean value"");
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
            
            var result = await _context.{entity.InternalNamePlural}
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
            
            var newRecord = new {entity.InternalName}{{
{string.Join(string.Concat(",", Environment.NewLine), postPropertyMapping)}
            }};            

            _context.{entity.InternalNamePlural}.Add(newRecord);
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

            var entity = new Entities.{entity.InternalName}() {{ Id = id }};
            var entityEntry = _context.{entity.InternalNamePlural}.Attach(entity);
            
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
