using System;

namespace MasterBuilder.SourceControl.Models
{
    /// <summary>
    /// VSTS API object
    /// </summary>
    public class GetProject
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GetProject() { }

        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }
    }
}
