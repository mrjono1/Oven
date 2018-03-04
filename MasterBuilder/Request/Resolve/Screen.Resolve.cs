using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    public partial class Screen
    {
        /// <summary>
        /// Fills in any missing values and records it can to assist templating
        /// </summary>
        internal bool Resolve(Project project, out string message)
        {
            var errors = new List<string>();
            if (EntityId.HasValue)
            {
                Entity = project.Entities.Single(e => e.Id == EntityId.Value);
            }
            if (ScreenSections == null || !ScreenSections.Any())
            {
                var screenSectionType = ScreenSectionType.Html;
                if (EntityId.HasValue)
                {
                    switch (ScreenType)
                    {
                        case ScreenType.Search:
                            screenSectionType = ScreenSectionType.Search;
                            break;
                        case ScreenType.Form:
                            screenSectionType = ScreenSectionType.Form;
                            break;
                        case ScreenType.View:
                            screenSectionType = ScreenSectionType.Form;
                            break;
                        case ScreenType.Html:
                            screenSectionType = ScreenSectionType.Html;
                            break;
                        default:
                            break;
                    }
                }
                ScreenSections = new ScreenSection[]
                {
                    new ScreenSection
                    {
                        Id = Id,
                        Title = Title,
                        EntityId = EntityId,
                        InternalName = InternalName,
                        ScreenSectionType = screenSectionType
                    }
                };
            }
            else
            {
                foreach (var screenSection in ScreenSections)
                {
                    if (!screenSection.EntityId.HasValue)
                    {
                        screenSection.EntityId = EntityId;
                    }
                }
            }

            if (string.IsNullOrEmpty(Path))
            {
                Path = Title.Kebaberize();
            }

            if (MenuItems != null)
            {
                var firstScreenSection = ScreenSections.First();
                if (firstScreenSection != null)
                {
                    var menuItems = new List<MenuItem>();
                    if (firstScreenSection.MenuItems != null)
                    {
                        menuItems.AddRange(firstScreenSection.MenuItems);
                    }
                    menuItems.AddRange(MenuItems);

                    firstScreenSection.MenuItems = menuItems;
                }
            }

            // Resolve child records
            foreach (var screenSection in ScreenSections)
            {
                if (!screenSection.Resolve(project, this, out string screenSectionMessage))
                {
                    errors.Add(screenSectionMessage);
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
    }
}
