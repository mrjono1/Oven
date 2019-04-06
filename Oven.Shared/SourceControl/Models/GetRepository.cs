using MongoDB.Bson;
using System;

namespace Oven.SourceControl.Models
{
    /// <summary>
    /// VSTS API object
    /// </summary>
    public class GetRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GetRepository() { }

        /// <summary>
        /// Identifier
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Project
        /// </summary>
        public GetProject Project { get; set; }

        /// <summary>
        /// Default Branch
        /// </summary>
        public string DefaultBranch { get; set; }

        /// <summary>
        /// Remote Url
        /// </summary>
        public string RemoteUrl { get; set; }
    }
}
