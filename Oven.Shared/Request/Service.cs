using Humanizer;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Oven.Request
{
    /// <summary>
    /// Service
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Register of all Entity Template Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<ObjectId, ServiceType> ServiceTypeDictonary = new Dictionary<ObjectId, ServiceType>
        {
            { new ObjectId("5ca8770b4a73264e4c06e00d"), ServiceType.WebService },
            { new ObjectId("5ca8770b4a73264e4c06e00e"), ServiceType.ExportService }
        };

        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId Id { get; set; }
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
        public ObjectId ServiceTypeId { get; set; }
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
