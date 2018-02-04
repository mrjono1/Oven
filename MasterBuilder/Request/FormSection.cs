using System.Collections.Generic;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Form Section Settings
    /// </summary>
    public class FormSection
    {
        /// <summary>
        /// Form Fields
        /// </summary>
        public IEnumerable<FormField> FormFields { get; set; }

        #region Internal Helper Properties
        /// <summary>
        /// Screen
        /// </summary>
        internal Screen Screen { get; set; }
        /// <summary>
        /// Screen Section
        /// </summary>
        internal ScreenSection ScreenSection { get; set; }
        #endregion
    }
}
