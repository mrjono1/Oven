using System.Collections.Generic;
using Oven.Request;

namespace Oven.Templates.DataAccessLayer.Services
{
    /// <summary>
    /// Get Method Template
    /// </summary>
    public class GetMethodTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly IEnumerable<ScreenSection> ScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public GetMethodTemplate(Project project, Screen screen, IEnumerable<ScreenSection> screenSections)
        {
            Project = project;
            Screen = screen;
            ScreenSections = screenSections;
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
        public virtual async Task<{Screen.FormResponseClass}> GetAsync(string id)
        {{
            if (string.IsNullOrWhiteSpace(id))
            {{
                throw new ArgumentNullException(); 
            }}
            if (!Guid.TryParse(id, out Guid guid))
            {{
                throw new ArgumentException(""Invalid id format"", ""id"");
            }}
            
            var filter = Builders<{Screen.Entity.InternalName}>.Filter.Eq(a => a.Id, id.ToLower());
            var findOptions = new FindOptions<{Screen.Entity.InternalName}, {Screen.FormResponseClass}>();
            var query = await _context.{Screen.Entity.InternalNamePlural}.FindAsync(filter, findOptions);
            var result = await query.SingleOrDefaultAsync();

            return result;
        }}";
        }
    }
}
