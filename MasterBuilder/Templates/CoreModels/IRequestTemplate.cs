using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

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

        public string GetFilePath()
        {
            return "Models";
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