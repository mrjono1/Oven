using System;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        [NonDefault]
        public Guid Id { get; set; }
        /// <summary>
        /// Entity Id
        /// </summary>
        [Required]
        [NonDefault]
        public Guid EntityId { get; set; }
    }
}
