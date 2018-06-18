using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Models.Reference
{
    /// <summary>
    /// Reference Request Template
    /// </summary>
    public class ReferenceRequestTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly FormField FormField;

        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceRequestTemplate(Project project, Screen screen, FormField formField)
        {
            Project = project;
            Screen = screen;
            FormField = formField;
        }
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{FormField.ReferenceRequestClass}.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "models", $"{Screen.InternalName.ToLowerInvariant()}" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var propertyStrings = new List<string>();
            if (FormField.Property.FilterExpression != null)
            {
                var expressionPartial = new Evaluate.TsExpressionPartial(Screen, Screen.ScreenSections);
                var properties = expressionPartial.GetFilterProperties(FormField.Property.FilterExpression);

                foreach (var property in properties)
                {
                    if (property.PropertyType == PropertyType.PrimaryKey)
                    {
                        propertyStrings.Add($@"    /**
    * {property.Property.Entity.Title} Id
    */
    {property.Property.Entity.InternalName.Camelize()}Id: {property.TypeTypeScript};");
                    }
                    else
                    {
                        propertyStrings.Add($@"    /**
    * {property.Title}
    */
    {property.InternalNameJavaScript}: {property.TypeTypeScript};");
                    }
                }
            }
            return $@"export class {FormField.ReferenceRequestClass} {{
    /**
    * Page Number
    */
    page: number;
    /**
    * Page Size
    */
    pageSize: number;
    /**
    * Query String
    */
    query: string;{(propertyStrings.Any() ? Environment.NewLine : string.Empty)}{string.Join(Environment.NewLine, propertyStrings)}
}}";
        }
    }
}
