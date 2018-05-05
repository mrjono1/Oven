using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Seed Data and Settings
    /// </summary>
    public class Seed
    {
        /// <summary>
        /// Register of all Seed Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, SeedType> SeedTypeDictonary = new Dictionary<Guid, SeedType>
        {
            { new Guid("{8A07A94D-4A5F-420F-B02A-4B2223B1213B}"), SeedType.AddIfNone },
            { new Guid("{2729F45B-269F-42B1-BBA9-3E76DC9D1CBB}"), SeedType.EnsureAllAdded },
            { new Guid("{6989AE9F-D5BD-4861-ABE6-0142EDDE6130}"), SeedType.EnsureAllUpdated }
        };

        /// <summary>
        /// JSON Array of JSON objects that represent seed data for this entity
        /// </summary>
        public string JsonData { get; set; }

        /// <summary>
        /// Seed Type Id
        /// </summary>
        [Required]
        [NonDefault]
        public Guid SeedTypeId { get; set; }

        /// <summary>
        /// Seed Type
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public SeedType SeedType
        {
            get
            {
                return SeedTypeDictonary[SeedTypeId];
            }
            set
            {
                SeedTypeId = SeedTypeDictonary.SingleOrDefault(v => v.Value == value).Key;
            }
        }
    }
}
