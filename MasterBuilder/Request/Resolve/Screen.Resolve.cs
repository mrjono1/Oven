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
            
            if (ScreenSections == null || !ScreenSections.Any())
            {
                var screenSectionType = ScreenSectionTypeEnum.Html;
                if (EntityId.HasValue)
                {
                    switch (ScreenType)
                    {
                        case ScreenTypeEnum.Search:
                            screenSectionType = ScreenSectionTypeEnum.Search;
                            break;
                        case ScreenTypeEnum.Edit:
                            screenSectionType = ScreenSectionTypeEnum.Form;
                            break;
                        case ScreenTypeEnum.View:
                            screenSectionType = ScreenSectionTypeEnum.Form;
                            break;
                        case ScreenTypeEnum.Html:
                            screenSectionType = ScreenSectionTypeEnum.Html;
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
                        ScreenSectionType = screenSectionType,
                        NavigateToScreenId = NavigateToScreenId
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
