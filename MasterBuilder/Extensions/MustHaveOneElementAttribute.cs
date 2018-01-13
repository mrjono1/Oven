using System.Collections;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Must have at least one Element
    /// </summary>
    public class MustHaveOneElementAttribute : ValidationAttribute
    {
        /// <summary>
        /// Required non default attribute
        /// </summary>
        public MustHaveOneElementAttribute()
            : base("The {0} field requires at least 1 element.")
        {
        }

        /// <summary>
        /// Is Valid
        /// </summary>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (value is IList list)
            {
                return list.Count > 0;
            }
            return false;
        }
    }
}
