using System.Collections.Generic;

namespace MasterBuilder.Request
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
        /// Search Item C#
        /// </summary>
        internal string SearchItemClassCSharp
        {
            get
            {
                if (Screen.ScreenType == ScreenTypeEnum.Search)
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
        internal string SearchRequestClassCSharp
        {
            get
            {
                if (Screen.ScreenType == ScreenTypeEnum.Search)
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
        internal string SearchResponseClassCSharp
        {
            get
            {
                if (Screen.ScreenType == ScreenTypeEnum.Search)
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
