using Oven.Request;

namespace Oven.Templates.Api.Entities
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
        public async Task<bool> DeleteAsync(Guid id)
        {{
            if (id == Guid.Empty)
            {{
                throw new ArgumentNullException(); 
            }}
            
            _context.{Entity.InternalNamePlural}.Remove(new {Entity.InternalName}() {{ Id = id }});
            await _context.SaveChangesAsync();

            return true;
        }}";
        }
    }
}
