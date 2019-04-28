using System;
using System.Collections.Generic;
using Humanizer;
using Oven.Request;

namespace Oven.Templates.Api.Controllers
{
    /// <summary>
    /// Contoller Edit Method Template
    /// </summary>
    public class ControllerGetMethodTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public ControllerGetMethodTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

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
        public async Task<IActionResult> GetAsync([FromServices] I{Screen.Entity.InternalName}Service {Screen.Entity.InternalName.Camelize()}Service, string id)
        {{
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (string.IsNullOrWhiteSpace(id) || !ObjectId.TryParse(id, out ObjectId objectId))
            {{
                return NotFound();
            }}
            
            var result = await {Screen.Entity.InternalName.Camelize()}Service.GetAsync(objectId);

            if (result == null)
            {{
                return NotFound();
            }}

            return Ok(result);
        }}";
        }
    }
}
