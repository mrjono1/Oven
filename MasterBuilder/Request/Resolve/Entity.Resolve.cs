using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Request
{
    public partial class Entity
    {
        /// <summary>
        /// Fills in any missing values and records it can to assist templating
        /// </summary>
        internal bool Resolve(Project project, out string message)
        {
            var errors = new List<string>();

            GenerateScreen(project, this);

            // Resolve child records
            foreach (var properties in Properties)
            {
                if (!properties.Resolve(project, this, out string propertyMessage))
                {
                    errors.Add(propertyMessage);
                }
            }

            if (errors.Any())
            {
                message = string.Join(Environment.NewLine, errors);
                return false;
            }
            else
            {
                message = "Success";
                return true;
            }
        }

        private void GenerateScreen(Project project, Entity entity)
        {
            if (EntityTemplate == EntityTemplateEnum.Reference)
            {
                var screenFound = project.Screens.Where(s => s.EntityId == Id).Any();

                if (screenFound)
                {
                    return;
                }

                if (Properties == null || !Properties.Any() || !Properties.Any(p => p.PropertyType == PropertyTypeEnum.PrimaryKey))
                {
                    var properties = new List<Property>
                    {
                        new Property()
                        {
                            Id = entity.Id,
                            Title = "Id",
                            InternalName = "Id",
                            PropertyType = PropertyTypeEnum.PrimaryKey
                        }
                    };
                    if (Properties != null)
                    {
                        properties.AddRange(Properties);
                    }
                    Properties = properties;
                }

                var editScreenId = Properties.SingleOrDefault(p => p.PropertyType == PropertyTypeEnum.PrimaryKey).Id; // Just grabing a reproduceable id
                var screens = new List<Screen>(project.Screens)
                {
                    new Screen()
                    {
                        Id = Id, // TODO: The id should be reproduceable I don't like this
                        EntityId = Id,
                        Title = Title.Pluralize(),
                        ScreenType = ScreenTypeEnum.Search,
                        Path = InternalNamePlural.Kebaberize(),
                        NavigateToScreenId = editScreenId,
                        Template = ScreenTemplateEnum.Reference
                    },
                    new Screen()
                    {
                        Id = editScreenId,
                        EntityId = Id,
                        Title = Title,
                        ScreenType = ScreenTypeEnum.Edit,
                        Path = InternalName.Kebaberize()
                    }
                };
                project.Screens = screens;
            }
        }
    }
}