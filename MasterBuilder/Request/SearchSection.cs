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
    }
}
