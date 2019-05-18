using MongoDB.Bson;
using Newtonsoft.Json;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kitchen.Screens
{
    public class Project
    {
        private static Entity Entity
        {
            get
            {
                return Entities.Project.GetEntity();
            }
        }
        public static Screen GetScreen()
        {
            return new Screen()
            {
                Id = new ObjectId("5ca877024a73264e4c06dfac"),
                EntityId = Entity.Id,
                ScreenType = ScreenType.Form,
                Title = "Project",
                Path = "project",
                ScreenSections = new ScreenSection[]
                {
                    new ScreenSection
                    {
                        Id = new ObjectId("5ca877024a73264e4c06dfad"),
                        Title = "Project",
                        InternalName = "Project",
                        ScreenSectionType = ScreenSectionType.Form,
                        EntityId = Entity.Id,
                        FormSection = new FormSection
                        {
                            Id = new ObjectId("5cdfce5f753fba01d569b00d"),
                            FormFields = new FormField[]
                            {
                                new FormField
                                {
                                    // Title
                                    EntityPropertyId = new ObjectId("5ca876ee4a73264e4c06dedd"),
                                    Ordinal = 10,
                                },
                                new FormField
                                {
                                    // Internal Name
                                    EntityPropertyId = new ObjectId("5ca876f04a73264e4c06dee1"),
                                    Ordinal = 20,
                                },
                                new FormField
                                {
                                    // Major Version
                                    EntityPropertyId = new ObjectId("5ca876f14a73264e4c06dee6"),
                                    Ordinal = 30,
                                },
                                new FormField
                                {
                                    // Minor Version
                                    EntityPropertyId = new ObjectId("5ca876f14a73264e4c06dee8"),
                                    Ordinal = 40,
                                },
                                new FormField
                                {
                                    // Build Version
                                    EntityPropertyId = new ObjectId("5ca876f14a73264e4c06deea"),
                                    Ordinal = 50,
                                },
                                new FormField
                                {
                                    // Created Date
                                    EntityPropertyId = new ObjectId("5ca876f14a73264e4c06deec"),
                                    Ordinal = 60,
                                    // Needs to be read only
                                },
                                new FormField
                                {
                                    // Default Screen
                                    EntityPropertyId = new ObjectId("5ca876f14a73264e4c06deee"),
                                    Ordinal = 70,
                                },
                                new FormField
                                {
                                    // Enable Custom Code
                                    EntityPropertyId = new ObjectId("5ca876f24a73264e4c06def1"),
                                    Ordinal = 80,
                                },
                            }
                        }
                    },
                    new ScreenSection
                    {
                        Id = new ObjectId("5ca877024a73264e4c06dfae"),
                        Title = "Entities",
                        InternalName = "Entities",
                        ScreenSectionType = ScreenSectionType.Search,
                        EntityId = new ObjectId("5ca876f24a73264e4c06def3"),
                        NavigateToScreenId = new ObjectId("5ca877034a73264e4c06dfaf"),
                        MenuItems = new MenuItem[]
                        {
                            new MenuItem
                            {
                                Id = new ObjectId("5ca877034a73264e4c06dfb0"),
                                MenuItemType = MenuItemType.New,
                                ScreenId = new ObjectId("5ca877034a73264e4c06dfaf"),
                                Title = "New"
                            }
                        },
                        SearchSection = new SearchSection
                        {
                            SearchColumns = new SearchColumn[]
                            {
                                new SearchColumn
                                {
                                    // Title
                                    EntityPropertyId = new ObjectId("5ca876f24a73264e4c06def5"),
                                    Ordinal = 10
                                },
                                new SearchColumn
                                {
                                    // Internal Name
                                    EntityPropertyId = new ObjectId("5ca876f24a73264e4c06def6"),
                                    Ordinal = 20
                                }
                            }
                        }
                    },
                    new ScreenSection
                    {
                        Id = new ObjectId("5ca877034a73264e4c06dfb1"),
                        Title = "Screens",
                        InternalName = "Screens",
                        ScreenSectionType = ScreenSectionType.Search,
                        EntityId = new ObjectId("5ca876f14a73264e4c06deef"),
                        NavigateToScreenId = new ObjectId("5ca877034a73264e4c06dfb2"),
                        MenuItems = new MenuItem[]
                        {
                            new MenuItem
                            {
                                Id = new ObjectId("5ca877034a73264e4c06dfb3"),
                                MenuItemType = MenuItemType.New,
                                ScreenId = new ObjectId("5ca877034a73264e4c06dfb2"),
                                Title = "New"
                            }
                        },
                        SearchSection = new SearchSection
                        {
                            SearchColumns = new SearchColumn[]
                            {
                                new SearchColumn
                                {
                                    // Title
                                    EntityPropertyId = new ObjectId("5ca876f74a73264e4c06df2a")
                                },
                                new SearchColumn
                                {
                                    // Path/Slug
                                    EntityPropertyId = new ObjectId("5ca876f74a73264e4c06df2d")
                                },
                                new SearchColumn
                                {
                                    // Screen Type
                                    EntityPropertyId = new ObjectId("5ca876f74a73264e4c06df31")
                                },
                                new SearchColumn
                                {
                                    // Entity
                                    EntityPropertyId = new ObjectId("5ca876f74a73264e4c06df2e")
                                }
                            }
                        }
                    },
                    new ScreenSection
                    {
                        Id = new ObjectId("5ca877034a73264e4c06dfb4"),
                        Title = "Menu Items",
                        InternalName = "MenuItems",
                        ScreenSectionType = ScreenSectionType.Search,
                        EntityId = new ObjectId("5ca876f64a73264e4c06df1e"),
                        NavigateToScreenId = new ObjectId("5ca877034a73264e4c06dfb5"),
                        MenuItems = new MenuItem[]
                        {
                            new MenuItem
                            {
                                Id = new ObjectId("5ca877034a73264e4c06dfb6"),
                                MenuItemType = MenuItemType.New,
                                ScreenId = new ObjectId("5ca877034a73264e4c06dfb5"),
                                Title = "New"
                            }
                        }
                    },
                    new ScreenSection
                    {
                        Id = new ObjectId("5ca877034a73264e4c06dfb7"),
                        Title = "Services",
                        InternalName = "Services",
                        ScreenSectionType = ScreenSectionType.Search,
                        EntityId = new ObjectId("5ca876fa4a73264e4c06df52"),
                        NavigateToScreenId = new ObjectId("5ca877034a73264e4c06dfb8"),
                        MenuItems = new MenuItem[]
                        {
                            new MenuItem
                            {
                                Id = new ObjectId("5ca877034a73264e4c06dfb9"),
                                MenuItemType = MenuItemType.New,
                                ScreenId = new ObjectId("5ca877034a73264e4c06dfb8"),
                                Title = "New"
                            }
                        }
                    },
                    new ScreenSection
                    {
                        Id = new ObjectId("5ca877034a73264e4c06dfba"),
                        Title = "Dependencies",
                        InternalName = "NuGetDependencies",
                        ScreenSectionType = ScreenSectionType.Search,
                        OrderBy = "Include",
                        EntityId = new ObjectId("5ca876ff4a73264e4c06df89"),
                        NavigateToScreenId = new ObjectId("5ca877044a73264e4c06dfbb"),
                        MenuItems = new MenuItem[]
                        {
                            new MenuItem
                            {
                                Id = new ObjectId("5ca877044a73264e4c06dfbc"),
                                MenuItemType = MenuItemType.New,
                                ScreenId = new ObjectId("5ca877044a73264e4c06dfbb"),
                                Title = "New"
                            }
                        }
                    },
                    new ScreenSection
                    {
                        Id = new ObjectId("5ca877044a73264e4c06dfbd"),
                        Title = "Dependency Libraries",
                        InternalName = "NuGetPackageSources",
                        ScreenSectionType = ScreenSectionType.Search,
                        OrderBy = "Key",
                        EntityId = new ObjectId("5ca877004a73264e4c06df98"),
                        NavigateToScreenId = new ObjectId("5ca877044a73264e4c06dfbe"),
                        MenuItems = new MenuItem[]
                        {
                            new MenuItem
                            {
                                Id = new ObjectId("5ca877044a73264e4c06dfbf"),
                                MenuItemType = MenuItemType.New,
                                ScreenId = new ObjectId("5ca877044a73264e4c06dfbe"),
                                Title = "New"
                            }
                        }

                    },
                },
                MenuItems = new MenuItem[]
                {
                    new MenuItem
                    {
                        Id = new ObjectId("5ca877044a73264e4c06dfc0"),
                        Title = "Publish",
                        Icon = "glyphicon glyphicon-cloud-upload",
                        MenuItemType = MenuItemType.ServerFunction,
                        ServerCode = $@"var data = await _exportService.ExportProjectAsync(id);
        var result = await _ovenApiService.PublishAsync(data);
        return Ok(result.Content);"
                    }
                }
            };
        }
    }
}
