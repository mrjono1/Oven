using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates
{
    public class EntityMapTemplate
    {
        public static string Evaluate(Project project, Entity entity)
        {
            return $@"
using System;

namespace {project.InternalName}.Entities
{{
    public class {entity.InternalName}
    {{
        
    {{
}}";
        }


    }
}
