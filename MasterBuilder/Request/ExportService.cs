using System;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Export Service
    /// </summary>
    public class ExportService
    {
        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Entity Id
        /// </summary>
        public Guid EntityId { get; set; }
    }
}
