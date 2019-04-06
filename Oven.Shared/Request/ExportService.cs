using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;

namespace Oven.Request
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
        public ObjectId Id { get; set; }
        /// <summary>
        /// Entity Id
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId EntityId { get; set; }
    }
}
