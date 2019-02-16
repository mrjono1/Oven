using Oven.Request;
using System;
using System.Collections.Generic;

namespace Oven.Helpers
{
    public class ScreenItem
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
        public List<ScreenItem> ChildScreenItems { get; set; }
        public IEnumerable<Guid> ParentScreenSectionIds { get; set; }
    }
}
