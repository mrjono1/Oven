using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    public class ModelPropertyTemplate
    {

        public static string Evaluate(Property property)
        {
            return $@"      public {property.Type} {property.InternalName} {{ get; set; }}";
        }
    }
}
