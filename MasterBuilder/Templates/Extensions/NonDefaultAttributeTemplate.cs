using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Extensions
{
    /// <summary>
    /// Non Default Attribute Template
    /// </summary>
    internal class NonDefaultAttributeTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public NonDefaultAttributeTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "NonDefaultAttribute.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new[] { "Extensions" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"namespace System.ComponentModel.DataAnnotations
{{
    /// <summary>
    /// Required non default attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NonDefaultAttribute : ValidationAttribute
    {{
        /// <summary>
        /// Required non default attribute
        /// </summary>
        public NonDefaultAttribute()
            : base(""The {{0}} field requires a non-default value."")
        {{
        }}

        /// <summary>
        /// Stops you from having to test for things like Guid.Empty
        /// </summary>
        /// <param name=""value"">The value to test</param>
        /// <returns><c>false</c> if the <paramref name=""value""/> is equal the default value of an instance of its own type.</returns>
        /// <remarks>Is meant for use with primitive types or structs like DateTime, Guid, etc. Specifically ignores null values so that it can be combined with RequiredAttribute. Should not be used with Strings.</remarks>
        /// <example>
        /// [NonDefault] 
        /// public Guid SomeId {{ get; set;}}
        /// </example>
        public override bool IsValid(object value)
        {{
            return value == null || !Equals(value, Activator.CreateInstance(value.GetType()));
        }}
    }}
}}";
        }
    }
}
