using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterBuilder.Request;
using Microsoft.AspNetCore.Mvc;

namespace MasterBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    public class BuilderController : Controller
    {
        // POST api/values
        [HttpPost("publish")]
        public async Task<IActionResult> Post([FromBody]Project project)
        {
            var builder = new MasterBuilder.Builder();

            string result = null;
            try
            {
                result = await builder.Run("C:\\Temp", project);
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
