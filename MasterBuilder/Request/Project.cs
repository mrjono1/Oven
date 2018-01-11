using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Project
    /// </summary>
    public class Project
    {
        internal static readonly Guid MasterBuilderId = new Guid("{D1CB7777-6E61-486B-B15E-05B97B57D0FC}");
        /// <summary>
        /// Constructor sets default values
        /// </summary>
        public Project()
        {
            CleanDirectoryIgnoreDirectories = new string[]
            {
                "bin",
                "obj",
                "node_modules",
                "wwwroot",
                "Properties",
                "dist"
            };

            DefaultNugetReferences = new Dictionary<string, string>
            {
                { "Microsoft.AspNetCore.All", "2.0.3" },
                { "Microsoft.EntityFrameworkCore", "2.0.1"},
                { "Microsoft.EntityFrameworkCore.SqlServer", "2.0.1"},
                { "Microsoft.AspNetCore.JsonPatch", "2.0.0" },
                { "Swashbuckle.AspNetCore", "1.1.0" },
                { "Swashbuckle.AspNetCore.Swagger", "1.1.0" },
                { "Swashbuckle.AspNetCore.SwaggerUi", "1.1.0" },
                { "RestSharp", "106.1.0"}
            };
            ImutableDatabase = true;
        }
        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Internal Name
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Major version
        /// </summary>
        public int MajorVersion { get; set; }
        /// <summary>
        /// Minor Version
        /// </summary>
        public int MinorVersion { get; set; }
        /// <summary>
        /// Build Version
        /// </summary>
        public int BuildVersion { get; set; }

        /// <summary>
        /// Semantic Version
        /// </summary>
        internal string Version
        {
            get
            {
                return $"{MajorVersion}.{MinorVersion}.{BuildVersion}";
            }
        }
        /// <summary>
        /// Entities
        /// </summary>
        public IEnumerable<Entity> Entities { get; set; }
        /// <summary>
        /// Screens
        /// </summary>
        public IEnumerable<Screen> Screens { get; set; }
        /// <summary>
        /// Menu Items
        /// </summary>
        public IEnumerable<MenuItem> MenuItems { get; set; }
        /// <summary>
        /// Service configurations
        /// </summary>
        public IEnumerable<Service> Services { get; set; }

        /// <summary>
        /// Folders to ignore when cleaning out the directory on build
        /// </summary>
        public string[] CleanDirectoryIgnoreDirectories { get; set; }
        /// <summary>
        /// SQL Database Connection String
        /// </summary>
        public string DatabaseConnectionString { get; set; }
        /// <summary>
        /// If true database tables and columns are all uniqueidentifiers making database 
        /// hard to use but less chance of needing to change database columns which can result in data loss
        /// </summary>
        public bool? ImutableDatabase { get; set; }
        /// <summary>
        /// Default Screen to load
        /// </summary>
        public Guid? DefaultScreenId { get; set; }
        /// <summary>
        /// The List of Default Nuget Packages
        /// </summary>
        internal Dictionary<string, string> DefaultNugetReferences { get; set; }
        /// <summary>
        /// Allow Destructive Database Change, things like dropping columns, tables, keys
        /// </summary>
        public bool AllowDestructiveDatabaseChanges { get; set; }
        
        /// <summary>
        /// Validate a whole project, also may resolve some issues or perform upgrades
        /// </summary>
        /// <param name="message">returns "Success" or details an issue found</param>
        internal bool Validate(out string message)
        {
            var messages = new List<string>();

            // Validate Entities
            if (Entities == null || !Entities.Any())
            {
                messages.Add("No Entities have been defined");
                message = string.Join(Environment.NewLine, messages);
                return false;
            }
            foreach (var entity in Entities)
            {
                if (!entity.Validate(this, out string entityMessage))
                {
                    messages.Add(entityMessage);
                }
            }
            
            // Validate Screens
            if (Screens == null || !Screens.Any())
            {
                messages.Add("No Screens have been defined");
                message = string.Join(Environment.NewLine, messages);
                return false;
            }
            if (Screens.Select(i => i.Id).Distinct().Count() != Screens.Count())
            {
                messages.Add("Duplicate screen ids defined");
            }
            foreach (var screen in Screens)
            {
                if (!screen.Validate(out string screenMessage))
                {
                    messages.Add(screenMessage);
                }
            }


            if (MenuItems == null || !MenuItems.Any())
            {
                messages.Add("No Menu Items have been defined");
                message = string.Join(Environment.NewLine, messages);
                return false;
            }
            foreach (var menuItem in MenuItems)
            {
                if (!menuItem.Validate(this, out string screenMessage))
                {
                    messages.Add(screenMessage);
                }
            }

            GenerateAdminScreenAndMenu();

            // Set Default Values for nullable fields

            if (!ImutableDatabase.HasValue)
            {
                ImutableDatabase = true;
            }

            if (!DefaultScreenId.HasValue)
            {
                DefaultScreenId = Screens.FirstOrDefault().Id;
            }
            
            if (messages.Any())
            {
                message = string.Join(Environment.NewLine, messages);
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
            //TODO this is the wrong place for this code

            var administrationScreen = Screens.Where(s => s.InternalName.Equals("Administration", StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            
            if (administrationScreen == null)
            {
                administrationScreen = new Screen()
                {
                    Id = new Guid("{43037072-42F2-4B5C-A72E-1A08F149709A}"),
                    InternalName = "Administration",
                    Title = "Administration",
                    ScreenType = ScreenTypeEnum.Html,
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

                foreach (var screen in Screens.Where(a => a.Template == ScreenTemplateEnum.Reference))
                {
                    var menuItem = new MenuItem
                    {
                        ScreenId = screen.Id,
                        MenuItemType = MenuItemTypeEnum.ApplicationLink
                    };
                    menuItems.Add(menuItem);
                }
                
                administrationSection = new ScreenSection()
                {
                    Id = new Guid("{0F93AE3B-930D-4F6B-B73F-2EB63F225FAD}"),
                    InternalName = "Administration",
                    Title = "Administration",
                    ScreenSectionType = ScreenSectionTypeEnum.MenuList,
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
