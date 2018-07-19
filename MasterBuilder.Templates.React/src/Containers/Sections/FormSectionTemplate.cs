using Humanizer;
using MasterBuilder.Request;
using MasterBuilder.Templates.React.Src.Containers.Sections.FormFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.React.src.Containers.Sections
{
    /// <summary>
    /// Form Section Builder
    /// </summary>
    public class FormSectionTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormSectionTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;

            Evaluate();
        }

        public string Body { get; private set; }

        private void Evaluate()
        {
            var formFieldTemplates = new List<IFormFieldTemplate>();

            foreach (var formField in ScreenSection.FormSection.FormFields)
            {
                switch (formField.PropertyType)
                {
                    case PropertyType.PrimaryKey:
                        break;
                    case PropertyType.String:
                        formFieldTemplates.Add(new TextFormFieldTemplate(Project, Screen, ScreenSection, formField));
                        break;
                    case PropertyType.Integer:
                        break;
                    case PropertyType.DateTime:
                        break;
                    case PropertyType.Boolean:
                        break;
                    case PropertyType.ParentRelationshipOneToMany:
                        break;
                    case PropertyType.ReferenceRelationship:
                        break;
                    case PropertyType.Double:
                        break;
                    case PropertyType.ParentRelationshipOneToOne:
                        break;
                    case PropertyType.Uniqueidentifier:
                        break;
                    case PropertyType.Spatial:
                        break;
                    default:
                        break;
                }
            }

            var imports = new List<string>();
            var fields = new List<string>();

            foreach (var template in formFieldTemplates)
            {
                imports.AddRange(template.Imports);
                fields.Add(template.Elements);
            }

            Body = $@"      <form noValidate>
{string.Join(Environment.NewLine, fields)}
      </form>";

            _imports = imports.Distinct();
        }
        
        internal IEnumerable<string> ComponentWillMount()
        {
            return new string[]
            {
                $@"        if (!this.props.new){{
            this.props.{ScreenSection.Entity.InternalName.Camelize()}Actions.fetchItemIfNeeded(this.props.id);
        }}"
            };
        }

        internal IEnumerable<string> Props()
        {
            return new string[] 
            {
                $"{ScreenSection.Entity.InternalName.Camelize()}Item"
            };
        }

        internal IEnumerable<string> Render()
        {
            return new string[]
            {
                $@"        if (!{ScreenSection.Entity.InternalName.Camelize()}Item) {{
            return (
                <Paper>Loading...</Paper>
            );
        }}"
            };
        }

        internal IEnumerable<string> MapDispatchToProps()
        {
            return new string[]
            {
                $"{ScreenSection.Entity.InternalName.Camelize()}Actions: bindActionCreators(createEntityActions('{ScreenSection.Entity.InternalName.Camelize()}', '{ScreenSection.Entity.InternalNamePlural.Camelize()}', '{ScreenSection.Entity.InternalName.ToUpperInvariant()}'), dispatch)"
            };
        }

        internal IEnumerable<string> MapStateToProps()
        {
            return new string[]
            {
                "id: ownProps.match.params.id",
                "new: ownProps.match.params.id === 'new'",
                $"{ScreenSection.Entity.InternalName.Camelize()}Item: ownProps.match.params.id !== 'new' ? state.{ScreenSection.Entity.InternalName.Camelize()}.byId[ownProps.match.params.id] : {{}}"
            };
        }
        
        private IEnumerable<string> _imports;
        internal IEnumerable<string> Imports()
        {
            var imports = new List<string>();

            imports.AddRange(_imports);

            return imports;
        }
    }
}
