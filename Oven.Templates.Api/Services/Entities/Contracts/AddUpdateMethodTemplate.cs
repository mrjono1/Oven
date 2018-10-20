using Oven.Request;

namespace Oven.Templates.Api.Entities.Contracts
{
    /// <summary>
    /// Contoller Edit Method Template
    /// </summary>
    public class AddUpdateMethodTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public AddUpdateMethodTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Update
        /// </summary>
        internal string UpdateMethod()
        {
            return $@"
        /// <summary>
        /// {Screen.Title} Update
        /// </summary>
        Task<Guid> UpdateAsync(Guid id, {Screen.InternalName}Request request);";
        }

        /// <summary>
        /// Add Record
        /// </summary>
        internal string AddMethod()
        {
            return $@"
        /// <summary>
        /// {Screen.Title} Add
        /// </summary>
        Task<Guid> CreateAsync({Screen.InternalName}Request request);";
        }

    }
}
