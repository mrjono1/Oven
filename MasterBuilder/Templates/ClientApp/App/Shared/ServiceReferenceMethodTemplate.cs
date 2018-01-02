using MasterBuilder.Request;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App.Shared
{
    /// <summary>
    /// Service Reference Method Template
    /// </summary>
    public class ServiceReferenceMethodTemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        private bool _generateMethod;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="project"></param>
        /// <param name="entity"></param>
        public ServiceReferenceMethodTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
            
            _generateMethod = (from e in Project.Entities
                            where e.Properties != null
                            from property in e.Properties
                            where property.Type == PropertyTypeEnum.ReferenceRelationship &&
                            property.ParentEntityId.HasValue &&
                            property.ParentEntityId.Value == Entity.Id
                            select entity).Any();
        }

        /// <summary>
        /// Imports
        /// </summary>
        public string[] Imports()
        {
            if (_generateMethod)
            {
                return new string[] {
                    $"import {{ {Entity.InternalName}ReferenceItem }} from '../models/{Entity.InternalName.ToLowerInvariant()}/{Entity.InternalName}ReferenceItem';",
                    $"import {{ ReferenceRequest }} from '../models/ReferenceRequest';",
                    $"import {{ {Entity.InternalName}ReferenceResponse }} from '../models/{Entity.InternalName.ToLowerInvariant()}/{Entity.InternalName}ReferenceResponse';"
                };
            }
            else
            {
                return new string[] { };
            }
        }

        /// <summary>
        /// Method
        /// </summary>
        internal string Method()
        {
            if (_generateMethod)
            {
                return $@"  get{Entity.InternalName}Reference(request: ReferenceRequest){{
        return this.http.post<{Entity.InternalName}ReferenceResponse>(`${{this.baseUrl}}/api/{Entity.InternalName}/{Entity.InternalName}Reference`, request);
    }}";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
