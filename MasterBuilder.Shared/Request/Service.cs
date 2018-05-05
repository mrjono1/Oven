using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Service
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Register of all Entity Template Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, ServiceType> ServiceTypeDictonary = new Dictionary<Guid, ServiceType>
        {
            { new Guid("{87D5B4C5-9862-4282-8BB2-E2707A17036A}"), ServiceType.WebService },
            { new Guid("{DD6A4434-16E1-41BF-B75B-C78E3A8D46BF}"), ServiceType.ExportService }
        };

        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        [Required]
        [NonDefault]
        public Guid Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        private string internalName;
        /// <summary>
        /// Calculated Internal Name
        /// </summary>
        [JsonIgnore]
        public string InternalName
        {
            get
            {
                if (internalName == null)
                {
                    internalName = Title.Dehumanize();
                }
                return internalName;
            }
        }
        /// <summary>
        /// Service Type
        /// </summary>
        [Required]
        [NonDefault]
        public Guid ServiceTypeId { get; set; }
        /// <summary>
        /// Service Type
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public ServiceType ServiceType
        {
            get
            {
                return ServiceTypeDictonary[ServiceTypeId];
            }
            set
            {
                ServiceTypeId = ServiceTypeDictonary.SingleOrDefault(v => v.Value == value).Key;
            }
        }
        /// <summary>
        /// Required if Service Type = Web Service
        /// </summary>
        public WebService WebService { get; set; }
        /// <summary>
        /// Requried if Service Type = Export Service
        /// </summary>
        public ExportService ExportService { get; set; }
    }
}
