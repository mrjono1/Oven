using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Request
{
    /// <summary>
    /// Project - Resolve
    /// </summary>
    partial class Project
    {
        /// <summary>
        /// Fills in any missing values and records it can to assist templating
        /// </summary>
        internal bool Resolve(out string message)
        {
            var errors = new List<string>();

            // Set Default Values for nullable fields
            if (!ImutableDatabase.HasValue)
            {
                ImutableDatabase = true;
            }
            if (!DefaultScreenId.HasValue)
            {
                DefaultScreenId = Screens.FirstOrDefault().Id;
            }

            // Still could be null if no screens have been setup yet
            if (DefaultScreenId.HasValue)
            {
                DefaultScreen = Screens.Single(a => a.Id == DefaultScreenId);
            }

            // Resolve child records
            foreach (var entity in Entities)
            {
                if (!entity.Resolve(this, out string entityMessage))
                {
                    errors.Add(entityMessage);
                }
            }
            foreach (var screen in Screens)
            {
                if (!screen.Resolve(this, out string screenMessage))
                {
                    errors.Add(screenMessage);
                }
            }

            GenerateAdminScreenAndMenu();

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


        private void GenerateAdminScreenAndMenu()
        {
            var administrationScreen = Screens.Where(s => s.InternalName.Equals("Administration", StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

            if (administrationScreen == null)
            {
                administrationScreen = new Screen()
                {
                    Id = new Guid("{43037072-42F2-4B5C-A72E-1A08F149709A}"),
                    Title = "Administration",
                    ScreenType = ScreenType.Html,
                    Path = "administration"
                };

                var screens = new List<Screen>(Screens)
                {
                    administrationScreen
                };
                Screens = screens.ToArray();

                var menus = new List<MenuItem>(MenuItems)
                {
                    new MenuItem()
                    {
                        ScreenId = administrationScreen.Id,
                        Title = "Administration"
                    }
                };
                MenuItems = menus;
            }

            ScreenSection administrationSection = null;

            if (administrationScreen.ScreenSections != null)
            {
                administrationSection = administrationScreen.ScreenSections.Where(s => s.InternalName.Equals("Administration", StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            }

            if (administrationSection == null)
            {
                var menuItems = new List<MenuItem>();

                foreach (var screen in Screens.Where(a => a.Template == ScreenTemplate.Reference))
                {
                    var menuItem = new MenuItem
                    {
                        ScreenId = screen.Id,
                        MenuItemType = MenuItemType.ApplicationLink
                    };
                    menuItems.Add(menuItem);
                }

                administrationSection = new ScreenSection()
                {
                    Id = new Guid("{0F93AE3B-930D-4F6B-B73F-2EB63F225FAD}"),
                    InternalName = "Administration",
                    Title = "Administration",
                    ScreenSectionType = ScreenSectionType.MenuList,
                    MenuListMenuItems = menuItems
                };

                var sections = new List<ScreenSection>(){
                    administrationSection
                };
                if (administrationScreen.ScreenSections != null)
                {
                    sections.AddRange(administrationScreen.ScreenSections);
                }
                administrationScreen.ScreenSections = sections.ToArray();
            }
        }
    }
}
