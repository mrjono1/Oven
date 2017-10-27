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
                InternalName = "TestReference",
                Id = Guid.NewGuid(),
                Version = "0.0.1",
                Title = "Test Reference",
                DatabaseConnectionString = "Data Source=.\\\\SQLEXPRESS;Initial Catalog=MacroFinder;Integrated Security=True"
            };
            project.Entities = new Entity[]
            {
                new Entity()
                {
                    Id = Guid.NewGuid(),
                    InternalName = "StreetTypes",
                    Title = "Street Types",
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
                            InternalName = "Title",
                            Type = PropertyTypeEnum.String
                        },
                        new Property()
                        {
                            Id = Guid.NewGuid(),
                            InternalName = "Code",
                            Type = PropertyTypeEnum.String
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
                }
            };

            Project = project;
        }
    }
}
