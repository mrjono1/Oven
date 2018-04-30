using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Screen Feature
    /// </summary>
    public class ScreenFeature
    {
        /// <summary>
        /// Register of all Feature Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, Feature> FeatureDictonary = new Dictionary<Guid, Feature>
        {
            { new Guid("{6114120E-BD93-4CE4-A673-7DC295F93CFE}"), Feature.New }
        };

        /// <summary>
        /// Identifier
        /// </summary>
        [Required]
        [NonDefault]
        public Guid Id { get; set; }
        /// <summary>
        /// Feature Id
        /// </summary>
        [Required]
        [NonDefault]
        public Guid FeatureId { get; set; }
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