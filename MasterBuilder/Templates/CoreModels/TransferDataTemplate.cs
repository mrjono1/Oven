using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.CoreModels
{
    public class TransferDataTemplate: ITemplate
    {
        private Project Project { get; set; }

        public TransferDataTemplate(Project project)
        {
            Project = project;
        }

        public string GetFileName()
        {
            return "TransferData.cs";
        }

        public string GetFilePath()
        {
            return "CoreModels";
        }

        public string GetFileContent()
        {
            return $@"namespace {Project.InternalName}.CoreModels
{{
  public class TransferData
  {{
    public dynamic request {{ get; set; }}

    // Your data here ?
    public object thisCameFromDotNET {{ get; set; }}
  }}
}}";
        }
    }
}
