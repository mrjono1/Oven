using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oven.Request
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
        [JsonIgnore]
        public Screen Screen { get; set; }
        /// <summary>
        /// Screen Section
        /// </summary>
        [JsonIgnore]
        public ScreenSection ScreenSection { get; set; }
        /// <summary>
        /// Entity
        /// </summary>
        [JsonIgnore]
        public Entity Entity { get; set; }
        #endregion
    }
}
