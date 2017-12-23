using System;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Web Service Operation
    /// </summary>
    public class WebServiceOperation
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Internal Name
        /// </summary>
        public string InternalName { get; set; }
        /// <summary>
        /// Relative Route
        /// </summary>
        public string RelativeRoute { get; set; }
        // TODO: convert to relationship
        /// <summary>
        /// Http verb eg. GET, POST, PUT, PATCH
        /// </summary>
        public string Verb { get; set; }
    }
}
