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
        private readonly Screen Screen;
        private readonly FormField FormField;

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceReferenceMethodTemplate(Project project, Screen screen, FormField formField)
        {
            Project = project;
            Screen = screen;
            FormField = formField;
        }

        /// <summary>
        /// Imports
        /// </summary>
        public string[] Imports()
        {
            return new string[] {
                $"import {{ {FormField.ReferenceItemClass} }} from '../models/{Screen.InternalName.ToLowerInvariant()}/{FormField.ReferenceItemClass}';",
                $"import {{ ReferenceRequest }} from '../models/ReferenceRequest';",
                $"import {{ {FormField.ReferenceResponseClass} }} from '../models/{Screen.InternalName.ToLowerInvariant()}/{FormField.ReferenceResponseClass}';"
            };
        }

        /// <summary>
        /// Method
        /// </summary>
        internal string Method()
        { 
            return $@"    get{FormField.Property.InternalName}References(query: string, page: number, pageSize: number){{
        let request = new ReferenceRequest();
        request.query = query;
        request.pageSize = pageSize;
        request.page = page;
        return this.http.post<{FormField.ReferenceResponseClass}>(`${{this.baseUrl}}/api/{Screen.InternalName}/{FormField.Property.InternalName}References`, request);
    }}";

        }
    }
}
