using Oven.Request;

namespace Oven.Templates.DataAccessLayer.Services
{
    /// <summary>
    /// Delete Method Template
    /// </summary>
    public class DeleteMethodTemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public DeleteMethodTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Delete
        /// </summary>
        internal string DeleteMethod()
        {
            return $@"
        /// <summary>
        /// {Entity.Title} Delete
        /// </summary>
        public virtual async Task<bool> DeleteAsync(string id)
        {{
            if (string.IsNullOrWhiteSpace(id))
            {{
                throw new ArgumentNullException(); 
            }}
            if (!Guid.TryParse(id, out Guid guid))
            {{
                throw new ArgumentException(""Invalid Guid"", ""id"");
            }}
            
            await _context.{Entity.InternalNamePlural}.DeleteOneAsync(record => record.Id == id);

            return true;
        }}";
        }
    }
}
