using MasterBuilder.Request;
using MasterBuilder.Templates.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder
{
    public class Test
    {
        public Project Project { get; set; }
        public Test()
        {
            var project = new Project
            {
                Id = new Guid("{D1CB7777-6E61-486B-B15E-05B97B57D0FC}"),
                InternalName = "TestReference",
                BuildVersion = 1,
                Title = "Test Reference",
                DatabaseConnectionString = "Data Source=.\\\\SQLEXPRESS;Initial Catalog=MasterBuilder;Integrated Security=True",
                ImutableDatabase = false,
                DefaultScreenId = new Guid("{C59B48E0-73B1-4393-8D6E-64CFE06304B2}"),
                AllowDestructiveDatabaseChanges = true
            };
            project.Entities = new Entity[]
            {
                new Entity()
                {
                    Id = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                    InternalName = "Project",
                    Title = "Project",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{C3E14B66-FF43-478A-95D0-39524F6555B5}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplateId = new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}")
                        },
                        new Property()
                        {
                            Id = new Guid("{CB6D802C-1F26-4D23-9272-6396E6268D72}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{8D5A78EB-B24D-4789-A498-1D37B57BF63D}"),
                                    ValidationType = ValidationTypeEnum.Unique
                                },
                                new Validation
                                {
                                    Id = new Guid("{CDE9AA00-9880-4689-993A-3C6C37D7FC0E}"),
                                    ValidationType = ValidationTypeEnum.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{5657C193-061A-430E-BB29-183A510DD92E}"),
                                    ValidationType = ValidationTypeEnum.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{883DB867-2098-4CA0-AE63-87DE09FDEF76}"),
                            InternalName = "InternalName",
                            Type = PropertyTypeEnum.String,
                            Title = "Internal Name",
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{F60F66AC-F6AD-45E2-9850-9A474CC43C4E}"),
                                    ValidationType = ValidationTypeEnum.Unique
                                },
                                new Validation
                                {
                                    Id = new Guid("{FF3EF7FD-7A8F-45A5-ACB0-C0A2F97C816B}"),
                                    ValidationType = ValidationTypeEnum.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation
                                {
                                    Id = new Guid("{2BE86AB2-5542-4E67-AF78-17E7A95B7B4E}"),
                                    ValidationType = ValidationTypeEnum.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{527365A6-D0BF-4239-85DF-BF8647B6F372}"),
                            InternalName = "DatabaseConnectionString",
                            Type = PropertyTypeEnum.String,
                            Title = "Database Connection String",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{09ED3A71-3F3A-46BB-B24B-D2796E796A39}"),
                                    ValidationType = ValidationTypeEnum.MaximumLength,
                                    IntegerValue = 500
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{F1EEF37B-F40D-44D4-832F-ACEC4B63D147}"),
                            InternalName = "MajorVersion",
                            Type = PropertyTypeEnum.Integer,
                            Title = "Major Version",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{09ED3A71-3F3A-46BB-B24B-D2796E796A39}"),
                                    ValidationType = ValidationTypeEnum.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{97E822CB-7A95-4C14-BED4-6CE19602FEE8}"),
                            InternalName = "MinorVersion",
                            Type = PropertyTypeEnum.Integer,
                            Title = "Minor Version",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{18954CB6-7C4B-42E7-AF4D-F242461D16C2}"),
                                    ValidationType = ValidationTypeEnum.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{D7E750B0-159D-464C-A88C-6E67673CFAF2}"),
                            InternalName = "BuildVersion",
                            Type = PropertyTypeEnum.Integer,
                            Title = "Build Version",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{B3784D79-72C0-4DC6-86C3-4ECD256821D1}"),
                                    ValidationType = ValidationTypeEnum.Required
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{62C6E4B9-5164-4A36-B90B-DC8129E09D2B}"),
                            InternalName = "AllowDestructiveDatabaseChanges",
                            Type = PropertyTypeEnum.Boolean,
                            Title = "Allow Destructive Database Changes",
                            ValidationItems = new Validation[]
                            {
                               // new Validation
                               // {
                               //     Id = new Guid("{20E93766-3D57-4DB1-820C-77C2E0809061}"),
                               //     ValidationType = ValidationTypeEnum.Required
                               // }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{0FBD3AA3-E6DD-4CCB-9FAA-E8AA8C8009F8}"),
                            InternalName = "CreatedDate",
                            Type = PropertyTypeEnum.DateTime,
                            Title = "Created Date",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{E9625AE5-85A7-42FF-95D1-D39265F35628}"),
                                    ValidationType = ValidationTypeEnum.Required
                                }
                            }
                        }
                    }
                },
                new Entity()
                {
                    Id = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}"),
                    InternalName = "Entity",
                    Title = "Entity",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{4B5077DA-1516-44C4-81CC-D0CE25BBBCF0}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplateId = new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}")
                        },
                        new Property()
                        {
                            Id = new Guid("{6D09DE04-2FDA-4091-A4DA-B3448ABA1A52}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{8D5A78EB-B24D-4789-A498-1D37B57BF63D}"),
                                    ValidationType = ValidationTypeEnum.Unique
                                },
                                new Validation
                                {
                                    Id = new Guid("{CDE9AA00-9880-4689-993A-3C6C37D7FC0E}"),
                                    ValidationType = ValidationTypeEnum.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{5657C193-061A-430E-BB29-183A510DD92E}"),
                                    ValidationType = ValidationTypeEnum.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{1CB8E873-07C7-461A-8859-672CE66C8513}"),
                            InternalName = "InternalName",
                            Type = PropertyTypeEnum.String,
                            Title = "Internal Name",
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{F60F66AC-F6AD-45E2-9850-9A474CC43C4E}"),
                                    ValidationType = ValidationTypeEnum.Unique
                                },
                                new Validation
                                {
                                    Id = new Guid("{FF3EF7FD-7A8F-45A5-ACB0-C0A2F97C816B}"),
                                    ValidationType = ValidationTypeEnum.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{2BE86AB2-5542-4E67-AF78-17E7A95B7B4E}"),
                                    ValidationType = ValidationTypeEnum.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{E6C4C4D9-A3E8-45B6-8B71-F33E6E159483}"),
                            InternalName = "Project",
                            Type = PropertyTypeEnum.ParentRelationship,
                            Title = "Project",
                            ParentEntityId = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{5657C193-061A-430E-BB29-183A510DD92E}"),
                                    ValidationType = ValidationTypeEnum.Required
                                },
                            }
                        }
                    }
                },
                new Entity()
                {
                    Id = new Guid("{DE9790AD-6FC3-4CE3-B63B-EEAA1DF7CFCB}"),
                    InternalName = "Property",
                    Title = "Property",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{D34BF79B-F052-4090-BBDD-8FAE69A256C4}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplateId = new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}")
                        },
                        new Property()
                        {
                            Id = new Guid("{863F7481-3190-42AF-879C-53535BD468E6}"),
                            InternalName = "Entity",
                            Type = PropertyTypeEnum.ParentRelationship,
                            Title = "Entity",
                            ParentEntityId = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{D69A337F-7171-4CAE-9E9D-492FE9578D89}"),
                                    ValidationType = ValidationTypeEnum.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{6F7F0BBE-B6E2-4766-BA5D-2A9F6540D4E0}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{8BEA3D51-07FB-4111-A01F-1624B8B91A57}"),
                                    ValidationType = ValidationTypeEnum.Unique
                                },
                                new Validation
                                {
                                    Id = new Guid("{ACB68BF7-5842-42C7-B77A-88AC71CA22B1}"),
                                    ValidationType = ValidationTypeEnum.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{1EACE07B-FD49-4B18-AA3F-36DF87D63B6E}"),
                                    ValidationType = ValidationTypeEnum.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{C52F7B8E-CAD0-40FF-8E89-B313A290A96E}"),
                            InternalName = "InternalName",
                            Type = PropertyTypeEnum.String,
                            Title = "Internal Name",
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{7CBFA8F5-364D-4EDF-8563-22BE16CE2B16}"),
                                    ValidationType = ValidationTypeEnum.Unique
                                },
                                new Validation
                                {
                                    Id = new Guid("{74789541-1939-4F0F-9098-EBDF0AE70A1C}"),
                                    ValidationType = ValidationTypeEnum.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{035339EF-C09F-4412-8779-BD2088387757}"),
                                    ValidationType = ValidationTypeEnum.Required
                                },
                            }
                        },
                    }
                },
                new Entity()
                {
                    Id = new Guid("{FF2103AE-FE34-410C-BC03-042AF7449D2D}"),
                    InternalName = "Validation",
                    Title = "Validation",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{B9E9AC32-5942-4DFF-8AFD-DEDC26795824}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplateId = new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}")
                        },
                        new Property()
                        {
                            Id = new Guid("{3690A6AE-0573-40F3-8680-0BCE13931EE3}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{D7C58A33-816E-45D7-8E8A-E7FCC44C270E}"),
                                    ValidationType = ValidationTypeEnum.MaximumLength,
                                    IntegerValue = 200
                                },
                                new Validation{
                                    Id = new Guid("{07CC8E0F-E69A-4B93-892A-2795B8E34C48}"),
                                    ValidationType = ValidationTypeEnum.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{BA0702F8-6F26-4A25-9300-31B44F14B3A8}"),
                            InternalName = "Property",
                            Type = PropertyTypeEnum.ParentRelationship,
                            Title = "Property",
                            ParentEntityId = new Guid("{DE9790AD-6FC3-4CE3-B63B-EEAA1DF7CFCB}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{FC73C9C5-953A-414F-9C20-4B5C57F4F709}"),
                                    ValidationType = ValidationTypeEnum.Required
                                },
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{89E340FC-A7BB-44D4-A783-45801941B752}"),
                            InternalName = "ValidationType",
                            Type = PropertyTypeEnum.ReferenceRelationship,
                            Title = "Validation Type",
                            ParentEntityId = new Guid("{DE9790AD-6FC3-4CE3-B63B-EEAA1DF7CFCB}"),
                            ValidationItems = new Validation[]
                            {
                                new Validation{
                                    Id = new Guid("{707BD4C0-7ED4-4865-9168-176B4E843E72}"),
                                    ValidationType = ValidationTypeEnum.Required
                                },
                            }
                        }
                    }
                },
                new Entity()
                {
                    Id = new Guid("{B833CE8A-1877-45A6-9FC3-161266524082}"),
                    InternalName = "WeatherForecast",
                    Title = "Weather Forecast",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{ABC2AA1F-903E-4D9E-BF5B-9BBD73575F6C}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            PropertyTemplateId = new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}")
                        },
                        new Property()
                        {
                            Id = new Guid("{7A3B9922-ECCC-40F4-BD4D-96C130F5149C}"),
                            InternalName = "DateFormatted",
                            Type = PropertyTypeEnum.String
                        },
                        new Property()
                        {
                            Id = new Guid("{01D31925-FC89-4067-888C-DB8B7FB9FA5F}"),
                            InternalName = "TemperatureC",
                            Type = PropertyTypeEnum.Integer
                        },
                        new Property()
                        {
                            Id = new Guid("{DAA3D69A-339B-48E5-983B-319FBB2F0B1F}"),
                            InternalName = "Summary",
                            Type = PropertyTypeEnum.String
                        },
                        new Property()
                        {
                            Id = new Guid("{18D635F4-2B8C-4D1F-A609-6E586D48E468}"),
                            InternalName = "TemperatureF",
                            Type = PropertyTypeEnum.Integer,
                            Calculation = "32 + (int)(TemperatureC / 0.5556)"
                        }
                    }
                }
            };

            project.Screens = new Screen[]
            {
                new Screen()
                {
                    Id = new Guid("{C59B48E0-73B1-4393-8D6E-64CFE06304B2}"),
                    ScreenTypeId = new Guid("{7A37305E-C518-4A16-91AE-BCF2AE032A9C}"), // Html
                    Title = "Home",
                    InternalName = "Home",
                    Path = "home",
                    TemplateId = new Guid("{79FEFA81-D6F7-4168-BCAF-FE6494DC3D72}"),
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{532605D4-DCD3-4B5B-AC63-7CC6E90F6792}"),
                            Title = "Home",
                            InternalName = "Home",
                            ScreenSectionType = ScreenSectionTypeEnum.Html,
                            Html = @"
<p>Welcome to your new single-page application, built with:</p>
<ul>
    <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
    <li><a href='https://angular.io/'>Angular</a> and <a href='http://www.typescriptlang.org/'>TypeScript</a> for client-side code</li>
    <li><a href='https://webpack.github.io/'>Webpack</a> for building and bundling client-side resources</li>
    <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
</ul>
<p>To help you get started, we've also set up:</p>
<ul>
    <li><strong>Client-side navigation</strong>. For example, click <em>Counter</em> then <em>Back</em> to return here.</li>
    <li><strong>Server-side prerendering</strong>. For faster initial loading and improved SEO, your Angular app is prerendered on the server. The resulting HTML is then transferred to the browser where a client-side copy of the app takes over.</li>
    <li><strong>Webpack dev middleware</strong>. In development mode, there's no need to run the <code>webpack</code> build tool. Your client-side resources are dynamically built on demand. Updates are available as soon as you modify any file.</li>
    <li><strong>Hot module replacement</strong>. In development mode, you don't even need to reload the page after making most changes. Within seconds of saving changes to files, your Angular app will be rebuilt and a new instance injected is into the page.</li>
    <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and the <code>webpack</code> build tool produces minified static CSS and JavaScript files.</li>
</ul>"
                        }
                    }
                },         
                new Screen()
                {
                    Id = new Guid("{EAA8BF91-1F76-473F-8A0D-AB3DF8BD4B93}"),
                    EntityId = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                    ScreenType = ScreenTypeEnum.Search,
                    Title = "Projects",
                    InternalName = "Projects",
                    Path = "projects",
                    NavigateToScreenId = new Guid("{835D26D3-2349-4914-AB85-2195756A5DAA}"),
                    ScreenFeatures = new ScreenFeature[]
                    {
                        new ScreenFeature{
                            Id = new Guid("{00CAB28C-F1CA-4FDC-9184-AC7DDA7FD3C5}"),
                            Feature = FeatureEnum.New
                        }
                    },
                    MenuItems = new MenuItem[]
                    {
                        new MenuItem
                        {
                            Id = new Guid("{FCB3AFDB-5CEB-41E7-AE2B-6122ECCB966D}"),
                            Title = "Publish",
                            Icon = "glyphicon glyphicon-cloud-upload"
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{835D26D3-2349-4914-AB85-2195756A5DAA}"),
                    EntityId = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                    ScreenType = ScreenTypeEnum.Edit,
                    Title = "Project",
                    InternalName = "Project",
                    Path = "project",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{BCB84DAB-B0AD-45B9-975D-5C07DE4F8990}"),
                            Title = "Project",
                            InternalName = "Project",
                            ScreenSectionTypeId = new Guid("{DC1169A8-8F49-45E9-9969-B64BEF4D0F42}") // Form
                        },
                        new ScreenSection
                        {
                            Id = new Guid("{903FEB8F-DC3C-49AA-AB1F-BB5C82F20DC5}"),
                            Title = "Entities",
                            InternalName = "Entities",
                            ScreenSectionTypeId = new Guid("{0637300C-B76E-45E2-926A-055BB335129F}"), // Search
                            EntityId = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}"),
                            NavigateToScreenId = new Guid("{B1CE9862-EA2F-4EBC-95FF-D6FB87F21EE7}"),
                            MenuItems = new MenuItem[]
                            {
                                new MenuItem
                                {
                                    Id = new Guid("{AE228BBC-8A38-4D7D-B3B0-7964A281B7A5}"),
                                    MenuItemType = MenuItemTypeEnum.New,
                                    ScreenId = new Guid("{B1CE9862-EA2F-4EBC-95FF-D6FB87F21EE7}"),
                                    Title = "New"
                                }
                            }
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{B1CE9862-EA2F-4EBC-95FF-D6FB87F21EE7}"),
                    EntityId = new Guid("{149F1936-1EE1-481F-9038-A6B766B85BF3}"),
                    ScreenType = ScreenTypeEnum.Edit,
                    Title = "Entity",
                    InternalName = "Entity",
                    Path = "entity",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{8130C0F4-F8F7-4A7A-9793-85B8939589EB}"),
                            Title = "Entity",
                            InternalName = "Entity",
                            ScreenSectionTypeId = new Guid("{DC1169A8-8F49-45E9-9969-B64BEF4D0F42}") // Form
                        },
                        new ScreenSection
                        {
                            Id = new Guid("{C2A19FAF-5C62-43D2-8FA4-2293B4B68569}"),
                            Title = "Properties",
                            InternalName = "Properties",
                            ScreenSectionTypeId = new Guid("{0637300C-B76E-45E2-926A-055BB335129F}"), // Search
                            EntityId = new Guid("{DE9790AD-6FC3-4CE3-B63B-EEAA1DF7CFCB}"),
                            NavigateToScreenId = new Guid("{064AB31A-E92A-4647-A517-2A1BAC54EE73}"),
                            MenuItems = new MenuItem[]
                            {
                                new MenuItem
                                {
                                    Id = new Guid("{C236C6DC-A21A-44DF-B125-FFA46E1810FC}"),
                                    MenuItemType = MenuItemTypeEnum.New,
                                    ScreenId = new Guid("{064AB31A-E92A-4647-A517-2A1BAC54EE73}"),
                                    Title = "New"
                                }
                            }
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{064AB31A-E92A-4647-A517-2A1BAC54EE73}"),
                    EntityId = new Guid("{DE9790AD-6FC3-4CE3-B63B-EEAA1DF7CFCB}"),
                    ScreenType = ScreenTypeEnum.Edit,
                    Title = "Property",
                    InternalName = "Property",
                    Path = "property",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{03934004-41EF-40A7-B317-723019001DCB}"),
                            Title = "Property",
                            InternalName = "Property",
                            ScreenSectionTypeId = new Guid("{DC1169A8-8F49-45E9-9969-B64BEF4D0F42}") // Form
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{8E1A9AB1-799E-46B4-8364-85FB087C643E}"),
                    EntityId = new Guid("{91104448-B314-41C3-8573-2BDF7CCBB701}"),
                    ScreenType = ScreenTypeEnum.Search,
                    Title = "Validation Types",
                    InternalName = "ValidationTypes",
                    Path = "validation-types",
                    NavigateToScreenId = new Guid("{5D9BA697-C64B-40EE-9DF4-F88BD683713F}"),
                    ScreenFeatures = new ScreenFeature[]
                    {
                        new ScreenFeature{
                            Id = new Guid("{DF498A46-08B7-450E-8CE4-3BCC1B711A0D}"),
                            FeatureId = new Guid("{6114120E-BD93-4CE4-A673-7DC295F93CFE}") // New
                        }
                    }
                },
                new Screen()
                {
                    Id = new Guid("{5D9BA697-C64B-40EE-9DF4-F88BD683713F}"),
                    EntityId = new Guid("{91104448-B314-41C3-8573-2BDF7CCBB701}"),
                    ScreenType = ScreenTypeEnum.Edit,
                    Title = "Validation Type",
                    InternalName = "ValidationType",
                    Path = "validation-type",
                    ScreenSections = new ScreenSection[]
                    {
                        new ScreenSection
                        {
                            Id = new Guid("{459E59DD-D382-4061-94EF-3E73B47BFCB2}"),
                            Title = "Validation Type",
                            InternalName = "ValidationType",
                            ScreenSectionTypeId = new Guid("{DC1169A8-8F49-45E9-9969-B64BEF4D0F42}") // Form
                        }
                    }
                },
            };

            project.MenuItems = new MenuItem[]
            {
                new MenuItem
                {
                    Title = "Home",
                    ScreenId = new Guid("{C59B48E0-73B1-4393-8D6E-64CFE06304B2}"),
                    Icon = "glyphicon glyphicon-home"
                },
                new MenuItem
                {
                    Title = "Projects",
                    ScreenId = new Guid("{EAA8BF91-1F76-473F-8A0D-AB3DF8BD4B93}"),
                    Icon = "glyphicon glyphicon-th-list"
                },
                new MenuItem
                {
                    Title = "Validation Types",
                    ScreenId = new Guid("{8E1A9AB1-799E-46B4-8364-85FB087C643E}"),
                    Icon = "glyphicon glyphicon-th-list"
                }
            };

            project.WebServices = new WebService[]
            {
                new WebService
                {
                    Id = new Guid("{359525A8-CCA2-4AC1-9348-23057D616A75}"),
                    Title = "Master Builder Api",
                    InternalName = "MasterBuilderApi",
                    DefaultBaseEndpoint = "https://localhost:44398",
                    Operations = new WebServiceOperation[]
                    {
                        new WebServiceOperation
                        {
                            Id = new Guid("{99B9358B-2266-47BC-957A-DC6EF459D4A1}"),
                            Title = "Publish",
                            InternalName = "Publish",
                            Verb = "POST",
                            RelativeRoute = "/api/builder/publish"
                        }
                    }
                }
            };

            var entities = new List<Entity>(project.Entities);
            entities.AddRange(ReferenceEntities());
            project.Entities = entities;

            Project = project;
        }

        private IEnumerable<Entity> ReferenceEntities()
        {
            return new Entity[]
            {
                // Validation Type
                new Entity()
                {
                    Id = new Guid("{91104448-B314-41C3-8573-2BDF7CCBB701}"),
                    InternalName = "ValidationType",
                    Title = "Validation Type",
                    EntityTemplate = EntityTemplateEnum.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{03020776-6F7E-41CD-9475-6E2CA72E92B4}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplate = PropertyTemplateEnum.PrimaryKey
                        },
                        new Property()
                        {
                            Id = new Guid("{771B57F0-F3D8-4E12-AA93-C801447BDB83}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplateEnum.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedTypeEnum.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new Guid("{17CC19D3-8E91-432E-98F7-4D9368DE3C44}"),
                                Title = "Email"
                            },
                            new {
                                Id = new Guid("{F7788E3D-7753-4491-98B1-AE78E16CDD0E}"),
                                Title = "Maximum Length"
                            },
                            new {
                                Id = new Guid("{0046F484-17EB-4665-AE59-45189BB203A9}"),
                                Title = "Maximum Value"
                            },
                            new {
                                Id = new Guid("{35D78EB6-F5DE-4E7B-AE79-B69A1D3DC7C9}"),
                                Title = "Minimum Length"
                            },
                            new {
                                Id = new Guid("{A679CB09-DE53-42F7-BB89-7E29947B51A1}"),
                                Title = "Minimum Value"
                            },
                            new {
                                Id = new Guid("{C0A88F1A-AAA8-47DA-A75B-94490915616C}"),
                                Title = "Pattern"
                            },
                            new {
                                Id = new Guid("{BD110234-F05D-42AB-BF2E-382B83093D0C}"),
                                Title = "Required"
                            },
                            new {
                                Id = new Guid("{CB9A60D3-42B3-411F-8FCE-2FC36C812A16}"),
                                Title = "Required True"
                            },
                            new {
                                Id = new Guid("{890C7A9E-09AE-4BB8-970E-85C564F753F1}"),
                                Title = "Unique"
                            }
                        })
                    }
                },
                // Feature
                new Entity()
                {
                    Id = new Guid("{D0E141A6-42CE-4AD3-A95E-24D40537342F}"),
                    InternalName = "Feature",
                    Title = "Feature",
                    EntityTemplate = EntityTemplateEnum.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{087C5441-AC68-4DE0-A1DE-03A59B6C58B5}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplate = PropertyTemplateEnum.PrimaryKey
                        },
                        new Property()
                        {
                            Id = new Guid("{732EA747-96A8-4437-A18A-B1BB990160A5}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplateEnum.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedTypeEnum.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new Guid("{6114120E-BD93-4CE4-A673-7DC295F93CFE}"),
                                Title = "New"
                            }
                        })
                    }
                },
                // Menu Item Type
                new Entity()
                {
                    Id = new Guid("{092F60B1-EE1E-4451-A771-013376C93E65}"),
                    InternalName = "MenuItemType",
                    Title = "Menu Item Type",
                    EntityTemplate = EntityTemplateEnum.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{543BCF58-4929-4386-8B01-FBF4C1680430}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplate = PropertyTemplateEnum.PrimaryKey
                        },
                        new Property()
                        {
                            Id = new Guid("{7D223CCB-41AF-4888-B441-61B3BDBBE56B}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplateEnum.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedTypeEnum.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new Guid("{51810E74-59E6-44AF-B6D3-1B8ECF82EE54}"),
                                Title = "Application Link"
                            },
                            new {
                                Id = new Guid("{A7B39F29-3887-4744-A4E3-926607DB15D2}"),
                                Title = "New"
                            }
                        })
                    }
                },
                // Property Template
                new Entity()
                {
                    Id = new Guid("{08A8E760-8620-44A9-9A15-646B6A53C881}"),
                    InternalName = "PropertyTemplate",
                    Title = "Property Template",
                    EntityTemplate = EntityTemplateEnum.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{ACEF7F12-2CF4-46AC-BFCC-60EA3E017E9F}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplate = PropertyTemplateEnum.PrimaryKey
                        },
                        new Property()
                        {
                            Id = new Guid("{732EA747-96A8-4437-A18A-B1BB990160A5}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplateEnum.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedTypeEnum.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}"),
                                Title = "Primary Key"
                            },
                            new {
                                Id = new Guid("{1B966A14-45B9-4E34-92BB-E2D46D97C5C3}"),
                                Title = "Reference Title"
                            }
                        })
                    }
                },
                // Property Type
                new Entity()
                {
                    Id = new Guid("{0B543B54-60AB-4FEA-BBD7-320AD50F3A06}"),
                    InternalName = "PropertyType",
                    Title = "Property Type",
                    EntityTemplate = EntityTemplateEnum.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{2E259BAE-F1E8-4B10-8672-8FDEC3061C80}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplate = PropertyTemplateEnum.PrimaryKey
                        },
                        new Property()
                        {
                            Id = new Guid("{8B61C0F2-9800-483C-A33E-0EFEDA6482BB}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplateEnum.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedTypeEnum.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new Guid("{4247CAB3-DA47-4921-81B4-1DFF78909859}"),
                                Title = "Uniqueidentifier"
                            },
                            new {
                                Id = new Guid("{A05F5788-04C3-487D-92F1-A755C73230D4}"),
                                Title = "String"
                            },
                            new {
                                Id = new Guid("{F126388B-8A6E-41DB-A98A-A0E511016441}"),
                                Title = "Integer"
                            },
                            new {
                                Id = new Guid("{25E3A798-5F63-4A1E-93B3-A0BCE69836BC}"),
                                Title = "Date Time"
                            },
                            new {
                                Id = new Guid("{2C1D2E2A-3531-41D9-90D3-3632C368B12A}"),
                                Title = "Boolean"
                            },
                            new {
                                Id = new Guid("{8BB0B472-E8C4-4DCF-9EF4-FFA088B5A175}"),
                                Title = "Parent Relationship"
                            },
                            new {
                                Id = new Guid("{B42A437F-3DED-4B5F-A573-1CCEC1B2D58E}"),
                                Title = "Reference Relationship"
                            }
                        })
                    }
                },
                // Screen Section Type
                new Entity()
                {
                    Id = new Guid("{6B3442DE-02EA-4A89-BBC9-3C7E698C94EF}"),
                    InternalName = "ScreenSectionType",
                    Title = "Screen Section Type",
                    EntityTemplate = EntityTemplateEnum.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{4BBA98F7-4C6F-4E60-BE3F-06180D8A6141}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplate = PropertyTemplateEnum.PrimaryKey
                        },
                        new Property()
                        {
                            Id = new Guid("{4A67B6BF-99A0-4446-A61C-AF3EB857099C}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplateEnum.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedTypeEnum.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new Guid("{DC1169A8-8F49-45E9-9969-B64BEF4D0F42}"),
                                Title = "Form"
                            },
                            new {
                                Id = new Guid("{0637300C-B76E-45E2-926A-055BB335129F}"),
                                Title = "Search"
                            },
                            new {
                                Id = new Guid("{4270A420-64CB-4A2C-B718-2C645DB2B57B}"),
                                Title = "Grid"
                            },
                            new {
                                Id = new Guid("{38EF9B44-A993-479B-91EC-1FE436E91556}"),
                                Title = "Html"
                            },
                        })
                    }
                },
                // Screen Type
                new Entity()
                {
                    Id = new Guid("{C04282DB-CB85-445D-BB4B-AEBB3801DAC7}"),
                    InternalName = "ScreenType",
                    Title = "Screen Type",
                    EntityTemplate = EntityTemplateEnum.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{C9DBA086-ACC8-4BD9-8EEB-2B2AAF38CC78}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplate = PropertyTemplateEnum.PrimaryKey
                        },
                        new Property()
                        {
                            Id = new Guid("{749ACD4D-1873-4314-B0A1-E61EED934D3C}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplateEnum.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedTypeEnum.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}"),
                                Title = "Search"
                            },
                            new {
                                Id = new Guid("{9B422DE1-FACE-4A63-9A46-0BD1AF3D47F4}"),
                                Title = "Edit"
                            },
                            new {
                                Id = new Guid("{ACE5A965-7005-4E34-9C66-AF0F0CD15AE9}"),
                                Title = "View"
                            },
                            new {
                                Id = new Guid("{7A37305E-C518-4A16-91AE-BCF2AE032A9C}"),
                                Title = "Html"
                            }
                        })
                    }
                },
                // Seed Type
                new Entity()
                {
                    Id = new Guid("{EA6A9786-573A-4821-824C-3FB5322D2A51}"),
                    InternalName = "SeedType",
                    Title = "Seed Type",
                    EntityTemplate = EntityTemplateEnum.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{623C2ADB-9FAD-43F6-8DDD-970B21D49CCE}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplate = PropertyTemplateEnum.PrimaryKey
                        },
                        new Property()
                        {
                            Id = new Guid("{E67EC40A-C1B6-4465-BA91-D40A488317BC}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplateEnum.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedTypeEnum.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new Guid("{8A07A94D-4A5F-420F-B02A-4B2223B1213B}"),
                                Title = "Add if none"
                            },
                            new {
                                Id = new Guid("{2729F45B-269F-42B1-BBA9-3E76DC9D1CBB}"),
                                Title = "Ensure All Added"
                            },
                            new {
                                Id = new Guid("{6989AE9F-D5BD-4861-ABE6-0142EDDE6130}"),
                                Title = "Ensure All Updated"
                            }
                        })
                    }
                },
                // Entity Template
                new Entity()
                {
                    Id = new Guid("{E20337EA-37F3-48D1-96F7-3CF2A40A7F52}"),
                    InternalName = "EntityTemplate",
                    Title = "Entity Template",
                    EntityTemplate = EntityTemplateEnum.Reference,
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = new Guid("{5D0BEB0A-4B91-4B40-83F0-8EAE7426CCF6}"),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier,
                            Title = "Id",
                            PropertyTemplate = PropertyTemplateEnum.PrimaryKey
                        },
                        new Property()
                        {
                            Id = new Guid("{83CFE967-1FEF-44F1-A3DC-C03FC2F0B167}"),
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String,
                            Title = "Title",
                            PropertyTemplate = PropertyTemplateEnum.ReferenceTitle
                        }
                    },
                    Seed = new Seed
                    {
                        SeedType = SeedTypeEnum.EnsureAllUpdated,
                        JsonData = JsonConvert.SerializeObject(new []
                        {
                            new {
                                Id = new Guid("{B79D1C90-6320-4A07-9753-2A41110611C8}"),
                                Title = "Reference"
                            }
                        })
                    }
                }
            };
        }
    }
}
