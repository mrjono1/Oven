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
        public async Task<string> Post([FromBody]Project project)
        {
            var builder = new MasterBuilder.Builder();
            
            return await builder.Run("C:\\Temp", project);
        }
    }
}
