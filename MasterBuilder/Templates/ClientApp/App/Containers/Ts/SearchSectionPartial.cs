using Humanizer;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Ts
{
    /// <summary>
    /// Search Section Partial
    /// </summary>
    public class SearchSectionPartial
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;


        /// <summary>
        /// Constructor
        /// </summary>
        public SearchSectionPartial(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        internal IEnumerable<string> GetImports()
        {
            return new string[]
            {
                $"import {{ {ScreenSection.SearchSection.SearchItemClass} }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{ScreenSection.SearchSection.SearchItemClass}';",
                $"import {{ {ScreenSection.SearchSection.SearchRequestClass} }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{ScreenSection.SearchSection.SearchRequestClass}';",
                $"import {{ {ScreenSection.SearchSection.SearchResponseClass} }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{ScreenSection.SearchSection.SearchResponseClass}';",
                "import { Observable } from 'rxjs/Observable';",
                $"import {{ {Screen.InternalName}Service }} from '../../shared/{Screen.InternalName.ToLowerInvariant()}.service';"
            };
        }

        internal IEnumerable<string> GetConstructorParameters()
        {
            return new string[]
            {
                $"private {Screen.InternalName.Camelize()}Service: {Screen.InternalName}Service"
            };
        }

        internal IEnumerable<string> GetConstructorBodySections()
        {
            return new string[]
            {
                $@"        this.{ScreenSection.SearchSection.SearchRequestClass.Camelize()} = new {ScreenSection.SearchSection.SearchRequestClass}();
        this.{ScreenSection.SearchSection.SearchRequestClass.Camelize()}.page = 1;
        this.{ScreenSection.SearchSection.SearchRequestClass.Camelize()}.pageSize = 20;"
            };
        }

        internal IEnumerable<string> GetClassProperties()
        {
            var properties = new List<string>();
            foreach (var searchColumn in ScreenSection.SearchSection.SearchColumns.OrderBy(a => a.Ordinal).ThenBy(a => a.Title))
            {
                switch (searchColumn.PropertyType)
                {
                    case PropertyType.PrimaryKey:
                    case PropertyType.ParentRelationshipOneToMany:
                        // Skip as not displayed
                        break;
                    default:
                        properties.Add($"'{searchColumn.InternalNameCSharp.Camelize()}'");
                        break;
                }
            }
            
            return new string[]
            {
                $"public {ScreenSection.SearchSection.SearchResponseClass.Camelize()}: {ScreenSection.SearchSection.SearchResponseClass};",
                $"public {ScreenSection.SearchSection.SearchRequestClass.Camelize()}: {ScreenSection.SearchSection.SearchRequestClass};",
                $"public {ScreenSection.InternalName.Camelize()}Columns = [{string.Join(",", properties)}];",
                $"public {ScreenSection.InternalName.Camelize()}DataSource = new Observable<{ScreenSection.SearchSection.SearchItemClass}[]>();"
        };
        }

        internal IEnumerable<string> GetOnNgInitBodySections()
        {
            string parentPropertyFilterString = null;
            Entity parentEntity = null;
            Property parentProperty = (from p in ScreenSection.SearchSection.Entity.Properties
                                  where p.PropertyType == PropertyType.ParentRelationshipOneToMany
                                  select p).SingleOrDefault();
            if (parentProperty != null)
            {
                parentEntity = (from s in Project.Entities
                                where s.Id == parentProperty.ParentEntityId
                                select s).SingleOrDefault();
                parentPropertyFilterString = $"this.{ScreenSection.SearchSection.SearchRequestClass.Camelize()}.{parentEntity.InternalName.Camelize()}Id = params.id;";
            }

            return new string[]
            {
                $@"        this.route.params.subscribe(params => {{
            this.{ScreenSection.SearchSection.SearchRequestClass.Camelize()} = new {ScreenSection.SearchSection.SearchRequestClass}();
            this.{ScreenSection.SearchSection.SearchRequestClass.Camelize()}.page = 1;
            this.{ScreenSection.SearchSection.SearchRequestClass.Camelize()}.pageSize = 20;
            {parentPropertyFilterString}

            this.{ScreenSection.InternalName.Camelize()}DataSource = this.{Screen.InternalName.Camelize()}Service.{Screen.InternalName.Camelize()}{ScreenSection.InternalName.Pluralize()};
            this.{Screen.InternalName.Camelize()}Service.load{Screen.InternalName}{ScreenSection.InternalName}(this.{ScreenSection.SearchSection.SearchRequestClass.Camelize()});
        }});"
            };
        }
    }
}
