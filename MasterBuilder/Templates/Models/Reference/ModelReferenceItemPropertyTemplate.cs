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
            return $@"        /// <summary>
        /// {property.Title}
        /// </summary>
        [Display(Name = ""{property.Title}"")]
        [Required]
        public {property.GetTypeCs(true)} {property.InternalNameCSharp} {{ get; set; }}";
        }
    }
}
