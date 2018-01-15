using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Form Field
    /// </summary>
    public class FormField
    {
        /// <summary>
        /// Entity Property Id
        /// </summary>
        public Guid EntityPropertyId { get; set; }

        #region Internal Helper Properties
        /// <summary>
        /// Entity Property
        /// </summary>
        internal Property Property { get; set; }
        #endregion
    }
}
