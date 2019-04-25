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
    /// Screen Feature
    /// </summary>
    public class ScreenFeature
    {
        /// <summary>
        /// Register of all Feature Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<ObjectId, Feature> FeatureDictonary = new Dictionary<ObjectId, Feature>
        {
            { new ObjectId("5ca877094a73264e4c06dff1"), Feature.New }
        };

        /// <summary>
        /// Identifier
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Feature Id
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId FeatureId { get; set; }
        /// <summary>
        /// Screen Type
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public Feature Feature
        {
            get
            {
                return FeatureDictonary[FeatureId];
            }
            set
            {
                FeatureId = FeatureDictonary.SingleOrDefault(v => v.Value == value).Key;
            }
        }
    }
}
