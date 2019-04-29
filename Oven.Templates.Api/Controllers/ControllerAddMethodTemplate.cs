using System;
using System.Collections.Generic;
using Humanizer;
using Oven.Request;

namespace Oven.Templates.Api.Controllers
{
    /// <summary>
    /// Contoller Edit Method Template
    /// </summary>
    public class ControllerAddMethodTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public ControllerAddMethodTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

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

            var id = await {Screen.Entity.InternalName.Camelize()}Service.CreateAsync(request);

            var result = await {Screen.Entity.InternalName.Camelize()}Service.GetAsync(id);

            if (result == null)
            {{
                return NotFound();
            }}

            return Ok(result);
        }}";
        }
    }
}
