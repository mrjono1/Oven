using System;
using System.Collections.Generic;
using Humanizer;
using Oven.Request;

namespace Oven.Templates.Api.Controllers
{
    /// <summary>
    /// Contoller Edit Method Template
    /// </summary>
    public class ControllerUpdateMethodTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public ControllerUpdateMethodTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

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
        [ProducesResponseType(typeof({Screen.FormResponseClass}), 200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> UpdateAsync([FromServices] I{Screen.Entity.InternalName}Service {Screen.Entity.InternalName.Camelize()}Service, [FromRoute]string id, [FromBody]{Screen.InternalName}Request request)
        {{            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}
          
            if (string.IsNullOrWhiteSpace(id) || !ObjectId.TryParse(id, out ObjectId objectId))
            {{
                return BadRequest();
            }}

            await {Screen.Entity.InternalName.Camelize()}Service.UpdateAsync(objectId, request);

            var result = await {Screen.Entity.InternalName.Camelize()}Service.GetAsync(objectId);

            if (result == null)
            {{
                return NotFound();
            }}

            return Ok(result);
        }}";
        }

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
            //            if (operation.value != null && !string.IsNullOrWhiteSpace(operation.value.ToString()) && ObjectId.TryParse(operation.value.ToString(), out ObjectId {formField.InternalNameCSharp.Camelize()}))
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
            //            if (operation.value != null && !string.IsNullOrWhiteSpace(operation.value.ToString()) && ObjectId.TryParse(operation.value.ToString(), out ObjectId {formField.InternalNameCSharp.Camelize()}))
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
        public async Task<IActionResult> {Screen.InternalName}([FromRoute]string id, [FromBody]JsonPatchDocument<{Screen.InternalName}Request> request)
        {{
            if (string.IsNullOrWhiteSpace(id) || request == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (!request.Operations.Any())
            {{
                return Ok();
            }}

            var entity = new DataAccessLayer.Entities.{Screen.Entity.InternalName}() {{ Id = id }};
            var entityEntry = _context.{Screen.Entity.InternalNamePlural}.Attach(entity);
            
            // do stuff
            foreach(var operation in request.Operations)
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
