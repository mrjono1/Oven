using MasterBuilder.Request;
using System.Collections.Generic;

namespace MasterBuilder.Helpers
{
    /// <summary>
    /// Helper class
    /// </summary>
    public class ScreenSectionEntityFormFields
    {
        /// <summary>
        /// Entity
        /// </summary>
        public Entity Entity { get; set; }
        /// <summary>
        /// Form Fields used
        /// </summary>
        public List<FormField> FormFields { get; set; }
        /// <summary>
        /// Child Entites
        /// </summary>
        public List<Entity> ChildEntities { get; set; }
    }
}
