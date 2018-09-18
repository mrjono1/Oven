using Humanizer;
using Oven.Request;
using Oven.Templates.React.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oven.Templates.React.Src.Resources
{
    /// <summary>
    /// Input Field Template
    /// </summary>
    public class CreateFormSectionPartialTemplate
    {
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;
        public IEnumerable<string> Imports { get; private set; }
        public string Content { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateFormSectionPartialTemplate(Screen screen, ScreenSection screenSection, bool isCreate)
        {
            Screen = screen;
            ScreenSection = screenSection;

            Evaluate(isCreate);
        }

        private void Evaluate(bool isCreate)
        {
            var fields = new List<string>();
            var imports = new List<string>();
            foreach (var field in ScreenSection.FormSection.FormFields)
            {
                if (field.PropertyType == PropertyType.PrimaryKey)
                {
                    // dont render primary key
                    continue;
                }
                else if (field.PropertyType == PropertyType.ParentRelationshipOneToMany)
                {
                    // dont render parent relationship
                    continue;
                }
                var template = new CreateEditInputPartialTemplate(Screen, field, isCreate);
                fields.Add(template.Content());
                imports.AddRange(template.ReactAdminImports());

                if (template.WrapInFormDataConsumer)
                {
                    imports.Add("FormDataConsumer");
                }
            }
            if (fields.Any())
            {
                Content = string.Join(Environment.NewLine, fields);
            }

            Imports = imports;
        }
    }
}
