using MasterBuilder.Request;
using System;
using System.Collections.Generic;
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
                $"import {{ {FormField.ReferenceRequestClass} }} from '../models/{Screen.InternalName.ToLowerInvariant()}/{FormField.ReferenceRequestClass}';",
                $"import {{ {FormField.ReferenceResponseClass} }} from '../models/{Screen.InternalName.ToLowerInvariant()}/{FormField.ReferenceResponseClass}';"
            };
        }

        /// <summary>
        /// Method
        /// </summary>
        internal string Method()
        {
            var parameters = new List<string>
            {
                "query: string",
                "page: number",
                "pageSize: number"
            };
            var propertyAssignment = new List<string>
            {
                "        request.query = query;",
                "        request.pageSize = pageSize;",
                "        request.page = page;"
            };

            if (FormField.Property.FilterExpression != null)
            {
                var expressionPartial = new Evaluate.TsExpressionPartial(Screen, Screen.ScreenSections);
                var properties = expressionPartial.GetFilterProperties(FormField.Property.FilterExpression);

                foreach (var property in properties)
                {
                    parameters.Add($@"{property.InternalNameTypeScript}: {property.TsType}");
                    propertyAssignment.Add($@"        request.{property.InternalNameTypeScript} = {property.InternalNameTypeScript};");
                }
            }

            return $@"    get{FormField.Property.InternalName}References({string.Join(", ", parameters)}){{
        let request = new {FormField.ReferenceRequestClass}();
{string.Join(Environment.NewLine, propertyAssignment)}
        return this.http.post<{FormField.ReferenceResponseClass}>(`${{this.baseUrl}}/api/{Screen.InternalName}/{FormField.Property.InternalName}References`, request);
    }}";

        }
    }
}
