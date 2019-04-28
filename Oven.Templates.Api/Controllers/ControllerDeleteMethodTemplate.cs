using System;
using System.Collections.Generic;
using Humanizer;
using Oven.Request;

namespace Oven.Templates.Api.Controllers
{
    /// <summary>
    /// Contoller Edit Method Template
    /// </summary>
    public class ControllerDeleteMethodTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public ControllerDeleteMethodTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

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
        public async Task<IActionResult> DeleteAsync([FromServices] I{Screen.Entity.InternalName}Service {Screen.Entity.InternalName.Camelize()}Service, string id)
        {{
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (string.IsNullOrWhiteSpace(id) || !ObjectId.TryParse(id, out ObjectId objectId))
            {{
                return BadRequest();
            }}
            
            if (!await {Screen.Entity.InternalName.Camelize()}Service.DeleteAsync(objectId))
            {{
                return BadRequest();
            }}
            return Ok();
        }}";
        }
    }
}
