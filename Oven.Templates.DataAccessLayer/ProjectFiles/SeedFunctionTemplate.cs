using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oven.Templates.DataAccessLayer.ProjectFiles
{
    public class SeedFunctionTemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public SeedFunctionTemplate(Project project)
        {
            Project = project;
        }

        public string GetFunction()
        {
            var functionCalls = new List<string>();
            var functions = new List<string>();

            foreach (var entity in Project.Entities.Where(e => e.Seed != null))
            {
                var content = Newtonsoft.Json.JsonConvert.SerializeObject(entity.Seed.JsonData);

                functionCalls.Add($"await {entity.InternalName}Seed();");

                functions.Add($@"        private async Task {entity.InternalName}Seed()
        {{
            var content = {content};
            
            var items = JsonConvert.DeserializeObject<List<Models.{entity.InternalName}Request>>(content,
                new JsonSerializerSettings {{ Converters = new JsonConverter[] {{ new ObjectIdJsonConverter() }} }}
            );
            var service = new Services.{entity.InternalName}Service(this);

            foreach (var item in items)
            {{
                await service.UpsertAsync(item.Id, item, true);
            }}
        }}");
            }

            string seedData = null;
            if (functionCalls.Any())
            {
                seedData = $@"        
        #region Seed
        public async Task Seed()
        {{
{functionCalls.IndentLines(3)}
        }}

{string.Join(Environment.NewLine,  functions)}
        #endregion";
            }

            return seedData;
        }
    }
}
