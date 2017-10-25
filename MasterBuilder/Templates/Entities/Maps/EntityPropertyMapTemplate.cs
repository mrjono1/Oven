using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.Entities.Maps
{
    public class EntityPropertyMapTemplate
    {

        public static string Evaluate(Property property)
        {
            switch (property.Type)
            {
                default:
                    break;
            }
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
