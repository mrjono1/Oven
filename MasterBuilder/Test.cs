using MasterBuilder.Request;
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
                Version = "0.0.1",
                Title = "Test Reference",
                DatabaseConnectionString = "Data Source=.\\\\SQLEXPRESS;Initial Catalog=MacroFinder;Integrated Security=True"
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
                            Title = "Id"
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
                                    ValidationType = ValidationTypeEnum.MinimumLength,
                                    IntegerValue = 200
                                }
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
                                    ValidationType = ValidationTypeEnum.MinimumLength,
                                    IntegerValue = 500
                                }
                            }
                        },
                        new Property()
                        {
                            Id = new Guid("{F1EEF37B-F40D-44D4-832F-ACEC4B63D147}"),
                            InternalName = "Version",
                            Type = PropertyTypeEnum.String,
                            Title = "Version",
                            ValidationItems = new Validation[]
                            {
                                new Validation
                                {
                                    Id = new Guid("{09ED3A71-3F3A-46BB-B24B-D2796E796A39}"),
                                    ValidationType = ValidationTypeEnum.MinimumLength,
                                    IntegerValue = 50
                                }
                            }
                        }
                    }
                },
                new Entity()
                {
                    Id = Guid.NewGuid(),
                    InternalName = "WeatherForecast",
                    Title = "Weather Forecast",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            Id = Guid.NewGuid(),
                            InternalName = "Id",
                            Type = PropertyTypeEnum.Uniqueidentifier
                        },
                        new Property()
                        {
                            Id = Guid.NewGuid(),
                            InternalName = "DateFormatted",
                            Type = PropertyTypeEnum.String
                        },
                        new Property()
                        {
                            Id = Guid.NewGuid(),
                            InternalName = "TemperatureC",
                            Type = PropertyTypeEnum.Integer
                        },
                        new Property()
                        {
                            Id = Guid.NewGuid(),
                            InternalName = "Summary",
                            Type = PropertyTypeEnum.String
                        },
                        new Property()
                        {
                            Id = Guid.NewGuid(),
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
                    Id = Guid.NewGuid(),
                    InternalName = "SampleData",
                    ControllerCode = @"private static string[] Summaries = new[]
        {
            ""Freezing"", ""Bracing"", ""Chilly"", ""Cool"", ""Mild"", ""Warm"", ""Balmy"", ""Hot"", ""Sweltering"", ""Scorching""
        };

        [HttpGet(""[action]"")]
        public IEnumerable<Entities.WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Entities.WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString(""d""),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }"
                },
                new Screen()
                {
                    Id = new Guid("{EAA8BF91-1F76-473F-8A0D-AB3DF8BD4B93}"),
                    EntityId = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                    ScreenTypeId = new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}"), // Search
                    Title = "Projects",
                    InternalName = "Projects"
                },
                new Screen()
                {
                    Id = new Guid("{EAA8BF91-1F76-473F-8A0D-AB3DF8BD4B93}"),
                    EntityId = new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"),
                    ScreenTypeId = new Guid("{9B422DE1-FACE-4A63-9A46-0BD1AF3D47F4}"), // Edit
                    Title = "Project",
                    InternalName = "Project"
                }
            };



            Project = project;
        }
    }
}
