using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.CoreModels
{
    public class IRequestTemplate: ITemplate
    {
        private Project Project { get; set; }

        public IRequestTemplate(Project project)
        {
            Project = project;
        }

        public string GetFileName()
        {
            return "IRequest.cs";
        }

        public string[] GetFilePath()
        {
            return new[] { "CoreModels" };
        }

        public string GetFileContent()
        {
            return $@"namespace {Project.InternalName}.CoreModels
{{
    public class IRequest
    {{
        public object cookies {{ get; set; }}
        public object headers {{ get; set; }}
        public object host {{ get; set; }}
    }}
}}";
        }
    }
}