using Oven.Request;

namespace Oven.Templates.Api.Entities.Contracts
{
    /// <summary>
    /// Controller Search Method Template
    /// </summary>
    public class SearchMethodTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        internal static string Evaluate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
        {
            string actionName = null;
            
            if (entity.Id != screen.EntityId)
            {
                actionName = $"{screen.InternalName}{screenSection.InternalName}";
            }

            return $@"
        /// <summary>
        /// {screenSection.Title} Search
        /// </summary>
        Task<{screenSection.SearchSection.SearchItemClass}[]> Search{actionName}Async({screenSection.SearchSection.SearchRequestClass} request);";
        }
    }
}
