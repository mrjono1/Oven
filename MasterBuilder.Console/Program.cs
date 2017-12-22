using System;

namespace MasterBuilder.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            var builder = new MasterBuilder.Builder();

            var testProject = new Test();

            builder.Run("C:\\Temp", testProject.Project).Wait();
        }
    }
}
