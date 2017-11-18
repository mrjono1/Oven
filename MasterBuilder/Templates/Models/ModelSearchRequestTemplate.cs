using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    public class ModelSearchRequestTemplate
    {
        public static string FileName(string folder, Entity entity, Screen screen, ScreenSection screenSection)
        {
            var path = FileHelper.CreateFolder(folder, screen.InternalName);
            if (screen.EntityId.HasValue && screenSection.EntityId.HasValue && screen.EntityId != screenSection.EntityId)
            {
                return Path.Combine(path, $"{screen.InternalName}{screenSection.InternalName}Request.cs");
            }
            else
            {
                return Path.Combine(path, $"{screen.InternalName}Request.cs");
            }
        }

        public static string Evaluate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
        {
            var className = $"{screen.InternalName}Request";
            if (screen.EntityId.HasValue && screenSection.EntityId.HasValue && screen.EntityId != screenSection.EntityId)
            {
                className = $"{screen.InternalName}{screenSection.InternalName}Request";
            }

            string parentPropertyString = null;
            Entity parentEntity = null;
            var sectionEntity = (from e in project.Entities
                          where e.Id == screenSection.EntityId
                          select e).SingleOrDefault();
            if (sectionEntity != null)
            {
                var parentProperty = (from p in sectionEntity.Properties
                                      where p.Type == PropertyTypeEnum.ParentRelationship
                                      select p).SingleOrDefault();
                if (parentProperty != null)
                {
                    parentEntity = (from s in project.Entities
                                         where s.Id == parentProperty.ParentEntityId
                                         select s).SingleOrDefault();
                    parentPropertyString = $"public Guid? {parentEntity.InternalName}Id {{ get; set; }}";
                }
            }

            return $@"using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace {project.InternalName}.Models
{{
    public class {className}
    {{
        [Required]
        [DefaultValue(1)]
        public int Page {{ get; set; }}
        [Required]
        [DefaultValue(10)]
        public int PageSize {{ get; set; }}
        {parentPropertyString}
        // TODO: Search fields
    }}
}}";
        }


    }
}
