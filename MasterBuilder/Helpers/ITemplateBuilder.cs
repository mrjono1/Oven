using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterBuilder.Helpers
{
    /// <summary>
    /// Standard way of builing mutiple templates
    /// </summary>
    interface ITemplateBuilder
    {
        /// <summary>
        /// Get Templates
        /// </summary>
        /// <returns>ITemplate objects</returns>
        IEnumerable<ITemplate> GetTemplates();
    }
}
