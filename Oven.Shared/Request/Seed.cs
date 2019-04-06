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
    /// Seed Data and Settings
    /// </summary>
    public class Seed
    {
        /// <summary>
        /// Register of all Seed Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<ObjectId, SeedType> SeedTypeDictonary = new Dictionary<ObjectId, SeedType>
        {
            { new ObjectId("5ca8770b4a73264e4c06e006"), SeedType.AddIfNone },
            { new ObjectId("5ca8770b4a73264e4c06e007"), SeedType.EnsureAllAdded },
            { new ObjectId("5ca8770b4a73264e4c06e008"), SeedType.EnsureAllUpdated }
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
        public ObjectId SeedTypeId { get; set; }

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
