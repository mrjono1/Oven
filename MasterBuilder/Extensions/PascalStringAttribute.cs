using Humanizer;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Pascal String attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class PascalStringAttribute : ValidationAttribute
    {
        /// <summary>
        /// Pascal String attribute
        /// </summary>
        public PascalStringAttribute()
            : base("The {0} field is invalid.")
        {
        }

        /// <summary>
        /// Is Valid
        /// </summary>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var stringValue = value.ToString();
            var pascalValue = stringValue.Dehumanize();

            return stringValue.Equals(pascalValue, StringComparison.Ordinal);
        }
    }
}
