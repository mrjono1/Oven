using Newtonsoft.Json;
using System.Collections.Generic;

namespace Oven.Request
{
    /// <summary>
    /// Search Screen Section
    /// </summary>
    public class SearchSection
    {
        /// <summary>
        /// Search Columns
        /// </summary>
        public IEnumerable<SearchColumn> SearchColumns { get; set; }

        /// <summary>
        /// The <see cref="Property.InternalName"/> to order by, defaults to Title
        /// </summary>
        public string OrderBy { get; set; } = "Title";


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
        /// <summary>
        /// Search Item C#
        /// </summary>
        [JsonIgnore]
        public string SearchItemClass
        {
            get
            {
                if (Screen.ScreenType == ScreenType.Search)
                {
                    return $"{Screen.InternalName}Item";
                }
                else
                {
                    return $"{Screen.InternalName}{ScreenSection.InternalName}Item";
                }
            }
        }
        /// <summary>
        /// Search Request C#
        /// </summary>
        [JsonIgnore]
        public string SearchRequestClass
        {
            get
            {
                if (Screen.ScreenType == ScreenType.Search)
                {
                    return $"{Screen.InternalName}Request";
                }
                else
                {
                    return $"{Screen.InternalName}{ScreenSection.InternalName}Request";
                }
            }
        }
        /// <summary>
        /// Search Response C#
        /// </summary>
        [JsonIgnore]
        public string SearchResponseClass
        {
            get
            {
                if (Screen.ScreenType == ScreenType.Search)
                {
                    return $"{Screen.InternalName}Response";
                }
                else
                {
                    return $"{Screen.InternalName}{ScreenSection.InternalName}Response";
                }
            }
        }

        #endregion
    }
}
