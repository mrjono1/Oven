using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.Entities
{
    public class EntityPropertyTemplate
    {

        public static string Evaluate(Property property)
        {
            if (!property.HasCalculation)
            {
                return $@"      public {property.Type} {property.InternalName} {{ get; set; }}";
            }
            else
            {
                return $@"      public {property.Type} {property.InternalName} 
                                {{ 
                                    get
                                    {{
                                        return {property.Calculation};
                                    }}
                                }}";
            }
        }
    }
}
