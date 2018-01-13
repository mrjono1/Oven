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
        internal static readonly Dictionary<Guid, ServiceTypeEnum> ServiceTypeDictonary = new Dictionary<Guid, ServiceTypeEnum>
        {
            { new Guid("{87D5B4C5-9862-4282-8BB2-E2707A17036A}"), ServiceTypeEnum.WebService },
            { new Guid("{DD6A4434-16E1-41BF-B75B-C78E3A8D46BF}"), ServiceTypeEnum.ExportService }
        };

        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        [RequiredNonDefault]
        public Guid Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        private string _internalName;
        /// <summary>
        /// Calculated Internal Name
        /// </summary>
        internal string InternalName
        {
            get
            {
                if (_internalName == null)
                {
                    _internalName = Title.Dehumanize();
                }
                return _internalName;
            }
        }
        /// <summary>
        /// Service Type
        /// </summary>
        [RequiredNonDefault]
        public Guid ServiceTypeId { get; set; }
        /// <summary>
        /// Service Type
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public ServiceTypeEnum ServiceType
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
