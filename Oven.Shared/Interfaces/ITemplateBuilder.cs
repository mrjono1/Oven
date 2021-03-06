using System.Collections.Generic;

namespace Oven.Interfaces
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

        /// <summary>
        /// Get Template Builders
        /// </summary>
        /// <returns>ITemplateBuilder objects</returns>
        IEnumerable<ITemplateBuilder> GetTemplateBuilders();
    }
}
