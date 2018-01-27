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
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var outputDirectory = configuration["OutputDirectory"];
            new Program().Run(outputDirectory).Wait();
        }

        public async Task Run(string outputDirectory)
        {
            var builder = new MasterBuilder.Builder();

            var testProject = new MasterBuilderUiModel();

            string messages = await builder.Run(outputDirectory, testProject.Project);
            
            Console.WriteLine(messages);
            if (!messages.Equals("Success"))
            {
                Console.Read();
            }
        }
    }
}
