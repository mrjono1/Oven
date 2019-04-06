using Oven.Request;

namespace Oven.Templates.Api.Utilities
{
    /// <summary>
    /// Required ObjectId Attribute Template
    /// </summary>
    public class RequiredObjectIdAttributeTemplate
    {
        /// <summary>
        /// File name
        /// </summary>
        public static string FileName()
        {
            return "RequiredObjectIdAttribute.cs";
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
    sealed public class RequiredObjectIdAttribute : ValidationAttribute
    {{
        public override bool IsValid(Object value)
        {{
            if (value == null || (ObjectId)value == ObjectId.Empty)
                return false;

            return true;
        }}
    }}
}}";
        }
    }
}
