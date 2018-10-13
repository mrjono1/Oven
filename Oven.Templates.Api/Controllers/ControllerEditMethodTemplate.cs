using System;
using System.Collections.Generic;
using Humanizer;
using Oven.Request;

namespace Oven.Templates.Api.Controllers
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
        /// <summary>
        /// GET Verb method
        /// </summary>
        internal string GetMethod()
        {
            return $@"
        /// <summary>
        /// {Screen.Title} Get
        /// </summary>
        [HttpGet(""{{id}}"")]
        [ProducesResponseType(typeof({Screen.FormResponseClass}), 200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> GetAsync([FromServices] I{Screen.Entity.InternalName}Service {Screen.Entity.InternalName.Camelize()}Service, Guid id)
        {{
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (id == Guid.Empty)
            {{
                return NotFound();
            }}
            
            var result = await {Screen.Entity.InternalName.Camelize()}Service.GetAsync(id);

            if (result == null)
            {{
                return NotFound();
            }}

            return Ok(result);
        }}";
        }
        #endregion

        #region PUT (Update)
        /// <summary>
        /// PUT Verb Method, for updating records
        /// </summary>
        internal string PutMethod()
        {
            return $@"
        /// <summary>
        /// {Screen.Title} Update
        /// </summary>
        [HttpPut(""{{id}}"")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> UpdateAsync([FromServices] I{Screen.Entity.InternalName}Service {Screen.Entity.InternalName.Camelize()}Service, [FromRoute]Guid id, [FromBody]{Screen.InternalName}Request request)
        {{
            if (request == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}
            
            await {Screen.Entity.InternalName.Camelize()}Service.UpdateAsync(id, request);

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
            return $@"
        /// <summary>
        /// {Screen.Title} Add
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof({Screen.FormResponseClass}), 200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> CreateAsync([FromServices] I{Screen.Entity.InternalName}Service {Screen.Entity.InternalName.Camelize()}Service, [FromBody]{Screen.InternalName}Request request)
        {{
            if (request == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            var result = await {Screen.Entity.InternalName.Camelize()}Service.CreateAsync(request);

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
        public async Task<IActionResult> DeleteAsync([FromServices] I{Screen.Entity.InternalName}Service {Screen.Entity.InternalName.Camelize()}Service, Guid id)
        {{
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (id == Guid.Empty)
            {{
                return NotFound();
            }}
            
            await {Screen.Entity.InternalName.Camelize()}Service.DeleteAsync(id);

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
