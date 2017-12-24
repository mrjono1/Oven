using System;
using System.Threading.Tasks;

namespace MasterBuilder.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run().Wait();
        }

        public async Task Run()
        {
            var builder = new MasterBuilder.Builder();

            var testProject = new Test();

            string messages = await builder.Run("C:\\Temp", testProject.Project);
            
            Console.WriteLine(messages);
            if (!messages.Equals("Success"))
            {
                Console.Read();
            }
        }
    }
}
