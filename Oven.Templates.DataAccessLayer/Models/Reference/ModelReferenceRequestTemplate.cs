using Oven.Interfaces;
using Oven.Request;
using Oven.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.DataAccessLayer.Models.Reference
{
    /// <summary>
    /// Model Search Request Template
    /// </summary>
    public class ModelReferenceRequestTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly FormField FormField;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelReferenceRequestTemplate(Project project, Screen screen, FormField formField)
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
            return $"{FormField.ReferenceRequestClass}.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Models", Screen.InternalName, "Reference" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var propertyStrings = new List<string>();
            if (FormField.Property.FilterExpression != null)
            {
                var expressionPartial = new CsExpressionPartial(FormField.Property, FormField.Property.FilterExpression);
                var properties = expressionPartial.GetFilterProperties();

                foreach (var property in properties)
                {
                    propertyStrings.Add(ModelReferenceItemPropertyTemplate.Evaluate(property));
                }
            }

            return $@"using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace {Project.InternalName}.DataAccessLayer.Models.{Screen.InternalName}.Reference
{{
    /// <summary>
    /// {FormField.TitleValue} Reference Request
    /// </summary>
    public class {FormField.ReferenceRequestClass}
    {{
        /// <summary>
        /// Page
        /// </summary>
        [Required]
        [DefaultValue(1)]
        public int Page {{ get; set; }}
        /// <summary>
        /// Page Size
        /// </summary>
        [Required]
        [DefaultValue(10)]
        public int PageSize {{ get; set; }}
        /// <summary>
        /// Query
        /// </summary>
        public string Query {{ get; set; }}{(propertyStrings.Any() ? Environment.NewLine : string.Empty)}{string.Join(Environment.NewLine, propertyStrings)}
    }}
}}";
        }


    }
}
