﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterBuilder.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MasterBuilder.Api.Controllers
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
            var builder = new MasterBuilder.Builder();

            string result = null;
            try
            {
                string outputDirectory = _iconfiguration.GetValue<string>("OutputDirectory");
                result = await builder.Run(outputDirectory, project);
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
