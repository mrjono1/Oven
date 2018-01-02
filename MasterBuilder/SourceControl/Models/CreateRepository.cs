using System;

namespace MasterBuilder.SourceControl.Models
{

    /// <summary>
    /// VSTS API object
    /// </summary>
    public class CreateRepository
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Project
        /// </summary>
        public RepoProject Project { get; set; }

        /// <summary>
        /// Repository Project
        /// </summary>
        public class RepoProject
        {
            /// <summary>
            /// Identifier
            /// </summary>
            public Guid Id { get; set; }
        }
    }
}
