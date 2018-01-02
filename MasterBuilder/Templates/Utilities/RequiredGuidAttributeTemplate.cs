using MasterBuilder.Request;

namespace MasterBuilder.Templates.Utilities
{
    /// <summary>
    /// Required Guid Attribute Template
    /// </summary>
    public class RequiredGuidAttributeTemplate
    {
        /// <summary>
        /// File name
        /// </summary>
        public static string FileName()
        {
            return "RequiredGuidAttribute.cs";
        }

        /// <summary>
        /// Evaluate
        /// </summary>
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
