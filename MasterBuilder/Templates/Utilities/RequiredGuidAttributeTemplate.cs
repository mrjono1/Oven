using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.Utilities
{
    public class RequiredGuidAttributeTemplate
    {
        public static string FileName()
        {
            return "RequiredGuidAttribute.cs";
        }
        public static string Evaluate(Project project)
        {
            return $@"using System;
using System.ComponentModel.DataAnnotations;

namespace {project.InternalName}
{{
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class RequiredGuidAttribute : ValidationAttribute
    {{
        public override bool IsValid(Object value)
        {{
            if (value == null || (Guid)value == Guid.Empty)
                return false;

            return true;
        }}
    }}
}}";
        }
    }
}
