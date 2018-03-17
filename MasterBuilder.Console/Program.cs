using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MasterBuilder.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
#if DEBUG
                .AddJsonFile($"appsettings.Development.json", optional: true)
#endif
                ;

            IConfigurationRoot configuration = builder.Build();

            var builderSettings = new BuilderSettings()
            {
                OutputDirectory = configuration["OutputDirectory"],
                GitUserName = configuration["GitUserName"],
                GitEmail = configuration["GitEmail"],
                VstsPersonalAccessToken = configuration["VstsPersonalAccessToken"]
            };

            new Program().Run(builderSettings).Wait();
        }

        public async Task Run(BuilderSettings builderSettings)
        {
            var builder = new MasterBuilder.Builder(builderSettings);

            var testProject = new MasterBuilderUiModel();

            string messages = await builder.Run(testProject.Project);
            
            Console.WriteLine(messages);
            if (!messages.Equals("Success"))
            {
                Console.Read();
            }
        }
    }
}
