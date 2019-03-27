using System;
using System.Collections.Generic;
using System.Linq;
using Oven.Helpers;
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
        
        public string GetProperty(FormField formField, List<ScreenItem> parentScreenItems = null)
        {
            var entityObjects = string.Empty;
            if (parentScreenItems != null && parentScreenItems.Any())
            {
                entityObjects = $@"{string.Join(".", parentScreenItems.Select(p => $@"{p.Entity.InternalName}"))}.";
            }
            return $@"{formField.InternalNameCSharp} = p.{entityObjects}{formField.InternalNameCSharp}";
        }

        public List<string> GetScreenItemProperties(ScreenItem screenItem, List<ScreenItem> parentScreenItems = null)
        {
            var properties = new List<string>();
            foreach (var formField in screenItem.FormFields)
            {
                properties.Add(GetProperty(formField, parentScreenItems));
            }

            
            foreach (var childScreenItem in screenItem.ChildScreenItems)
            {
                var childProperties = new List<string>();
                var childScreenItems = new List<ScreenItem> { childScreenItem };
                if (parentScreenItems != null)
                {
                    childScreenItems.AddRange(parentScreenItems);
                }

                childProperties.AddRange(GetScreenItemProperties(childScreenItem, childScreenItems));
                properties.Add($@"{childScreenItem.Entity.InternalName} = new {childScreenItem.Entity.InternalName}Response
{{
{childProperties.IndentLines(1, ",")}
}}");
            }

            return properties;
        }

        /// <summary>
        /// GET Verb method
        /// </summary>
        internal string GetMethod()
        {
            var screenItem = RequestTransforms.GetScreenFieldsNested(Screen);

            var properties = GetScreenItemProperties(screenItem);

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
            var query = _context.{Screen.Entity.InternalNamePlural}.Find(filter)
                .Project(p => new {Screen.FormResponseClass}() {{
{properties.IndentLines(5, ",")}
                }});
            var result = await query.SingleOrDefaultAsync();

            return result;
        }}";
        }
    }
}
