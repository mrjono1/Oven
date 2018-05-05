using Humanizer;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Shared
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

        internal string PrivateProperty
        {
            get
            {
                return $"_{FormField.ReferenceItemClass.Camelize()}";
            }
        }
        internal string DataStore
        {
            get
            {
                return $"{FormField.ReferenceItemClass.Camelize()}DataStore";
            }
        }
        internal string DataStoreProperty
        {
            get
            {
                return $"{FormField.ReferenceResponseClass.Camelize().Pluralize()}";
            }
        }

        internal IEnumerable<string> Properties()
        {
            return new string[]
            {
            $@"    private {PrivateProperty}: BehaviorSubject<{FormField.ReferenceItemClass}[]>;",
            $@"    private {DataStore}: {{
        {DataStoreProperty}: {FormField.ReferenceItemClass}[]
    }};"
            };
        }

        /// <summary>
        /// Method
        /// </summary>
        internal IEnumerable<string> Method()
        {
            var methods = new List<string>();

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
                    if (property.PropertyType == PropertyType.PrimaryKey)
                    {
                        parameters.Add($@"{property.Property.Entity.InternalName.Camelize()}Id: {property.TypeTypeScript}");
                        propertyAssignment.Add($@"            request.{property.Property.Entity.InternalName.Camelize()}Id = {property.Property.Entity.InternalName.Camelize()}Id;");
                    }
                    else
                    {
                        parameters.Add($@"{property.InternalNameTypeScript}: {property.TypeTypeScript}");
                        propertyAssignment.Add($@"            request.{property.InternalNameTypeScript} = {property.InternalNameTypeScript};");
                    }
                }
            }

//            methods.Add($@"    get{FormField.Property.InternalName}References({string.Join(", ", parameters)}){{
//        let request = new {FormField.ReferenceRequestClass}();
//{string.Join(Environment.NewLine, propertyAssignment)}
//        return this.http.post<{FormField.ReferenceResponseClass}>(`${{this.baseUrl}}/api/{Screen.InternalName}/{FormField.Property.InternalName}References`, request);
//    }}");
            // Get property
            methods.Add($@"    /**
     * Gets an observable list of {FormField.ReferenceItemClass}
     */
    get {FormField.Property.InternalName.Camelize()}References() {{
        return this.{PrivateProperty}.asObservable();
    }}");

            // Load method
            methods.Add($@"    load{FormField.Property.InternalName}References({string.Join(", ", parameters)}) {{
        let request = new {FormField.ReferenceRequestClass}();
{string.Join(Environment.NewLine, propertyAssignment)}
            this.http.post<{FormField.ReferenceResponseClass}>(`${{this.baseUrl}}/api/{Screen.InternalName}/{FormField.Property.InternalName}References`, request).subscribe(data => {{
            this.{DataStore}.{DataStoreProperty} = data.items;
            this.{PrivateProperty}.next(Object.assign({{}}, this.{DataStore}).{DataStoreProperty});
        }}, error => console.log('Could not load {FormField.ReferenceRequestClass}'));
    }}");
            return methods;
        }

        internal IEnumerable<string> ConstructorExpressions()
        {
            return new string[]{
                $@"        this.{DataStore} = {{ {DataStoreProperty}: [] }};",
                $@"        this.{PrivateProperty} = <BehaviorSubject<{FormField.ReferenceItemClass}[]>>new BehaviorSubject([]);"
            };
        }
    }
}
