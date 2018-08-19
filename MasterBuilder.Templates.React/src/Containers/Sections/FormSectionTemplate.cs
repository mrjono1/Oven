using Humanizer;
using MasterBuilder.Helpers;
using MasterBuilder.Request;
using MasterBuilder.Templates.React.Src.Containers.Sections.FormFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.React.Src.Containers.Sections
{
    /// <summary>
    /// Form Section Builder
    /// </summary>
    public class FormSectionTemplate : ISectionTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;
        private readonly ScreenItem ScreenItem;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormSectionTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;


            ScreenItem = RequestTransforms.GetScreenFieldsNested(Screen);
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

            var imports = new List<string>()
            {
                "import Button from '@material-ui/core/Button';"
            };
            var fields = new List<string>();

            foreach (var template in formFieldTemplates)
            {
                imports.AddRange(template.Imports);
                fields.Add(template.Elements);
            }

            Body = $@"<form noValidate autoComplete=""off"">
    <Button variant=""contained"" color=""primary"" type=""submit"" onClick={{this.onSubmit}}>
        Save
    </Button>
{string.Join(Environment.NewLine, fields).IndentLines(4)}
</form>";

            _imports = imports.Distinct();
        }

        public IEnumerable<string> Constructor()
        {
            return new string[]
            {
                "        this.onSubmit = this.onSubmit.bind(this);",
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
        public IEnumerable<string> Imports()
        {
            var imports = new List<string>();

            imports.AddRange(_imports);

            return imports;
        }

        public IEnumerable<string> Methods()
        {
            var methods =  new List<string> {
                $@"onSubmit(event) {{
    event.preventDefault();
    console.log('submit');
}}"
            };

            if (ScreenItem.FormFields.Any() || ScreenItem.ChildScreenItems.Any())
            {
                var properties = GetProperties(ScreenItem);
                methods.Add($@"defaultItem() {{
    return {{
{string.Join(string.Concat(",", Environment.NewLine), properties).IndentLines(8)}
    }};
}}");
            }


            return methods;
    //    if (this.props.new) {{
    //        var item = this.props.{ScreenSection.Entity.InternalName.Camelize()}Actions.createEntity(this.props.{ScreenSection.Entity.InternalName.Camelize()}Item);
    //        this.props.history.push(`/{Screen.Path}/${{item.id}}`);
    //    }} else {{
    //        this.props.history.push(`/{Screen.Path}/new`);
    //        //this.props.{ScreenSection.Entity.InternalName.Camelize()}Actions.updateEntity(this.props.id, this.props.{ScreenSection.Entity.InternalName.Camelize()}Item);
    //    }}
    //}}"
    //        };
        }

        private IEnumerable<string> GetProperties(ScreenItem screenItem, int indent = 0)
        {
            var properties = new List<string>();

            foreach (var formField in screenItem.FormFields)
            {
                properties.Add(GetProperty(formField));
            }

            foreach (var childScreenItem in screenItem.ChildScreenItems)
            {
                indent++;
                var childProperties = GetProperties(childScreenItem, indent);
                if (childProperties.Any())
                {
                    properties.Add($@"{childScreenItem.Entity.InternalName.Camelize()}: {{
{string.Join(string.Concat(",", Environment.NewLine), childProperties).IndentLines(4)}
}}");
                }
            }
//                if (effe.ChildEntities != null)
//                {
//                    foreach (var childEntityFormFieldEntity in effe.ChildEntities)
//                    {
//                        var childProperties = new List<string>();
//                        var childObjectName = $"{objectName}.{childEntityFormFieldEntity.InternalName}";
//                        foreach (var effe in groupedFields)
//                        {
//                            if (effe.Entity.Id == childEntityFormFieldEntity.Id)
//                            {
//                                childProperties.AddRange(GetProperties(effe, effes, childObjectName, level + 1));
//                            }
//                        }

//                        var parentPropertyInternalName = (from p in childEntityFormFieldEntity.Properties
//                                                          where p.PropertyType == PropertyType.ParentRelationshipOneToOne
//                                                          select p).Single().InternalName;

//                        properties.Add($@"                            {new string(' ', 4 * level)}{childEntityFormFieldEntity.InternalName} = {childObjectName} == null || !{childObjectName}.{parentPropertyInternalName}Id.HasValue ? null : new {childEntityFormFieldEntity.InternalName}Response{{
//{string.Join(string.Concat(",", Environment.NewLine), childProperties)}
//                            {new string(' ', 4 * level)}}}");
//                    }
//                }
//            }

            return properties.Distinct().OrderBy(a => a);
        }

        private string GetProperty(FormField formField)
        {
            var defaultValue = "null";

            switch (formField.PropertyType)
            {
                case PropertyType.String:
                    if (formField.Property.DefaultStringValue != null)
                    {
                        defaultValue = $"'{formField.Property.DefaultStringValue}'";
                    }
                    break;
                case PropertyType.Integer:
                    if (formField.Property.DefaultIntegerValue.HasValue)
                    {
                        defaultValue = formField.Property.DefaultIntegerValue.Value.ToString();
                    }
                    break;
                case PropertyType.Boolean:
                    if (formField.Property.DefaultBooleanValue.HasValue)
                    {
                        defaultValue = formField.Property.DefaultBooleanValue.Value ? "true" : "false";
                    }
                    break;
                case PropertyType.Double:
                    if (formField.Property.DefaultDoubleValue.HasValue)
                    {
                        defaultValue = formField.Property.DefaultDoubleValue.Value.ToString();
                    }
                    break;
            }

            return $"{formField.InternalNameJavaScript}: {defaultValue}";
        }
    }
}
