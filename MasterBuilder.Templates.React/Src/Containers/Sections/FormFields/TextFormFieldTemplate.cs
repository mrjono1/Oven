using Humanizer;
using MasterBuilder.Request;
using System.Collections.Generic;

namespace MasterBuilder.Templates.React.Src.Containers.Sections.FormFields
{
    internal class TextFormFieldTemplate : IFormFieldTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;
        private readonly FormField FormField;

        /// <summary>
        /// Constructor
        /// </summary>
        public TextFormFieldTemplate(Project project, Screen screen, ScreenSection screenSection, FormField formField)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
            FormField = formField;
        }

        public string Elements
        {
            get
            {
                return $@"        <TextField
          name=""{FormField.InternalNameJavaScript}""
          label=""{FormField.TitleValue}""
          value={{{ScreenSection.Entity.InternalName.Camelize()}Item.{FormField.InternalNameJavaScript}}}
        />";
            }
        }

        public IEnumerable<string> Imports => new string[] { "import TextField from '@material-ui/core/TextField';" };
    }
}
