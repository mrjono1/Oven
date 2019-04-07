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
            var seed = new Dictionary<string, string>();
            foreach (var entity in Project.Entities.Where(e => e.Seed != null))
            {
                var content = Newtonsoft.Json.JsonConvert.SerializeObject(entity.Seed.JsonData);

                var seedStringBuilder = new StringBuilder($@"        private async Task {entity.InternalName}Seed()
              {{
                  var content = {content};
                  // TODO: possibly could convert this to delegate so only executed if needed
                  var items = JsonConvert.DeserializeObject<List<Entities.{entity.InternalName}>>(content);");

                      seedStringBuilder.AppendLine($@"
                  if (!{entity.InternalNamePlural}.Any())
                  {{
                      await {entity.InternalNamePlural}.AddRangeAsync(items);
                  }}");

                      switch (entity.Seed.SeedType)
                      {
                          case SeedType.EnsureAllAdded:
                              // TODO: can this be done better with AttachRange?
                      seedStringBuilder.AppendLine($@"           else
                  {{
                      foreach (var item in items)
                      {{
                          var existing = await {entity.InternalNamePlural}.AnyAsync(p => p.Id == item.Id);
                          if (!existing){{
                              await {entity.InternalNamePlural}.AddAsync(item);
                          }}
                      }}
                  }}");
                              break;
                          case SeedType.EnsureAllUpdated:
                              seedStringBuilder.AppendLine($@"            else
                  {{
                      foreach (var item in items)
                      {{
                          var existing = await {entity.InternalNamePlural}.AsNoTracking().SingleOrDefaultAsync(a => a.Id == item.Id);
                          if (existing == null){{
                             await {entity.InternalNamePlural}.AddAsync(item);
                          }}
                          else
                          {{
                              {entity.InternalNamePlural}.Attach(item);
                          }}
                      }}
                  }}");
                              break;
                      }

                      seedStringBuilder.AppendLine(@"            await SaveChangesAsync();
              }");
                      seed.Add($"            await {entity.InternalName}Seed();", seedStringBuilder.ToString());
            }

            string seedData = null;
            if (seed.Any())
            {
                seedData = $@"        #region Seed
        internal async Task Seed()
        {{
{string.Join(Environment.NewLine, seed.Keys)}
        }}

{string.Join(Environment.NewLine, seed.Values)}
        #endregion";
            }

            return $@"
        public async Task Seed(){{

        }}";
        }
    }
}
