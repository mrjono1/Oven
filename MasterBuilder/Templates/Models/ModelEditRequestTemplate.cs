﻿using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    public class ModelEditRequestTemplate
    {
        public static string FileName(string folder, Entity entity, Screen screen)
        {
            var path = FileHelper.CreateFolder(folder, screen.InternalName);
            return Path.Combine(path, $"{screen.InternalName}Request.cs");
        }

        public static string Evaluate(Project project, Entity entity, Screen screen)
        {
            StringBuilder properties = null;
            if (entity.Properties != null)
            {
                properties = new StringBuilder();
                foreach (var item in entity.Properties)
                {
                    properties.AppendLine(ModelPropertyTemplate.Evaluate(item, true));
                }
            }

            return $@"using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace {project.InternalName}.Models
{{
    public class {screen.InternalName}Request
    {{
{properties}
    }}
}}";
        }


    }
}