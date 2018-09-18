using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Reflection;

namespace Oven.Helpers
{
    /// <summary>
    /// Custom Contract Resolver
    /// </summary>
    public class CustomContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Create Property
        /// </summary>
        protected override JsonProperty CreateProperty(
            MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType != typeof(string) &&
                typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = instance =>
                {
                    // this value could be in public property
                    if (member.MemberType == MemberTypes.Property)
                    {
                        IEnumerable enumerable = instance
                                .GetType()
                                .GetProperty(member.Name)
                                .GetValue(instance, null) as IEnumerable;
                        return enumerable == null;
                    }
                    else
                    {
                        return true;
                    }
                };
            }

            return property;
        }
    }
}
