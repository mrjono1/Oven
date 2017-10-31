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
                DatabaseConnectionString = "Data Source=.\\\\SQLEXPRESS;Initial Catalog=MacroFinder;Integrated Security=True",
             //   ImutableDatabase = false,
                DefaultScreenId = new Guid("{C59B48E0-73B1-4393-8D6E-64CFE06304B2}")
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
                    Id = new Guid("{C59B48E0-73B1-4393-8D6E-64CFE06304B2}"),
                    ScreenTypeId = new Guid("{7A37305E-C518-4A16-91AE-BCF2AE032A9C}"), // Html
                    Title = "Home",
                    InternalName = "Home",
                    Path = "home",
                    TemplateId = new Guid("{79FEFA81-D6F7-4168-BCAF-FE6494DC3D72}"),
                    Html = @"<h1>Hello, world!</h1>
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
                },
                new Screen()
                {
                    Id = new Guid("{5AF4DA25-77C4-4DDE-946C-7FF266976E83}"),
                    ScreenTypeId = new Guid("{7A37305E-C518-4A16-91AE-BCF2AE032A9C}"), // Html
                    Title = "Counter",
                    InternalName = "Counter",
                    Path = "counter",
                    Html = @"<h1>Counter</h1>

<p>This is a simple example of an Angular component.</p>

<p>Current count: <strong>{{ currentCount }}</strong></p>

<button (click)=""incrementCounter()"">Increment</button>"
                },
                new Screen()
                {
                    Id = new Guid("{13C78359-33E6-4DD1-8E16-2C5D92305FE2}"),
                    ScreenTypeId = new Guid("{7A37305E-C518-4A16-91AE-BCF2AE032A9C}"), // Html
                    Title = "Fetch Data",
                    InternalName = "FetchData",
                    Path = "fetch-data",
                    Html = @"<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from the server.</p>

<p *ngIf=""!forecasts""><em>Loading...</em></p>

<table class='table' *ngIf=""forecasts"">
    <thead>
        <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
        </tr>
    </thead>
    <tbody>
        <tr *ngFor=""let forecast of forecasts"">
            <td>{{ forecast.dateFormatted }}</td>
            <td>{{ forecast.temperatureC }}</td>
            <td>{{ forecast.temperatureF }}</td>
            <td>{{ forecast.summary }}</td>
        </tr>
    </tbody>
</table>"
                },
                new Screen()
                {
                    Id = Guid.NewGuid(),
                    InternalName = "SampleData",
                    ScreenTypeId = new Guid("{7A37305E-C518-4A16-91AE-BCF2AE032A9C}"), // Html
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
                    InternalName = "Projects",
                    Path = "projects"
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
                    Title = "Counter",
                    ScreenId = new Guid("{5AF4DA25-77C4-4DDE-946C-7FF266976E83}"),
                    Icon = "glyphicon glyphicon-education"
                },
                new MenuItem
                {
                    Title = "Fetch Data",
                    ScreenId = new Guid("{13C78359-33E6-4DD1-8E16-2C5D92305FE2}"),
                    Icon = "glyphicon glyphicon-th-list"
                },
                new MenuItem
                {
                    Title = "Projects",
                    ScreenId = new Guid("{EAA8BF91-1F76-473F-8A0D-AB3DF8BD4B93}"),
                    Icon = "glyphicon glyphicon-th-list"
                }
            };

            Project = project;
        }
    }
}
