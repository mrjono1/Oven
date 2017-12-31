using System;
using System.Collections.Generic;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Web Service
    /// </summary>
    public class WebService
    {
        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Default Base Endpoint: This can be overriden for each environment
        /// </summary>
        public string DefaultBaseEndpoint { get; set; }
        /// <summary>
        /// Operations
        /// </summary>
        public IEnumerable<WebServiceOperation> Operations { get; set; }
    }
}
