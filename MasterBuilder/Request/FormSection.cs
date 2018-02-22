using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Form Section Settings
    /// </summary>
    public partial class FormSection
    {
        /// <summary>
        /// Form Fields
        /// </summary>
        [MustHaveOneElement]
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
        /// <summary>
        /// Entity
        /// </summary>
        internal Entity Entity { get; set; }
        /// <summary>
        /// Form Response Class
        /// </summary>
        internal string FormResponseClass
        {
            get
            {
                return $"{Screen.InternalName}Response";
            }
        }
        /// <summary>
        /// Form Request Class
        /// </summary>
        internal string FormRequestClass
        {
            get
            {
                return $"{Screen.InternalName}Request";
            }
        }
        #endregion
    }
}
