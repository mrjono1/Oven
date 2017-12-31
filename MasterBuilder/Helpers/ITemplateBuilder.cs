using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterBuilder.Helpers
{
    /// <summary>
    /// Standard way of builing mutiple templates
    /// </summary>
    internal interface ITemplateBuilder
    {
        /// <summary>
        /// Get Templates
        /// </summary>
        /// <returns>ITemplate objects</returns>
        IEnumerable<ITemplate> GetTemplates();
    }
}
