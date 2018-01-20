using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.ClientApp.App.Models.Reference
{
    /// <summary>
    /// Reference response template
    /// </summary>
    public class ReferenceResponseTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly FormField FormField;

        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceResponseTemplate(Project project, Screen screen, FormField formField)
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
            return $"{FormField.ReferenceResponseClass}.ts";
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
            return $@"import {{ {FormField.ReferenceItemClass} }} from './{FormField.ReferenceItemClass}';
import {{ Observable }} from 'rxjs/Observable';

export class {FormField.ReferenceResponseClass} {{
    items: Observable<{FormField.ReferenceItemClass}>;
    totalPages: number;
    totalItems: number;
}}";
        }
    }
}
