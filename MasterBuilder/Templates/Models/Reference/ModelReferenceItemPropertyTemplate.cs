using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Reference Item Property Template
    /// </summary>
    public class ModelReferenceItemPropertyTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        public static string Evaluate(Property property)
        {
            if (property.PropertyType == PropertyType.PrimaryKey)
            {
                return $@"        /// <summary>
        /// {property.Entity.Title} {property.Title}
        /// </summary>
        [Display(Name = ""{property.Entity.Title} {property.Title}"")]
        [Required]
        public {property.GetTypeCs(true)} {property.Entity.InternalName}{property.InternalNameCSharp} {{ get; set; }}";
            }
            else
            {
                return $@"        /// <summary>
        /// {property.Title}
        /// </summary>
        [Display(Name = ""{property.Title}"")]
        [Required]
        public {property.GetTypeCs(true)} {property.InternalNameCSharp} {{ get; set; }}";
            }
        }
    }
}
