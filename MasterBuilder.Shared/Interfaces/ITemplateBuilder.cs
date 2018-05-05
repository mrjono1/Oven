using System.Collections.Generic;

namespace MasterBuilder.Interfaces
{
    /// <summary>
    /// Standard way of builing mutiple templates
    /// </summary>
    public interface ITemplateBuilder
    {
        /// <summary>
        /// Get Templates
        /// </summary>
        /// <returns>ITemplate objects</returns>
        IEnumerable<ITemplate> GetTemplates();
    }
}
