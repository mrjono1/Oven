using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.Api.Controllers
{
    /// <summary>
    /// Administration Controller Template
    /// </summary>
    public class AdministrationControllerTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public AdministrationControllerTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => "AdministrationController.cs";

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath() => new string[] { "Controllers" };

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using {Project.InternalName}.DataAccessLayer.Models;
using {Project.InternalName}.DataAccessLayer;
using {Project.InternalName}.DataAccessLayer.Entities;
using {Project.InternalName}.DataAccessLayer.Services.Contracts;
using System.Diagnostics;
using Kitchen.DataAccessLayer.Services;

namespace {Project.InternalName}.Controllers
{{
    /// <summary>
    /// Administration Controller
    /// </summary>
    [Route(""api/Administration"")]
    public class AdministrationController : ControllerBase
    {{
        /// <summary>
        /// Seed
        /// </summary>
        [HttpGet(""seed"")]
        public async Task<IActionResult> Seed([FromServices] IAdministrationService administrationService)
        {{
            await administrationService.Seed();

            return Ok();
        }}
    }}
}}";
        }
    }
}
