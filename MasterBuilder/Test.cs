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
                DatabaseConnectionString = "Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;"
            };
            project.Entities = new Entity[]
            {
                new Entity()
                {
                    InternalName = "StreetTypes",
                    Title = "Street Types",
                    Properties = new Property[]
                    {
                    new Property()
                    {
                        InternalName = "Title",
                        Type = "string"
                    },
                    new Property()
                    {
                        InternalName = "Code",
                        Type = "string"
                    }
                    }
                },
                new Entity()
                {
                    InternalName = "WeatherForecast",
                    Title = "Weather Forecast",
                    Properties = new Property[]
                    {
                        new Property()
                        {
                            InternalName = "DateFormatted",
                            Type = "string"
                        },
                        new Property()
                        {
                            InternalName = "TemperatureC",
                            Type = "int"
                        },
                        new Property()
                        {
                            InternalName = "Summary",
                            Type = "string"
                        },
                        new Property()
                        {
                            InternalName = "TemperatureF",
                            Type = "int",
                            Calculation = "32 + (int)(TemperatureC / 0.5556)"
                        }
                    }
                }
            };

            project.Screens = new Screen[]
            {
                new Screen()
                {
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
