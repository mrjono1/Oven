using System;
using System.Threading.Tasks;
using Oven.Request;
using Oven.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Oven.Api.Controllers
{
    /// <summary>
    /// Builder Controller
    /// </summary>
    [Route("api/[controller]")]
    public class BuilderController : Controller
    {
        private readonly IConfiguration _iconfiguration;

        /// <summary>
        /// Constructor
        /// </summary>
        public BuilderController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        /// <summary>
        /// Publish a project
        /// </summary>
        /// <response code="200">Project published</response>
        /// <response code="400">Project settings invalid</response>
        [HttpPost("publish")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody]Project project)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            var builderSettings = _iconfiguration.Get<BuilderSettings>();

            var builder = new BuildSolution(builderSettings);

            string result = null;
            try
            {
                result = await builder.RunAsync(project);
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }

            if (result.Equals("Success"))
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
